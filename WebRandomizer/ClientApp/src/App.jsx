import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Multiworld from './components/Multiworld';
import Markdown from './components/Markdown';
import Home from './components/Home';
import Configure from './components/Configure';
import Permalink from './components/Permalink';
import instructionsMd from './resources/markdown/instructions.md';

export default function App() {
    return (
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/configure/:randomizer_id' component={Configure} />
            <Route path='/instructions' render={(props) => <Markdown {...props} source={instructionsMd} />} />
            <Route path='/multiworld/:session_id' component={Multiworld} />
            <Route path='/seed/:seed_id' component={Permalink} />
        </Layout>
    );
}
