import React, { Component } from 'react';
import { Card, CardBody, CardHeader, Button } from 'reactstrap';

export class Connection extends Component {
    static displayName = Connection.name;

    constructor(props) {
        super(props);
        this.handleConnect = this.handleConnect.bind(this);
    }

    handleConnect(e) {
        this.props.onConnectClick(e);
    }

    getStateName() {
        return ["Disconnected", "Connected"][this.props.connState];
    }

    getStatus() {
        if (this.props.connState === 0) {
            return "bg-danger text-white";
        } else {
            return "bg-success text-white";
        }
    }

    render() {
        return (
            <Card>
                <CardHeader className={this.getStatus()}>Game Connection</CardHeader>
                <CardBody>
                    Status: {this.getStateName()}
                    <br />
                    Device: {this.props.clientData.device}
                    <br />
                    Version: {this.props.connInfo[0]}                    
                    {this.props.connState === 0 ? (<div><br /><Button color="primary" onClick={this.handleConnect}>Connect</Button></div>) : ""}
                </CardBody>
            </Card>
        );
    }
}
