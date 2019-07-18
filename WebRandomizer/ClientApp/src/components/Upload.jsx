import React, { Component } from 'react';
import { Form, Row, Col, Button } from 'reactstrap';

export class Upload extends Component {
    static displayName = Upload.name;

    constructor(props) {
        super(props);
        this.fileInputSM = React.createRef();
        this.fileInputLTTP = React.createRef();
        this.localForage = require('localforage');
    }

    handleSubmitRom = async (e) => {
        e.preventDefault();
        const smFile = this.fileInputSM.current.files[0];
        const lttpFile = this.fileInputLTTP.current.files[0];

        let fileDataSM = null;
        let fileDataLTTP = null;

        try {
            fileDataSM = await this.readFile(smFile);
        } catch (err) {
            console.log("Could not read uploaded SM file data", err);
            return;
        }

        try {
            fileDataLTTP = await this.readFile(lttpFile);
        } catch (err) {
            console.log("Could not read uploaded LTTP file data", err);
            return;
        }

        let fileData = this.mergeROMS(fileDataSM, fileDataLTTP);

        try {
            await this.localForage.setItem("baseRomCombo", fileData);
        } catch (err) {
            console.log("Could not store file to localforage:", err);
            return;
        }

        this.props.onUpload();
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
            };

            fileReader.readAsArrayBuffer(file);
        });
    }

    async mergeROMS(smRomBuffer, alttpRomBuffer) {
        let smRom = new Uint8Array(smRomBuffer);
        let alttpRom = new Uint8Array(alttpRomBuffer);

        let data = new Uint8Array(0x600000);

        let pos = 0;
        for (let i = 0; i < 0x40; i++) {
            let hi_bank = smRom.slice((i * 0x8000), (i * 0x8000) + 0x8000);
            let lo_bank = smRom.slice(((i + 0x40) * 0x8000), ((i + 0x40) * 0x8000) + 0x8000);

            data.set(lo_bank, pos);
            data.set(hi_bank, pos + 0x8000);
            pos += 0x10000;
        }

        pos = 0x400000;
        for (let i = 0; i < 0x20; i++) {
            let hi_bank = alttpRom.slice((i * 0x8000), (i * 0x8000) + 0x8000);
            data.set(hi_bank, pos + 0x8000);
            pos += 0x10000;
        }

        return new Blob([data]);
    }

    render() {
        return (
            <Form onSubmit={this.handleSubmitRom}>
                <h6>No ROM uploaded, please upload a valid ROM file.</h6>
                <Row className="justify-content-between">
                    <Col md="6">SM ROM: <input type="file" ref={this.fileInputSM} /></Col>
                    <Col md="6">ALTTP ROM: <input type="file" ref={this.fileInputLTTP} /></Col>
                </Row>
                <Row>
                    <Col md="2">
                        <br />
                        <Button type="submit" color="primary">Upload Files</Button>
                    </Col>
                </Row>
            </Form>
        );
    }
}
