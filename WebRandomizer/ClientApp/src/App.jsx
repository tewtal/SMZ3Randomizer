import React, { Suspense, lazy } from 'react';
import { Route, Switch } from 'react-router';
import Layout from './components/Layout';
import Smz3Home from './components/Smz3Home';
import SmHome from './components/SmHome';

import MultiworldInstructionsMd from './resources/markdown/mwinstructions.md';
import Smz3InformationMd from './resources/markdown/smz3information.md';
import SmInformationMd from './resources/markdown/sminformation.md';
import ResourcesMd from './resources/markdown/resources.md';
import Smz3ChangelogMd from './resources/markdown/smz3changelog.md';
import SmChangelogMd from './resources/markdown/smchangelog.md';

import { GameTraitsCtx, resolveGameTraits } from './game/traits';
import { resolveGameId } from './site';

const LogicLog = lazy(() => import('./logiclog/LogicLog'));
const Markdown = lazy(() => import('./components/Markdown'));
const Configure = lazy(() => import('./components/Configure'));
const Multiworld = lazy(() => import('./components/Multiworld'));
const Permalink = lazy(() => import('./components/Permalink'));

export default function App() {
    const gameId = resolveGameId(document.location.href);
    const traits = resolveGameTraits(gameId);

    return (
        <GameTraitsCtx.Provider value={traits}>
            <Layout>
                <Suspense fallback={<div></div>}>
                    <Switch>
                        <Route exact path="/" component={gameId === 'sm' ? SmHome : Smz3Home} />
                        <Route path="/logic" component={LogicLog} />
                        <Route path="/mwinstructions" render={(props) => <Markdown {...props} source={MultiworldInstructionsMd} />} />
                        <Route path="/information" render={(props) => <Markdown {...props} source={gameId === 'sm' ? SmInformationMd : Smz3InformationMd} />} />
                        <Route path="/resources" render={(props) => <Markdown {...props} source={ResourcesMd} />} />
                        <Route path="/changelog" render={(props) => <Markdown {...props} source={gameId === 'sm' ? SmChangelogMd : Smz3ChangelogMd} />} />
                        <Route exact path="/configure" component={Configure} />
                        {/* Todo: Remove this game specific configure path in v12 */}
                        <Route path="/configure/:randomizer_id" component={Configure} />
                        <Route path="/multiworld/:session_id" component={Multiworld} />
                        <Route path="/seed/:seed_id" component={Permalink} />
                    </Switch>
                </Suspense>
            </Layout>
        </GameTraitsCtx.Provider>
    );
}
