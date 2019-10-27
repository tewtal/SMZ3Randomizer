import React, { Component } from 'react';
const ReactMarkdown = require('react-markdown');

export class Markdown extends Component {
  static displayName = Markdown.name;
  constructor(props) {
      super(props);
      this.state = {helpText: ""};
  }

  async componentDidMount() 
  {
    try {
        let response = await fetch(this.props.mdLink);
        let text = await response.text();
        this.setState({helpText: text});
    } catch (err) {
        this.setState({helpText: "Could not load help text: " + err});
    }
  }

  render () {
    return (
      <div>
          <ReactMarkdown source={this.state.helpText} />
      </div>
    );
  }
}
