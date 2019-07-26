import React, { Component } from 'react';
import { Form, Row, Col, Card, CardBody, Button } from 'reactstrap';
import styled from 'styled-components';
import { saveAs } from 'file-saver';
import { Upload } from './Upload';
import { PlayerSprite } from './PlayerSprite';
import { readAsArrayBuffer, applyIps, applySeed } from '../file_handling';
import { parse_rdc } from '../file_handling/rdc';
import sprites from '../files/sprite/inventory.json';
import baseIps from '../files/zsm_190803.ips';
import spriteEngineIps from '../files/zsm_sm_sprite_engine.ips';

export class Patch extends Component {
    static displayName = Patch.name;

    constructor(props) {
        super(props);
        this.localForage = require('localforage');
        this.state = { patchState: 'upload' };
        this.sprites = {
            z3: [{ title: 'Link' }, ...sprites.z3],
            sm: [{ title: 'Samus' }, ...sprites.sm],
        };

    }

    async componentDidMount() {
        let fileData = await this.localForage.getItem("baseRomCombo");
        if (fileData != null) {
            this.setState({ patchState: 'download' });
        }
    }

    handleUploadRoms = () => {
        this.setState({ patchState: 'download' });
    }

    onZ3SpriteChange = (index) => this.onSpriteChange('z3', index)
    onSMSpriteChange = (index) => this.onSpriteChange('sm', index)

    onSpriteChange(game, index) {
        this.setState({ [`${game}_sprite`]: this.sprites[game][index] });
    }

    handleDownloadRom = async () => {
        try {
            /* find world by clients worldId to make sure we get the right world */
            let world = null;
            for (let i = 0; i < this.props.sessionData.seed.worlds.length; i++) {
                if (this.props.sessionData.seed.worlds[i].worldId === this.props.clientData.worldId) {
                    world = this.props.sessionData.seed.worlds[i];
                }
            }

            if (world !== null) {
                let patchedData = await this.prepareRom(world.patch);
                saveAs(new Blob([patchedData]), this.props.fileName);
            }
        } catch (err) {
            console.log(err);
        }
    }

    async prepareRom(world_patch) {
        const rom_blob = await this.localForage.getItem("baseRomCombo");
        const rom = new Uint8Array(await readAsArrayBuffer(rom_blob));
        const base_patch = new Uint8Array(await (await fetch(baseIps)).arrayBuffer());
        const sprite_patch = new Uint8Array(await (await fetch(spriteEngineIps)).arrayBuffer());
        world_patch = Uint8Array.from(atob(world_patch), c => c.charCodeAt(0));

        applyIps(rom, base_patch);
        applyIps(rom, sprite_patch);
        await this.applySprite(rom, 'link_sprite', this.state.z3_sprite);
        await this.applySprite(rom, 'samus_sprite', this.state.sm_sprite);
        applySeed(rom, world_patch);

        return rom;
    }

    async applySprite(rom, block, sprite = {}) {
        if (sprite.path) {
            const url = `${process.env.PUBLIC_URL}/sprites/${sprite.path}`;
            const rdc = new Uint8Array(await (await fetch(url)).arrayBuffer());
            // Todo: do something with the author field
            const [author, blocks] = parse_rdc(rdc);
            blocks[block] && blocks[block](rom);
        }
    }

    handleSubmit = (e) => e.preventDefault()

    render() {
        const uploading = this.state.patchState === 'upload';

        const component = uploading ? <Upload onUpload={this.handleUploadRoms} /> :
            <Form onSubmit={this.handleSubmit}>
                <Row>
                    <Col md="6">
                        <PlayerSprite options={this.sprites.z3} onChange={this.onZ3SpriteChange} />
                    </Col>
                </Row>
                <Row>
                    <Col md="6">
                        <PlayerSprite options={this.sprites.sm} onChange={this.onSMSpriteChange} />
                    </Col>
                </Row>
                <Row>
                    <Col md="6">
                        <Button color="primary" onClick={this.handleDownloadRom}>Download ROM</Button>
                    </Col>
                </Row>
            </Form>;

        return (
            <Card>
                <CardBody>
                    {component}
                </CardBody>
            </Card>
        );
    }
}
