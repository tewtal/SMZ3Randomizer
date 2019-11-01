import React, { Component } from 'react';
import { Card, CardHeader, CardBody, Button } from 'reactstrap'

export class Spoiler extends Component {
    static displayName = Spoiler.name;

    constructor(props) {
        super(props);
        this.state = { showSpoiler: false }
        this.handleShowSpoiler = this.handleShowSpoiler.bind(this);
    }

    handleShowSpoiler(e) {
        this.setState({
            showSpoiler: !this.state.showSpoiler
        });
    }

    render() {
        if (this.props.sessionData === null) {
            return "";
        }

        let spoiler = JSON.parse(this.props.sessionData.seed.spoiler);

        const playthrough = [];
        for (let i = 0; i < spoiler.length; i++) {
            let sphere = spoiler[i];
            for (let location in sphere)
            {
                playthrough.push(<li>{location} - {sphere[location]}</li>);
            }
        }

        if (this.state.showSpoiler === true) {
            return (
                <div>
                    <Button onClick={this.handleShowSpoiler} color="primary">Hide spoiler</Button>
                    <Card>
                        <CardHeader>
                            Seed Playthrough
                    </CardHeader>
                        <CardBody>
                            <ul>
                                {playthrough}
                            </ul>
                        </CardBody>
                    </Card>
                </div>
            );
        } else {
            return (
                <Button onClick={this.handleShowSpoiler}>Show spoiler</Button>
            );
        }
    }
}
