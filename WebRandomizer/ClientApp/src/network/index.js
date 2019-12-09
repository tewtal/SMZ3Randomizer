import { HubConnectionBuilder } from '@microsoft/signalr';

import { create_message, connect, send, clearBusy, readData, writeData } from '../snes/usb2snes';

export default function network(sessionGuid, react) {
    const methods = {};

    let connection = null;
    let socket = null;
    let inPtr = -1;
    let outPtr = -1;
    let itemInPtr = -1;
    let itemOutPtr = -1;
    let eventLoopTimer = 0;
    let MessageBaseAddress = 0xE03700;
    let ItemsBaseAddress = 0xE04000;

    let session = {
        guid: sessionGuid,
        state: 0,
        data: null,
    };
    let clientData = null;
    let device = {
        state: 0,
        version: '',
        list: null,
        selecting: false,
        selected: null,
    };
    let hub = { state: 0 };
    let game = {
        state: 0,
        inEvents: [],
        outEvents: [],
        writeQueue: [],
    };

    /* get a snapshot of the state that is immutable vs the internal state */
    function snapshot() {
        return {
            session: {
                ...session,
                data: session.data && { ...session.data },
            },
            clientData: clientData && { ...clientData },
            device: {
                ...device,
                list: device.list && [...device.list]
            },
            hub: { ...hub },
            game: {
                ...game,
                inEvents: [...game.inEvents],
                outEvents: [...game.outEvents],
                writeQueue: [...game.writeQueue],
            },
        };
    }

    connection = new HubConnectionBuilder()
        .withUrl('/multiworldHub')
        .build();

    connection.onclose(async () => {
        hub.state = 0;
        react.state(snapshot());
        startHub();
    });

    connection.on('UpdateClients', clients => {
        if (session.data != null) {
            session.data.clients = clients;
            react.state(snapshot());
        }
    });

    methods.start = () => {
        startSession();
        react.gameStatus('Detecting game...');
        eventLoopTimer = setTimeout(eventLoop, 200);
        react.state(snapshot());
    };

    methods.stop = () => {
        clearTimeout(eventLoopTimer);
    };

    async function startSession() {
        session.state = 0;
        react.state(snapshot());
        react.sessionStatus('Initializing session...');

        try {
            const response = await fetch(`/api/multiworld/session/${session.guid}`);
            if (response.status !== 200) {
                session.state = 0;
                react.state(snapshot());
                react.sessionStatus('Session not found');
                return;
            }

            const sessionData = await response.json();
            session.data = sessionData;
            session.state = 1;
            react.state(snapshot());
            react.sessionStatus('Session found, connecting to server');

            await startHub();
        } catch (error) {
            session.state = 0;
            react.state(snapshot());
            react.sessionStatus(`Error trying to establish session: ${error}`);
        }
    }

    async function startHub() {
        try {
            hub.state = 0;
            react.state(snapshot());
            await connection.start();

            const registered = await connection.invoke('RegisterConnection', session.guid);
            if (registered) {
                hub.state = 1;
                react.state(snapshot());
                react.sessionStatus('Session found, connected to server');

                /* Check if we have locally stored client data, so we can register back to the session */
                if (clientData === null) {
                    const clientGuid = localStorage.getItem('clientGuid');
                    const sessionGuid = localStorage.getItem('sessionGuid');
                    if (sessionGuid === session.guid && clientGuid != null && clientGuid !== '') {
                        /* The stored session matches and we have a client id, register as this player */
                        const client = await connection.invoke('RegisterPlayer', session.guid, clientGuid);
                        if (client != null) {
                            clientData = client;
                            react.state(snapshot());
                            react.sessionStatus(`Session found, registered as player: ${clientData.name}`);
                        }
                    }
                }
            } else {
                hub.state = 0;
                react.state(snapshot());
                react.sessionStatus('Session found, but could not connect to session');
            }
        } catch (error) {
            console.log('Could not start connection to signalR hub:', error);
            setTimeout(startHub, 5000);
        }
    }

    methods.onRegisterPlayer = onRegisterPlayer;
    async function onRegisterPlayer(clientGuid) {
        /* If we're already registered, unregister from the old world first */
        if (clientData != null) {
            try {
                const response = await connection.invoke('UnregisterPlayer', session.guid, clientData.guid);
                if (response === false) {
                    console.log('Could not unregister session');
                    return;
                }
            } catch (error) {
                console.log('Could not unregister session:', error);
                return;
            }

            clientData = null;
            react.state(snapshot());
        }

        try {
            /* Register our session, returns the specific client data for us */
            const client = await connection.invoke('RegisterPlayer', session.guid, clientGuid);
            if (client === null) {
                console.log('Could not register client, try reloading the page');
                return;
            }

            clientData = client;
            react.state(snapshot());
            react.sessionStatus(`Session found, registered as player: ${clientData.name}`);
        } catch (error) {
            console.log('Could not register client:', error);
        }
    }

    function socket_onclose() {
        console.log('Connection closed');
        clearBusy();

        if (device.state !== 0) {
            setTimeout(onConnect, 1000);
            console.log('Trying to reconnect');
        }
        device.state = 0;
        react.state(snapshot());
    }

    methods.onConnect = onConnect;
    async function onConnect() {
        try {
            if (device.state === 1) {
                device.state = 0;
                react.state(snapshot());
                socket.close();
                return;
            }

            socket = await connect('ws://localhost:8080');
            socket.onclose = socket_onclose;

            const response = await send(create_message('DeviceList', []));
            const deviceList = JSON.parse(response.data);
            const firstDevice = deviceList.Results[0];

            if (!firstDevice) {
                /* Set to 1 to signal a reconnect to socket_onclose */
                device.state = 1;
                react.state(snapshot());
                socket.close();
                return;
            }

            if (deviceList.Results.length === 1) {
                await attachDevice(firstDevice);
            } else if (!device.selecting) {
                device = { ...device, selecting: true, list: deviceList, selected: firstDevice };
                react.state(snapshot());
            } else if (device.selected != null) {
                const attached = await attachDevice(device.selected);
                if (attached) {
                    device = { ...device, selecting: false, list: null, selected: null };
                    react.state(snapshot());
                }
            }
        }
        catch (error) {
            console.log('Can not connect to the websocket, retrying:', error);
            device.state = 0;
            react.state(snapshot());
            setTimeout(onConnect, 5000);
        }
    }

    async function attachDevice(device) {
        try {
            const attached = await send(create_message('Attach', [device]), true, 500);
            if (attached === true) {
                const response = await send(create_message('Info', []));
                const deviceInfo = JSON.parse(response.data);
                await send(create_message('Name', [`Randomizer.live [${device}]`]), true);

                clientData = { ...clientData, device, state: 5, };
                device = { ...device, state: 1, version: deviceInfo.Results[0] };
                react.state(snapshot());
                const client = await connection.invoke('UpdateClient', clientData);
                if (client) {
                    clientData.client = client;
                    react.state(snapshot());
                }

                return true;
            }
        } catch (error) {
            console.log('Could not attach to device:', error);
            /* Set to 1 to signal a reconnect to socket_onclose */
            device.state = 1;
            react.state(snapshot());
            socket.close();
        }
        return false;
    }

    methods.onDeviceSelect = onDeviceSelect;
    function onDeviceSelect(device) {
        device.selected = device;
        react.state(snapshot());
    }

    async function eventLoop() {
        if (hub.state === 1 && device.state === 1) {
            if (game.state === 1) {
                await syncSentItems();
                await syncReceivedItems();
            } else {
                await detectGame();
            }
        }

        eventLoopTimer = setTimeout(eventLoop, 1000);
    }

    /* Try to detect the game by looking at the specific hashes */
    async function detectGame() {
        const mappings = [
            [0x00FF50, 0xE03700, 0xE04000], /* SNES */
            [0x407F50, 0xE03700, 0xE04000], /* SNES9x */
            [0xC0FF50, 0x703700, 0x704000], /* Retroarch */
        ];

        /* Check platform mappings */
        for (const mapping in mappings) {
            const [addr] = mapping;
            const seedData = await readData(addr, 0x50);
            const seedGuid = String.fromCharCode.apply(null, seedData.slice(0x10, 0x30));
            const clientGuid = String.fromCharCode.apply(null, seedData.slice(0x30, 0x50));
            if (seedGuid === session.data.seed.guid && clientGuid === clientData.guid) {
                [, MessageBaseAddress, ItemsBaseAddress] = mapping;
                game.state = 1;
                react.state(snapshot());
                react.gameStatus('Game detected, have fun!');
                return;
            }
        }
    }

    /* Checks for new outgoing items in the multiworld item list */
    async function syncSentItems() {
        try {
            const snesItemSendPtrs = await readData(ItemsBaseAddress + 0x680, 0x04);

            itemInPtr = ushort_le_value(snesItemSendPtrs, 0x00);
            const snesItemOutPtr = ushort_le_value(snesItemSendPtrs, 0x02);

            while (itemInPtr < snesItemOutPtr) {
                const itemAddress = itemInPtr * 0x08;
                const message = await readData(ItemsBaseAddress + 0x700 + itemAddress, 0x08);
                try {
                    const ok = await handleItemMessage(message);
                    if (ok) {
                        itemInPtr += 1;
                        await writeData(ItemsBaseAddress + 0x680, new Uint8Array(ushort_le_bytes(itemInPtr)));
                    } else {
                        /* if handling a message fails, bail out completely and retry next time */
                        return;
                    }
                } catch (error) {
                    console.log(error);
                    return;
                }
            }
        } catch (error) {
            console.log(error);
        }
    }

    async function handleItemMessage(msg) {
        const worldId = ushort_le_value(msg, 0x00);
        const itemId = ushort_le_value(msg, 0x02);
        const itemIndex = ushort_le_value(msg, 0x04);
        const seq = itemInPtr;
        return await sendItem(worldId, itemId, itemIndex, seq);
    }

    async function sendItem(worldId, itemId, itemIndex, seq) {
        try {
            return await connection.invoke('SendItem', session.data.guid,
                parseInt(worldId, 10), parseInt(itemId, 10), parseInt(itemIndex, 10), parseInt(seq, 10));
        } catch (error) {
            console.log('Error sending item to player', error);
            return false;
        }
    }

    async function syncReceivedItems() {
        try {
            /* Make sure we're synced to the SNES */
            const snesItemSendPtrs = await readData(ItemsBaseAddress + 0x600, 0x04);
            itemOutPtr = ushort_le_value(snesItemSendPtrs, 0x02);

            /* Ask the server for all items from our last known item sequence */
            const events = await connection.invoke('GetEvents', session.data.guid, 'ItemReceived', parseInt(itemOutPtr, 10));
            for (let i = 0; i < events.length; i++) {
                const { playerId, itemId } = events[i];
                const msg = [...ushort_le_bytes(playerId), ...ushort_le_bytes(itemId)];
                const ok = await sendItemMessage(msg);
                if (!ok) {
                    console.log('Error when writing item resync');
                    return;
                }
            }
        } catch (error) {
            console.log(error);
        }
    }

    async function sendItemMessage(data) {
        try {
            await writeData(ItemsBaseAddress + itemOutPtr * 0x04, new Uint8Array(data));
            itemOutPtr += 1;
            await writeData(ItemsBaseAddress + 0x602, new Uint8Array(ushort_le_bytes(itemOutPtr)));
            return true;
        } catch (error) {
            return false;
        }
    }

    async function readMessages() {
        /* Reads messages from the SNES message outbox */
        try {
            const snesMsg = await readData(MessageBaseAddress + 0x100, 0x090);

            if (inPtr === -1 || outPtr === -1) {
                inPtr = snesMsg[0x086];
                outPtr = snesMsg[0x080];
            }

            const snesOutPtr = snesMsg[0x084];

            const snesHistory = [];
            let historyPointer = snesOutPtr;

            for (let i = 0; i < 8; i++) {
                const histAddr = historyPointer * 0x10;
                const histMsg = snesMsg.slice(histAddr, histAddr + 0x10);
                const itemId = ushort_le_value(histMsg, 0x02);
                const worldId = ushort_le_value(histMsg, 0x04);

                if (itemId > 0) {
                    snesHistory.push([worldId, itemId]);
                }

                historyPointer += 1;
                historyPointer = historyPointer === 8 ? 0 : historyPointer;
            }

            game.outEvents = snesHistory;
            react.state(snapshot());

            while (inPtr !== snesOutPtr) {
                const msgAddress = inPtr * 0x10;
                const message = snesMsg.slice(msgAddress, msgAddress + 0x10);
                try {
                    const ok = await handleMessage(message);
                    if (ok) {
                        inPtr += 1;
                        inPtr = inPtr === 8 ? 0 : inPtr
                        await writeData(MessageBaseAddress + 0x186, new Uint8Array([inPtr]));
                    } else {
                        /* if handling a message fails, bail out completely and retry next time */
                        return;
                    }
                } catch (error) {
                    return;
                }
            }
        } catch (error) {
            console.log('Error reading SNES messages');
        }
    }

    async function handleMessage(msg) {
        const msgType = ushort_le_value(msg, 0x00);
        switch (msgType) {
            default:
                {
                    /* Invalid message, ignore and move on */
                    return true;
                }
        }
    }

    async function sendMessage(data) {
        try {
            await writeData(MessageBaseAddress + outPtr * 0x10, new Uint8Array(data));
            outPtr += 1;
            outPtr = outPtr === 16 ? 0 : outPtr
            await writeData(MessageBaseAddress + 0x0180, new Uint8Array([outPtr]));
            return true;
        } catch (error) {
            return false;
        }
    };

    function ushort_le_value(array, offset) {
        return array[offset] + (array[offset + 1] << 8);
    }

    function ushort_le_bytes(x) {
        return [x && 0xFF, (x >> 8) && 0xFF];
    }

    return methods;
}
