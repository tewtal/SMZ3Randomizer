import React, { Suspense, lazy } from 'react';
import { Route, Switch } from 'react-router';
import { Container } from 'reactstrap';

import GlobalStyle from './site/GlobalStyle';
import Smz3Home from './site/Smz3Home';
import SmHome from './site/SmHome';
import { NavMenu, NavMenuItem, NavMenuDropdown } from './ui/NavMenu';

import MultiworldInstructionsMd from './resources/markdown/mwinstructions.md';
import ResourcesMd from './resources/markdown/resources.md';
import Smz3InformationMd from './resources/markdown/smz3information.md';
import SmInformationMd from './resources/markdown/sminformation.md';
import Smz3ChangelogMd from './resources/markdown/smz3changelog.md';
import SmChangelogMd from './resources/markdown/smchangelog.md';

import { GameTraitsCtx, resolveGameTraits } from './game/traits';
import { resolveGameId } from './site/domain';

const Markdown = lazy(() => import('./ui/Markdown'));
const LogicLog = lazy(() => import('./logiclog/LogicLog'));
const Configure = lazy(() => import('./configure'));
const Multiworld = lazy(() => import('./components/Multiworld'));
const Permalink = lazy(() => import('./components/Permalink'));

export default function App() {
    const gameId = resolveGameId(document.location.href);
    const traits = resolveGameTraits(gameId);

    return (
        <GameTraitsCtx.Provider value={traits}>
            <GlobalStyle />
            <NavMenu
                brand={<NavMenuItem to="/">Home</NavMenuItem>}
                nav={<NavMenuItem to="/configure">Generate randomized game</NavMenuItem>}
                dropdown={
                    <NavMenuDropdown title="Help">
                        <NavMenuItem to="/information">Information</NavMenuItem>
                        <NavMenuItem to="/mwinstructions">Multiworld instructions</NavMenuItem>
                        {gameId === 'smz3' && <NavMenuItem to="/logic">Logic Log</NavMenuItem>}
                        <NavMenuItem to="/resources">Resources</NavMenuItem>
                        <NavMenuItem to="/changelog">Changes</NavMenuItem>
                    </NavMenuDropdown>
                }
            />
            <Container className="mb-5">
                <Suspense fallback={<div></div>}>
                    <Switch>
                        <Route exact path="/" component={gameId === 'sm' ? SmHome : Smz3Home} />
                        <Route path="/mwinstructions"
                            render={props => <Markdown {...props} source={MultiworldInstructionsMd} />}
                        />
                        <Route path="/resources"
                            render={props => <Markdown {...props} source={ResourcesMd} />}
                        />
                        <Route path="/information"
                            render={props => <Markdown {...props} source={gameId === 'sm' ? SmInformationMd : Smz3InformationMd} />}
                        />
                        <Route path="/changelog"
                            render={props => <Markdown {...props} source={gameId === 'sm' ? SmChangelogMd : Smz3ChangelogMd} />}
                        />
                        <Route path="/logic" component={LogicLog} />
                        <Route exact path="/configure" component={Configure} />
                        {/* Todo: Remove this game specific configure path in v12 */}
                        <Route path="/configure/:randomizerId" component={Configure} />
                        <Route path="/multiworld/:sessionSlug" component={Multiworld} />
                        <Route path="/seed/:seedSlug" component={Permalink} />
                    </Switch>
                </Suspense>
            </Container>
        </GameTraitsCtx.Provider>
    );
}
