import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Card, CardHeader, CardBody } from 'reactstrap';
import { Button, Form, Input } from 'reactstrap';
import InputGroup from './util/PrefixInputGroup';
import { Modal, ModalHeader, ModalBody, Progress } from 'reactstrap';
import BootstrapSwitchButton from 'bootstrap-switch-button-react';
import styled from 'styled-components';

import { encode } from 'slugid';

const InputWithoutSpinner = styled(Input)`
  /* For firefox */
  appearance: textfield;
  /* For Chromium */
  &::-webkit-inner-spin-button,
  &::-webkit-outer-spin-button { 
    appearance: none;
  }
`;

export default function Configure(props) {
    const randomizer_id = props.match.params.randomizer_id;

    const [options, setOptions] = useState(null);
    const [names, setNames] = useState({});
    const [modal, setModal] = useState(false);
    const [randomizer, setRandomizer] = useState(null);
    const [errorMessage, setErrorMessage] = useState(null);

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
    }, []); /* eslint-disable-line react-hooks/exhaustive-deps */

    async function createGame(e) {
        e.preventDefault();
        setModal(true);

        try {
            if (options["gamemode"] === "multiworld") {
                for (let p = 0; p < parseInt(options["players"]); p++) {
                    options["player-" + p] = names[p];
                }
            }

            let response = await fetch(`/api/randomizers/${randomizer_id}/generate`,
                {
                    method: "POST",
                    cache: "no-cache",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(options)
                });
            let data = await response.json();
            setModal(false);
            if (options["gamemode"] === "multiworld") {
                props.history.push('/multiworld/' + encode(data.guid));
            } else {
                props.history.push('/seed/' + encode(data.guid));
            }
        } catch (error) {
            console.log(error);
            setModal(false);
        }
    }

    function updateOption(key, value) {
        setOptions({...options, [key]: value});
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
            return opt.type === 'seed' ? (
                <Col key={opt.key} md="6">
                    <InputGroup prefix={opt.description}>
                        <InputWithoutSpinner type="number" id={opt.key} min={0} max={0x7FFF_FFFF} value={options[opt.key]}
                            onChange={(e) => updateOption(opt.key, e.target.value)}
                        />
                    </InputGroup>
                </Col>
            )
            : opt.type === 'dropdown' ? (
                <Col key={opt.key} md="6">
                    <InputGroup prefix={opt.description}>
                        <Input type="select" id={opt.key} value={options[opt.key]} onChange={(e) => updateOption(opt.key, e.target.value)}>
                    {Object.entries(opt.values).map(([k, v]) => <option key={k} value={k}>{v}</option>)}
                        </Input>
                    </InputGroup>
                </Col>
            )
            : opt.type === 'checkbox' ? (
                <Col key={opt.key} md="6">
                    <InputGroup prefixClassName="mr-1" prefix={opt.description}>
                        <BootstrapSwitchButton id={opt.key} onlabel="Yes" offlabel="No" width="80" checked={options[opt.key]}
                            onChange={checked => updateOption(opt.key, checked.toString())}
                        />
                    </InputGroup>
                </Col>
            )
            : opt.type === 'players' && options["gamemode"] === "multiworld" ? (
                <Col key={opt.key} md="6">
                    <InputGroup prefix={opt.description}>
                        <Input id={opt.key} value={options[opt.key]} onChange={(e) => updateOption(opt.key, e.target.value)} />
                    </InputGroup>
                </Col>
            )
            : null;
        });

        const formOptionGroups = chunk(formOptions, 2);

        /* Chromium did not respect "off", so we're forced to use "new-passwod" */
        const playerInputs = [];
        for (let p = 0; p < parseInt(options["players"]); p++) {
            playerInputs.push(
                <Col key={"playerInput" + p} md={{ size: 5, offset: 1 - (p % 2) }}>
                    <InputGroup prefix={`Name ${p + 1}`}>
                        <Input autoComplete="new-password" value={names[p] || ''} required pattern=".*[A-Za-z\d].*"
                            onChange={(e) => setNames({ ...names, [p]: e.target.value })}
                        />
                    </InputGroup>
                </Col>
            );
        }

        return (
            <Container>
                <Row className="justify-content-md-center">
                    <Col md="10">
                        <Card>
                            <CardHeader className="bg-primary text-white">
                                {randomizer.name} - {randomizer.version}
                            </CardHeader>
                            <CardBody>
                                <Form autoComplete="off" onSubmit={createGame}>
                                    {formOptionGroups.map((optionGroup, i) => (
                                        <Row key={i} className="form-group">
                                            {optionGroup}
                                        </Row>
                                    ))}
                                    {options["gamemode"] === "multiworld" && (
                                        <Row className="form-group">
                                            {playerInputs}
                                        </Row>
                                    )}
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
