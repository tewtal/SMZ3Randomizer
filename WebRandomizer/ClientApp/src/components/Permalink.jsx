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
    }, []); /* eslint-disable-line react-hooks/exhaustive-deps */

    let content;
    if (seed) {
        const world = seed.worlds[0];
        const settings = JSON.parse(world.settings);
        content = (<>
            <Card className="mb-3">
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
                    {settings && seed.gameId === 'smz3' && (<>
                        <Row>
                            <Col>Super Metroid Logic: {{
                                normal: 'Normal',
                                hard: 'Hard'
                            }[settings.smlogic]
                            }</Col>
                        </Row>
                        <Row>
                            <Col>First Sword: {{
                                randomized: 'Randomized',
                                early: 'Early',
                                uncle: 'Uncle assured'
                            }[settings.swordlocation]
                            }</Col>
                        </Row>
                        <Row>
                            <Col>Morph Ball: {{
                                randomized: 'Randomized',
                                early: 'Early',
                                original: 'Original location'
                            }[settings.morphlocation]
                            }</Col>
                        </Row>
                    </>)}
                    {settings && seed.gameId === 'sm' && (<>
                        <Row>
                            <Col>Logic: {{
                                casual: 'Casual',
                                tournament: 'Tournament'
                            }[settings.logic]
                            }</Col>
                        </Row>
                        <Row>
                            <Col>Item Placement: {{
                                full: 'Full randomization',
                                split: 'Major/Minor split'
                            }[settings.placement]
                            }</Col>
                        </Row>
                    </>)}
                    {settings && settings.race === 'true' && (
                        <Row>
                            <Col>Race Rom (no spoilers)</Col>
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
