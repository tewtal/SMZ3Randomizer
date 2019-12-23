import React, { useState, useEffect } from 'react';
import { Patch } from './Patch';
import { decode } from 'slugid';
import { Container, Row, Col, Card, CardHeader, CardBody, Button, Form, Input, InputGroup, InputGroupAddon, InputGroupText, Modal, ModalHeader, ModalBody, Progress } from 'reactstrap';

export function Permalink(props) {
    const seedGuid = decode(props.match.params.seed_id).replace(/-/g,"");
    const [seed, setSeed] = useState(null);
    const [errorMessage, setErrorMessage] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                var response = await fetch(`/api/seed/${seedGuid}`);
                if (response && response.ok) {
                    var result = await response.json();
                    setSeed(result);
                } else {
                    setErrorMessage("Cannot load metadata for the specified seed.");
                }
            } catch (error) {
                setErrorMessage(error.toString());
            }
        };

        fetchData();
    }, []);

    if (seed) {
        return (
            <Container>
                <Row className="justify-content-md-center">
                    <Col md="10">
                        <Card>
                            <CardHeader className="bg-primary text-white">
                                {seed.gameName}
                            </CardHeader>
                            <CardBody>
                                <Row>
                                    <Col>Seed guid: {props.match.params.seed_id}</Col>
                                </Row>
                                <Row>
                                    <Col>Seed number: {seed.seedNumber}</Col>
                                </Row>
                                <Row>
                                    <Col>Type: {seed.type}</Col>
                                </Row>
                                <br />
                                <Patch world={seed.worlds[0]} fileName={`${seed.gameName} - ${seed.seedNumber}.sfc`} />
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </Container>
        );
    } else {
        return (
            <Container>
                <Row className="justify-content-md-center">
                    <Col md="10">
                        <Card>
                            <CardHeader className={errorMessage ? "bg-danger text-white" : "bg-primary text-white"}>
                                {errorMessage ? <div>Something went wrong :(</div> : <div>Game information</div>}
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
