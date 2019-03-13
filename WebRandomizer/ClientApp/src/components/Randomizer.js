import React, { Component } from 'react';

export class Randomizer extends Component {
    static displayName = Randomizer.name;
    constructor(props) {
        super(props);
        this.state = { seed: [] };
        this.generateSeed = this.generateSeed.bind(this);
    }

    async generateSeed() {
        try {
            let response = await fetch("/api/randomizer/generate",
                {
                    method: "POST",
                    cache: "no-cache",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({ options: { "mode": "standard", "difficulty": "tournament" } })
                });
            let data = await response.json();
            console.log("Success:", data);
            this.setState({
                seed: data.seed
            });
        } catch (err) {
            console.log("Error:", err);
        }
    }

    render() {
        return (
            <div>
                <h1>Randomize!</h1>
                <input type="button" className="button button-primary" onClick={this.generateSeed} value="Generate Seed" />
                <div>{this.state.seed}</div>
            </div>
        );
    }
}
