import React, { useState } from 'react';
import { Row, Col, Card, CardHeader, CardBody, Button, Nav, NavItem, NavLink, InputGroup, InputGroupAddon, InputGroupText, Input } from 'reactstrap';
import styled from 'styled-components';
import isEmpty from 'lodash/isEmpty';
import { sortBy, uniq } from 'lodash';

const SmallNavLink = styled(NavLink)`
        font-size: .87em;
        font-weight: bold;
        padding-top: 6px;
        padding-bottom: 6px;
        padding-right: 9px;
        padding-left: 9px;
    `;

const SearchInputGroup = styled(InputGroup)`
        margin-top: 10px;
        margin-bottom: 15px;
    `;

const LocationTable = styled.table.attrs(props => ({
    className: "table table-sm table-borderless"
}))`
    margin-bottom: 25px;
    > tbody > tr { border-bottom: 1px solid #e0e0e0; };
`;

export default function Spoiler(props) {
    const [show, setShow] = useState(false);
    const [spoiler, setSpoiler] = useState(null);
    const [spoilerArea, setSpoilerArea] = useState("playthrough");
    const [searchText, setSearchText] = useState("");

    const toggleSpoiler = async () => {
        if (!show && !spoiler) {
            try {
                let response = await fetch(`/api/spoiler/${props.seedData.guid}`);
                let result = await response.json();
                setSpoiler(result);
            } catch {}
        }

        setShow(s => !s);
    }

    const updateSearchText = (e) => {
        setSearchText(e.target.value);
        if (e.target.value !== "" && spoilerArea === "playthrough") {
            setSpoilerArea("all");
        }
    }    

    if (props.seedData === null || props.seedData.spoiler === "[]")
        return null;

    let locations = spoiler ? spoiler.locations : [];
    if (spoiler && searchText && searchText !== "") {
        const re = new RegExp(searchText, "i");
        locations = spoiler.locations.filter(l => l.locationName.match(re) || l.itemName.match(re));

        if (spoilerArea !== "all" && spoilerArea !== "playthrough" && !locations.find(l => l.locationArea === spoilerArea)) {
            setSpoilerArea("all");
        }
    }

    return (
        <Card>
            <CardHeader >
                <Row className="align-items-center justify-content-between">
                    <Col>Spoiler log</Col>
                    <Col><Button outline className="float-right" color="secondary" onClick={toggleSpoiler}>{show ? "Hide" : "Show"}</Button></Col>
                </Row>
            </CardHeader>
            {show && (<CardBody>
                {spoiler
                    ? <div>
                        <SearchInputGroup>
                            <InputGroupAddon addonType="prepend">
                                <InputGroupText><span role="img" aria-label="search">🔎</span></InputGroupText>
                            </InputGroupAddon>
                            <Input key="searchInput" placeholder="Find a location or item" onChange={updateSearchText} value={searchText} />
                        </SearchInputGroup>
                        <div>
                            <Nav pills style={{ marginBottom: "10px" }}>
                                <NavItem>
                                    <SmallNavLink href="#" active={spoilerArea === "playthrough"} onClick={() => setSpoilerArea("playthrough")}>Playthrough</SmallNavLink>
                                </NavItem>
                                <NavItem>
                                    <SmallNavLink href="#" active={spoilerArea === "all"} onClick={() => setSpoilerArea("all")}>All</SmallNavLink>
                                </NavItem>
                                {uniq(sortBy(locations, l => l.locationId).map(l => l.locationArea)).map((area, i) => (
                                    <NavItem key={i}>
                                        <SmallNavLink href="#" active={spoilerArea === area} onClick={() => setSpoilerArea(area)}>{area}</SmallNavLink>
                                    </NavItem>
                                ))}
                            </Nav>
                            {spoilerArea === 'playthrough'
                                ? <Card>
                                    <CardBody>
                                        {JSON.parse(props.seedData.spoiler).filter(sphere => !isEmpty(sphere)).map((sphere, i) => (
                                            <div key={i}>
                                                <h6>Sphere {i + 1}</h6>
                                                <LocationTable>
                                                    <tbody>
                                                        {Object.entries(sphere).map(([location, item], j) => (
                                                            <tr key={j}>
                                                                <td style={{ width: '60%' }}>{location}</td>
                                                                <td>{item}</td>
                                                            </tr>
                                                        ))}
                                                    </tbody>
                                                </LocationTable>
                                            </div>
                                        ))}
                                    </CardBody>
                                  </Card>                                        
                                : <Card>
                                    <CardBody>
                                        {uniq(sortBy(locations.filter(l => spoilerArea === 'all' || l.locationArea === spoilerArea), l => l.locationRegion).map(l => l.locationRegion)).map((r, i) => (
                                            <div key={i}>
                                                <h6>{r}</h6>
                                                <LocationTable>
                                                    <tbody>
                                                    {locations.filter(l => (spoilerArea === 'all' || l.locationArea === spoilerArea) && l.locationRegion === r).map((l, j) => (
                                                        <tr key={j}>
                                                            <td style={{ width: '60%' }}>{l.locationName}{spoiler.seed.players > 1 ? ` - ${spoiler.seed.worlds[l.worldId].player}` : ''}</td>
                                                            <td>{l.itemName}{spoiler.seed.players > 1 ? ` - ${spoiler.seed.worlds[l.itemWorldId].player}` : ''}</td>
                                                        </tr>
                                                    ))}
                                                    </tbody>
                                                </LocationTable>
                                            </div>
                                        ))}
                                    </CardBody>
                                </Card>
                                }
                            </div>
                      </div>
                    : "Loading..."}
            </CardBody>)}
        </Card>
    );
}
