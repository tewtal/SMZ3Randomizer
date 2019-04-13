import React, { Component } from 'react';
import { Seed } from './Seed';
import { Patch } from './Patch';
import { HubConnectionBuilder } from '@aspnet/signalr';
import { Connection } from './Connection';
import { Runner } from './Runner';
import { Spoiler } from './Spoiler';
import { create_message, connect, send, clearBusy } from '../usb2snes';

export class Multiworld extends Component {
    static displayName = Multiworld.name;
    constructor(props) {
        super(props);
        this.connection = null;
        this.socket = null;

        this.state = {
            sessionId: this.props.match.params.session_id,
            sessionState: 0,
            sessionInfo: "",
            sessionData: null,
            hubState: 0,
            hubConnection: null,
            clientData: null,
            connState: 0,
            connInfo: [""],
        };


        this.handleRegisterPlayer = this.handleRegisterPlayer.bind(this);
        this.handleConnect = this.handleConnect.bind(this);
    }

    async componentDidMount() {
        this.connection = new HubConnectionBuilder()
            .withUrl("/multiworldHub")
            .build();

        this.setState({
            hubConnection: this.connection
        });

        this.connection.onclose(async () => {
            this.setState({ hubState: 0 });
            await this.startHub();
        });

        this.connection.on("UpdateClients", async (clients) => {
            if (this.state.sessionData != null) {
                this.state.sessionData.clients = clients;
                this.setState({
                    sessionData: this.state.sessionData
                });
            }
        });

        if (this.state.sessionId) {
            localStorage.setItem("sessionGuid", this.state.sessionId);
            this.startSession();
        }
    }

    async handleRegisterPlayer(clientGuid) {
        /* If we're already registered, unregister from the old world first */
        localStorage.setItem("clientGuid", clientGuid);
        if (this.state.clientData !== null) {
            try {
                let response = await this.connection.invoke("UnregisterPlayer", this.state.sessionId, this.state.clientData.guid);
                if (response === false) {
                    console.log("Could not unregister session");
                    return;
                }
            } catch (err) {
                console.log("Could not unregister session", err);
                return;
            }

            this.setState({ clientData: null });
        }

        try {
            /* Register our session, returns a the specific client data for us */
            let client = await this.connection.invoke("RegisterPlayer", this.state.sessionId, clientGuid);
            if (client === null) {
                console.log("Could not register client, try reloading the page.");
                return;
            }

            this.setState({
                clientData: client,
                sessionInfo: "Session found, registered as player: " + client.name
            });
        } catch (err) {
            console.log("Could not register client:", err);
        }
    }

    socket_onclose() {
        console.log("Connection closed.");
        clearBusy();

        if (this.state.connState !== 0) {
            setTimeout(this.handleConnect, 1000);
            console.log("Trying to reconnect");
        }
        this.setState({ connState: 0, connDevice: "None" });
    };

    async handleConnect() {
        try {
            if (this.state.connState === 1) {
                this.state.connState = 0;
                this.socket.close();
                return;
            }

            this.socket = await connect("ws://localhost:8080");
            this.socket.onclose = this.socket_onclose.bind(this);


            let deviceList = await send(create_message("DeviceList", []));
            let devices = JSON.parse(deviceList.data);
            let firstDevice = devices.Results[0];

            if (!firstDevice) {
                this.state.connState = 1;
                this.socket.close();
                return;
            }

            try {
                let ok = await send(create_message("Attach", [firstDevice]), true, 500);
                if (ok === true) {
                    let result = await send(create_message("Info", []));
                    let info = JSON.parse(result.data);
                    ok = await send(create_message("Name", ["Randomizer.live [" + firstDevice + "]"]), true);

                    info.Results.isSnes = (firstDevice.includes("COM") || firstDevice.includes("SD2SNES"));
                    this.state.clientData.device = firstDevice;
                    this.state.clientData.state = 5;
                    this.setState({ connState: 1, clientData: this.state.clientData, connInfo: info.Results });

                    let client = await this.connection.invoke("UpdateClient", this.state.clientData);
                    if (client) {
                        this.state.clientData = client;
                        this.setState({ clientData: this.state.clientData });
                    }
                }
            } catch (err) {
                console.log("Couldn't attach to device:", err);
                this.state.connState = 1;
                this.socket.close();
                return;
            }
        }
        catch (err) {
            console.log("Can't connect to the websocket - retrying", err);
            this.setState({ connState: 0, connDevice: "None" });
            setTimeout(this.handleConnect, 5000);
        }
    }

    async startSession() {
        this.setState({
            sessionState: 0,
            sessionInfo: "Initializing session..."
        });       

        try {
            let response = await fetch("/api/multiworld/session/" + this.state.sessionId);
            if (response.status !== 200) {
                this.setState({
                    sessionState: 0,
                    sessionInfo: "Session not found"
                });
                return;
            }

            let sessionData = await response.json();
            this.setState({
                sessionData: sessionData,
                sessionState: 1,
                sessionInfo: "Session found, connecting to server"
            });

            await this.startHub();
        } catch (err) {
            this.setState({
                sessionState: 0,
                sessionInfo: "Error trying to establish session: " + err
            });
        }
    }

    async startHub() {
        try {
            this.setState({ hubState: 0 });
            await this.connection.start();

            let registered = await this.connection.invoke("RegisterConnection", this.state.sessionId);
            if (registered) {
                this.setState({
                    hubState: 1,
                    sessionInfo: "Session found, connected to server"
                });

                /* Check if we have locally stored client data, so we can register back to the session */
                if (this.state.clientData === null) {
                    let clientGuid = localStorage.getItem("clientGuid");
                    let sessionGuid = localStorage.getItem("sessionGuid");
                    if (sessionGuid === this.state.sessionId && clientGuid !== null && clientGuid !== "") {
                        /* The stored session matches and we have a client id, register as this player */
                        let client = await this.connection.invoke("RegisterPlayer", this.state.sessionId, clientGuid);
                        if (client !== null) {
                            this.setState({ clientData: client, sessionInfo: "Session found, registered as player: " + client.name });
                        }
                    }
                }

            } else {
                this.setState({
                    hubState: 0,
                    sessionInfo: "Session found, but could not connect to session"
                });
            }
        } catch (err) {
            console.log("Could not start connection to signalR hub:", err);
            setTimeout(() => this.startHub(), 5000);
        }
    }

    render() {
        return (
            <div>
                {this.state.sessionId ? (<Seed onRegisterPlayer={this.handleRegisterPlayer} sessionState={this.state.sessionState} sessionInfo={this.state.sessionInfo} sessionId={this.state.sessionId} sessionData={this.state.sessionData} />) : ""}
                <br />
                {this.state.clientData !== null ? (<Patch patchData={this.state.sessionData.seed.worlds[this.state.clientData.worldId].patch} fileName={this.state.sessionData.seed.gameName + " - " + this.state.sessionData.seed.seedNumber + " - " + this.state.clientData.name + ".sfc"} />) : ""}
                <br />
                {this.state.clientData !== null ? (<Connection connState={this.state.connState} clientData={this.state.clientData} connInfo={this.state.connInfo} onConnectClick={this.handleConnect} />) : ""}
                <br />
                {this.state.connState === 1 ? (<Runner connState={this.state.connState} sessionData={this.state.sessionData} clientData={this.state.clientData} hubState={this.state.hubState} hubConnection={this.state.hubConnection} connInfo={this.state.connInfo} />) : ""}
                <br />
                {this.state.sessionData !== null ? (<Spoiler sessionData={this.state.sessionData} />) : ""}
            </div>
        );
    }
}
