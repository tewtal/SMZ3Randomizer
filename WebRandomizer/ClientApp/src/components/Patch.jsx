import React, { Component } from 'react';
import { Form, Row, Col, Card, CardBody, Button } from 'reactstrap';
import { saveAs } from 'file-saver';
import { Upload } from './Upload';
import { readAsArrayBuffer, applyIps, applySeed } from '../file_handling';
import baseIps from '../files/zsm_190803.ips';

export class Patch extends Component {
    static displayName = Patch.name;

    constructor(props) {
        super(props);
        this.localForage = require('localforage');
        this.state = { patchState: 'upload' };
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
        world_patch = Uint8Array.from(atob(world_patch), c => c.charCodeAt(0));

        applyIps(rom, base_patch);
        applySeed(rom, world_patch);

        return rom;
    }

    handleSubmit = (e) => e.preventDefault()

    render() {
        const uploading = this.state.patchState === 'upload';

        const component = uploading ? <Upload onUpload={this.handleUploadRoms} /> :
            <Form onSubmit={this.handleSubmit}>
                <Row className="justify-content-between">
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
