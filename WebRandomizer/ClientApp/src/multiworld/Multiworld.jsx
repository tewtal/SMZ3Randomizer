import React, { useState, useEffect, useRef, useContext, useCallback } from 'react';
import { useParams } from 'react-router-dom';
import { Row, Col } from 'reactstrap';

import MessageCard from '../ui/MessageCard'

import Session from './Session';
import Connection from './Connection';
import Game from './Game';
import Patch from '../patch';
import Spoiler from '../spoiler';
import { smz3ItemNames, smItemNames } from '../game/item_lookup'

import { GameTraitsCtx } from '../game/traits';
import { gameServiceHost, adjustHostname } from '../site/domain';

import { decode } from 'slugid';

import differenceBy from 'lodash/differenceBy';
import isEmpty from 'lodash/isEmpty';
import minBy from 'lodash/minBy';
import maxBy from 'lodash/maxBy';

import { tryParseJson } from '../util';

// Message types to handle from WASM
const MessageType = {
    ConsoleDisconnected: 0,
    ConsoleReconnecting: 1,
    ConsoleConnected: 2,
    ConsoleError: 3,
    GameState: 4,
    ItemFound: 5,
    ItemReceived: 6,
    ItemsConfirmed: 7,
};

export default function Multiworld() {
    const { sessionSlug } = useParams();

    const randomizerClient = useRef(null);
    const eventLoopHandle = useRef(null);
    const statusLoopHandle = useRef(null);
    const [events, setEvents] = useState([]);
    const [state, setState] = useState(null);
    const [sessionStatus, setSessionStatus] = useState('');
    const [gameStatus, setGameStatus] = useState('');
    const [deviceStatus, setDeviceStatus] = useState('');
    const game = useContext(GameTraitsCtx);
    const itemLookup = game.id === "smz3" ? smz3ItemNames : smItemNames;

    const formatEvent = useCallback(event => {
        const worlds = state.session.seed.worlds;
        return {
            ...event,
            from_player: event.from_world_id === -1 ? "SERVER" : worlds.find(w => w.world_id === event.from_world_id).player_name,
            to_player: event.to_world_id === -1 ? "SERVER" : worlds.find(w => w.world_id === event.to_world_id).player_name,
            item_name: itemLookup[event.item_id],
            message: event.message.replace("<itemId>", itemLookup[event.item_id]),
        };
    }, [state, itemLookup]);

    const handleMessage = (message, args) => {
        switch (message) {
            case MessageType.GameState:
                setGameStatus(args[0]);
                break;
            case MessageType.ConsoleDisconnected:
                setDeviceStatus("Disconnected");
                break;
            case MessageType.ConsoleReconnecting:
                setDeviceStatus("Disconnected - Attempting to reconnect");
                break;
            case MessageType.ConsoleConnected:
                setDeviceStatus("Connected");
                setState(prevState => ({ ...prevState, device: { ...prevState.device, uri: args[0] } }));
                break;
            case MessageType.ConsoleError:
                setDeviceStatus(`Disconnected - ${args[0]}`);
                clearTimeout(eventLoopHandle.current);
                setState(prevState => ({ ...prevState, device: null, status: 1 }));
                break;
            default:
                break;
        }        
    };

    useEffect(() => {

        const initSession = async () => {            

            /* Webassembly files must be imported async */
            const { RandomizerClient } = await import("randomizer-client");

            const sessionGuid = decode(sessionSlug).replace(/-/g, "");
            if (sessionGuid) {
                let session = null;

                try {
                    if (randomizerClient.current === null) {
                        RandomizerClient.init();
                        randomizerClient.current = new RandomizerClient(`https://${gameServiceHost(window.location.href)}`, sessionGuid, handleMessage);
                    }
                    session = await randomizerClient.current.initialize();
                } catch {
                    setSessionStatus("Could not initialize session, try reloading the page.");
                    setState({ status: 0, session: null, clientData: null, device: null, patch: null });
                    return;
                }

                const clientGuid = localStorage.getItem(sessionGuid);
                if (clientGuid) {
                    try {
                        let clientData = await randomizerClient.current.login_player(clientGuid);
                        await updatePlayer(clientData);
                    } catch {
                        setSessionStatus("Could not re-authenticate to session, try reloading the page.")
                        setState({ status: 0, session: null, clientData: null, device: null, patch: null });
                        return;
                    }
                } else {
                    setState({ status: 1, session: session, clientData: null, device: null, patch: null });
                }

                setSessionStatus("Initialized");
                setDeviceStatus("Disconnected");
                setEvents([]);
            }
        };

        initSession();
        return () => {            
            clearTimeout(eventLoopHandle.current);
            clearTimeout(statusLoopHandle.current);
            statusLoopHandle.current = null;
            eventLoopHandle.current = null;
        };
    }, []); /* eslint-disable-line react-hooks/exhaustive-deps */

    useEffect(() => {

        async function statusLoop() {
            const minUnConfirmed = minBy(events.filter(e => !e.confirmed), 'id');
            const maxReceived = maxBy(events, 'id');
            const minId = minUnConfirmed != null
                ? minUnConfirmed.id
                : (maxReceived != null ? maxReceived.id + 1 : 0);

            try {
                const report = await randomizerClient.current.get_report(minId, [0, 1, 2, 3]);
                setState(prevState => {
                    return {
                        ...prevState, session: {
                            ...prevState.session, seed: {
                                ...prevState.session.seed, worlds: report.worlds
                            }
                        }
                    }
                });

                const newEvents = report.events.map(formatEvent);
                setEvents(prevEvents => isEmpty(newEvents) ? prevEvents : [...differenceBy(prevEvents, newEvents, 'id'), ...newEvents]);
            } catch (e) {
                console.log(e);
            }
        }

        clearTimeout(statusLoopHandle.current);
        statusLoopHandle.current = setTimeout(async () => await statusLoop(), 5000);
    }, [events, formatEvent]);

    async function onRegisterPlayer(sessionGuid, worldId) {
        if (!state.clientData) {
            try {
                let clientData = await randomizerClient.current.register_player(worldId);
                localStorage.setItem(sessionGuid, clientData.client_guid);
                await updatePlayer(clientData);

            } catch (e) {
                console.log("Error while registering player", e);
            }
        }
    }

    async function onUnregisterPlayer() {
        const ok = window.confirm("This will unregister you from the session, are you sure?");
        if (ok && state && state.clientData) {
            try {
                await randomizerClient.current.unregister_player();
                localStorage.removeItem(state.session.guid);
                clearTimeout(eventLoopHandle.current);
                clearTimeout(statusLoopHandle.current);
                setState({ status: 1, session: session, clientData: null, device: null, patch: null });
            } catch (e) {
                console.log("Error while unregistering player", e);
            }
        }
    }

    async function onForfeit() {
        const ok = window.confirm("This will remove you completely from the session and can not be undone, are you sure you want to forfeit?");
        if (ok) {
            await randomizerClient.current.forfeit();
            localStorage.removeItem(state.session.guid);
            clearTimeout(eventLoopHandle.current);
            clearTimeout(statusLoopHandle.current);
            setState({ status: 1, session: session, clientData: null, device: null, patch: null });
        }
    }

    async function onRemove(worldId) {
        const ok = window.confirm("Vote to remove this player from the session, are you sure?")
        if (ok) {
            try {
                await randomizerClient.current.send_event(5, worldId, -1, -1, -1, true, "");

            } catch (e) {
                console.log(e);
            }

        }
    }

    async function updatePlayer(clientData) {
        let sessionData = await randomizerClient.current.initialize();
        let patchData = await randomizerClient.current.get_patch();
        setState({ status: 2, session: sessionData, clientData: clientData, device: null, patch: patchData });
    }

    async function onConnect() {
        try {
            setDeviceStatus("Requesting device list");
            const devices = await randomizerClient.current.list_devices();
            if (devices.length === 0) {
                setDeviceStatus("Got an empty device list, make sure the emulator or sd2snes/fxpak is connected");
            } else if (devices.length > 1) {
                setDeviceStatus("Select a device");
                setState((prevState) => ({ ...prevState, status: 2, device: { selecting: true, list: devices } }));
            } else {
                await onDeviceSelect(devices[0]);
            }
        } catch {
            setDeviceStatus("Could not get device list, press 'Connect' to try again");
            return;
        }
    }

    async function onDeviceSelect(device) {
        try {
            await randomizerClient.current.start(device.uri);
            setDeviceStatus("Connected");
            setState((prevState) => ({ ...prevState, status: 3, device: { state: 1, selecting: false, list: null, name: device.name, uri: device.uri } }));

            await eventLoop();
        } catch {
            setDeviceStatus("Could not start device session, try again");
        }
    }

    async function eventLoop() {
        try {
            await randomizerClient.current.update();
        } catch (e) {
            console.log(e);
        }
        eventLoopHandle.current = setTimeout(async () => await eventLoop(), 1000);
    }

    async function onChatMessage(message) {
        const event = await randomizerClient.current.send_event(2, -1, 0, 0, -1, true, message);
        setEvents((prevEvents) => [...prevEvents, formatEvent(event.event)]);
    }

    async function onResendEvent(event) {
        const resentEvent = await randomizerClient.current.send_event(event.event_type, event.to_world_id, event.item_id, -1, -1, false, `RESENT: ${event.message}`);
        setEvents((prevEvents) => [...prevEvents, formatEvent(resentEvent.event)]);
    }

    if (state === null) return null;

    const { session, clientData, device, status, patch } = state;
    const { race } = session ? tryParseJson(session.seed.worlds[0].settings) : {};
    const gameMismatch = session && session.seed.game_id !== game.id;

    return !gameMismatch ? (
        <>
            {session && session.guid && (
                <Row className="mb-3">
                    <Col>
                        <Session clientData={clientData} status={status} session={session} sessionStatus={sessionStatus}
                            onRegisterPlayer={onRegisterPlayer}
                            onUnregisterPlayer={onUnregisterPlayer}
                            onForfeit={onForfeit}
                            onRemove={onRemove}
                        />
                    </Col>
                </Row>
            )}
            {session && clientData !== null && status < 3 && (
                <Row className="mb-3">
                    <Col>
                        <Patch
                            seed={{guid: session.seed.guid, mode: session.seed.game_mode, gameId: session.seed.game_id, gameName: session.seed.game_name, gameVersion: session.seed.game_version, seedNumber: session.seed.number}}
                            world={{ ...session.seed.worlds.find(world => world.world_id === clientData.world_id), player: clientData.player_name, patch: btoa(String.fromCharCode.apply(null, new Uint8Array(patch))) }}
                        />
                    </Col>
                </Row>
            )}
            {session && clientData !== null && (
                <Row className="mb-3">
                    <Col>
                        <Connection clientData={clientData} device={device} deviceStatus={deviceStatus}
                            onConnect={onConnect}
                            onDeviceSelect={onDeviceSelect}
                        />
                    </Col>
                </Row>
            )}
            {device && device.state === 1 && (
                <Row className="mb-3">
                    <Col>
                        <Game gameStatus={gameStatus} clientData={clientData} events={events}
                            onChatMessage={onChatMessage}
                            onResendEvent={onResendEvent}
                        />
                    </Col>
                </Row>
            )}
            {session && race === 'false' && (
                <Row className="mb-3">
                    <Col>
                        <Spoiler seedGuid={session.seed.guid} />
                    </Col>
                </Row>
            )}
        </>) :
        (
            <MessageCard error={true} title="This is not quite right :O"
                msg={<a href={adjustHostname(document.location.href, session.seed.gameId)}>Go to the correct domain here</a>}
            />
        );
}
