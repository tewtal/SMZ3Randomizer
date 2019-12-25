import React, { useState } from 'react';
import { Card, CardHeader, CardBody, Button } from 'reactstrap';

import isEmpty from 'lodash/isEmpty';
import map from 'lodash/map';

export default function Spoiler(props) {
    const [show, setShow] = useState(false);

    if (props.sessionData === null)
        return null;
    else if (!show)
        return <Button onClick={() => setShow(true)}>Show spoiler</Button>;

    return (
        <div>
            <Button color="primary" onClick={() => setShow(false)}>Hide spoiler</Button>
            <Card>
                <CardHeader>Seed Playthrough</CardHeader>
                <CardBody>
                    {JSON.parse(props.sessionData.seed.spoiler).filter(sphere => !isEmpty(sphere)).map(sphere => (
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
                </CardBody>
            </Card>
        </div>
    );
}
