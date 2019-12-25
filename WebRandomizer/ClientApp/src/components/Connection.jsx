import React from 'react';
import { Card, CardBody, CardHeader, Button, Input } from 'reactstrap';
import classNames from 'classnames';

export default function Connection(props) {
    const { clientData, device } = props;
    const { onDeviceSelect, onConnect } = props;

    return (
        <Card>
            <CardHeader
                className={classNames({
                        'bg-danger': device.state === 0,
                        'bg-success': device.state !== 0,
                    }, 'text-white'
                )}>
                Game Connection
            </CardHeader>
            <CardBody>
                Status: {['Disconnected', 'Connected'][device.state]}<br />
                {!device.selecting ?
                    <div>Device: {clientData.device}</div> :
                    <div>
                        Multiple USB2SNES Devices detected, please select one below:<br />
                        <Input type="select" onChange={(e) => onDeviceSelect(e.target.value)}>
                            {device.list.Results.map((result, i) =>
                                <option key={`device-${i}`}>{result}</option>
                            )}
                        </Input>
                    </div>
                }
                Version: {device.version}
                {device.state === 0 &&
                    <div><br /><Button color="primary" onClick={onConnect}>Connect</Button></div>
                }
            </CardBody>
        </Card>
    );
}
