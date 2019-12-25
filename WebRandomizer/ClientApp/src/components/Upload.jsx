import React, { useState, useRef } from 'react';
import { Form, Row, Col, Button } from 'reactstrap';
import { readAsArrayBuffer } from '../file/util';
import { mergeRoms } from '../file/rom';
import { h32 } from 'xxhashjs';

import localForage from 'localforage';

import some from 'lodash/some';
import map from 'lodash/map';
import compact from 'lodash/compact';
import hasIn from 'lodash/hasIn';

/* "SMZ3" as UTF8 in big-endian */
const HashSeed = 0x534D5A33;
const Z3Hash = 0x8AC8FD15; 
const SMHash = 0xCADB4883;

export default function Upload(props) {
    const [canUpload, setCanUpload] = useState(false);
    const fileInputSM = useRef(null);
    const fileInputZ3 = useRef(null);

    async function onSubmitRom() {
        const smFile = fileInputSM.current.files[0];
        const z3File = fileInputZ3.current.files[0];

        let fileDataSM = null;
        let fileDataZ3 = null;
        const mismatch = {};

        try {
            fileDataSM = new Uint8Array(await readAsArrayBuffer(smFile));
            mismatch.SM = h32(fileDataSM.buffer, HashSeed).toNumber() !== SMHash;
        } catch (error) {
            console.log("Could not read uploaded SM file data:", error);
            return;
        }

        try {
            fileDataZ3 = new Uint8Array(await readAsArrayBuffer(z3File));
            mismatch.ALTTP = h32(fileDataZ3.buffer, HashSeed).toNumber() !== Z3Hash;
        } catch (error) {
            console.log("Could not read uploaded ALTTP file data:", error);
            return;
        }

        if (some(mismatch)) {
            const games = compact(map(mismatch, (truth, name) => truth ? name : null));
            alert(`Incorrect ${games.join(', ')} rom file(s)`);
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
