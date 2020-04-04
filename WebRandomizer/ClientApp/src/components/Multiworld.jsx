import React, { useState, useEffect, useRef } from 'react';
import { decode } from 'slugid';
import Seed from './Seed';
import Patch from './Patch';
import Connection from './Connection';
import Game from './Game';
import Spoiler from './Spoiler';

import Network from '../network';

export default function Multiworld(props) {
    const network = useRef(null);
    const [state, setState] = useState(null);
    const [sessionStatus, setSessionStatus] = useState('');
    const [gameStatus, setGameStatus] = useState('');

    useEffect(() => {
        const sessionGuid = decode(props.match.params.session_id).replace(/-/g, "");
        network.current = new Network(sessionGuid, {
            setState: setState,
            setSessionStatus: setSessionStatus,
            setGameStatus: setGameStatus
        });

        if (sessionGuid) {
            localStorage.setItem('sessionGuid', sessionGuid);
            network.current.start();
            return () => network.current.stop();
        }
    }, []);

    function onRegisterPlayer(clientGuid) {
        localStorage.setItem('clientGuid', clientGuid);
        network.current.onRegisterPlayer(clientGuid);
    }

    function onConnect() {
        network.current.onConnect();
    }

    function onDeviceSelect(device) {
        network.current.onDeviceSelect(device);
    }

    /* State will be delivered from network.start(), but we start out without anything */
    if (state === null) return null;

    const { session, clientData, device } = state;

    return (
        <div>
            {session.guid && <Seed session={session} sessionStatus={sessionStatus} onRegisterPlayer={onRegisterPlayer} />}
            <br />
            {clientData !== null && <Patch seed={session.data.seed} world={session.data.seed.worlds.find(world => world.worldId === clientData.worldId)} />}
            <br />
            {clientData !== null && <Connection clientData={clientData} device={device} onConnect={onConnect} onDeviceSelect={onDeviceSelect} />}
            <br />
            {device.state === 1 && <Game gameStatus={gameStatus} network={network.current} />}
            <br />
            {session.data !== null && <Spoiler seedData={session.data.seed} />}
        </div>
    );
}
