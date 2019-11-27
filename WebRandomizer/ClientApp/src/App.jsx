import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Randomizer from './components/Randomizer';
import Multiworld from './components/Multiworld';

import Markdown from './components/Markdown';
import homeMd from './resources/markdown/home.md';
import instructionsMd from './resources/markdown/instructions.md';

export default function App() {
    return (
        <Layout>
            <Route exact path="/" render={(props) => <Markdown {...props} source={homeMd} />} />
            <Route path="/randomizer" component={Randomizer} />
            <Route path="/instructions" render={(props) => <Markdown {...props} source={instructionsMd} />} />
            <Route path="/multiworld/:session_id" component={Multiworld} />
        </Layout>
    );
}
