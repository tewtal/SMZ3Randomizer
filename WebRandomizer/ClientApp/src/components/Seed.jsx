import React from 'react';
import { Row, Col, Card, CardHeader, CardBody, Button } from 'reactstrap';

import PlainList from '../ui/PlainList';

import classNames from 'classnames';

export default function Seed(props) {
    const { session, sessionStatus } = props;
    const { seed, clients } = session.data || {};

    const { onRegisterPlayer } = props;

    return (
        <Card>
            <CardHeader
                className={classNames({
                        'bg-success': session.state > 0,
                        'bg-danger': session.state === 0
                    }, 'text-white'
                )}>
                <PlainList>
                    <li>Session: {session.guid}</li>
                    <li>Game: {session.state > 0 ? seed.gameName : 'Loading...'}</li>
                    <li>Status: {sessionStatus}</li>
                </PlainList>
            </CardHeader>
            <CardBody>
                <Row>
                    {session.state > 0 && seed.worlds.map((world, i) => {
                        const client = clients.find(client => client.guid === world.guid);
                        return (
                            <Col key={`player-${i}`} md="3">
                                <h5>{world.player}</h5>
                                {client == null ?
                                    <Button color="primary" onClick={() => onRegisterPlayer(session.guid, world.guid)}>Register as this player</Button> :
                                    <Button disabled color={client.state < 5 ? 'secondary' : client.state === 5 ? 'warning' : 'success'}>
                                        {client.state < 5 ? 'Registered' : client.state === 5 ? 'Connected' : 'Ready'}
                                    </Button>
                                }
                            </Col>
                        );
                    })}
                </Row>
            </CardBody>
        </Card>
    );
}
