import React, { Component } from 'react';
import { Card, CardBody, CardHeader, Button, Input } from 'reactstrap';

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

    handleDeviceSelect(e) {
        this.props.onDeviceSelect(e);
    }

    renderDeviceSelect() {
        let renderDevices = [];
        for (let i = 0; i < this.props.deviceList.Results.length; i++) {
            renderDevices.push(<option key={"device-" + i}>{this.props.deviceList.Results[i]}</option>);
        }

        return (
            <div>
                Multiple USB2SNES Devices detected, please select one below:<br />
                <Input type="select" id="device" onChange={(e) => this.handleDeviceSelect(e)}>
                    {renderDevices}
                </Input>
            </div>
        );
    }

    render() {       
        return (
            <Card>
                <CardHeader className={this.getStatus()}>Game Connection</CardHeader>
                <CardBody>
                    Status: {this.getStateName()}
                    <br />
                    {this.props.deviceSelect === false ? (<div>Device: {this.props.clientData.device}</div>) : this.renderDeviceSelect()}
                    Version: {this.props.connInfo[0]}                    
                    {this.props.connState === 0 ? (<div><br /><Button color="primary" onClick={this.handleConnect}>Connect</Button></div>) : ""}
                </CardBody>
            </Card>
        );
    }
}
