/* eslint-disable no-mixed-operators */
import React, { Component } from 'react';
import { readData, writeData } from '../usb2snes';
import { Button, Row, Col } from 'reactstrap';

export class Runner extends Component {
    static displayName = Runner.name;

    constructor(props) {
        super(props);
        this.state = { inEvents: [], outEvents: [] };
        this.writeQueue = [];
        this.eventLoop = this.eventLoop.bind(this);
        this.readMessages = this.readMessages.bind(this);
        this.handleMessage = this.handleMessage.bind(this);
        this.sendMessage = this.sendMessage.bind(this);
        this.detectGame = this.detectGame.bind(this);
        this.receiveItem = this.receiveItem.bind(this);
        this.sendItem = this.sendItem.bind(this);
        this.resend = this.resend.bind(this);
        this.sendSelectedItem = this.sendSelectedItem.bind(this);

        this.sendItemRef = React.createRef();
        this.sendPlayerRef = React.createRef();

        this.timerHandle = 0;
        this.inPtr = -1;
        this.outPtr = -1;
        this.MessageBaseAddress = 0xE03700;
        this.itemNames = {
            0x60: "ProgressiveTunic",
            0x5F: "ProgressiveShield",
            0x5E: "ProgressiveSword",
            0x0B: "Bow",
            0x58: "SilverArrows",
            0x0C: "BlueBoomerang",
            0x2A: "RedBoomerang",
            0x0A: "Hookshot",
            0x29: "Mushroom",
            0x0D: "Powder",
            0x07: "Firerod",
            0x08: "Icerod",
            0x0f: "Bombos",
            0x10: "Ether",
            0x11: "Quake",
            0x12: "Lamp",
            0x09: "Hammer",
            0x13: "Shovel",
            0x14: "Flute",
            0x21: "Bugnet",
            0x1D: "Book",
            0x16: "Bottle",
            0x15: "Somaria",
            0x18: "Byrna",
            0x19: "Cape",
            0x1A: "Mirror",
            0x4B: "Boots",
            0x61: "ProgressiveGlove",
            0x1E: "Flippers",
            0x1F: "MoonPearl",
            0x4E: "HalfMagic",
            0x17: "HeartPiece",
            0x3E: "HeartContainer",
            0x3F: "HeartContainerRefill",
            0x28: "ThreeBombs",
            0x43: "Arrow",
            0x44: "TenArrows",
            0x34: "OneRupee",
            0x35: "FiveRupees",
            0x36: "TwentyRupees",
            0x47: "TwentyRupees2",
            0x41: "FiftyRupees",
            0x40: "OneHundredRupees",
            0x46: "ThreeHundredRupees",
            0x51: "BombUpgrade5",
            0x52: "BombUpgrade10",
            0x53: "ArrowUpgrade5",
            0x54: "ArrowUpgrade10",
            0xC2: "Missile",
            0xC3: "Super",
            0xC4: "PowerBomb",
            0xB0: "Grapple",
            0xB1: "XRay",
            0xC0: "ETank",
            0xC1: "ReserveTank",
            0xBB: "Charge",
            0xBC: "Ice",
            0xBD: "Wave",
            0xBE: "Spazer",
            0xBF: "Plasma",
            0xB2: "Varia",
            0xB6: "Gravity",
            0xB4: "Morph",
            0xB9: "Bombs",
            0xB3: "SpringBall",
            0xB5: "ScrewAttack",
            0xB7: "HiJump",
            0xB8: "SpaceJump",
            0xBA: "SpeedBooster",
            0x2B: "BottleWithRedPotion",
            0x2C: "BottleWithGreenPotion",
            0x2D: "BottleWithBluePotion",
            0x3D: "BottleWithFairy",
            0x3C: "BottleWithBee",
            0x48: "BottleWithGoldBee",
            0x2E: "RedContent",
            0x2F: "GreenContent",
            0x30: "BlueContent",
            0x0E: "BeeContent"
        };
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
                gameState: 1, gameStatus: "Game detected, have fun!"
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
                gameState: 1, gameStatus: "Game detected, have fun!"
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
                gameState: 1, gameStatus: "Game detected, have fun!"
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
            return await this.props.hubConnection.invoke("SendItem", this.props.sessionData.guid, parseInt(worldId, 10), parseInt(itemId, 10));
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

            if (this.inPtr === -1 || this.outPtr === -1) {
                this.inPtr = snesMsg[0x086];
                this.outPtr = snesMsg[0x080];
            }

            let snesOutPtr = snesMsg[0x084];

            let snesHistory = [];
            let historyPointer = snesOutPtr;

            for (let i = 0; i < 8; i++) {
                let histAddr = (historyPointer * 0x10);
                let histMsg = snesMsg.slice(histAddr, histAddr + 0x10);
                let itemId = (histMsg[2] + (histMsg[3] << 8));
                let worldId = (histMsg[4] + (histMsg[5] << 8));

                if (itemId > 0) {
                    snesHistory.push([worldId, itemId]);
                }

                historyPointer++;
                historyPointer = (historyPointer === 8) ? 0 : historyPointer;
            }

            this.setState({ outEvents: snesHistory });

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

    async resend(e) {
        let worldId = e.target.dataset.world;
        let itemId = e.target.dataset.itemid;
        await this.sendItem(worldId, itemId);
    }

    async sendSelectedItem(e) {
        let selectedPlayer = this.sendPlayerRef.current.options[this.sendPlayerRef.current.selectedIndex];
        let selectedItem = this.sendItemRef.current.options[this.sendItemRef.current.selectedIndex];

        let worldId = selectedPlayer.dataset.world;
        let itemId = selectedItem.dataset.itemid;

        await this.sendItem(worldId, itemId);
    }

    render() {
        const sentItems = [];
        const itemNames = [];
        const playerNames = [];

        let lastEvents = this.state.outEvents.reverse();
        for (let i = 0; i < this.state.outEvents.length; i++) {
            sentItems.push(<tr><td>{this.props.sessionData.seed.worlds[lastEvents[i][0]].player}</td><td>{this.itemNames[lastEvents[i][1]]}</td><td><Button data-world={lastEvents[i][0]} data-itemid={lastEvents[i][1]} color="primary" onClick={this.resend}>Resend item</Button></td></tr>); 
        }

        for (let key in this.itemNames) {
            itemNames.push(<option key={"send-item-" + key} data-itemid={key}>{this.itemNames[key]}</option>);
        }

        for (let i = 0; i < this.props.sessionData.seed.players; i++) {
            playerNames.push(<option key={"send-player-" + i} data-world={i}>{this.props.sessionData.seed.worlds[i].player}</option>);
        }

        return (
            <div className="container">
                <div className="row">
                    <div className="col-sm-4">
                        <div className="card">
                            <div className="card-body">
                                <h5 className="card-title">Game information</h5>
                                <p className="card-text">Status: {this.state.gameStatus}</p>
                            </div>
                        </div>
                    </div>
                    <div className="col-sm-8">
                        <div className="card">
                            <div className="card-body">
                                <Row>
                                    <Col>
                                    <h5 className="card-title">Sent items</h5>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <th>Player</th>
                                                    <th>Item</th>
                                                    <th></th>
                                                </tr>
                                                {sentItems}
                                                <tr>
                                                    <td>
                                                        <select id="send-player" ref={this.sendPlayerRef}>
                                                            {playerNames}
                                                        </select>
                                                    </td>
                                                    <td>
                                                        <select id="send-item" ref={this.sendItemRef}>
                                                            {itemNames}
                                                        </select>
                                                    </td>
                                                    <td>
                                                        <Button onClick={this.sendSelectedItem} color="danger">Send</Button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </Col>
                                </Row>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
