import React, { Component } from 'react';
import { Row, Col, Card, CardHeader, CardBody, Button } from 'reactstrap'

export class Seed extends Component {
    static displayName = Seed.name;

    constructor(props) {
        super(props);
        this.state = {}
        this.handleRegisterPlayer = this.handleRegisterPlayer.bind(this);
    }

    handleRegisterPlayer(e) {
        this.props.onRegisterPlayer(e.target.dataset["player"]);
    }

    render() {
        const playerInfo = [];

        if (this.props.sessionState > 0) {
            for (let p = 0; p < this.props.sessionData.seed.players; p++) {
            /* find a potential client that matches this */
                let client = null;
                for (let c = 0; c < this.props.sessionData.clients.length; c++) {
                    if (this.props.sessionData.clients[c].guid === this.props.sessionData.seed.worlds[p].guid) {
                        client = this.props.sessionData.clients[c];
                        break;
                    }
                }
                if (client == null) {
                    playerInfo.push(
                        <Col key={"player-" + p} md="3">
                            <h5>{this.props.sessionData.seed.worlds[p].player}</h5>
                            <Button key={"player-" + p} data-player={this.props.sessionData.seed.worlds[p].guid} color="primary" onClick={this.handleRegisterPlayer}>Register as this player</Button>
                        </Col>
                    );
                } else {

                    let playerState = "";
                    let playerColor = "secondary";

                    if (client.state < 5) {
                        playerState = "Registered";
                        playerColor = "secondary";
                    }
                    else if (client.state === 5) {
                        playerState = "Connected";
                        playerColor = "warning";
                    }
                    else {
                        playerState = "Ready";
                        playerColor = "success";
                    }

                    playerInfo.push(
                        <Col key={"player-" + p} md="3">
                            <h5>{this.props.sessionData.seed.worlds[p].player}</h5>
                            <Button key={"player-" + p} color={playerColor} disabled>{playerState}</Button>
                        </Col>
                    );
                }
            }
        }

        let headerColor = this.props.sessionState === 0 ? "bg-danger text-white" : "bg-success text-white";

        return (
            <Card>
                <CardHeader className={headerColor}>
                    Session: {this.props.sessionId}<br />
                    Game: {this.props.sessionState > 0 ? this.props.sessionData.seed.gameName : "Loading..."}<br />
                    Status: {this.props.sessionInfo}
                </CardHeader>
                <CardBody>
                    <Row>
                        {playerInfo}
                    </Row>
                </CardBody>
            </Card>
        );
    }
}
