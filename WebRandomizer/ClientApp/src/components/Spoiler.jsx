import React, { useState } from 'react';
import { Row, Col, Card, CardHeader, CardBody, Button } from 'reactstrap';

import isEmpty from 'lodash/isEmpty';
import map from 'lodash/map';

export default function Spoiler(props) {
    const [show, setShow] = useState(false);

    if (props.seedData === null || props.seedData.spoiler === "[]")
        return null;

    return (
        <Card>
            <CardHeader >
                <Row className="align-items-center justify-content-between">
                    <Col>Playthrough</Col>
                    <Col><Button outline className="float-right" color="secondary" onClick={() => setShow(!show)}>{show ? "Hide" : "Show"}</Button></Col>
                </Row>
            </CardHeader>
            {show && (<CardBody>
                {JSON.parse(props.seedData.spoiler).filter(sphere => !isEmpty(sphere)).map(sphere => (
                    <Card>
                        <CardBody>
                            <ul>
                                {map(sphere, (item, location) =>
                                    <li>{location} - {item}</li>
                                )}
                            </ul>
                        </CardBody>
                    </Card>
                ))}
            </CardBody>)}
        </Card>
    );
}
