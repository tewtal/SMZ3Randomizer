import React, { useState, useEffect, useContext } from 'react';
import styled from 'styled-components';
import { Form, Row, Col, Card, CardBody } from 'reactstrap';
import { Label, Button, Input, InputGroupAddon, InputGroupText } from 'reactstrap';
import BootstrapSwitchButton from 'bootstrap-switch-button-react';
import InputGroup from './util/PrefixInputGroup';
import DropdownSelect from './util/DropdownSelect';
import DownloadInfoTooltip from './util/DownloadInfoTooltip';
import Upload from './Upload';

import { GameTraitsCtx } from '../game/traits';

import { prepareRom } from '../file/rom';

import localForage from 'localforage';
import { saveAs } from 'file-saver';
import { encode } from 'slugid';

import compact from 'lodash/compact';
import join from 'lodash/join';
import set from 'lodash/set';
import attempt from 'lodash/attempt';
import defaultTo from 'lodash/defaultTo';

import inventory from '../resources/sprite/inventory';
import baseIpsSMZ3 from '../resources/zsm.ips.gz';
import baseIpsSM from '../resources/sm.ips.gz';

const baseIps = {
    sm: baseIpsSM,
    smz3: baseIpsSMZ3
};

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
    const [patchState, setPatchState] = useState('upload');
    const [z3Sprite, setZ3Sprite] = useState({});
    const [smSprite, setSMSprite] = useState({});
    const [smSpinjumps, setSMSpinjumps] = useState(false);
    const [z3HeartColor, setZ3HeartColor] = useState('red');
    const [z3HeartBeep, setZ3HeartBeep] = useState('half');
    const [smEnergyBeep, setSMEnergyBeep] = useState(true);

    const game = useContext(GameTraitsCtx);

    const sprites = {
        z3: [{ title: 'Link' }, ...inventory.z3],
        sm: [{ title: 'Samus' }, ...inventory.sm]
    };

    const { seed, world } = props;

    useEffect(() => {
        attempt(async () => {
            const fileDataSM = await localForage.getItem('baseRomSM');
            const fileDataLTTP = await localForage.getItem('baseRomLTTP');
            if ((!game.z3 || fileDataLTTP !== null) && fileDataSM !== null) {
                setPatchState('download');
            }
        });
    }, [patchState, game.z3]);

    useEffect(() => {
        let settings;
        if ((settings = restore())) {
            const { z3: z3Sprite, sm: smSprite, spinjumps } = settings.sprites || {};
            const { z3_heart_color, z3_heart_beep, sm_energy_beep } = settings;
            setZ3Sprite(sprites.z3.find(x => x.title === z3Sprite) || {});
            setSMSprite(sprites.sm.find(x => x.title === smSprite) || {});
            setSMSpinjumps(defaultTo(spinjumps, false));
            setZ3HeartColor(defaultTo(z3_heart_color, 'red'));
            setZ3HeartBeep(defaultTo(z3_heart_beep, 'half'));
            setSMEnergyBeep(defaultTo(sm_energy_beep, true));
        }
    }, []); /* eslint-disable-line react-hooks/exhaustive-deps */

    async function onDownloadRom() {
        try {
            if (world !== null) {
                const settings = { z3Sprite, smSprite, smSpinjumps, z3HeartColor, z3HeartBeep, smEnergyBeep };
                const patchedData = await prepareRom(world.patch, settings, baseIps[game.id], game);
                saveAs(new Blob([patchedData]), constructFileName());
            }
        } catch (error) {
            console.log(error);
        }
    }

    function constructFileName() {
        const { gameId, gameVersion, guid, seedNumber, mode } = seed;
        const settings = world.settings && JSON.parse(world.settings);

        /* compact works as long as seedNumber is string, since 0 is a valid number */
        const parts = compact([
            gameId.toUpperCase(),
            `V${gameVersion}`,
            /* either no settings present, or construct the parts */
            ...(!settings ? []
                : gameId === 'smz3' ? smz3Parts(settings) :
                    gameId === 'sm' ? smParts(settings) : []
            ),
            seedNumber || encode(guid),
            mode === 'multiworld' ? world.player : null
        ]);

        function smz3Parts({ smlogic, swordlocation, morphlocation, keyshuffle }) {
            return [
                `ZLn+SL${smlogic[0]}`,
                swordlocation && swordlocation !== 'randomized' ? `S${swordlocation[0]}` : null,
                morphlocation && morphlocation !== 'randomized' ? `M${morphlocation[0]}` : null,
                keyshuffle && keyshuffle !== 'none' ? `K${keyshuffle[0]}` : null
            ];
        }

        function smParts({ logic, placement }) {
            return [
                `L${logic[0]}`,
                `I${placement[0]}`
            ];
        }

        return `${join(parts, '-')}.sfc`;
    }

    const onUploadRoms = () => setPatchState('download');

    const onZ3SpriteChange = (i) => {
        setZ3Sprite(sprites.z3[i]);
        persist(set(restore() || {}, 'sprites.z3', sprites.z3[i].title));
    };
    const onSMSpriteChange = (i) => {
        setSMSprite(sprites.sm[i]);
        persist(set(restore() || {}, 'sprites.sm', sprites.sm[i].title));
    };
    const onSpinjumpToggle = () => {
        setSMSpinjumps(!smSpinjumps);
        persist(set(restore() || {}, 'sprites.spinjumps', !smSpinjumps));
    };
    const onZ3HeartColorChange = (value) => {
        setZ3HeartColor(value);
        persist(set(restore() || {}, 'z3_heart_color', value));
    };
    const onZ3HeartBeepChange = (value) => {
        setZ3HeartBeep(value);
        persist(set(restore() || {}, 'z3_heart_beep', value));
    };
    const onSMEnergyBeepToggle = () => {
        setSMEnergyBeep(!smEnergyBeep);
        persist(set(restore() || {}, 'sm_energy_beep', !smEnergyBeep));
    };

    function restore() {
        let value = localStorage.getItem('persist');
        return value && JSON.parse(value);
    }

    function persist(values) {
        localStorage.setItem('persist', JSON.stringify(values));
    }

    const component = patchState === 'upload' ? <Upload onUpload={onUploadRoms} /> : (
        <Form onSubmit={(e) => e.preventDefault()}>
            <Row className="mb-3">
                <Col md={game.smz3 ? 10 : 6}>
                    <SpriteSettings game={game} sprites={sprites} settings={{ z3Sprite, smSprite, smSpinjumps }}
                        onZ3SpriteChange={onZ3SpriteChange}
                        onSMSpriteChange={onSMSpriteChange}
                        onSpinjumpToggle={onSpinjumpToggle}
                    />
                </Col>
            </Row>
            {game.z3 && (
                <Row className="mb-3">
                    <Col md="5">
                        <InputGroup prefix="Heart Beep">
                            <Input type="select" value={z3HeartBeep} onChange={(e) => onZ3HeartBeepChange(e.target.value)}>
                                <option value="off">Off</option>
                                <option value="quarter">Quarter Speed</option>
                                <option value="half">Half Speed</option>
                                <option value="normal">Normal Speed</option>
                                <option value="double">Double Speed</option>
                            </Input>
                        </InputGroup>
                    </Col>
                    <Col md="4">
                        <InputGroup prefix="Heart Color">
                            <Input type="select" value={z3HeartColor} onChange={(e) => onZ3HeartColorChange(e.target.value)}>
                                <option value="red">Red</option>
                                <option value="green">Green</option>
                                <option value="blue">Blue</option>
                                <option value="yellow">Yellow</option>
                            </Input>
                        </InputGroup>
                    </Col>
                </Row>
            )}
            <Row className="mb-3">
                <Col md="4">
                    <InputGroup prefixClassName="mr-1" prefix="Energy Beep">
                        <BootstrapSwitchButton width="80" onlabel="On" offlabel="Off" checked={smEnergyBeep}
                            onChange={onSMEnergyBeepToggle}
                        />
                    </InputGroup>
                </Col>
            </Row>
            <Row>
                <Col className="d-flex align-items-center" md="6">
                    <Button color="primary" onClick={onDownloadRom}>Download ROM</Button>
                    <DownloadInfoTooltip className="ml-2" gameId={game.id} />
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

function SpriteSettings(props) {
    const { sprites, settings } = props;
    const { z3Sprite, smSprite, smSpinjumps } = settings;
    const game = useContext(GameTraitsCtx);

    let value;
    return (
        <InputGroup className="flex-nowrap" prefix="Play as">
            {game.z3 && (
                <DropdownSelect placeholder="Select Z3 sprite"
                    index={(value = sprites.z3.findIndex(x => x.title === z3Sprite.title)) < 0 ? 0 : value}
                    onIndexChange={props.onZ3SpriteChange}>
                    {sprites.z3.map(({ title }, i) => <SpriteOption key={title}><Z3Sprite index={i} />{title}</SpriteOption>)}
                </DropdownSelect>
            )}
            <DropdownSelect placeholder="Select SM sprite"
                index={(value = sprites.sm.findIndex(x => x.title === smSprite.title)) < 0 ? 0 : value}
                onIndexChange={props.onSMSpriteChange}>
                {sprites.sm.map(({ title }, i) => <SpriteOption key={title}><SMSprite index={i} />{title}</SpriteOption>)}
            </DropdownSelect>
            <InputGroupAddon addonType="append">
                <InputGroupText tag={Label} title="Enable separate space/screw jump animations">
                    <Input type="checkbox" addon={true} checked={smSpinjumps} onChange={props.onSpinjumpToggle} />{' '}
                    <JumpSprite which="space" /> / <JumpSprite which="screw" />
                </InputGroupText>
            </InputGroupAddon>
        </InputGroup>
    );
}
