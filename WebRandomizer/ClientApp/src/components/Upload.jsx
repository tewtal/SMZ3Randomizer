import React, { useState, useRef } from 'react';
import { Form, Row, Col, Button } from 'reactstrap';
import { readAsArrayBuffer } from '../file/util';
import { mergeRoms } from '../file/rom';

import localForage from 'localforage';

import hasIn from 'lodash/hasIn';

export default function Upload(props) {
    const [canUpload, setCanUpload] = useState(false);
    const fileInputSM = useRef(null);
    const fileInputZ3 = useRef(null);

    async function onSubmitRom() {
        const smFile = fileInputSM.current.files[0];
        const z3File = fileInputZ3.current.files[0];

        let fileDataSM = null;
        let fileDataZ3 = null;

        try {
            fileDataSM = new Uint8Array(await readAsArrayBuffer(smFile));
        } catch (error) {
            console.log("Could not read uploaded SM file data:", error);
            return;
        }

        try {
            fileDataZ3 = new Uint8Array(await readAsArrayBuffer(z3File));
        } catch (error) {
            console.log("Could not read uploaded ALTTP file data:", error);
            return;
        }

        const fileData = mergeRoms(fileDataSM, fileDataZ3);

        try {
            await localForage.setItem('baseRomCombo', new Blob([fileData]));
        } catch (error) {
            console.log("Could not store file to localforage:", error);
            return;
        }

        props.onUpload();
    }

    const onFileSelect = () => {
        setCanUpload(
            hasIn(fileInputSM.current, 'files[0]') &&
            hasIn(fileInputZ3.current, 'files[0]')
        );
    }

    return (
        <Form onSubmit={(e) => { e.preventDefault(); onSubmitRom(); }}>
            <h6>No ROM uploaded, please upload a valid ROM file.</h6>
            <Row className="justify-content-between">
                <Col md="6">SM ROM: <input type="file" ref={fileInputSM} onChange={onFileSelect} /></Col>
                <Col md="6">ALTTP ROM: <input type="file" ref={fileInputZ3} onChange={onFileSelect} /></Col>
            </Row>
            <Row>
                <Col md="2">
                    <br />
                    <Button type="submit" color="primary" disabled={!canUpload}>Upload Files</Button>
                </Col>
            </Row>
        </Form>
    );
}
