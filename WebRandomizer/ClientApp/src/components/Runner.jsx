/* eslint-disable no-mixed-operators */
import React, { Component } from 'react';
import { Container, Row, Col, Card, CardBody, CardTitle, CardText } from 'reactstrap';

import { readData, writeData } from '../usb2snes';

export default class Runner extends Component {
    static displayName = Runner.name;

    constructor(props) {
        super(props);

        this.state = { inEvents: [], outEvents: [] };

        this.writeQueue = [];
        this.timerHandle = 0;
        this.inPtr = -1;
        this.outPtr = -1;
        this.itemInPtr = -1;
        this.itemOutPtr = -1;
        this.MessageBaseAddress = 0xE03700;
        this.ItemsBaseAddress = 0xE04000;
    }

    componentDidMount() {
        this.setState({ gameState: 0, gameInfo: 'Detecting game...' });
        this.timerHandle = setTimeout(this.eventLoop, 200);
    }

    componentWillUnmount() {
        clearTimeout(this.timerHandle);
    }

    eventLoop = async () => {
        if (this.props.hubState === 1 && this.props.connState === 1) {
            if (this.state.gameState === 1) {
                await this.syncSentItems();
                await this.syncReceivedItems();
            } else {
                /* Try to detect the game by looking at the specific hashes */
                await this.detectGame();
            }
        }

        this.timerHandle = setTimeout(this.eventLoop, 1000);
    };

    detectGame = async () => {
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
                [, this.MessageBaseAddress, this.ItemsBaseAddress] = mapping;
                this.setState({ gameState: 1, gameStatus: "Game detected, have fun!" });
                return;
            }
        }
    };

    sendItem = async (worldId, itemId, itemIndex, seq) => {
        try {
            return await this.props.hubConnection.invoke('SendItem', this.props.sessionData.guid,
                parseInt(worldId, 10), parseInt(itemId, 10), parseInt(itemIndex, 10), parseInt(seq, 10));
        } catch (err) {
            console.log('Error sending item to player', err);
            return false;
        }
    };

    syncReceivedItems = async () => {
        try {
            /* Make sure we're synced to the SNES */
            const snesItemSendPtrs = await readData(this.ItemsBaseAddress + 0x600, 0x04);
            this.itemOutPtr = snesItemSendPtrs[0x02] + (snesItemSendPtrs[0x03] << 8);

            /* Ask the server for all items from our last known item sequence */
            const events = await this.props.hubConnection.invoke('GetEvents', this.props.sessionData.guid,
                'ItemReceived', parseInt(this.itemOutPtr, 10));
            for (let i = 0; i < events.length; i++) {
                const { playerId, itemId } = events[i];
                const message = [playerId & 0xFF, (playerId >> 8) & 0xFF, itemId & 0xFF, (itemId >> 8) & 0xFF];
                const ok = await this.sendItemMessage(message);
                if (!ok) {
                    console.log('Error when writing item resync');
                    return;
                }
            }
        } catch (err) {
            console.log(err);
        }
    };

    syncSentItems = async () => {
        /* Checks for new outgoing items in the multiworld item list */
        try {
            const snesItemSendPtrs = await readData(this.ItemsBaseAddress + 0x680, 0x04);

            this.itemInPtr = snesItemSendPtrs[0x00] + (snesItemSendPtrs[0x01] << 8);
            const snesItemOutPtr = snesItemSendPtrs[0x02] + (snesItemSendPtrs[0x03] << 8);

            while (this.itemInPtr < snesItemOutPtr) {
                const itemAddress = (this.itemInPtr * 0x08);
                const message = await readData(this.ItemsBaseAddress + 0x700 + itemAddress, 0x08);
                try {
                    const ok = await this.handleItemMessage(message);
                    if (ok) {
                        this.itemInPtr++;
                        await writeData(this.ItemsBaseAddress + 0x680, new Uint8Array([this.itemInPtr]));
                    } else {
                        /* if handling a message fails, bail out completely and retry next time */
                        return;
                    }
                } catch (err) {
                    console.log(err);
                    return;
                }
            }
        } catch (err) {
            console.log(err);
        }
    };

    handleItemMessage = async (message) => {
        const worldId = message[0x00] + (message[0x01] << 8);
        const itemId = message[0x02] + (message[0x03] << 8);
        const itemIndex = message[0x04] + (message[0x05] << 8);
        const seq = this.itemInPtr;
        return await this.sendItem(worldId, itemId, itemIndex, seq);
    };

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
                const histAddr = (historyPointer * 0x10);
                const histMsg = snesMsg.slice(histAddr, histAddr + 0x10);
                const itemId = (histMsg[2] + (histMsg[3] << 8));
                const worldId = (histMsg[4] + (histMsg[5] << 8));

                if (itemId > 0) {
                    snesHistory.push([worldId, itemId]);
                }

                historyPointer++;
                historyPointer = (historyPointer === 8) ? 0 : historyPointer;
            }

            this.setState({ outEvents: snesHistory });

            while (this.inPtr !== snesOutPtr) {
                const msgAddress = (this.inPtr * 0x10);
                const message = snesMsg.slice(msgAddress, msgAddress + 0x10);
                try {
                    const ok = await this.handleMessage(message);
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
            console.log('Error reading SNES messages');
        }
    }

    handleMessage = async (msg) => {
        const msgType = msg[0] + (msg[1] << 8);
        switch (msgType) {
            default:
                {
                    /* Invalid message, ignore and move on */
                    return true;
                }
        }
    };

    sendItemMessage = async (data) => {
        try {
            await writeData(this.ItemsBaseAddress + (this.itemOutPtr * 0x04), new Uint8Array(data));
            this.itemOutPtr++;
            await writeData(this.ItemsBaseAddress + 0x602, new Uint8Array([this.itemOutPtr]));

            return true;
        } catch (err) {
            return false;
        }
    };

    sendMessage = async (data) => {
        try {
            await writeData(this.MessageBaseAddress + (this.outPtr * 0x10), new Uint8Array(data));

            this.outPtr++;
            this.outPtr = this.outPtr === 16 ? 0 : this.outPtr

            await writeData(this.MessageBaseAddress + 0x0180, new Uint8Array([this.outPtr]));

            return true;
        } catch (err) {
            return false;
        }
    };

    render() {
        return (
            <Container>
                <Row>
                    <Col sm="4">
                        <Card>
                            <CardBody>
                                <CardTitle tag="h5">Game information</CardTitle>
                                <CardText>Status: {this.state.gameInfo}</CardText>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </Container>
        );
    }
}
