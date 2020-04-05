import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Card, CardHeader, CardBody } from 'reactstrap';
import Patch from './Patch';
import Spoiler from './Spoiler';

import classNames from 'classnames';

import { decode } from 'slugid';

import attempt from 'lodash/attempt';

export default function Permalink(props) {
    const seedSlug = props.match.params.seed_id;
    const seedGuid = decode(seedSlug).replace(/-/g, "");
    const [seed, setSeed] = useState(null);
    const [errorMessage, setErrorMessage] = useState(null);

    useEffect(() => {
        attempt(async () => {
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
        })
    }, []);

    let content;
    if (seed) {
        const world = seed.worlds[0];
        content = (<>
            <Card>
                <CardHeader className="bg-primary text-white">
                    {seed.gameName}
                </CardHeader>
                <CardBody>
                    <Row>
                        <Col>Seed: {seedSlug}</Col>
                    </Row>
                    {seed.seedNumber && (
                        <Row>
                            <Col>Seed number: {seed.seedNumber}</Col>
                        </Row>
                    )}
                    <Row className="mt-3">
                        <Col>
                            <Patch seed={seed} world={world} />
                        </Col>
                    </Row>
                </CardBody>
            </Card>
            <Spoiler seedData={seed} />
        </>);
    }
    else {
        content = (
            <Card>
                <CardHeader className={classNames({
                        'bg-danger': errorMessage,
                        'bg-primary': !errorMessage
                    }, 'text-white'
                )}>
                    {errorMessage ? <div>Something went wrong :(</div> : <div>Game information</div>}
                </CardHeader>
                <CardBody>
                    {errorMessage ? <p>{errorMessage}</p> : <p>Please wait, loading...</p>}
                </CardBody>
            </Card>
        );
    }

    return (
        <Container>
            <Row className="justify-content-md-center">
                <Col md="10">
                    {content}
                </Col>
            </Row>
        </Container>
    );
}
