import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import { Form, Row, Col, Card, CardBody } from 'reactstrap';
import { Label, Button, Input, InputGroupAddon, InputGroupText } from 'reactstrap';
import InputGroup from './util/PrefixInputGroup';
import DropdownSelect from './util/DropdownSelect';
import Upload from './Upload';

import { readAsArrayBuffer } from '../file/util';
import { applyIps, applySeed } from '../file/rom';
import { parse_rdc } from '../file/rdc';

import localForage from 'localforage';
import { saveAs } from 'file-saver';
import { inflate } from 'pako';

import attempt from 'lodash/attempt';

import inventory from '../resources/sprite/inventory.json';
import baseIps from '../resources/zsm.ips.gz';

const SpriteOption = styled.div`
    display: flex;
    white-space: nowrap;
    > * { flex: none; }
`;

/* through bootstrap "$input-btn-padding-x" */
const inputPaddingX = '.75rem';

const Z3Sprite = styled.option`
    width: 16px;
    height: 24px;
    margin-right: ${inputPaddingX};
    background-size: auto 24px;
    background-position: -${props => props.index * 16}px 0;
    background-image: url(${process.env.PUBLIC_URL}/sprites/z3.png);
`;

const SMSprite = styled(Z3Sprite)`
    background-image: url(${process.env.PUBLIC_URL}/sprites/sm.png);
`;

const JumpSprite = styled.span`
    width: 17px;
    height: 17px;
    background-size: auto 17px;
    background-image: url(${process.env.PUBLIC_URL}/sprites/jump_${props => props.which}.png);
`;

export default function Patch(props) {
    const [mode, setMode] = useState('upload');
    const [z3Sprite, setZ3Sprite] = useState({});
    const [smSprite, setSMSprite] = useState({});
    const [spinjumps, setSpinjumps] = useState(true);

    const sprites = {
        z3: [{ title: 'Link' }, ...inventory.z3],
        sm: [{ title: 'Samus' }, ...inventory.sm],
    };

    useEffect(() => {
        attempt(async () => {
            const fileData = await localForage.getItem('baseRomCombo');
            if (fileData != null)
                setMode('download');
        });
    }, [mode]);

    async function onDownloadRom() {
        try {
            const worlds = props.sessionData.seed.worlds;
            const world = worlds.find(world => world.worldId === props.clientData.worldId);
            if (world != null) {
                downloadRom(world, { z3Sprite, smSprite, spinjumps }, props.fileName);
            }
        } catch (error) {
            console.log(error);
        }
    }

    const onUploadRoms = () => setMode('download');

    const onZ3SpriteChange = (i) => setZ3Sprite(sprites.z3[i]);
    const onSMSpriteChange = (i) => setSMSprite(sprites.sm[i]);

    const component = mode === 'upload' ? (
        <Upload onUpload={onUploadRoms} />
    ) : (
        <Form onSubmit={(e) => e.preventDefault()}>
            <Row className="mb-3">
                <Col md="8">
                    <InputGroup className="flex-nowrap" prefix="Play as">
                        <DropdownSelect placeholder="Select Z3 sprite" initialIndex={0} onIndexChange={onZ3SpriteChange}>
                            {sprites.z3.map(({ title }, i) => <SpriteOption key={title}><Z3Sprite index={i} />{title}</SpriteOption>)}
                        </DropdownSelect>
                        <DropdownSelect placeholder="Select SM sprite" initialIndex={0} onIndexChange={onSMSpriteChange}>
                            {sprites.sm.map(({ title }, i) => <SpriteOption key={title}><SMSprite index={i} />{title}</SpriteOption>)}
                        </DropdownSelect>
                        <InputGroupAddon addonType="append">
                            <InputGroupText tag={Label} title="Enable separate space/screw jump animations">
                                <Input type="checkbox" addon={true} checked={spinjumps} onChange={() => setSpinjumps(!spinjumps)} />{' '}
                                <JumpSprite which="space" /> / <JumpSprite which="screw" />
                            </InputGroupText>
                        </InputGroupAddon>
                    </InputGroup>
                </Col>
            </Row>
            <Row>
                <Col md="6">
                    <Button color="primary" onClick={onDownloadRom}>Download ROM</Button>
                </Col>
            </Row>
        </Form>
    );

    return (
        <Card>
            <CardBody>
                {component}
            </CardBody>
        </Card>
    );
}

async function downloadRom(world, settings, fileName) {
    const patchedData = await prepareRom(world.patch, settings);
    saveAs(new Blob([patchedData]), fileName);
}

async function prepareRom(world_patch, settings) {
    const rom_blob = await localForage.getItem('baseRomCombo');
    const rom = new Uint8Array(await readAsArrayBuffer(rom_blob));
    const base_patch = maybeCompressed(new Uint8Array(await (await fetch(baseIps, { cache: 'no-store' })).arrayBuffer()));
    world_patch = Uint8Array.from(atob(world_patch), c => c.charCodeAt(0));

    applyIps(rom, base_patch);
    await applySprite(rom, 'link_sprite', settings.z3Sprite);
    await applySprite(rom, 'samus_sprite', settings.smSprite);
    if (settings.spinjumps) {
        enableSeparateSpinjumps(rom);
    }
    applySeed(rom, world_patch);

    return rom;
}

function enableSeparateSpinjumps(rom) {
    rom[0x34F500] = 0x01;
}

async function applySprite(rom, block, sprite) {
    if (sprite.path) {
        const url = `${process.env.PUBLIC_URL}/sprites/${sprite.path}`;
        const rdc = maybeCompressed(new Uint8Array(await (await fetch(url)).arrayBuffer()));
        // Todo: do something with the author field
        const [author, blocks] = parse_rdc(rdc);
        blocks[block] && blocks[block](rom);
    }
}

function maybeCompressed(data) {
    const big = false;
    const isGzip = new DataView(data.buffer).getUint16(0, big) == 0x1f8b;
    return isGzip ? inflate(data) : data;
}
