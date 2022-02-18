import React, { useState } from 'react';
import { Card, CardBody, CardHeader, Button, Input } from 'reactstrap';

import PlainList from '../ui/PlainList';

import classNames from 'classnames';

export default function Connection(props) {
    const { device, deviceStatus } = props;
    const { onDeviceSelect, onConnect } = props;
    const [selectedDevice, setSelectedDevice] = useState(null);
    const deviceState = device ? device.state : 0;

    if (device && device.selecting && device.list && selectedDevice === null) {
        setSelectedDevice(device.list[0]);
    }

    return (
        <Card>
            <CardHeader
                className={classNames({
                    'bg-danger': deviceState === 0,
                    'bg-success': deviceState !== 0
                    }, 'text-white'
                )}>
                Game Connection
            </CardHeader>
            <CardBody>
                <PlainList>
                    <li>Status: {deviceStatus}</li>
                    {device && (!device.selecting
                        ? <><li>Device: {device.uri}</li><li>Name: {device.name}</li></>
                        : (<>
                            <li><br />Multiple devices detected, please select one below:</li>
                            <li>
                                <Input type="select" onChange={(e) => setSelectedDevice(device.list[e.target.selectedIndex])}>
                                    {device.list.map((d, i) =>
                                        <option key={`device-${i}`}>{d.name} - {d.uri}</option>
                                    )}
                                </Input>
                                <Button className="mt-3" color="primary" onClick={() => onDeviceSelect(selectedDevice)}>Select device</Button>
                            </li>
                        </>)
                    )}
                    
                </PlainList>
                {deviceState === 0 && (
                    <Button className="mt-3" color="primary" onClick={onConnect}>Connect</Button>
                )}
            </CardBody>
        </Card>
    );
}
