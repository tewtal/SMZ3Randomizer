import React, { Component } from 'react';
import { Container, Row, Col, Card, CardHeader, CardBody, Button, Form, Input, InputGroup, InputGroupAddon, InputGroupText, Modal, ModalHeader, ModalBody, Progress } from 'reactstrap';

export default class Randomizer extends Component {
    static displayName = Randomizer.name;
    constructor(props) {
        super(props);
        this.state = { showGeneratingDialog: false, seed: [], players: 2, playerNames: ["", ""], logic: "casual", seedNumber: "", password: "" };
        this.updatePlayers = this.updatePlayers.bind(this);
        this.updatePlayerName = this.updatePlayerName.bind(this);
        this.updateLogic = this.updateLogic.bind(this);
        this.updateSeedNumber = this.updateSeedNumber.bind(this);
        this.updatePassword = this.updatePassword.bind(this);
        this.createGame = this.createGame.bind(this);
    }

    async createGame(e) {
        e.preventDefault();

        this.setState({ showGeneratingDialog: true });

        try {
            let opts = { "mode": "multiworld", "logic": this.state.logic, "worlds": this.state.players, "seed": this.state.seedNumber, "password": this.state.password };

            for (let p = 0; p < this.state.players; p++) {
                opts["player-" + p] = this.state.playerNames[p];
            }

            let response = await fetch("/api/randomizer/generate",
                {
                    method: "POST",
                    cache: "no-cache",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({ options: opts })
                });
            let data = await response.json();
            this.setState({ showGeneratingDialog: false });
            this.props.history.push('/multiworld/' + data.guid);
        } catch (error) {
            this.setState({ showGeneratingDialog: false });
            console.log("Error:", error);
        }
    }

    updatePlayers(e) {
        let players = e.target.value;
        let playerNames = this.state.playerNames;

        for (let p = 0; p < this.state.players; p++) {
            if (playerNames[p] === undefined) {
                playerNames[p] = ""
            }
        }

        this.setState({ playerNames: playerNames, players: players });
    }

    updatePlayerName(e) {
        let playerId = e.target.id.split('-')[1];
        let playerNames = this.state.playerNames;

        playerNames[playerId] = e.target.value;

        this.setState({
            playerNames: playerNames
        });
    }

    updateLogic(e) {
        this.setState({ logic: e.target.value });
    }

    updateSeedNumber(e) {
        this.setState({ seedNumber: e.target.value });
    }

    updatePassword(e) {
        this.setState({ password: e.target.value });
    }

    render() {

        let playerInputs = [];

        for (let p = 0; p < this.state.players; p++) {
            playerInputs.push(
                <Col key={"playerInput" + p} md={{ span: 5, offset: (1 - (p % 2)) }}>
                    <InputGroup>
                        <InputGroupAddon addonType="prepend">
                            <InputGroupText>Name {p + 1}</InputGroupText>
                        </InputGroupAddon>
                        <Input id={"player-" + p} defaultValue={this.state.playerNames[p]} onChange={this.updatePlayerName} />
                    </InputGroup>
                </Col>);
        }

        return (
            <Container>
                <Row className="justify-content-md-center">
                    <Col md="8">
                        <Card>
                            <CardHeader className="bg-primary text-white">
                                New Randomized Game - SMZ3 Multiworld 1.0-beta2
                            </CardHeader>
                            <CardBody>
                                <Form onSubmit={this.createGame}>
                                    <Row className="form-group">
                                        <Col md="3">
                                            <InputGroup>
                                                <InputGroupAddon addonType="prepend">
                                                    <InputGroupText>Players</InputGroupText>
                                                </InputGroupAddon>
                                                <Input id="players" defaultValue={this.state.players} onChange={this.updatePlayers} />
                                            </InputGroup>
                                        </Col>
                                        <Col md="3">
                                            <InputGroup>
                                                <InputGroupAddon addonType="prepend">
                                                    <InputGroupText>Seed</InputGroupText>
                                                </InputGroupAddon>
                                                <Input id="seed" defaultValue={this.state.seedNumber} onChange={this.updateSeedNumber} />
                                            </InputGroup>
                                        </Col>
                                        <Col md="1"></Col>
                                        <Col md="5">
                                            <InputGroup>
                                                <InputGroupAddon addonType="prepend">
                                                    <InputGroupText>Logic</InputGroupText>
                                                </InputGroupAddon>
                                                <Input type="select" id="logic" defaultValue={this.state.logic} onChange={this.updateLogic}>
                                                    <option value="casual">Casual</option>
                                                    <option value="tournament">Tournament</option>
                                                </Input>
                                            </InputGroup>
                                        </Col>
                                    </Row>
                                    <Row className="form-group">
                                        {playerInputs}
                                    </Row>
                                    <Row className="form-group">
                                        <Col md="6">
                                            <Button color="success" type="submit">Create Multiworld Game</Button>
                                        </Col>
                                    </Row>
                                </Form>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>

                <Modal isOpen={this.state.showGeneratingDialog} backdrop="static" autoFocus>
                    <ModalHeader>Generating new game</ModalHeader>
                    <ModalBody>
                        <p>Please wait while a new game is generated</p>
                        <Progress animated color="info" value={100} />
                    </ModalBody>
                </Modal>

            </Container>
        );
    }
}
