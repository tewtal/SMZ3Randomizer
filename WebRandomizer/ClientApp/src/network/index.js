import { HubConnectionBuilder } from '@microsoft/signalr';

import { create_message, connect, send, clearBusy, readData, writeData } from '../snes/usb2snes';

export default class Network {

    constructor(sessionGuid, react) {
        this.connection = null;
        this.socket = null;
        this.inPtr = -1;
        this.outPtr = -1;
        this.itemInPtr = -1;
        this.itemOutPtr = -1;
        this.eventLoopTimer = 0;
        this.MessageBaseAddress = 0xE03700;
        this.ItemsBaseAddress = 0xE04000;

        this.session = {
            guid: sessionGuid,
            state: 0,
            data: null,
        };
        this.clientData = null;
        this.device = {
            state: 0,
            version: '',
            list: null,
            selecting: false,
            selected: null,
        };
        this.hub = { state: 0 };
        this.game = {
            state: 0,
            inEvents: [],
            outEvents: [],
            writeQueue: [],
        };

        this.react = react;

        this.init();
    }

    /* get a snapshot of the state that is immutable vs the internal state */
    snapshot() {
        return {
            session: {
                ...this.session,
                data: this.session.data && { ...this.session.data },
            },
            clientData: this.clientData && { ...this.clientData },
            device: {
                ...this.device,
                list: this.device.list && [...this.device.list],
            },
            hub: { ...this.hub },
            game: {
                ...this.game,
                inEvents: [...this.game.inEvents],
                outEvents: [...this.game.outEvents],
                writeQueue: [...this.game.writeQueue],
            },
        };
    }

    init() {
        this.connection = new HubConnectionBuilder()
            .withUrl('/multiworldHub')
            .build();

        this.connection.onclose(() => {
            this.hub.state = 0;
            this.react.state(this.snapshot());
            this.startHub();
        });

        this.connection.on('UpdateClients', clients => {
            if (this.session.data != null) {
                this.session.data.clients = clients;
                this.react.state(this.snapshot());
            }
        });
    }

    start() {
        this.startSession();
        this.react.gameStatus('Detecting game...');
        this.eventLoopTimer = setTimeout(this.eventLoop, 200);
        this.react.state(this.snapshot());
    }

    stop() {
        clearTimeout(this.eventLoopTimer);
    }

    async startSession() {
        this.session.state = 0;
        this.react.state(this.snapshot());
        this.react.sessionStatus('Initializing session...');

        try {
            const response = await fetch(`/api/multiworld/session/${this.session.guid}`);
            if (response.status !== 200) {
                this.session.state = 0;
                this.react.state(this.snapshot());
                this.react.sessionStatus('Session not found');
                return;
            }

            const sessionData = await response.json();
            this.session.data = sessionData;
            this.session.state = 1;
            this.react.state(this.snapshot());
            this.react.sessionStatus('Session found, connecting to server');

            await this.startHub();
        } catch (error) {
            this.session.state = 0;
            this.react.state(this.snapshot());
            this.react.sessionStatus(`Error trying to establish session: ${error}`);
        }
    }

    startHub = async () => {
        try {
            this.hub.state = 0;
            this.react.state(this.snapshot());
            await this.connection.start();

            const registered = await this.connection.invoke('RegisterConnection', this.session.guid);
            if (registered) {
                this.hub.state = 1;
                this.react.state(this.snapshot());
                this.react.sessionStatus('Session found, connected to server');

                /* Check if we have locally stored client data, so we can register back to the session */
                if (this.clientData === null) {
                    const clientGuid = localStorage.getItem('clientGuid');
                    const sessionGuid = localStorage.getItem('sessionGuid');
                    if (sessionGuid === this.session.guid && clientGuid != null && clientGuid !== '') {
                        /* The stored session matches and we have a client id, register as this player */
                        const client = await this.connection.invoke('RegisterPlayer', this.session.guid, clientGuid);
                        if (client != null) {
                            this.clientData = client;
                            this.react.state(this.snapshot());
                            this.react.sessionStatus(`Session found, registered as player: ${client.name}`);
                        }
                    }
                }
            } else {
                this.hub.state = 0;
                this.react.state(this.snapshot());
                this.react.sessionStatus('Session found, but could not connect to session');
            }
        } catch (error) {
            console.log('Could not start connection to signalR hub:', error);
            setTimeout(this.startHub, 5000);
        }
    };

    async onRegisterPlayer(clientGuid) {
        /* If we're already registered, unregister from the old world first */
        if (this.clientData != null) {
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
            this.react.state(this.snapshot());
        }

        try {
            /* Register our session, returns the specific client data for us */
            const client = await this.connection.invoke('RegisterPlayer', this.session.guid, clientGuid);
            if (client === null) {
                console.log('Could not register client, try reloading the page');
                return;
            }

            this.clientData = client;
            this.react.state(this.snapshot());
            this.react.sessionStatus(`Session found, registered as player: ${client.name}`);
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
        this.device.state = 0;
        this.react.state(this.snapshot());
    };

    onConnect = async () => {
        try {
            if (this.device.state === 1) {
                this.device.state = 0;
                this.react.state(this.snapshot());
                this.socket.close();
                return;
            }

            this.socket = await connect('ws://localhost:8080');
            this.socket.onclose = this.socket_onclose;

            const response = await send(create_message('DeviceList', []));
            const deviceList = JSON.parse(response.data);
            const firstDevice = deviceList.Results[0];

            if (!firstDevice) {
                /* Set to 1 to signal a reconnect to socket_onclose */
                this.device.state = 1;
                this.react.state(this.snapshot());
                this.socket.close();
                return;
            }

            if (deviceList.Results.length === 1) {
                await this.attachDevice(firstDevice);
            } else if (!this.device.selecting) {
                this.device = { ...this.device, selecting: true, list: deviceList, selected: firstDevice };
                this.react.state(this.snapshot());
            } else if (this.device.selected != null) {
                const attached = await this.attachDevice(this.device.selected);
                if (attached) {
                    this.device = { ...this.device, selecting: false, list: null, selected: null };
                    this.react.state(this.snapshot());
                }
            }
        }
        catch (error) {
            console.log('Can not connect to the websocket, retrying:', error);
            this.device.state = 0;
            this.react.state(this.snapshot());
            setTimeout(this.onConnect, 5000);
        }
    };

    async attachDevice(device) {
        try {
            const attached = await send(create_message('Attach', [device]), true, 500);
            if (attached === true) {
                const response = await send(create_message('Info', []));
                const deviceInfo = JSON.parse(response.data);
                await send(create_message('Name', [`Randomizer.live [${device}]`]), true);

                this.clientData = { ...this.clientData, device, state: 5, };
                this.device = { ...this.device, state: 1, version: deviceInfo.Results[0] };
                this.react.state(this.snapshot());
                const client = await this.connection.invoke('UpdateClient', this.clientData);
                if (client) {
                    this.clientData.client = client;
                    this.react.state(this.snapshot());
                }

                return true;
            }
        } catch (error) {
            console.log('Could not attach to device:', error);
            /* Set to 1 to signal a reconnect to socket_onclose */
            this.device.state = 1;
            this.react.state(this.snapshot());
            this.socket.close();
        }
        return false;
    }

    onDeviceSelect(device) {
        this.device.selected = device;
        this.react.state(this.snapshot());
    }

    eventLoop = async () => {
        if (this.hub.state === 1 && this.device.state === 1) {
            if (this.game.state === 1) {
                await this.syncSentItems();
                await this.syncReceivedItems();
            } else {
                await this.detectGame();
            }
        }

        this.eventLoopTimer = setTimeout(this.eventLoop, 1000);
    };

    /* Try to detect the game by looking at the specific hashes */
    async detectGame() {
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
            if (seedGuid === this.session.data.seed.guid && clientGuid === this.clientData.guid) {
                [, this.MessageBaseAddress, this.ItemsBaseAddress] = mapping;
                this.game.state = 1;
                this.react.state(this.snapshot());
                this.react.gameStatus('Game detected, have fun!');
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
            this.react.state(this.snapshot());

            while (this.inPtr !== snesOutPtr) {
                const msgAddress = this.inPtr * 0x10;
                const message = snesMsg.slice(msgAddress, msgAddress + 0x10);
                try {
                    const ok = await this.handleMessage(message);
                    if (ok) {
                        this.inPtr += 1;
                        this.inPtr = this.inPtr === 8 ? 0 : this.inPtr
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
            this.outPtr = this.outPtr === 16 ? 0 : this.outPtr
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
    return [x && 0xFF, (x >> 8) && 0xFF];
}
