import React, { useState } from 'react';
import { Row, Col, Card, CardHeader, CardBody, Button, Nav, NavItem, NavLink } from 'reactstrap';
import styled from 'styled-components';
import isEmpty from 'lodash/isEmpty';
import { map, sortBy, uniq } from 'lodash';

export default function Spoiler(props) {
    const [show, setShow] = useState(false);
    const [spoiler, setSpoiler] = useState(null);
    const [spoilerArea, setSpoilerArea] = useState("playthrough");

    const SmallNavLink = styled(NavLink)`
        font-size: .87em;
        font-weight: bold;
        padding-top: 6px;
        padding-bottom: 6px;
        padding-right: 9px;
        padding-left: 9px;
    `;

    const toggleSpoiler = async () => {
        if (!show && !spoiler) {
            try {
                let response = await fetch(`/api/spoiler/${props.seedData.guid}`);
                let result = await response.json();
                setSpoiler(result);
            } catch (e) {
                /* Do error things */
                console.log(e);
            }            
        }

        setShow(s => !s);
    }

    if (props.seedData === null || props.seedData.spoiler === "[]")
        return null;

    return (
        <Card>
            <CardHeader >
                <Row className="align-items-center justify-content-between">
                    <Col>Spoiler log</Col>
                    <Col><Button outline className="float-right" color="secondary" onClick={() => toggleSpoiler()}>{show ? "Hide" : "Show"}</Button></Col>
                </Row>
            </CardHeader>
            {show && (<CardBody>
                {spoiler
                    ? <div>
                        <Nav pills>
                            <NavItem>
                                <SmallNavLink href="#" active={spoilerArea === "playthrough"} onClick={() => setSpoilerArea("playthrough")}>Playthrough</SmallNavLink>
                            </NavItem>
                            <NavItem>
                                <SmallNavLink href="#" active={spoilerArea === "all"} onClick={() => setSpoilerArea("all")}>All</SmallNavLink>
                            </NavItem>
                            {uniq(sortBy(spoiler.locations, l => l.locationId).map(l => l.locationArea)).map(area => (
                                <NavItem>
                                    <SmallNavLink href="#" active={spoilerArea === area} onClick={() => setSpoilerArea(area)}>{area}</SmallNavLink>
                                </NavItem>
                            ))}
                        </Nav>
                            {spoilerArea === 'playthrough'
                                ? JSON.parse(props.seedData.spoiler).filter(sphere => !isEmpty(sphere)).map(sphere => (
                                    <Card>
                                        <CardBody>
                                            <ul>
                                                {map(sphere, (item, location) =>
                                                    <li>{location} - {item}</li>
                                                )}
                                            </ul>
                                        </CardBody>
                                    </Card>
                                ))                               
                            : <Card>
                                <CardBody>
                                    {uniq(sortBy(spoiler.locations.filter(l => spoilerArea === 'all' || l.locationArea === spoilerArea), l => l.locationRegion).map(l => l.locationRegion)).map(r => (
                                        <p>
                                            <h6>{r}</h6>
                                            <ul>
                                                {spoiler.locations.filter(l => (spoilerArea === 'all' || l.locationArea === spoilerArea) && l.locationRegion === r).map(l => (
                                                    <li>{`${l.locationName} - ${l.itemName}`}</li>
                                                ))}
                                            </ul>
                                        </p>
                                    ))}
                                </CardBody>
                            </Card>
                            }
                      </div>
                    : "Loading..."}
            </CardBody>)}
        </Card>
    );
}
