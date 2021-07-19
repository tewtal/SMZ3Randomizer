import React, { useState } from 'react';
import { Row, Col, Card, CardBody, Collapse } from 'reactstrap';
import { InputGroupAddon, Nav, NavItem, NavLink } from 'reactstrap';
import { LevelCardHeader, LogicMarkdown, ToggleButton, IntroLabel } from './styled';
import InputGroup from '../components/util/PrefixInputGroup';
import Markdown from '../components/Markdown';

import { PlusSquareFill, DashSquareFill } from '../components/util/BootstrapIcon';

import classNames from 'classnames';

import find from 'lodash/find';
import initial from 'lodash/initial';
import tail from 'lodash/tail';
import last from 'lodash/last';

import raw from 'raw.macro';

import content from './content';

const introText = raw('./intro.md');

export default function LogicLog() {
    const [showIntro, setShowIntro] = useState(false);
    const [SMLogic, setSMLogic] = useState('Normal');
    const [keyShuffle, setKeyShuffle] = useState('');
    const [tabState, setTabState] = useState({});

    const parts = active(tabState, content);
    const bars = initial(parts);
    const { Content } = last(parts);

    return <>
        <Row>
            <Col>
                <Introduction show={showIntro} setShow={setShowIntro} />
            </Col>
        </Row>
        <Row>
            <Col>
                <Toggle prefix="SM Logic" value={SMLogic} setValue={setSMLogic} options={[
                    { value: 'Normal' },
                    { value: 'Hard' }
                ]} />
            </Col>
        </Row>
        <Row>
            <Col>
                <Toggle prefix="Key Shuffle" value={keyShuffle} setValue={setKeyShuffle} options={[
                    { name: 'None', value: '' },
                    { name: 'Keysanity', value: 'Ks' }
                ]} />
            </Col>
        </Row>
        <Row>
            <Col>
                <Card>
                    {bars
                        .map((bar, i) => [bar, bars.slice(1, i + 1).map(x => x.name)])
                        .map(([bar, path], i) => (
                            <LevelCardHeader key={i} level={bars.length - i}>
                                <Nav card tabs>
                                    {bar.tabs.map(({ name }) => (
                                        <NavItem key={name}>
                                            <NavLink
                                                className={classNames({ active: name === bar.active })}
                                                onClick={() => setTabState(activate(tabState, [...path, name]))}
                                            >
                                                {name}
                                            </NavLink>
                                        </NavItem>
                                    ))}
                                </Nav>
                            </LevelCardHeader>
                        ))
                    }
                    <CardBody>
                        <Content Markdown={LogicMarkdown} mode={{ logic: SMLogic, keysanity: keyShuffle }} />
                    </CardBody>
                </Card>
            </Col>
        </Row>
    </>;
}

function Introduction({ show, setShow }) {
    const Icon = show ? DashSquareFill : PlusSquareFill;
    return <>
        <IntroLabel onClick={() => setShow(!show)}>
            <Icon className="icon text-primary mr-1" />
            <strong>Introduction</strong>
        </IntroLabel>
        <Collapse isOpen={show}>
            <Markdown text={introText} />
        </Collapse>
    </>;
}

function Toggle({ value, setValue, prefix, options: [a, b] }) {
    return (
        <InputGroup prefix={prefix} className="mb-3">
            {button(a)}
            {button(b)}
        </InputGroup>
    );

    function button(option) {
        return (
            <InputGroupAddon addonType="append">
                <ToggleButton
                    color={value === option.value ? 'primary' : 'light'}
                    onClick={() => setValue(option.value)}
                >
                    {option.name || option.value}
                </ToggleButton>
            </InputGroupAddon>
        );
    }
}

function active(state, content) {
    state = state || {};
    content = content || {};
    const { tabs } = content;
    if (tabs) {
        const _active = state.active;
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
    for (const first of path) {
        return { ...state, [first]: activate(state[first], tail(tabs)) };
    }
    return { ...state, active: last(tabs) };
}
