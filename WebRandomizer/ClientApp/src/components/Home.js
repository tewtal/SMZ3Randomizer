import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Randomizer</h1>
        <p>This is a Randomizer, it randomizes things.</p>
        <ul>
          <li><a href='/randomizer'>Create new randomized game</a></li>
        </ul>
      </div>
    );
  }
}
