import React, { useState, useEffect } from 'react';
import { encode } from 'slugid';
import { Container, Row, Col, Card, CardHeader, CardBody, Button, Form, Input, InputGroup, InputGroupAddon, InputGroupText, Modal, ModalHeader, ModalBody, Progress } from 'reactstrap';

export function Configure(props) {
    const [options, setOptions] = useState(null);
    const [modal, setModal] = useState(false);
    const [randomizer, setRandomizer] = useState(null);
    const [errorMessage, setErrorMessage] = useState(null);
    const randomizer_id = props.match.params.randomizer_id;

    useEffect(() => {
        const fetchData = async () => {
            try {
                var response = await fetch(`/api/randomizers/${randomizer_id}`);
                if (response) {
                    var result = await response.json();
                    setOptions(result.options.reduce((obj, opt) => { return { ...obj, [opt.key]: opt.default !== null ? opt.default : "" }; }, {}));
                    setRandomizer(result);
                } else {
                    setErrorMessage("Cannot load metadata for the specified randomizer.");
                }
            } catch (error) {
                setErrorMessage(error);
            }
        };

        fetchData();
    }, []);

    async function createGame(e) {
        e.preventDefault();
        setModal(true);

        // temp fix        
        options["mode"] = "multiworld";
        options["worlds"] = "1";
        options["player-0"] = "Player 1";

        try {
            let response = await fetch(`/api/randomizers/${randomizer_id}/generate`,
                {
                    method: "POST",
                    cache: "no-cache",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({ options: options })
                });
            let data = await response.json();
            setModal(false);
            props.history.push('/seed/' + encode(data.guid));
        } catch (error) {
            console.log(error);
            setModal(false);
        }
    }

    function updateOption(e, key) {
        setOptions({...options, [key]: e.target.value});
    }

    function chunk(array, size) {
        return array.reduce((chunks, item, i) => {
            if (i % size === 0) {
                chunks.push([item]);
            } else {
                chunks[chunks.length - 1].push(item);
            }
            return chunks;
        }, []);
    }

    if (randomizer) {
        const formOptions = randomizer.options.map(opt => {
            let inputElement = null;
            switch (opt.type) {
                case 'input':
                    inputElement = (
                        <Col key={opt.key} md="6">
                            <InputGroup>
                                <InputGroupAddon addonType="prepend">
                                    <InputGroupText>{opt.description}</InputGroupText>
                                </InputGroupAddon>
                                <Input id={opt.key} defaultValue={options[opt.key]} onChange={(e) => updateOption(e, opt.key)} />
                            </InputGroup>
                        </Col>
                    );
                    break;
                case 'dropdown':
                    inputElement = (
                        <Col key={opt.key} md="6">
                            <InputGroup>
                                <InputGroupAddon addonType="prepend">
                                    <InputGroupText>{opt.description}</InputGroupText>
                                </InputGroupAddon>
                                <Input type="select" id={opt.key} defaultValue={options[opt.key]} onChange={(e) => updateOption(e, opt.key)}>
                                    {Object.entries(opt.values).map(([k,v]) => <option key={k} value={k}>{v}</option>)}
                                </Input>
                            </InputGroup>
                        </Col>
                    );
                    break;
            }

            return inputElement;
        });

        const formOptionGroups = chunk(formOptions, 2);

        return (
            <Container>
                <Row className="justify-content-md-center">
                    <Col md="10">
                        <Card>
                            <CardHeader className="bg-primary text-white">
                                {randomizer.name} - {randomizer.version}
                            </CardHeader>
                            <CardBody>
                                <Form onSubmit={createGame}>
                                    {formOptionGroups.map((optionGroup, i) => (
                                        <Row key={i} className="form-group">
                                            {optionGroup}
                                        </Row>
                                    ))}
                                    <Row className="form-group">
                                        <Col md="6">
                                            <Button color="success" type="submit">Generate Game</Button>
                                        </Col>
                                    </Row>
                                </Form>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>

                <Modal isOpen={modal} backdrop="static" autoFocus>
                    <ModalHeader>Generating new game</ModalHeader>
                    <ModalBody>
                        <p>Please wait while a new game is generated</p>
                        <Progress animated color="info" value={100} />
                    </ModalBody>
                </Modal>

            </Container>
        );
    } else {
        return (
            <Container>
                <Row className="justify-content-md-center">
                    <Col md="10">
                        <Card>
                            <CardHeader className={errorMessage ? "bg-danger text-white" : "bg-primary text-white"}>
                                {errorMessage ? <div>Something went wrong :(</div> : <div>Create new randomized game</div>}
                            </CardHeader>
                            <CardBody>
                                {errorMessage ? <p>{errorMessage}</p> : <p>Please wait, loading...</p>}
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </Container>
        );
    }
}
