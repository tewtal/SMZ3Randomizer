import React, { useState } from 'react';
import { Card, CardHeader, CardBody, Button } from 'reactstrap';

import flatMap from 'lodash/flatMap';
import map from 'lodash/map';

export default function Spoiler(props) {
    const [show, setShow] = useState(false);

    if (props.sessionData == null)
        return null;
    else if (!show)
        return <Button onClick={() => setShow(true)}>Show spoiler</Button>;

    return (
        <div>
            <Button color="primary" onClick={() => setShow(false)}>Hide spoiler</Button>
            <Card>
                <CardHeader>Seed Playthrough</CardHeader>
                <CardBody>
                    <ul>
                        {flatMap(JSON.parse(props.sessionData.seed.spoiler), sphere =>
                            map(sphere, (item, location) =>
                                <li>{location} - {item}</li>
                            )
                        )}
                    </ul>
                </CardBody>
            </Card>
        </div>
    );
}
