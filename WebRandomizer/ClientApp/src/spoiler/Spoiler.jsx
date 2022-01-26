import React, { useState } from 'react';
import { Row, Col, Card, CardHeader, CardBody, Nav, NavItem, Input, Button } from 'reactstrap';
import InputGroup from '../ui/PrefixInputGroup';
import { SmallNavLink, StyledTable } from './styled';

import { SearchIcon, DownloadIcon } from './styled';

import { saveAs } from 'file-saver';
import { encode } from 'slugid';

import { tryParseJson } from '../util';
import filter from 'lodash/filter';
import some from 'lodash/some';
import includes from 'lodash/includes';
import sortBy from 'lodash/sortBy';
import uniq from 'lodash/uniq';
import initial from 'lodash/initial';
import last from 'lodash/last';
import isEmpty from 'lodash/isEmpty';
import escapeRegExp from 'lodash/escapeRegExp';

export default function Spoiler({ seedGuid }) {
    const [show, setShow] = useState(false);
    const [spoiler, setSpoiler] = useState(null);
    const [spoilerArea, setSpoilerArea] = useState('playthrough');
    const [searchText, setSearchText] = useState('');

    const seed = spoiler ? spoiler.seed : {};
    const playthrough = filter(tryParseJson(seed.spoiler), sphere => !isEmpty(sphere));

    async function toggleSpoiler() {
        if (!show && !spoiler) {
            try {
                const response = await fetch(`/api/spoiler/${seedGuid}`);
                const result = await response.json();
                setSpoiler(result);
            } catch { }
        }

        setShow(show => !show);
    }

    function updateSearchText(e) {
        const { value } = e.target;
        setSearchText(value);
        if (value && spoilerArea === 'playthrough') {
            setSpoilerArea('all');
        }
    }

    async function downloadSpoiler() {
        /* Prepare a human-readable JSON dump of the spoiler data */
        const s = {
            seed: { ...seed, spoiler: null },
            ...(seed.gameId === 'smz3'
                ? { playthrough: initial(playthrough), prizes: last(playthrough) }
                : { playthrough }
            ),
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

        const text = unescape(JSON.stringify(s, null, 4));

        const blob = new Blob([text], { type: 'text/plain;charset=utf-8' });
        saveAs(blob, `${seed.gameName} v${seed.gameVersion} - ${encode(seed.guid)} - Spoiler.txt`);
    }

    let locations = spoiler ? spoiler.locations : [];
    if (spoiler && searchText) {
        const pattern = new RegExp(escapeRegExp(searchText), 'i');
        locations = filter(spoiler.locations, l => pattern.test(l.locationName) || pattern.test(l.itemName));

        if (!includes(['all', 'playthrough', 'prizes'], spoilerArea) && !some(locations, { locationArea: spoilerArea })) {
            setSpoilerArea('all');
        }
    }

    return (
        <Card>
            <CardHeader>
                <Row className="align-items-center justify-content-between">
                    <Col>Spoiler log</Col>
                    <Col><Button outline className="float-right" color="secondary" onClick={toggleSpoiler}>{show ? 'Hide' : 'Show'}</Button></Col>
                </Row>
            </CardHeader>
            {show && <CardBody>
                {spoiler ? <div>
                    <Row>
                        <Col md="9">
                            <InputGroup className="mb-3" prefix={<SearchIcon />}>
                                <Input key="searchInput" placeholder="Find a location or item" onChange={updateSearchText} value={searchText} />
                            </InputGroup>
                        </Col>
                        <Col>
                            <Button outline color="primary" className="float-right" onClick={downloadSpoiler}><DownloadIcon /> Download</Button>
                        </Col>
                    </Row>
                    <div>
                        <Nav pills className="mb-2">
                            <NavItem>
                                <SmallNavLink href="#" active={spoilerArea === 'playthrough'} onClick={() => setSpoilerArea('playthrough')}>Playthrough</SmallNavLink>
                            </NavItem>
                            {seed.gameId === 'smz3' && <NavItem>
                                <SmallNavLink href="#" active={spoilerArea === 'prizes'} onClick={() => setSpoilerArea('prizes')}>Prizes</SmallNavLink>
                            </NavItem>}
                            <NavItem>
                                <SmallNavLink href="#" active={spoilerArea === 'all'} onClick={() => setSpoilerArea('all')}>All</SmallNavLink>
                            </NavItem>
                            {uniq(sortBy(locations, l => l.locationId).map(l => l.locationArea)).map((area, i) => (
                                <NavItem key={i}>
                                    <SmallNavLink href="#" active={spoilerArea === area} onClick={() => setSpoilerArea(area)}>{area}</SmallNavLink>
                                </NavItem>
                            ))}
                        </Nav>
                        <Card>
                            <CardBody>
                                {spoilerArea === 'playthrough' ? <>
                                    {playthrough.map((sphere, i) => (
                                        <div key={i}>
                                            {i < (playthrough.length - 1) || seed.gameId === 'sm'
                                                ? <h6>Sphere {i + 1}</h6>
                                                : <h6>Prizes and Requirements</h6>
                                            }
                                            <StyledTable className="mb-4">
                                                <tbody>
                                                    {Object.entries(sphere).map(([location, item], j) => (
                                                        <tr key={j}>
                                                            <td>{location}</td>
                                                            <td>{item}</td>
                                                        </tr>
                                                    ))}
                                                </tbody>
                                            </StyledTable>
                                        </div>
                                    ))}
                                </>
                                : spoilerArea === 'prizes' ? <div>
                                    <h6>Prizes and Requirements</h6>
                                    <StyledTable className="mb-4">
                                        <tbody>
                                            {Object.entries(playthrough[playthrough.length - 1]).map(([location, item], i) => (
                                                <tr key={i}>
                                                    <td>{location}</td>
                                                    <td>{item}</td>
                                                </tr>
                                            ))}
                                        </tbody>
                                    </StyledTable>
                                </div>
                                : <>
                                    {uniq(sortBy(locations.filter(l => spoilerArea === 'all' || l.locationArea === spoilerArea), l => l.locationRegion).map(l => l.locationRegion)).map((r, i) => (
                                        <div key={i}>
                                            <h6>{r}</h6>
                                            <StyledTable className="mb-4">
                                                <tbody>
                                                    {locations.filter(l => (spoilerArea === 'all' || l.locationArea === spoilerArea) && l.locationRegion === r).map((l, j) => (
                                                        <tr key={j}>
                                                            <td>{l.locationName}{seed.players > 1 ? ` - ${seed.worlds[l.worldId].player}` : ''}</td>
                                                            <td>{l.itemName}{seed.players > 1 ? ` - ${seed.worlds[l.itemWorldId].player}` : ''}</td>
                                                        </tr>
                                                    ))}
                                                </tbody>
                                            </StyledTable>
                                        </div>
                                    ))}
                                </>}
                            </CardBody>
                        </Card>
                    </div>
                </div>
                : 'Loading...'}
            </CardBody>}
        </Card>
    );
}
