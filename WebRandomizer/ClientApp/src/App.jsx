import React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Randomizer } from './components/Randomizer';
import { Multiworld } from './components/Multiworld';

import { Markdown } from './components/Markdown';
import instructionsMd from './files/markdown/instructions.md';

export default function App() {
    return (
        <Layout>
            <Route exact path="/" component={Home} />
            <Route path="/randomizer" component={Randomizer} />
            <Route path="/instructions" render={(props) => <Markdown {...props} mdLink={instructionsMd} />} />
            <Route path="/multiworld/:session_id" component={Multiworld} />
        </Layout>
    );
}
