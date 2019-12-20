import React, { Component } from 'react';
import { Container, Row, Col, Card, CardHeader, CardBody, Button, Form, Input, InputGroup, InputGroupAddon, InputGroupText, Modal, ModalHeader, ModalBody, Progress } from 'reactstrap';

export class Configure extends Component {
    static displayName = Configure.name;
    constructor(props) {
        super(props);
        this.state = { showGeneratingDialog: false, options: [], randomizer: null, error: false };
        this.randomizer_id = this.props.match.params.randomizer_id;
        this.options = [];
    }

    componentDidMount() {
        fetch(`/api/randomizers/${this.randomizer_id}`)
            .then(res => res.json())
            .then(
                (result) => {
                    if (result) {
                        this.options = result.options.reduce((obj, opt) => { return { ...obj, [opt.key]: opt.default !== null ? opt.default : "" }; }, {});
                        this.setState({ randomizer: result, options: this.options });
                    } else {
                        this.setState({
                            error: true,
                            errorMessage: `Could not load randomizer metadata for: ${this.randomizer_id}`
                        });
                    }
                },
                (error) => this.setState({ error: true, errorMessage: error })
            );
    }

    createGame = async (e) => {
        e.preventDefault();
        this.setState({ showGeneratingDialog: true });

        // temp fix
        this.options["mode"] = "multiworld";
        this.options["worlds"] = "1";
        this.options["player-0"] = "Player 1";

        let response = await fetch(`/api/randomizers/${this.randomizer_id}/generate`,
            {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ options: this.options })
            });
        let data = await response.json();
        console.log(data.spoiler);
        this.setState({ showGeneratingDialog: false });
    }

    updateOption = (e, key) => {
        this.options[key] = e.target.value;
        this.setState({ options: this.options });
    }

    chunk(array, size) {
        return array.reduce((chunks, item, i) => {
            if (i % size === 0) {
                chunks.push([item]);
            } else {
                chunks[chunks.length - 1].push(item);
            }
            return chunks;
        }, []);
    }

    render() {
        if (this.state.randomizer) {
            const options = this.state.randomizer.options.map(opt => {
                let inputElement = null;
                switch (opt.type) {
                    case 'input':
                        inputElement = (
                            <Col key={opt.key} md="4">
                                <InputGroup>
                                    <InputGroupAddon addonType="prepend">
                                        <InputGroupText>{opt.description}</InputGroupText>
                                    </InputGroupAddon>
                                    <Input id={opt.key} defaultValue={this.state.options[opt.key]} onChange={(e) => this.updateOption(e, opt.key)} />
                                </InputGroup>
                            </Col>
                        );
                        break;
                    case 'dropdown':
                        inputElement = (
                            <Col key={opt.key} md="4">
                                <InputGroup>
                                    <InputGroupAddon addonType="prepend">
                                        <InputGroupText>{opt.description}</InputGroupText>
                                    </InputGroupAddon>
                                    <Input type="select" id={opt.key} defaultValue={this.state.options[opt.key]} onChange={(e) => this.updateOption(e, opt.key)}>
                                        {Object.entries(opt.values).map(([k,v]) => <option key={k} value={k}>{v}</option>)}
                                    </Input>
                                </InputGroup>
                            </Col>
                        );
                        break;
                }

                return inputElement;
            });

            const optionGroups = this.chunk(options, 3);

            return (
                <Container>
                    <Row className="justify-content-md-center">
                        <Col md="10">
                            <Card>
                                <CardHeader className="bg-primary text-white">
                                    {this.state.randomizer.name} - {this.state.randomizer.version}
                                </CardHeader>
                                <CardBody>
                                    <Form onSubmit={this.createGame}>
                                        {optionGroups.map((options, i) => (
                                            <Row key={i} className="form-group">
                                                {options}
                                            </Row>
                                        ))}
                                        <Row className="form-group">
                                            <Col md="6">
                                                <Button color="success" type="submit">Generate Game</Button>
                                            </Col>
                                        </Row>
                                    </Form>
                                </CardBody>
                            </Card>
                        </Col>
                    </Row>

                    <Modal isOpen={this.state.showGeneratingDialog} backdrop="static" autoFocus>
                        <ModalHeader>Generating new game</ModalHeader>
                        <ModalBody>
                            <p>Please wait while a new game is generated</p>
                            <Progress animated color="info" value={100} />
                        </ModalBody>
                    </Modal>

                </Container>
            );
        } else {
            return (
                <Container>
                    <Row className="justify-content-md-center">
                        <Col md="10">
                            <Card>
                                <CardHeader className={this.state.error === false ? "bg-primary text-white" : "bg-danger text-white"}>
                                    {this.state.error === false ? <div>Create new randomized game</div> : <div>Something went wrong :(</div>}
                                </CardHeader>
                                <CardBody>
                                    {this.state.error === false ? <p>Please wait, loading...</p> : <p>{this.state.errorMessage}</p>}
                                </CardBody>
                            </Card>
                        </Col>
                    </Row>
                </Container>
            );
        }
    }
}
