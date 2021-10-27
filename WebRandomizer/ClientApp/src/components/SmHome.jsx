import React from 'react';
import { Jumbotron, Container, Row, Col, Button } from 'reactstrap';

export default function SmHome() {
    return (
        <>
            <Jumbotron>
                <Container>
                    <h1 className="display-4">Super Metroid Item Randomizer</h1>
                    <br />
                    <p>This is a relatively simple Super Metroid randomizer that's based on the previous <a href="https://itemrando.supermetroid.run/">itemrando.supermetroid.run</a> randomizer.</p>
                    <p>The main purpose of it is to enable multiworld randomizer co-op for Super Metroid, but it's of course also great for regular single player games.</p>
                    <br />
                    <p>If you're looking for a more advanced and configurable Super Metroid randomizer for single player seeds, take a look at the <a href="http://varia.run/">VARIA Randomizer</a>.</p>
                </Container>
            </Jumbotron>
            <Container>
                <Row>
                    <Col md="4">
                        <h2>Get started</h2>
                        <p>Follow the link below to get to the game generation page and head directly into the action.</p>
                        <span className="align-bottom"><a href="/configure"><Button color="primary">Generate game</Button></a></span>
                    </Col>
                    <Col md="4">
                        <h2>Get help</h2>
                        <p>If this is your first time playing or you're looking for more information about the randomizer go here:</p>
                        <span className="align-bottom"><a href="/information"><Button color="primary">Information</Button></a></span>
                    </Col>
                    <Col md="4">
                        <h2>Get involved</h2>
                        <p>If you're looking to get involved with the randomizer community, take a look at the resources for more information.</p>
                        <span className="align-bottom"><a href="/resources"><Button color="primary">Resources</Button></a></span>
                    </Col>
                </Row>
                <Row style={{ marginTop: "30px" }}>
                    <Col md={{ size: 4, offset: 8 }}>
                        <h2>Donate</h2>
                        <p>Donations to help out with costs for keeping the randomizer running for everyone is greatly appreciated.</p>
                        <span className="align-bottom">
                            <form action="https://www.paypal.com/cgi-bin/webscr" method="post" target="_top">
                                <input type="hidden" name="cmd" value="_s-xclick" />
                                <input type="hidden" name="hosted_button_id" value="TD6E7WSKHXFA2" />
                                <input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif" border="0" name="submit" title="PayPal - The safer, easier way to pay online!" alt="Donate with PayPal button" />
                                <img alt="" border="0" src="https://www.paypal.com/en_US/i/scr/pixel.gif" width="1" height="1" />
                            </form>
                        </span>
                    </Col>
                </Row>
            </Container>
        </>
    );
}
