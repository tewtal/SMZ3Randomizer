import React, { useState } from 'react';
import { Row, Col, Card, CardHeader, CardBody, Nav, NavItem, Input, Button } from 'reactstrap';
import InputGroup from '../ui/PrefixInputGroup';
import { SmallNavLink, StyledTable } from './styled';

import { SearchIcon, DownloadIcon } from './styled';

import { saveAs } from 'file-saver';
import { encode } from 'slugid';

import { tryParseJson, sortGroupBy } from '../util';
import map from 'lodash/map';
import filter from 'lodash/filter';
import some from 'lodash/some';
import includes from 'lodash/includes';
import sortBy from 'lodash/sortBy';
import uniq from 'lodash/uniq';
import initial from 'lodash/initial';
import last from 'lodash/last';
import toPairs from 'lodash/toPairs';
import isEmpty from 'lodash/isEmpty';
import escapeRegExp from 'lodash/escapeRegExp';

export default function Spoiler({ seedGuid }) {
    const [show, setShow] = useState(false);
    const [spoiler, setSpoiler] = useState(null);
    const [spoilerArea, setSpoilerArea] = useState('playthrough');
    const [searchText, setSearchText] = useState('');

    const { seed = {} } = spoiler || {};
    const { worlds, players, spoiler: playthrough = [] } = seed;
    const multiworld = players > 1;

    async function toggleSpoiler() {
        if (!show && !spoiler) {
            try {
                const response = await fetch(`/api/spoiler/${seedGuid}`);
                const result = await response.json();
                result.seed.spoiler = filter(tryParseJson(result.seed.spoiler), sphere => !isEmpty(sphere));
                result.locations = sortBy(result.locations, 'locationId');
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
    let content;
    if (spoiler) {
        if (searchText) {
            const pattern = new RegExp(escapeRegExp(searchText), 'i');
            locations = filter(spoiler.locations, l => pattern.test(l.locationName) || pattern.test(l.itemName));

            if (!includes(['all', 'playthrough', 'prizes'], spoilerArea) && !some(locations, { locationArea: spoilerArea })) {
                setSpoilerArea('all');
            }
        }

        if (spoilerArea === 'playthrough') {
            content = seed.gameId === 'smz3' ? [
                ...sphereContent(initial(playthrough)),
                ...prizeReqContent(last(playthrough))
            ] : sphereContent(playthrough);
        }
        else if (spoilerArea === 'prizes')
            content = prizeReqContent(last(playthrough));
        else
            content = areaContent(locations);
    }

    function sphereContent(spheres) {
        return map(spheres, (sphere, i) => [`Sphere ${i + 1}`, toPairs(sphere)]);
    }

    function prizeReqContent(section) {
        return [['Prizes and Requirements', toPairs(section)]];
    }

    function areaContent(locations) {
        const locationsInArea = filter(locations, spoilerArea !== 'all' ? { locationArea: spoilerArea } : {});
        const locationsByRegion = sortGroupBy(locationsInArea, 'locationRegion');
        return map(locationsByRegion, ([region, locations]) =>
            [region, map(locations, ({ locationName, worldId, itemName, itemWorldId }) => [
                multiworld ? `${locationName} - ${worlds[worldId].player}` : locationName,
                multiworld ? `${itemName} - ${worlds[itemWorldId].player}` : itemName,
            ])]
        );
    }

    const areas = uniq(map(locations, 'locationArea'));

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
                            {map(areas, area => (
                                <NavItem key={area}>
                                    <SmallNavLink href="#" active={spoilerArea === area} onClick={() => setSpoilerArea(area)}>{area}</SmallNavLink>
                                </NavItem>
                            ))}
                        </Nav>
                        <Card>
                            <CardBody>
                                {map(content, ([title, entries]) => (
                                    <div key={title}>
                                        <h6>{title}</h6>
                                        <StyledTable className="mb-4">
                                            <tbody>
                                                {map(entries, ([key, value]) => (
                                                    <tr key={key}>
                                                        <td>{key}</td>
                                                        <td>{value}</td>
                                                    </tr>
                                                ))}
                                            </tbody>
                                        </StyledTable>
                                    </div>
                                ))}
                            </CardBody>
                        </Card>
                    </div>
                </div>
                : 'Loading...'}
            </CardBody>}
        </Card>
    );
}
