import React, { useState } from 'react';
import { Container, Row, Col, Card, CardHeader, CardBody } from 'reactstrap';
import { Form, Button, Input, InputGroup, InputGroupAddon, InputGroupText } from 'reactstrap';
import { Modal, ModalHeader, ModalBody, Progress } from 'reactstrap';

import times from 'lodash/times';
import fromPairs from 'lodash/fromPairs';

export default function Randomizer(props) {
    const [seed, setSeed] = useState('');
    const [logic, setLogic] = useState('casual');
    const [worlds, setWorlds] = useState(2);
    const [players, setPlayers] = useState(times(2, () => ''));
    const [generating, setGenerating] = useState(false);

    async function createGame() {
        setGenerating(true);

        try {
            const options = {
                mode: 'multiworld',
                password: '',
                seed,
                logic,
                worlds,
                ...fromPairs(times(worlds, i => [`player-${i}`, players[i]])),
            };

            const response = await fetch('/api/randomizer/generate', {
                method: 'POST',
                cache: 'no-cache',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ options })
            });
            const data = await response.json();
            setGenerating(false);
            props.history.push(`/multiworld/${data.guid}`);
        } catch (error) {
            setGenerating(false);
            console.log("Error:", error);
        }
    }

    function onPlayerCountChange(e) {
        const worlds = e.target.value;
        /* default any additional player name */
        times(worlds, i => players[i] = players[i] || '')
        setWorlds(worlds);
        setPlayers(players);
    }

    function onPlayerChange(e, index) {
        setPlayers(players.map((name, i) => i !== index ? name : e.target.value));
    }

    const form = (
        <Form onSubmit={(e) => { e.preventDefault(); createGame(); }}>
            <Row className="form-group">
                <Col md="3">
                    <InputGroup>
                        <InputGroupAddon addonType="prepend">
                            <InputGroupText>Players</InputGroupText>
                        </InputGroupAddon>
                        <Input value={worlds} onChange={onPlayerCountChange} />
                    </InputGroup>
                </Col>
                <Col md="3">
                    <InputGroup>
                        <InputGroupAddon addonType="prepend">
                            <InputGroupText>Seed</InputGroupText>
                        </InputGroupAddon>
                        <Input value={seed} onChange={(e) => setSeed(e.target.value)} />
                    </InputGroup>
                </Col>
                <Col md="1" />
                <Col md="5">
                    <InputGroup>
                        <InputGroupAddon addonType="prepend">
                            <InputGroupText>Logic</InputGroupText>
                        </InputGroupAddon>
                        <Input type="select" value={logic} onChange={(e) => setLogic(e.target.value)}>
                            <option value="casual">Casual</option>
                            <option value="tournament">Tournament</option>
                        </Input>
                    </InputGroup>
                </Col>
            </Row>
            <Row className="form-group">
                {players.slice(0, worlds).map((name, i) =>
                    <Col key={`player${i}`} md={{ span: 5, offset: (1 - (i % 2)) }}>
                        <InputGroup>
                            <InputGroupAddon addonType="prepend">
                                <InputGroupText>Name {i + 1}</InputGroupText>
                            </InputGroupAddon>
                            <Input value={name} onChange={(e) => onPlayerChange(e, i)} />
                        </InputGroup>
                    </Col>
                )}
            </Row>
            <Row className="form-group">
                <Col md="6">
                    <Button type="submit" color="success">Create Multiworld Game</Button>
                </Col>
            </Row>
        </Form>
    );

    const modal = (
        <Modal isOpen={generating} backdrop="static" autoFocus>
            <ModalHeader>Generating new game</ModalHeader>
            <ModalBody>
                <p>Please wait while a new game is generated</p>
                <Progress animated color="info" value={100} />
            </ModalBody>
        </Modal>
    );

    return (
        <Container>
            <Row className="justify-content-md-center">
                <Col md="8">
                    <Card>
                        <CardHeader className="bg-primary text-white">
                            New Randomized Game - SMZ3 Multiworld 1.0-beta2
                        </CardHeader>
                        <CardBody>
                            {form}
                        </CardBody>
                    </Card>
                </Col>
            </Row>
            {modal}
        </Container>
    );
}
