import React, { useState, useEffect, useContext } from 'react';
import { Container, Row, Col, Card, CardHeader, CardBody } from 'reactstrap';
import MessageCard from './util/MessageCard';
import Patch from './Patch';
import Spoiler from './Spoiler';

import { GameTraitsCtx } from '../game/traits';
import { adjustHostname } from '../site';

import { decode } from 'slugid';

import attempt from 'lodash/attempt';

export default function Permalink(props) {
    const seedSlug = props.match.params.seed_id;
    const seedGuid = decode(seedSlug).replace(/-/g, '');

    const [seed, setSeed] = useState(null);
    const [errorMessage, setErrorMessage] = useState(null);

    const game = useContext(GameTraitsCtx);

    useEffect(() => {
        attempt(async () => {
            try {
                var response = await fetch(`/api/seed/${seedGuid}`);
                if (response && response.ok) {
                    var result = await response.json();
                    setSeed(result);
                } else {
                    setErrorMessage('Cannot load metadata for the specified seed.');
                }
            } catch (error) {
                setErrorMessage(`${error}`);
            }
        });
    }, []); /* eslint-disable-line react-hooks/exhaustive-deps */

    const gameMismatch = seed && seed.gameId !== game.id;
    const world = seed && seed.worlds[0];
    const settings = world && tryParseJson(world.settings);
    const content = seed && !gameMismatch ? (
        <>
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
                    {settings && game.id === 'smz3' && (<>
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
                        <Row>
                            <Col>Key Shuffle: {{
                                none: 'None',
                                keysanity: 'Keysanity',
                            }[settings.keyshuffle]
                            }</Col>
                        </Row>
                    </>)}
                    {settings && game.id === 'sm' && (<>
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
        </>) :
        errorMessage ? <MessageCard error={true} title="Something went wrong :(" msg={errorMessage} /> :
        gameMismatch ? (
            <MessageCard error={true} title="This is not quite right :O"
                msg={<a href={adjustHostname(document.location.href, seed.gameId)}>Go to the correct domain here</a>}
            />
        ) :
        <MessageCard title="Game information" msg="Please wait, loading..." />;

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

function tryParseJson(text) {
    try {
        return JSON.parse(text);
    } catch (syntaxerror) {
        return null;
    }
}
