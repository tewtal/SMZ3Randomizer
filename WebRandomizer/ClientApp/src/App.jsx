import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import LogicLog from './logiclog/LogicLog';
import Markdown from './components/Markdown';
import Configure from './components/Configure';
import Multiworld from './components/Multiworld';
import Permalink from './components/Permalink';
import instructionsMd from './resources/markdown/instructions.md';

export default function App() {
    return (
        <Layout>
            <Route exact path="/" component={Home} />
            <Route path="/logic" component={LogicLog} />
            <Route path="/instructions" render={(props) => <Markdown {...props} source={instructionsMd} />} />
            <Route path="/configure/:randomizer_id" component={Configure} />
            <Route path="/multiworld/:session_id" component={Multiworld} />
            <Route path="/seed/:seed_id" component={Permalink} />
        </Layout>
    );
}
