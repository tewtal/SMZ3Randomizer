/* eslint-disable no-mixed-operators */
import React from 'react';
import { Container, Row, Col, Card, CardBody, CardTitle, CardText } from 'reactstrap';

export default function Game(props) {
    return (
        <Container>
            <Row>
                <Col sm="4">
                    <Card>
                        <CardBody>
                            <CardTitle tag="h5">Game information</CardTitle>
                            <CardText>Status: {props.gameStatus}</CardText>
                        </CardBody>
                    </Card>
                </Col>
            </Row>
        </Container>
    );
}
