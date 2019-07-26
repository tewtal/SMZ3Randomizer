import React from 'react';
import { Input } from 'reactstrap';

export class PlayerSprite extends React.Component {
    static displayName = PlayerSprite.name;

    constructor(props) {
        super(props);
        this.state = {};
    }

    handleChange = (e) => {
        const { value } = e.target;
        this.setState({ value });
        this.props.onChange(value);
    }

    render() {
        // Todo: try using reactstrap to build something that can show the sprite preview
        return <Input type="select" value={this.state.value} onChange={this.handleChange}>
            {this.props.options.map(({ title }, i) => <option value={i} key={title}>{title}</option>)}
        </Input>;
    }
}
