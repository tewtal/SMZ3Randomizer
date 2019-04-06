/* eslint-disable no-mixed-operators */
import React, { Component } from 'react';
import { readData, writeData } from '../usb2snes';

export class Runner extends Component {
    static displayName = Runner.name;

    constructor(props) {
        super(props);
        this.state = { events: [] };
        this.writeQueue = [];
        this.eventLoop = this.eventLoop.bind(this);
        this.readMessages = this.readMessages.bind(this);
        this.handleMessage = this.handleMessage.bind(this);
        this.sendMessage = this.sendMessage.bind(this);
        this.detectGame = this.detectGame.bind(this);
        this.receiveItem = this.receiveItem.bind(this);
        this.sendItem = this.sendItem.bind(this);
        this.timerHandle = 0;
        this.inPtr = -1;
        this.outPtr = -1;
        this.MessageBaseAddress = 0xE03700;
    }

    componentDidMount() {
        this.setState({ gameStatus: "Detecting game...", gameState: 0 });
        this.props.hubConnection.on("ReceiveItem", this.receiveItem);
        this.timerHandle = setTimeout(this.eventLoop, 200);
    }

    componentWillUnmount() {
        clearTimeout(this.timerHandle);
        this.props.hubConnection.off("ReceiveItem", this.receiveItem);
    }

    async eventLoop() {
        if (this.props.hubState === 1 && this.props.connState === 1) {
            if (this.state.gameState === 1) {
                await this.readMessages();
                await this.handleWriteQueue();
                this.timerHandle = setTimeout(this.eventLoop, 200);
                return;
            } else {
                /* Try to detect the game by looking at the specific hashes */
                await this.detectGame();
                this.timerHandle = setTimeout(this.eventLoop, 500);
                return;
            }
        } else {
            this.timerHandle = setTimeout(this.eventLoop, 1000);
            return;
        }
    }

    async detectGame() {
        /* Check SNES Mapping */
        let seedData = await readData(0x00FF50, 0x50);
        let seedGuid = String.fromCharCode.apply(null, seedData.slice(0x10, 0x30));
        let clientGuid = String.fromCharCode.apply(null, seedData.slice(0x30, 0x50));
        if (seedGuid === this.props.sessionData.seed.guid && clientGuid === this.props.clientData.guid) {
            this.MessageBaseAddress = 0xE03700;
            this.setState({
                gameState: 1, gameStatus: "Game detected and running, have fun!"
            });
            return;
        }

        /* Check SNES9x Mapping */
        seedData = await readData(0x407F50, 0x50);
        seedGuid = String.fromCharCode.apply(null, seedData.slice(0x10, 0x30));
        clientGuid = String.fromCharCode.apply(null, seedData.slice(0x30, 0x50));
        if (seedGuid === this.props.sessionData.seed.guid && clientGuid === this.props.clientData.guid) {
            this.MessageBaseAddress = 0xE03700;
            this.setState({
                gameState: 1, gameStatus: "Game detected and running, have fun!"
            });
            return;
        }

        /* Check Retroarch Mapping */
        seedData = await readData(0xC0FF50, 0x50);
        seedGuid = String.fromCharCode.apply(null, seedData.slice(0x10, 0x30));
        clientGuid = String.fromCharCode.apply(null, seedData.slice(0x30, 0x50));
        if (seedGuid === this.props.sessionData.seed.guid && clientGuid === this.props.clientData.guid) {
            this.MessageBaseAddress = 0x717700;
            this.setState({
                gameState: 1, gameStatus: "Game detected and running, have fun!"
            });
            return;
        }

    }

    receiveItem(worldId, itemId) {
        /* Push a message into the writeQueue to be written later by the main event loop */
        let message = [0x11, 0x10, worldId & 0xFF, (worldId << 8) & 0xFF, itemId & 0xFF, (itemId << 8) & 0xFF];
        this.writeQueue.push(message);
    }

    async sendItem(worldId, itemId) {
        try {
            return await this.props.hubConnection.invoke("SendItem", this.props.sessionData.guid, worldId, itemId);
        } catch (err) {
            console.log("Error sending item to player", err);
            return false;
        }            
    }

    async handleWriteQueue() {
        if (this.props.hubState === 1 && this.props.connState === 1) {
            while (this.writeQueue.length > 0) {
                let message = this.writeQueue.pop();
                try {
                    let ok = await this.sendMessage(message);
                    if (!ok) {
                        /* if there's an error while writing, push this message back and return completely */
                        this.writeQueue.push(message);
                        return;
                    }
                } catch (err) {
                    /* if there's an error while writing, push this message back and return completely */
                    this.writeQueue.push(message);
                    return;
                }
            }
        }
    }

    async readMessages() {
        /* Reads messages from the SNES message outbox */
        try {
            const snesMsg = await readData(this.MessageBaseAddress + 0x100, 0x090);

            /* If we got disconnected somehow, read back our pointers from the SNES to get back in sync */
            if (this.inPtr === -1 || this.outPtr === -1) {
                this.inPtr = snesMsg[0x086];
                this.outPtr = snesMsg[0x080];
            }

            let snesOutPtr = snesMsg[0x084];
            while (this.inPtr !== snesOutPtr) {
                let msgAddress = (this.inPtr * 0x10);
                let message = snesMsg.slice(msgAddress, msgAddress + 0x10);
                try {
                    let ok = await this.handleMessage(message);
                    if (ok) {
                        this.inPtr++;
                        this.inPtr = (this.inPtr === 8) ? 0 : this.inPtr
                        await writeData(this.MessageBaseAddress + 0x186, new Uint8Array([this.inPtr]));
                    } else {
                        /* if handling a message fails, bail out completely and retry next time */
                        return;
                    }
                } catch (err) {
                    return;
                }
            }
        } catch (err) {
            console.log("Error reading SNES messages");
        }
    }

    async handleMessage(msg) {
        const msgType = msg[0] + (msg[1] << 8);
        switch (msgType) {
            case 0x1001:
                {
                    /* 0x1001 = Send multiworld item to player */
                    let itemId = (msg[2] + (msg[3] << 8));
                    let worldId = (msg[4] + (msg[5] << 8));
                    let result = await this.sendItem(worldId, itemId);
                    return result;
                }
            default:
                {
                    /* Invalid message, ignore and move on */
                    return true;
                }
        }
    }

    async sendMessage(data) {
        try {
            await writeData(this.MessageBaseAddress + (this.outPtr * 0x10), new Uint8Array(data));

            this.outPtr++;
            this.outPtr = this.outPtr === 16 ? 0 : this.outPtr

            await writeData(this.MessageBaseAddress + 0x0180, new Uint8Array([this.outPtr]));

            return true;
        } catch (err) {
            return false;
        }

    }

    render() {
        return (
            <div className="container">
                <div className="row">
                    <div className="col-sm-6">
                        <div className="card">
                            <div className="card-body">
                                <h5 className="card-title">Game information</h5>
                                <p className="card-text">Status: {this.state.gameStatus}</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
