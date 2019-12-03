import React, { Component } from 'react';
import Seed from './Seed';
import Patch from './Patch';
import Connection from './Connection';
import Runner from './Runner';
import Spoiler from './Spoiler';

import { HubConnectionBuilder } from '@microsoft/signalr';

import { create_message, connect, send, clearBusy } from '../usb2snes';

export default class Multiworld extends Component {
    constructor(props) {
        super(props);

        this.connection = null;
        this.socket = null;

        this.state = {
            sessionGuid: this.props.match.params.session_guid,
            sessionState: 0,
            sessionInfo: '',
            sessionData: null,
            clientData: null,
            connState: 0,
            connInfo: [''],
            deviceList: null,
            deviceSelect: false,
            deviceSelected: null,
            hubState: 0,
            hubConnection: null,
        };
    }

    async componentDidMount() {
        this.connection = new HubConnectionBuilder()
            .withUrl('/multiworldHub')
            .build();

        this.setState({ hubConnection: this.connection });

        this.connection.onclose(async () => {
            this.setState({ hubState: 0 });
            await this.startHub();
        });

        this.connection.on('UpdateClients', (clients) => {
            if (this.state.sessionData != null) {
                this.setState({ sessionData: { ...this.state.sessionData, clients } });
            }
        });

        if (this.state.sessionGuid) {
            localStorage.setItem('sessionGuid', this.state.sessionGuid);
            this.startSession();
        }
    }

    handleRegisterPlayer = async (clientGuid) => {
        /* If we're already registered, unregister from the old world first */
        localStorage.setItem('clientGuid', clientGuid);
        if (this.state.clientData != null) {
            try {
                const response = await this.connection.invoke('UnregisterPlayer', this.state.sessionGuid, this.state.clientData.guid);
                if (response === false) {
                    console.log('Could not unregister session');
                    return;
                }
            } catch (err) {
                console.log('Could not unregister session:', err);
                return;
            }

            this.setState({ clientData: null });
        }

        try {
            /* Register our session, returns the specific client data for us */
            const client = await this.connection.invoke('RegisterPlayer', this.state.sessionGuid, clientGuid);
            if (client == null) {
                console.log('Could not register client, try reloading the page');
                return;
            }

            this.setState({
                clientData: client,
                sessionInfo: `Session found, registered as player: ${client.name}`,
            });
        } catch (err) {
            console.log('Could not register client:', err);
        }
    };

    socket_onclose = () => {
        console.log('Connection closed');
        clearBusy();

        if (this.state.connState !== 0) {
            setTimeout(this.handleConnect, 1000);
            console.log('Trying to reconnect');
        }
        this.setState({ connState: 0, connDevice: 'None' });
    };

    handleConnect = async () => {
        try {
            if (this.state.connState === 1) {
                this.setState({ connState: 0 });
                this.socket.close();
                return;
            }

            this.socket = await connect('ws://localhost:8080');
            this.socket.onclose = this.socket_onclose;

            const response = await send(create_message('DeviceList', []));
            const deviceList = JSON.parse(response.data);
            const firstDevice = deviceList.Results[0];

            if (!firstDevice) {
                this.setState({ connState: 1 });
                this.socket.close();
                return;
            }

            if (deviceList.Results.length === 1) {
                await this.attachDevice(firstDevice);
            } else if (!this.state.deviceSelect) {
                this.setState({ deviceSelect: true, deviceList, deviceSelected: firstDevice });
            } else if (this.state.deviceSelected != null) {
                const attached = await this.attachDevice(this.state.deviceSelected);
                if (attached) {
                    this.setState({ deviceSelect: false, deviceList: null, deviceSelected: null });
                }
            }
        }
        catch (err) {
            console.log('Can not connect to the websocket, retrying:', err);
            this.setState({ connState: 0, connDevice: 'None' });
            setTimeout(this.handleConnect, 5000);
        }
    };

    handleDevice = (device) => {
        this.setState({ deviceSelected: device });
    };

    async attachDevice(device) {
        try {
            const attached = await send(create_message('Attach', [device]), true, 500);
            if (attached === true) {
                const response = await send(create_message('Info', []));
                const info = JSON.parse(response.data);
                await send(create_message('Name', [`Randomizer.live [${device}]`]), true);

                info.Results.isSnes = (device.includes('COM') || device.includes('SD2SNES'));
                let clientData = { ...this.state.clientData, device, state: 5 };
                this.setState({ connState: 1, clientData, connInfo: info.Results });

                const client = await this.connection.invoke('UpdateClient', clientData);
                if (client) {
                    clientData = { ...clientData, client };
                    this.setState({ clientData });
                }

                return true;
            }
        } catch (err) {
            console.log('Could not attach to device:', err);
            this.setState({ connState: 1 });
            this.socket.close();
        }
        return false;
    }

    async startSession() {
        this.setState({
            sessionState: 0,
            sessionInfo: 'Initializing session...'
        });

        try {
            const response = await fetch(`/api/multiworld/session/${this.state.sessionGuid}`);
            if (response.status !== 200) {
                this.setState({
                    sessionState: 0,
                    sessionInfo: 'Session not found',
                });
                return;
            }

            const sessionData = await response.json();
            this.setState({
                sessionData,
                sessionState: 1,
                sessionInfo: 'Session found, connecting to server',
            });

            await this.startHub();
        } catch (err) {
            this.setState({
                sessionState: 0,
                sessionInfo: `Error trying to establish session: ${err}`,
            });
        }
    }

    async startHub() {
        try {
            this.setState({ hubState: 0 });
            await this.connection.start();

            const registered = await this.connection.invoke('RegisterConnection', this.state.sessionGuid);
            if (registered) {
                this.setState({
                    hubState: 1,
                    sessionInfo: 'Session found, connected to server',
                });

                /* Check if we have locally stored client data, so we can register back to the session */
                if (this.state.clientData == null) {
                    const clientGuid = localStorage.getItem('clientGuid');
                    const sessionGuid = localStorage.getItem('sessionGuid');
                    if (sessionGuid === this.state.sessionGuid && clientGuid != null && clientGuid !== '') {
                        /* The stored session matches and we have a client id, register as this player */
                        const clientData = await this.connection.invoke('RegisterPlayer', this.state.sessionGuid, clientGuid);
                        if (clientData != null) {
                            this.setState({
                                clientData,
                                sessionInfo: `Session found, registered as player: ${clientData.name}`,
                            });
                        }
                    }
                }

            } else {
                this.setState({
                    hubState: 0,
                    sessionInfo: 'Session found, but could not connect to session',
                });
            }
        } catch (err) {
            console.log('Could not start connection to signalR hub:', err);
            setTimeout(() => this.startHub(), 5000);
        }
    }

    render() {
        const { sessionGuid, sessionState, sessionInfo, sessionData, clientData } = this.state;
        const { connState, connInfo, deviceList, deviceSelect } = this.state;
        const { hubState, hubConnection } = this.state;

        return (
            <div>
                {sessionGuid && <Seed
                    sessionGuid={sessionGuid}
                    sessionState={sessionState}
                    sessionInfo={sessionInfo}
                    sessionData={sessionData}
                    onRegisterPlayer={this.handleRegisterPlayer}
                />}
                <br />
                {clientData != null && <Patch
                    sessionData={sessionData}
                    clientData={clientData}
                    fileName={`${sessionData.seed.gameName} - ${sessionData.seed.seedNumber} - ${clientData.name}.sfc`}
                />}
                <br />
                {clientData != null && <Connection
                    clientData={clientData}
                    connState={connState}
                    connInfo={connInfo}
                    deviceList={deviceList}
                    deviceSelect={deviceSelect}
                    onConnect={this.handleConnect}
                    onDeviceSelect={this.handleDevice}
                />}
                <br />
                {connState === 1 && <Runner
                    sessionData={sessionData}
                    clientData={clientData}
                    connState={connState}
                    connInfo={connInfo}
                    hubState={hubState}
                    hubConnection={hubConnection}
                />}
                <br />
                {sessionData != null && <Spoiler sessionData={sessionData} />}
            </div>
        );
    }
}
