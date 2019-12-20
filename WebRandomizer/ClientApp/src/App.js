import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Markdown } from './components/Markdown';
import { Randomizer } from './components/Randomizer';
import { Multiworld } from './components/Multiworld';
import { Configure } from './components/Configure';

import instructionsMd from './files/markdown/instructions.md';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/randomizer' component={Randomizer} />
            <Route path='/configure/:randomizer_id' component={Configure} />
            <Route path='/instructions' render={(props) => <Markdown {...props} mdLink={instructionsMd} />} />
            <Route path='/multiworld/:session_id' component={Multiworld} />
      </Layout>
    );
  }
}
