import React, { Component } from 'react';
import { Form, Row, Col, Card, CardBody, Button } from 'reactstrap';
import { saveAs } from 'file-saver';
import { Upload } from './Upload';
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
                let patchedData = await this.patchFile(Uint8Array.from(atob(world.patch), c => c.charCodeAt(0)));
                saveAs(new Blob([patchedData]), this.props.fileName);
            }
        } catch (err) {
            console.log(err);
        }
    }
    
    patchFile = async (patchData) => {
        return new Promise(async (resolve, reject) => {
            try {
                let i = 0;
                let fileBuf = await this.localForage.getItem("baseRomCombo");
                let outBuf = await this.readBlob(fileBuf);

                outBuf = await this.applyIPS(outBuf, baseIps);
                if (outBuf === null) {
                    reject(false);
                }

                while (i < patchData.length) {
                    let target = patchData[i] + (patchData[i + 1] << 8) + (patchData[i + 2] << 16) + (patchData[i + 3] << 24);
                    let size = patchData[i + 4] + (patchData[i + 5] << 8);
                    i += 6;
                    for (let j = 0; j < size; j++) {
                        outBuf[target + j] = patchData[i + j];
                    }
                    i += size;
                }
                resolve(outBuf);
            } catch (err) {
                reject(err);
            }
        });
    }

    async readBlob(blob) {
        const fileReader = new FileReader();
        return new Promise((resolve, reject) => {
            fileReader.onerror = () => {
                fileReader.abort();
                reject(new DOMException("Error parsing blob"));
            };

            fileReader.onload = (e) => {
                resolve(new Uint8Array(e.target.result));
            };

            fileReader.readAsArrayBuffer(blob);
        });
    }

    async applyIPS(fileBuf, ipsUrl) {
        try {
            let ips = await fetch(ipsUrl);
            let patchBuf = new Uint8Array(await ips.arrayBuffer());

            let i = 5;
            while (i < patchBuf.length) {
                let offset = (patchBuf[i] << 16) + (patchBuf[i + 1] << 8) + (patchBuf[i + 2]);
                let size = (patchBuf[i + 3] << 8) + (patchBuf[i + 4]);
                i += 5;
                if (size > 0) {
                    for (let j = 0; j < size; j++) {
                        fileBuf[offset + j] = patchBuf[i + j];
                    }
                    i += size;
                } else {
                    let rleSize = (patchBuf[i] << 8) + (patchBuf[i + 1]);
                    let rleByte = patchBuf[i + 2];
                    for (let j = 0; j < rleSize; j++) {
                        fileBuf[offset + j] = rleByte;
                    }
                    i += 3;
                }
            }

            return fileBuf;
        } catch (err) {
            return null;
        }
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
