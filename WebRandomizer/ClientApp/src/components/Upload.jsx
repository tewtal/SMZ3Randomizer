import React, { Component } from 'react';
import { Form, Row, Col, Button } from 'reactstrap';
import { readAsArrayBuffer, mergeRoms } from '../file_handling';

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
            fileDataSM = new Uint8Array(await readAsArrayBuffer(smFile));
        } catch (err) {
            console.log("Could not read uploaded SM file data", err);
            return;
        }

        try {
            fileDataLTTP = new Uint8Array(await readAsArrayBuffer(lttpFile));
        } catch (err) {
            console.log("Could not read uploaded LTTP file data", err);
            return;
        }

        const fileData = mergeRoms(fileDataSM, fileDataLTTP);

        try {
            await this.localForage.setItem("baseRomCombo", new Blob([fileData]));
        } catch (err) {
            console.log("Could not store file to localforage:", err);
            return;
        }

        this.props.onUpload();
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
