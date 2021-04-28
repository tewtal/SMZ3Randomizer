import React, { useState } from 'react';
import styled from 'styled-components';
import { Row, Col, Card, CardHeader, CardBody, Collapse } from 'reactstrap';
import { InputGroupAddon, Label, Button, Nav, NavItem, NavLink } from 'reactstrap';
import InputGroup from '../components/util/PrefixInputGroup';
import Markdown from '../components/Markdown';

import { PlusSquareFill, DashSquareFill } from '../components/util/BootstrapIcon';

import classnames from 'classnames';

import capitalize from 'lodash/capitalize';
import find from 'lodash/find';
import initial from 'lodash/initial';
import tail from 'lodash/tail';
import last from 'lodash/last';

import content from './content';

/* Implement "layered" card headers by increase the amount of black "shade" */
const StyledCardHeader = styled(CardHeader)`
  background-color: rgba(0,0,0,${({ level }) => .03 * level});
`;

/* Use a custom "bullet point" when displaying hierarchical logic */
const StyledMarkdown = styled(Markdown)`
  ul {
    list-style: none;
    padding: 0;

    ul {
      ul { padding-left: 1em; }

      li:before {
        content: "";
        display: inline-block;
        height: 1em;
        width: 1em;
        background-image: url(${process.env.PUBLIC_URL}/ui/logic_list_item.svg);
      }
    }
  }
`;

/* Skipped active, active+focus. color-yiq is bootstrap's contrast picker
 * between dark ($gray-900), and light ($white) */
const ToggleButton = styled(Button)`
  &.btn-light {
    color: #212529;               /* color: color-yiq(background) => $gray-900 */
    background-color: #E9ECEF;    /* background: $input-group-addon-bg */
    border-color: #CED4DA;        /* border: $input-group-addon-border-color */
    &:hover {
      color: #212529;             /* hover-color: color-yip(hover-background) => $gray-900 */
      background-color: #D2D8DE;  /* hover-background: darken(background, 7.5%) */
      border-color: #B1BBC4;      /* hover-border: darken(border, 10%) */
    }
    &:focus, &.focus {
      color: #212529;             /* hover-color */
      background-color: #D2D8DE;  /* hover-background */
      border-color: #B1BBC4;      /* hover-border */
      box-shadow: 0 0 0 .2rem /* $btn-focus-width */ rgba(180, 186, 191, .5); /* mix(color, border, 15%), .5 */
    }
  }
`;

const IntroLabel = styled(Label)`
  display: flex;
  align-items: center;
  & > .icon {
    width: 1.25em;
    height: 1.25em;
  }
`;

const introText =
`
The worlds are filled by this procedure:
- Assume all progression items are acquired
- Progression items (non-dungeon) and item locations are shuffled respectively
- Items are placed one by one. Dungeon items are placed first, followed by all other progression
  - An item is placed at the first location where the item can first be placed there and then the
    player can reach it with their remaining progression according to the logic
  - An item can only be placed cross world if the owning player can reach the same location in
    their world with their current progression *including* the item to be placed.

Some bias is applied based on game mode. For multiworld Morph Balls are placed within the last 20%
of the pool, and Moon Pearls within the last 40% (which makes them show up earlier).
For singleworld first sphere locations in Link to the Past are weighted down significantly, and
Green, and Pink Brinstar are weighted down slightly.
`;

export default function LogicLog() {
    const [showIntro, setShowIntro] = useState(false);
    const [SMLogic, setSMLogic] = useState('normal');
    const [Z3Logic, setZ3Logic] = useState('normal');
    const [tabState, setTabState] = useState({});

    const parts = active(tabState, content);
    const bars = initial(parts);
    const { normal, nmg, hard } = last(parts);

    const Icon = showIntro ? DashSquareFill : PlusSquareFill;

    const introduction = (
        <>
            <IntroLabel onClick={() => setShowIntro(!showIntro)}>
                <Icon className="icon text-primary mr-1" />
                <strong>Introduction</strong>
            </IntroLabel>
            <Collapse isOpen={showIntro}>
                <Markdown text={introText} />
            </Collapse>
        </>
    );

    const SMlogicButton = (logic, name) => (
        <InputGroupAddon addonType="append">
            <ToggleButton
                color={logic === name ? 'primary' : 'light'}
                onClick={() => setSMLogic(name)}
            >
                {capitalize(name)}
            </ToggleButton>
        </InputGroupAddon>
    );
    const SMtoggle = (
        <InputGroup prefix="SM Logic" className="mb-3">
            {SMlogicButton(SMLogic, 'normal')}
            {SMlogicButton(SMLogic, 'hard')}
        </InputGroup>
    );

    const Z3logicButton = (logic, name) => (
        <InputGroupAddon addonType="append">
            <ToggleButton
                color={logic === name ? 'primary' : 'light'}
                onClick={() => setZ3Logic(name)}
            >
                {capitalize(name)}
            </ToggleButton>
        </InputGroupAddon>
    );
    const Z3toggle = (
        <InputGroup prefix="Z3 Logic" className="mb-3">
            {Z3logicButton(Z3Logic, 'normal')}
            {Z3logicButton(Z3Logic, 'nmg')}
        </InputGroup>
    );

    const log = (
        <Card>
            {bars
                .map((bar, i) => [bar, bars.slice(1, i + 1).map(x => x.name)])
                .map(([bar, path], i) => (
                    <StyledCardHeader key={i} level={bars.length - i}>
                        <Nav card tabs>
                            {bar.tabs.map(({ name }) => (
                                <NavItem key={name}>
                                    <NavLink
                                        className={classnames({ active: name === bar.active })}
                                        onClick={() => setTabState(activate(tabState, [...path, name]))}>
                                        {name}
                                    </NavLink>
                                </NavItem>
                            ))}
                        </Nav>
                    </StyledCardHeader>
                ))
            }
            <CardBody>
                <StyledMarkdown text={SMLogic === 'hard' && hard || Z3Logic === 'nmg' && nmg || normal} />
            </CardBody>
        </Card>
    );

    return (
        <>
            <Row>
                <Col>
                    {introduction}
                </Col>
            </Row>
            <Row>
                <Col>
                    {SMtoggle}
                </Col>
                <Col>
                    {Z3toggle}
                </Col>
            </Row>
            <Row>
                <Col>
                    {log}
                </Col>
            </Row>
        </>
    );
}

function active(state, content) {
    state = state || {};
    content = content || {};
    const _active = state.active;
    if (content.tabs) {
        const { tabs } = content;
        const tab = _active ? find(tabs, { name: _active }) : tabs[0];
        return [
            { ...content, active: tab.name },
            ...active(state[tab.name], tab)
        ];
    }
    return [{ ...content }];
}

function activate(state, tabs) {
    const path = initial(tabs);
    const tab = last(tabs);
    for (const first of path) {
        return { ...state, [first]: activate(state[first], tail(tabs)) };
    }
    return { ...state, active: tab };
}
