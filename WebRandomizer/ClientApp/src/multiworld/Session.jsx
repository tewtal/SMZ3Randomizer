import React from 'react';
import { Row, Col, Card, CardHeader, CardBody, Button } from 'reactstrap';
import PlainList from '../ui/PlainList';
import classNames from 'classnames';

const clientStates = {
    0: { name: "Unregistered", color: "secondary" },
    1: { name: "Registering", color: "secondary" },
    2: { name: "Registered", color: "secondary" },
    3: { name: "Identifying", color: "secondary" },
    4: { name: "Patching", color: "secondary" },
    5: { name: "Ready", color: "info" },
    6: { name: "Playing", color: "info" },
    7: { name: "Forfeit", color: "dark" }
};

export default function Session(props) {
    const { status, session, sessionStatus, clientData } = props;
    const { seed } = session || {};

    const { onRegisterPlayer, onUnregisterPlayer, onRemove, onForfeit } = props;

    return (
        <Card>
            <CardHeader
                className={classNames({
                    'bg-success': status > 0,
                    'bg-danger': status === 0
                    }, 'text-white'
                )}>
                <PlainList>
                    <li>Session: {status > 0 ? session.guid: 'Loading...'}</li>
                    <li>Game: {status > 0 ? `${seed.game_name} v${seed.game_version}` : 'Loading...'}</li>
                    <li>Hash: {status > 0 ? seed.hash : 'Loading...'}</li>
                    <li>Status: {sessionStatus}</li>
                </PlainList>
            </CardHeader>
            <CardBody>
                <Row>
                    {status > 0 && seed.worlds.map((world, i) => {
                        return (
                            <Col key={`player-${i}`} md="3">
                                <h5>{world.player_name}</h5>
                                {world.client_state === 0 ? (
                                    (clientData === null && <Button size="md" color="primary" onClick={() => onRegisterPlayer(session.guid, world.world_id)}>Register</Button>) ||
                                    (clientData !== null && <Button size="md" disabled color="secondary">Unregistered</Button>)
                                ) : 
                                    (clientData === null && <Button size="md" disabled color={clientStates[world.client_state].color}>{clientStates[world.client_state].name}</Button>) ||
                                    (clientData !== null && world.world_id !== clientData.world_id && <div><Button className="mr-2" size="md" disabled color={clientStates[world.client_state].color}>{clientStates[world.client_state].name}</Button>{world.client_state <= 5 && <Button color="danger" onClick={() => onRemove(world.world_id)}>Remove</Button>}</div>) ||
                                    (clientData !== null && world.world_id === clientData.world_id && <div><Button className="mr-2" color="warning" onClick={onUnregisterPlayer}>Unregister</Button><Button color="danger" onClick={onForfeit}>Forfeit</Button></div>)
                                }
                            </Col>
                        );
                    })}
                </Row>
            </CardBody>
        </Card>
    );
}
