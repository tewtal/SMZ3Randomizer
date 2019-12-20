import React, { Component } from 'react';
const ReactMarkdown = require('react-markdown');
const homeMarkdown =
`
### Super Metroid and A Link to the Past Randomizer

To generate a Combo Rando seed, go to the [Configure SMZ3 Game](/configure/smz3) page.

To generate a Super Metroid seed, go to the [Configure SM Game](/configure/sm) page.

*Please note that this is still in beta and being under constant developement, so things might not work or the site might have some downtime.*

`;

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <ReactMarkdown source={homeMarkdown} />
    );
  }
}
