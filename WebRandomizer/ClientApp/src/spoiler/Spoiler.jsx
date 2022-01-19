import React, { useState } from 'react';
import styled from 'styled-components';
import { Row, Col, Card, CardHeader, CardBody, Nav, NavItem, NavLink } from 'reactstrap';
import { InputGroup, InputGroupAddon, InputGroupText, Input, Button } from 'reactstrap';

import { JournalArrowDown } from '../ui/BootstrapIcon';

import { saveAs } from 'file-saver';
import { encode } from 'slugid';

import isEmpty from 'lodash/isEmpty';
import sortBy from 'lodash/sortBy';
import uniq from 'lodash/uniq';
import escapeRegExp from 'lodash/escapeRegExp';

const SmallNavLink = styled(NavLink)`
  font-size: .87em;
  font-weight: bold;
  padding-top: 6px;
  padding-bottom: 6px;
  padding-right: 9px;
  padding-left: 9px;
`;

const SearchInputGroup = styled(InputGroup)`
  margin-bottom: 15px;
`;

const LocationTable = styled.table.attrs(props => ({
    className: "table table-sm table-borderless"
}))`
  margin-bottom: 25px;
  > tbody > tr {
    border-bottom: 1px solid #e0e0e0;
  }
`;

const DownloadIcon = styled(JournalArrowDown)`
  width: 1em;
  height: 1em;
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
            } catch { }
        }

        setShow(s => !s);
    }

    const updateSearchText = (e) => {
        setSearchText(e.target.value);
        if (e.target.value !== "" && spoilerArea === "playthrough") {
            setSpoilerArea("all");
        }
    }

    const downloadSpoiler = async () => {

        /* Prepare a human-readable JSON dump of the spoiler data */
        let s = {
            seed: { ...spoiler.seed, spoiler: null },
            playthrough: spoiler.seed.gameId === 'smz3' ? JSON.parse(spoiler.seed.spoiler).filter(sphere => !isEmpty(sphere)).slice(0, -1) : JSON.parse(spoiler.seed.spoiler).filter(sphere => !isEmpty(sphere)),
            prizes: spoiler.seed.gameId === 'smz3' ? JSON.parse(spoiler.seed.spoiler).slice(-1) : [],
            regions: uniq(sortBy(spoiler.locations.map(l => l.locationRegion))).map(r => {
                return {
                    region: r,
                    locations: spoiler.locations.filter(l => l.locationRegion === r).map(l => {
                        return {
                            name: l.locationName,
                            item: l.itemName
                        };
                    })
                }
            })
        };

        let spoilerText = unescape(JSON.stringify(s, null, 4));

        var blob = new Blob([spoilerText], {
            type: "text/plain;charset=utf-8"
        });

        saveAs(blob, `${props.seedData.gameName} v${props.seedData.gameVersion} - ${encode(props.seedData.guid)} - Spoiler.txt`);
    }

    if (props.seedData === null || props.seedData.spoiler === "[]")
        return null;

    let locations = spoiler ? spoiler.locations : [];
    if (spoiler && searchText) {
        const re = new RegExp(escapeRegExp(searchText), 'i');
        locations = spoiler.locations.filter(l => l.locationName.match(re) || l.itemName.match(re));

        if (spoilerArea !== "all" && spoilerArea !== "playthrough" && spoilerArea !== "prizes" && !locations.find(l => l.locationArea === spoilerArea)) {
            setSpoilerArea("all");
        }
    }

    let playthrough = JSON.parse(props.seedData.spoiler).filter(sphere => !isEmpty(sphere));

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
                        <Row>
                            <Col md="9">
                            <SearchInputGroup>
                                <InputGroupAddon addonType="prepend">
                                    <InputGroupText><span role="img" aria-label="search">🔎</span></InputGroupText>
                                </InputGroupAddon>
                                <Input key="searchInput" placeholder="Find a location or item" onChange={updateSearchText} value={searchText} />
                                </SearchInputGroup>
                            </Col>
                            <Col>
                                <Button outline color="primary" className="float-right" onClick={downloadSpoiler}><DownloadIcon /> Download</Button>
                            </Col>
                        </Row>
                        <div>
                            <Nav pills style={{ marginBottom: "10px" }}>
                                <NavItem>
                                    <SmallNavLink href="#" active={spoilerArea === "playthrough"} onClick={() => setSpoilerArea("playthrough")}>Playthrough</SmallNavLink>
                                </NavItem>
                                {props.seedData.gameId === 'smz3' && <NavItem>
                                    <SmallNavLink href="#" active={spoilerArea === "prizes"} onClick={() => setSpoilerArea("prizes")}>Prizes</SmallNavLink>
                                </NavItem>}
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
                                        {playthrough.map((sphere, i) => (
                                            <div key={i}>
                                                {i < (playthrough.length - 1) || props.seedData.gameId === 'sm' ? <h6>Sphere {i + 1}</h6> : <h6>Prizes and Requirements</h6>}
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
                                : spoilerArea === 'prizes'
                                    ? <Card>
                                        <CardBody>
                                            <div>
                                                <h6>Prizes and Requirements</h6>
                                                <LocationTable>
                                                    <tbody>
                                                        {Object.entries(playthrough[playthrough.length - 1]).map(([location, item], i) => (
                                                            <tr key={i}>
                                                                <td style={{ width: '60%' }}>{location}</td>
                                                                <td>{item}</td>
                                                            </tr>
                                                        ))}
                                                    </tbody>
                                                </LocationTable>
                                            </div>
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
