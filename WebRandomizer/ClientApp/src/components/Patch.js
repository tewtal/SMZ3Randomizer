import React, { Component } from 'react';
import { Form, Row, Col, Card, CardBody, Button } from 'reactstrap';
import { saveAs } from 'file-saver';
import baseIps from '../files/base.ips';

export class Patch extends Component {
    static displayName = Patch.name;

    constructor(props) {
        super(props);
        this.state = { patchState: 0 }
        this.handleSubmitRom = this.handleSubmitRom.bind(this);
        this.handleDownloadRom = this.handleDownloadRom.bind(this);
        this.patchFile = this.patchFile.bind(this);
        this.fileInput = React.createRef();
        this.localForage = require('localforage');
    }

    async componentDidMount() {
        let fileData = await this.localForage.getItem("baseRom");
        if (fileData != null) {
            this.setState({ patchState: 1 });
        }
    }

    async handleSubmitRom(e) {
        e.preventDefault();
        const file = this.fileInput.current.files[0];
        let fileData = null;

        try {
            fileData = await this.readFile(file);
        } catch (err) {
            console.log("Could not read uploaded file data", err);
            return;
        }

        try {
            await this.localForage.setItem("baseRom", new Blob([fileData]));
            this.setState({
                patchState: 1
            });
        } catch (err) {
            console.log("Could not store file to localforage:", err);
        }
    }

    async handleDownloadRom(e) {
        try {
            let patchedData = await this.patchFile(Uint8Array.from(atob(this.props.patchData), c => c.charCodeAt(0)));
            saveAs(new Blob([patchedData]), this.props.fileName);
        } catch (err) {
            console.log(err);
        }
    }
    
    async patchFile(patchData) {
        return new Promise(async (resolve, reject) => {
            try {
                let i = 0;
                let fileBuf = await this.localForage.getItem("baseRom");
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

    async readFile(file) {
        const fileReader = new FileReader();
        return new Promise((resolve, reject) => {
            fileReader.onerror = () => {
                fileReader.abort();
                reject(new DOMException("Error parsing file"));
            };

            fileReader.onload = (e) => {
                resolve(e.target.result);
            }

            fileReader.readAsArrayBuffer(file);
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
            }

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

    render() {
        const uploadState = (
            <Card>
                <CardBody>
                    <Form onSubmit={this.handleSubmitRom}>
                        <h6>No ROM uploaded, please upload a valid ROM file.</h6>
                        <Row className="justify-content-between">
                            <Col md="6">
                                <input type="file" ref={this.fileInput} />
                            </Col>
                            <Col md="2">
                                <Button type="submit" color="primary">Upload File</Button>
                            </Col>
                        </Row>
                    </Form>
                </CardBody>
            </Card>
        );

        const downloadState = (
            <Card>
                <CardBody>
                    <Form onSubmit={this.handleSubmitRom}>
                        <Row className="justify-content-between">
                            <Col md="6">
                                <Button color="primary" onClick={this.handleDownloadRom}>Download ROM</Button>
                            </Col>
                        </Row>
                    </Form>
                </CardBody>
            </Card>
        );

        return this.state.patchState === 0 ? uploadState : downloadState;
    }
}
