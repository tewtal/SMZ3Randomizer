import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import { Form, Row, Col, Card, CardBody } from 'reactstrap';
import { Label, Button, Input, InputGroupAddon, InputGroupText } from 'reactstrap';
import InputGroup from './util/PrefixInputGroup';
import DropdownSelect from './util/DropdownSelect';
import Upload from './Upload';

import { prepareRom } from '../file/rom';

import localForage from 'localforage';
import { saveAs } from 'file-saver';

import attempt from 'lodash/attempt';
import set from 'lodash/set';

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
    const [spinjumps, setSpinjumps] = useState(false);

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

    useEffect(() => {
        let settings;
        if ((settings = restore())) {
            const { z3, sm, spinjumps } = settings.sprites || {};
            setZ3Sprite(sprites.z3.find(x => x.title === z3) || {});
            setSMSprite(sprites.sm.find(x => x.title === sm) || {});
            setSpinjumps(spinjumps);
        }
    }, []);

    async function onDownloadRom() {
        try {
            const { sessionData: { seed }, clientData } = props;
            const world = seed.worlds.find(world => world.worldId === clientData.worldId);
            if (world != null) {
                const settings = { z3Sprite, smSprite, spinjumps };
                const patchedData = await prepareRom(world.patch, settings, baseIps);
                const fileName = `${seed.gameName} - ${seed.seedNumber} - ${clientData.name}.sfc`;
                saveAs(new Blob([patchedData]), fileName);
            }
        } catch (error) {
            console.log(error);
        }
    }

    const onUploadRoms = () => setMode('download');

    const onZ3SpriteChange = (i) => {
        setZ3Sprite(sprites.z3[i]);
        persist(set(restore() || {}, 'sprites.z3', sprites.z3[i].title));
    };
    const onSMSpriteChange = (i) => {
        setSMSprite(sprites.sm[i]);
        persist(set(restore() || {}, 'sprites.sm', sprites.sm[i].title));
    };
    const onSpinjumpToggle = () => {
        setSpinjumps(!spinjumps);
        persist(set(restore() || {}, 'sprites.spinjumps', !spinjumps));
    };

    function restore() {
        let value = localStorage.getItem('persist');
        return value && JSON.parse(value);
    }

    function persist(values) {
        localStorage.setItem('persist', JSON.stringify(values));
    }

    let value;
    const component = mode === 'upload' ? (
        <Upload onUpload={onUploadRoms} />
    ) : (
        <Form onSubmit={(e) => e.preventDefault()}>
            <Row className="mb-3">
                <Col md="8">
                    <InputGroup className="flex-nowrap" prefix="Play as">
                        <DropdownSelect
                            placeholder="Select Z3 sprite"
                            index={(value = sprites.z3.findIndex(x => x.title === z3Sprite.title)) < 0 ? 0 : value}
                            onIndexChange={onZ3SpriteChange}>
                            {sprites.z3.map(({ title }, i) => <SpriteOption key={title}><Z3Sprite index={i} />{title}</SpriteOption>)}
                        </DropdownSelect>
                        <DropdownSelect
                            placeholder="Select SM sprite"
                            index={(value = sprites.sm.findIndex(x => x.title === smSprite.title)) < 0 ? 0 : value}
                            onIndexChange={onSMSpriteChange}>
                            {sprites.sm.map(({ title }, i) => <SpriteOption key={title}><SMSprite index={i} />{title}</SpriteOption>)}
                        </DropdownSelect>
                        <InputGroupAddon addonType="append">
                            <InputGroupText tag={Label} title="Enable separate space/screw jump animations">
                                <Input type="checkbox" addon={true} checked={spinjumps} onChange={onSpinjumpToggle} />{' '}
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
