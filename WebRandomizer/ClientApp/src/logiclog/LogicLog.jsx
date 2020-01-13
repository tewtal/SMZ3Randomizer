import React, { useState } from 'react';
import styled from 'styled-components';
import { Row, Col, Card, CardHeader, CardBody } from 'reactstrap';
import { InputGroupAddon, Button, Nav, NavItem, NavLink } from 'reactstrap';
import InputGroup from '../components/util/PrefixInputGroup';
import Markdown from '../components/Markdown';

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
      ul { padding: 0 1em; }

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

export default function LogicLog() {
    const [SMLogic, setSMLogic] = useState('normal');
    const [tabState, setTabState] = useState({});

    const parts = active(tabState, content);
    const bars = initial(parts);
    const { normal, hard } = last(parts);

    const logicButton = (logic, name) => (
        <InputGroupAddon addonType="append">
            <Button
                color={logic === name ? 'primary' : 'secondary'}
                onClick={() => setSMLogic(name)}
            >
                {capitalize(name)}
            </Button>
        </InputGroupAddon>
    );

    const toggle = (
        <InputGroup prefix="SM Logic" className="mb-3">
            {logicButton(SMLogic, 'normal')}
            {logicButton(SMLogic, 'hard')}
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
                <StyledMarkdown text={SMLogic === 'hard' && hard || normal} />
            </CardBody>
        </Card>
    );

    return (
        <>
            <Row>
                <Col>
                    {toggle}
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
        return { ...state, [first]: activate(state[first], tail(tabs)) }
    }
    return { ...state, active: tab };
}
