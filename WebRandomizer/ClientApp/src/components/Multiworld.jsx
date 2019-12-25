import React, { useState, useEffect, useRef } from 'react';
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
        const sessionGuid = props.match.params.session_guid;
        network.current = new Network(sessionGuid, {
            state: setState,
            sessionStatus: setSessionStatus,
            gameStatus: setGameStatus,
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
    if (state == null) return null;

    const { session, clientData, device } = state;

    return (
        <div>
            {session.guid && <Seed
                session={session}
                sessionStatus={sessionStatus}
                onRegisterPlayer={onRegisterPlayer}
            />}
            <br />
            {clientData != null && <Patch
                sessionData={session.data}
                clientData={clientData}
            />}
            <br />
            {clientData != null && <Connection
                clientData={clientData}
                device={device}
                onConnect={onConnect}
                onDeviceSelect={onDeviceSelect}
            />}
            <br />
            {device.state === 1 && <Game
                gameStatus={gameStatus}
                network={network.current}
            />}
            <br />
            {session.data != null && <Spoiler sessionData={session.data} />}
        </div>
    );
}
