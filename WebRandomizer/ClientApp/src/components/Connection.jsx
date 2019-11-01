import React from 'react';
import { Card, CardBody, CardHeader, Button, Input } from 'reactstrap';
import classNames from 'classnames';

export function Connection(props) {
    const { clientData, connState, connInfo, deviceList, deviceSelect } = props;
    const { onDeviceSelect, onConnect } = props;

    return (
        <Card>
            <CardHeader
                className={classNames({
                        'bg-danger': connState === 0,
                        'bg-success': connState !== 0,
                    }, 'text-white'
                )}>
                Game Connection
            </CardHeader>
            <CardBody>
                Status: {['Disconnected', 'Connected'][connState]}<br />
                {!deviceSelect ?
                    <div>Device: {clientData.device}</div> :
                    <div>
                        Multiple USB2SNES Devices detected, please select one below:<br />
                        <Input type="select" onChange={(e) => onDeviceSelect(e.target.value)}>
                            {deviceList.Results.map((result, i) =>
                                <option key={`device-${i}`}>{result}</option>
                            )}
                        </Input>
                    </div>
                }
                Version: {connInfo[0]}
                {connState === 0 &&
                    <div><br /><Button color="primary" onClick={onConnect}>Connect</Button></div>
                }
            </CardBody>
        </Card>
    );
}
