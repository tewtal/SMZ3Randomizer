import React, { Component } from 'react';

export class Multiworld extends Component {
    static displayName = Multiworld.name;
    constructor(props) {
        super(props);
        this.state = { };
    }

    render() {
        return (
            <div>
                <h1>Play Multiworld!</h1>
            </div>
        );
    }
}
