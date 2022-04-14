import React, { useState, useEffect, useContext } from 'react';
import { useParams } from 'react-router-dom';
import { Container, Row, Col, Card, CardHeader, CardBody } from 'reactstrap';

import MessageCard from '../ui/MessageCard';

import Patch from '../patch';
import Spoiler from '../spoiler';

import { GameTraitsCtx } from '../game/traits';
import { adjustHostname } from '../site/domain';

import { decode } from 'slugid';

import { tryParseJson } from '../util';
import defaults from 'lodash/defaults';
import attempt from 'lodash/attempt';

export default function Permalink() {
    const { seedSlug } = useParams();
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
    if (settings) {
        defaults(settings, {
            opentower: 'sevencrystals',
            ganonvulnerable: 'sevencrystals',
            opentourian: 'fourbosses',
        });
    }

    const content = seed && !gameMismatch ? (
        <>
            <Card className="mb-3">
                <CardHeader className="bg-primary text-white">
                    {seed.gameName}
                </CardHeader>
                <CardBody>
                    <Row>
                        <Col md="3">Seed:</Col><Col> {seedSlug}</Col>
                    </Row>
                    {seed.seedNumber && (
                        <Row>
                            <Col md="3">Seed number:</Col><Col> {seed.seedNumber}</Col>
                        </Row>
                    )}
                    {settings && game.id === 'smz3' && (<>
                        <Row>
                            <Col md="3">Super Metroid Logic:</Col><Col> {{
                                normal: 'Normal',
                                hard: 'Hard'
                            }[settings.smlogic]
                            }</Col>
                        </Row>
                        <Row>
                            <Col md="3">Goal:</Col><Col> {{
                                defeatboth: 'Defeat Ganon and Mother Brain',
                                fastganondefeatmotherbrain: 'Fast Ganon and Defeat Mother Brain',
                                alldungeonsdefeatmotherbrain: 'All dungeons and Defeat Mother Brain'
                            }[settings.goal]
                            }</Col>
                        </Row>
                        <Row>
                            <Col md="3">Open Ganon's Tower:</Col><Col> {{
                                random: 'Randomized',
                                nocrystals: 'No Crystals',
                                onecrystal: 'One Crystal',
                                twocrystals: 'Two Crystals',
                                threecrystals: 'Three Crystals',
                                fourcrystals: 'Four Crystals',
                                fivecrystals: 'Five Crystals',
                                sixcrystals: 'Six Crystals',
                                sevencrystals: 'Seven Crystals',
                            }[settings.opentower]
                            }</Col>
                        </Row>
                        <Row>
                            <Col md="3">Ganon Vulnerable:</Col><Col> {{
                                random: 'Randomized',
                                nocrystals: 'No Crystals',
                                onecrystal: 'One Crystal',
                                twocrystals: 'Two Crystals',
                                threecrystals: 'Three Crystals',
                                fourcrystals: 'Four Crystals',
                                fivecrystals: 'Five Crystals',
                                sixcrystals: 'Six Crystals',
                                sevencrystals: 'Seven Crystals',
                            }[settings.ganonvulnerable]
                            }</Col>
                        </Row>
                        <Row>
                            <Col md="3">Open Tourian:</Col><Col> {{
                                random: 'Randomized',
                                nobosses: 'No Bosses',
                                oneboss: 'One Boss',
                                twobosses: 'Two Bosses',
                                threebosses: 'Three Bosses',
                                fourbosses: 'Four Bosses',
                            }[settings.opentourian]
                            }</Col>
                        </Row>
                        <Row>
                            <Col md="3">First Sword:</Col><Col> {{
                                randomized: 'Randomized',
                                early: 'Early',
                                uncle: 'Uncle assured'
                            }[settings.swordlocation]
                            }</Col>
                        </Row>
                        <Row>
                            <Col md="3">Morph Ball:</Col><Col> {{
                                randomized: 'Randomized',
                                early: 'Early',
                                original: 'Original location'
                            }[settings.morphlocation]
                            }</Col>
                        </Row>
                        <Row>
                            <Col md="3">Key Shuffle:</Col><Col> {{
                                none: 'None',
                                keysanity: 'Keysanity',
                            }[settings.keyshuffle]
                            }</Col>
                        </Row>
                    </>)}
                    {settings && game.id === 'sm' && (<>
                        <Row>
                            <Col md="3">Logic:</Col><Col> {{
                                casual: 'Casual',
                                tournament: 'Tournament'
                            }[settings.logic]
                            }</Col>
                        </Row>
                        <Row>
                            <Col md="3">Item Placement:</Col><Col> {{
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
            {settings && settings.race === 'false' && (
                <Spoiler seedGuid={seed.guid} />
            )}
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
