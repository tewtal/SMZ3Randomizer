/* eslint-disable no-mixed-operators, jsx-a11y/anchor-is-valid */
import React, { useRef, useState, useEffect } from 'react';
import { Row, Col, Card, CardBody, CardTitle, CardHeader } from 'reactstrap';
import { LogMessageRow } from './styled';

import { Ok, Delay, Msg, Issue, Reload } from './styled';

import sortBy from 'lodash/sortBy';

export default function Game(props) {
    const logEnd = useRef(null);
    const [eventCount, setEventCount] = useState(0);
    const { events, clientData } = props;
    const { onChatMessage, onResendEvent } = props;

    useEffect(() => {
        if (events.length !== eventCount) {
            logEnd.current?.scrollIntoView();
        }
        setEventCount(events.length);
    }, [events, eventCount])

    function renderMessage(event) {
        const Icon = event.event_type <= 1
            ? event.confirmed ? Ok : Delay
            : event.event_type === 2 ? Msg : Issue;

        const message = event.event_type === 0
            ? (event.from_world_id === clientData.world_id
                ? <>{event.item_location === -1 ? <b>Re-sent</b> : <span>Sent</span>} <span className="text-primary">{event.item_name}</span> to <span className="text-danger">{event.to_player}</span></>
                : <>{event.item_location === -1 ? <b>Re-received</b> : <span>Received</span>} <span className="text-primary">{event.item_name}</span> from <span className="text-danger">{event.from_player}</span></>)
            : (event.event_type === 2
                ? <>[<b>{event.from_player}</b>] {event.message}</>
                : <span className="text-danger">{event.message}</span>);

        return <>
            <Icon className="mr-1" />
            <div className="text-right mr-1"><i>[{event.time_stamp.substring(11)}]</i></div>
            <div className="mr-auto">{message}</div>
            {event.event_type === 0 && event.from_world_id === clientData.world_id && (
                <div><a onClick={() => onResendEvent(event)} role="button"><Reload /></a></div>
            )}
        </>;
    }

    function onChatKeyUp(e) {
        if (e.key === "Enter") {
            const message = e.target.value;
            e.target.value = "";
            onChatMessage(message);
        }
    }

    return (
        <div>
            <Card>
                <CardHeader className="bg-success text-white">
                    Game information
                </CardHeader>
                <CardBody>
                    <CardTitle tag="h5">{props.gameStatus}</CardTitle>
                    <Row>
                        <Col>
                            <h6>Game log</h6>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <div className="overflow-auto" style={{ "height": "25vh" }}>
                                {sortBy(events, 'id').map(event => (
                                    <LogMessageRow key={event.id}>
                                        {renderMessage(event)}
                                    </LogMessageRow>
                                ))}
                                <div ref={logEnd} />
                            </div>
                            <input type="text" className="w-100" onKeyUp={onChatKeyUp} />
                        </Col>
                    </Row>
                </CardBody>
            </Card>
        </div>
    );
}
