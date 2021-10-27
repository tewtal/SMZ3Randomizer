import React from 'react';
import { Card, CardBody, CardHeader, Button, Input } from 'reactstrap';

import PlainList from '../ui/PlainList';

import classNames from 'classnames';

export default function Connection(props) {
    const { clientData, device } = props;
    const { onDeviceSelect, onConnect } = props;

    return (
        <Card>
            <CardHeader
                className={classNames({
                        'bg-danger': device.state === 0,
                        'bg-success': device.state !== 0
                    }, 'text-white'
                )}>
                Game Connection
            </CardHeader>
            <CardBody>
                <PlainList>
                    <li>Status: {['Disconnected', 'Connected'][device.state]}</li>
                    {!device.selecting
                        ? <li>Device: {clientData.device}</li>
                        : (<>
                            <li>Multiple USB2SNES Devices detected, please select one below:</li>
                            <li>
                                <Input type="select" onChange={(e) => onDeviceSelect(e.target.value)}>
                                    {device.list.Results.map((result, i) =>
                                        <option key={`device-${i}`}>{result}</option>
                                    )}
                                </Input>
                            </li>
                        </>)
                    }
                    <li>Version: {device.version}</li>
                </PlainList>
                {device.state === 0 && (
                    <Button className="mt-3" color="primary" onClick={onConnect}>Connect</Button>
                )}
            </CardBody>
        </Card>
    );
}
