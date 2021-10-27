import { HubConnectionBuilder } from '@microsoft/signalr';
import { createMessage, connect, send, clearBusy, readData, writeData } from '../snes/usb2snes';
import cloneDeep from 'lodash/cloneDeep';

export default class Network {

    constructor(sessionGuid, gameServiceHost, react) {
        this.connection = null;
        this.socket = null;
        this.inPtr = -1;
        this.outPtr = -1;
        this.itemInPtr = -1;
        this.itemOutPtr = -1;
        this.eventLoopTimer = 0;
        this.MessageBaseAddress = null;
        this.ItemsBaseAddress = null;
        this.GameServiceHost = gameServiceHost;

        this.session = {
            guid: sessionGuid,
            state: 0,
            data: null
        };
        this.clientData = null;
        this.device = {
            state: 0,
            version: '',
            list: null,
            selecting: false,
            selected: null
        };
        this.hub = { state: 0 };
        this.game = {
            state: 0,
            inEvents: [],
            outEvents: [],
            writeQueue: []
        };

        this.react = react;

        this.init();
    }

    updateState() {
        this.react.setState({
            session: cloneDeep(this.session),
            clientData: cloneDeep(this.clientData),
            device: cloneDeep(this.device),
            hub: cloneDeep(this.hub),
            game: cloneDeep(this.game)
        });
    }

    init() {
        this.connection = new HubConnectionBuilder()
            .withUrl(`https://${this.GameServiceHost}/multiworldHub`)
            .build();

        this.connection.onclose(() => {
            this.hub.state = 0;
            this.updateState();
            this.startHub();
        });

        this.connection.on('UpdateClients', clients => {
            if (this.session.data !== null) {
                this.session.data.clients = clients;
                this.updateState();
            }
        });
    }

    start() {
        this.startSession();
        this.updateState();
    }

    stop() {
        clearTimeout(this.eventLoopTimer);
    }

    async startSession() {
        this.session.state = 0;
        this.updateState();
        this.react.setSessionStatus('Initializing session...');

        try {
            const response = await fetch(`https://${this.GameServiceHost}/api/multiworld/session/${this.session.guid}`);
            if (response.status !== 200) {
                this.session.state = 0;
                this.updateState();
                this.react.setSessionStatus('Session not found');
                return;
            }

            const sessionData = await response.json();
            this.session.data = sessionData;
            this.session.state = 1;
            this.updateState();
            this.react.setSessionStatus('Session found, connecting to server');

            await this.startHub();
        } catch (error) {
            this.session.state = 0;
            this.updateState();
            this.react.setSessionStatus(`Error trying to establish session: ${error}`);
        }
    }

    startHub = async () => {
        try {
            this.hub.state = 0;
            this.updateState();
            await this.connection.start();

            const registered = await this.connection.invoke('RegisterConnection', this.session.guid);
            if (registered) {
                this.hub.state = 1;
                this.updateState();
                this.react.setSessionStatus('Session found, connected to server');

                /* Check if we have locally stored client data, so we can register back to the session */
                if (this.clientData === null) {
                    let clientGuid = localStorage.getItem(this.session.guid);
                    /* Backwards compatibility with old lookup to avoid players losing their sessions
                       This can be removed once enough time has elapsed */
                    if (clientGuid === null || clientGuid === '') {
                        const sessionGuid = localStorage.getItem('sessionGuid');
                        if (sessionGuid === this.session.guid) {
                            clientGuid = localStorage.getItem('clientGuid');
                        }
                    }
                    /* End backwards compatibile lookup */
                    if (clientGuid !== null && clientGuid !== '') {
                        /* We have a client id for the current session, register as this player */
                        const client = await this.connection.invoke('RegisterPlayer', this.session.guid, clientGuid);
                        if (client !== null) {
                            this.clientData = client;
                            this.updateState();
                            this.react.setSessionStatus(`Session found, registered as player: ${client.name}`);
                        }
                    }
                }
            } else {
                this.hub.state = 0;
                this.updateState();
                this.react.setSessionStatus('Session found, but could not connect to session');
            }
        } catch (error) {
            console.log('Could not start connection to signalR hub:', error);
            setTimeout(this.startHub, 5000);
        }
    };

    async onRegisterPlayer(clientGuid) {
        /* If we're connected to USB2SNES, disconnect first */
        if (this.device.state === 1) {
            this.game = { state: 0, inEvents: [], outEvents: [], writeQueue: [] };
            this.device = { state: 0, version: '', list: null, selecting: false, selected: null };
            this.socket.close();
            this.updateState();
        }

        /* If we're already registered, unregister from the old world first */
        if (this.clientData !== null) {
            try {
                const response = await this.connection.invoke('UnregisterPlayer', this.session.guid, this.clientData.guid);
                if (response === false) {
                    console.log('Could not unregister session');
                    return;
                }
            } catch (error) {
                console.log('Could not unregister session:', error);
                return;
            }

            this.clientData = null;
            this.updateState();
        }

        try {
            /* Register our session, returns the specific client data for us */
            const client = await this.connection.invoke('RegisterPlayer', this.session.guid, clientGuid);
            if (client === null) {
                console.log('Could not register client, try reloading the page');
                return;
            }

            this.clientData = client;
            this.updateState();
            this.react.setSessionStatus(`Session found, registered as player: ${client.name}`);
        } catch (error) {
            console.log('Could not register client:', error);
        }
    }

    socket_onclose = () => {
        console.log('Connection closed');
        clearBusy();

        if (this.device.state !== 0) {
            setTimeout(this.onConnect, 1000);
            console.log('Trying to reconnect');
        }
        clearTimeout(this.eventLoopTimer);
        this.device.state = 0;
        this.game.state = 0;
        this.updateState();
    };

    onConnect = async () => {
        if (this.device.state === 1) {
            this.device.state = 0;
            this.updateState();
            this.socket.close();
            return;
        }

            try {
                this.socket = await connect('ws://localhost:23074');
                this.socket.onclose = this.socket_onclose;
            } catch {
                try {
                    this.socket = await connect('ws://localhost:8080');
                } catch (error) {
                    console.log('Could not connect to the USB2SNES service, retrying:', error);
                    this.device.state = 0;
                    this.game.state = 0;
                    this.updateState();
                    setTimeout(this.onConnect, 5000);
                    return;
                }
            }

            this.socket.onclose = this.socket_onclose;

        try {
            const response = await send(createMessage('DeviceList', []));
            const deviceList = JSON.parse(response.data);
            const firstDevice = deviceList.Results[0];

            if (!firstDevice) {
                /* Set to 1 to signal a reconnect to socket_onclose */
                this.device.state = 1;
                this.socket.close();
                return;
            }

            if (deviceList.Results.length === 1) {
                await this.attachDevice(firstDevice);
            } else if (!this.device.selecting) {
                this.device = { ...this.device, selecting: true, list: deviceList, selected: firstDevice };
                this.updateState();
            } else if (this.device.selected !== null) {
                const attached = await this.attachDevice(this.device.selected);
                if (attached) {
                    this.device = { ...this.device, selecting: false, list: null, selected: null };
                    this.updateState();
                }
            }
        }
        catch (error) {
            console.log('Can not connect to the websocket, retrying:', error);
            this.device.state = 0;
            this.game.state = 0;
            this.updateState();
            setTimeout(this.onConnect, 5000);
        }
    };

    async attachDevice(device) {
        try {
            const attached = await send(createMessage('Attach', [device]), true, 500);
            if (attached === true) {
                const response = await send(createMessage('Info', []));
                const deviceInfo = JSON.parse(response.data);
                await send(createMessage('Name', [`Randomizer.live [${device}]`]), true);

                this.clientData = { ...this.clientData, device, state: 5 };
                this.device = { ...this.device, state: 1, version: deviceInfo.Results[0] };
                this.updateState();
                const client = await this.connection.invoke('UpdateClient', this.clientData);
                if (client) {
                    this.clientData.client = client;
                    this.updateState();
                }

                this.react.setGameStatus('Detecting game...');
                this.eventLoopTimer = setTimeout(this.eventLoop, 200);
                return true;
            }
        } catch (error) {
            console.log('Could not attach to device:', error);
            /* Set to 1 to signal a reconnect to socket_onclose */
            this.device.state = 1;
            this.socket.close();
        }
        return false;
    }

    onDeviceSelect(device) {
        this.device.selected = device;
        this.updateState();
    }

    eventLoop = async () => {
        if (this.hub.state === 1 && this.device.state === 1) {
            if (this.game.state === 1) {
                await this.syncSentItems();
                await this.syncReceivedItems();
            } else {
                await this.detectGame();
            }

            this.eventLoopTimer = setTimeout(this.eventLoop, 1000);
        }
    };

    /* Try to detect the game by looking at the specific hashes */
    async detectGame() {
        const mappings = this.session.data.seed.gameId === "smz3" ?
            [
                //  [SeedData, MsgBase , ItemBase]
                [0xE046A0, 0xE03700, 0xE04000],  /* Any sane platform with proper SRAM mapping */
                [0x7046A0, 0x703700, 0x704000]   /* Retroarch (bsnes-mercury) */
            ] :
            [                                   /* Super Metroid */
                [0x1C4F00, 0xE01E00, 0xE02000], /* SNES / Snes9x */
                [0xB8CF00, 0x701E00, 0x702000]  /* Retroarch */
            ];

        /* Check platform mappings */
        for (let i = 0; i < mappings.length; i++) {
            const addr = mappings[i][0];
            const seedData = await readData(addr, 0x50);
            const seedGuid = String.fromCharCode.apply(null, seedData.slice(0x10, 0x30));
            const clientGuid = String.fromCharCode.apply(null, seedData.slice(0x30, 0x50));
            if (seedGuid === this.session.data.seed.guid && clientGuid === this.clientData.guid) {
                [, this.MessageBaseAddress, this.ItemsBaseAddress] = mappings[i];
                this.game.state = 1;
                this.updateState();
                this.react.setGameStatus('Game detected, have fun!');
                return;
            }
        }
    }

    /* Checks for new outgoing items in the multiworld item list */
    async syncSentItems() {
        try {
            const snesItemSendPtrs = await readData(this.ItemsBaseAddress + 0x680, 0x04);

            this.itemInPtr = ushort_le_value(snesItemSendPtrs, 0x00);
            const snesItemOutPtr = ushort_le_value(snesItemSendPtrs, 0x02);

            while (this.itemInPtr < snesItemOutPtr) {
                const itemAddress = this.itemInPtr * 0x08;
                const message = await readData(this.ItemsBaseAddress + 0x700 + itemAddress, 0x08);
                try {
                    const ok = await this.handleItemMessage(message);
                    if (ok) {
                        this.itemInPtr += 1;
                        await writeData(this.ItemsBaseAddress + 0x680, new Uint8Array(ushort_le_bytes(this.itemInPtr)));
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

    async handleItemMessage(msg) {
        const worldId = ushort_le_value(msg, 0x00);
        const itemId = ushort_le_value(msg, 0x02);
        const itemIndex = ushort_le_value(msg, 0x04);
        const seq = this.itemInPtr;
        return await this.sendItem(worldId, itemId, itemIndex, seq);
    }

    async sendItem(worldId, itemId, itemIndex, seq) {
        try {
            return await this.connection.invoke('SendItem', this.session.data.guid, worldId, itemId, itemIndex, seq);
        } catch (error) {
            console.log('Error sending item to player', error);
            return false;
        }
    }

    async syncReceivedItems() {
        try {
            /* Make sure we're synced to the SNES */
            const snesItemSendPtrs = await readData(this.ItemsBaseAddress + 0x600, 0x04);
            this.itemOutPtr = ushort_le_value(snesItemSendPtrs, 0x02);

            /* Ask the server for all items from our last known item sequence */
            const events = await this.connection.invoke('GetEvents', this.session.data.guid, 'ItemReceived', this.itemOutPtr);
            for (let i = 0; i < events.length; i++) {
                const { playerId, itemId } = events[i];
                const msg = [...ushort_le_bytes(playerId), ...ushort_le_bytes(itemId)];
                const ok = await this.sendItemMessage(msg);
                if (!ok) {
                    console.log('Error when writing item resync');
                    return;
                }
            }
        } catch (error) {
            console.log(error);
        }
    }

    async sendItemMessage(data) {
        try {
            await writeData(this.ItemsBaseAddress + this.itemOutPtr * 0x04, new Uint8Array(data));
            this.itemOutPtr += 1;
            await writeData(this.ItemsBaseAddress + 0x602, new Uint8Array(ushort_le_bytes(this.itemOutPtr)));
            return true;
        } catch (error) {
            return false;
        }
    }

    async readMessages() {
        /* Reads messages from the SNES message outbox */
        try {
            const snesMsg = await readData(this.MessageBaseAddress + 0x100, 0x090);

            if (this.inPtr === -1 || this.outPtr === -1) {
                this.inPtr = snesMsg[0x086];
                this.outPtr = snesMsg[0x080];
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

            this.game.outEvents = snesHistory;
            this.updateState();

            while (this.inPtr !== snesOutPtr) {
                const msgAddress = this.inPtr * 0x10;
                const message = snesMsg.slice(msgAddress, msgAddress + 0x10);
                try {
                    const ok = await this.handleMessage(message);
                    if (ok) {
                        this.inPtr += 1;
                        this.inPtr = this.inPtr === 8 ? 0 : this.inPtr;
                        await writeData(this.MessageBaseAddress + 0x186, new Uint8Array([this.inPtr]));
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

    async handleMessage(msg) {
        const msgType = ushort_le_value(msg, 0x00);
        switch (msgType) {
            default:
                {
                    /* Invalid message, ignore and move on */
                    return true;
                }
        }
    }

    async sendMessage(data) {
        try {
            await writeData(this.MessageBaseAddress + this.outPtr * 0x10, new Uint8Array(data));
            this.outPtr += 1;
            this.outPtr = this.outPtr === 16 ? 0 : this.outPtr;
            await writeData(this.MessageBaseAddress + 0x0180, new Uint8Array([this.outPtr]));
            return true;
        } catch (error) {
            return false;
        }
    }
}

function ushort_le_value(array, offset) {
    return array[offset] + (array[offset + 1] << 8);
}

function ushort_le_bytes(x) {
    return [x & 0xFF, (x >> 8) & 0xFF];
}
