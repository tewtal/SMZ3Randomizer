import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Randomizer } from './components/Randomizer';
import { Multiworld } from './components/Multiworld';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/randomizer' component={Randomizer} />
            <Route path='/multiworld/:session_id' component={Multiworld} />
      </Layout>
    );
  }
}
