import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Hello, world!</h1>
        <p>This is Randomizer</p>
        <ul>
          <li><a href='/randomizer'>RANDOMIZER</a></li>
        </ul>
      </div>
    );
  }
}
