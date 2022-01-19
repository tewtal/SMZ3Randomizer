import React, { useState } from 'react';
import { Row, Col, Card, CardBody, Collapse } from 'reactstrap';
import { InputGroupAddon, Nav, NavItem, NavLink } from 'reactstrap';
import { LevelCardHeader, LogicMarkdown, ToggleButton, IntroLabel } from './styled';

import InputGroup from '../ui/PrefixInputGroup';
import Markdown from '../ui/Markdown';
import { PlusSquareFill, DashSquareFill } from '../ui/BootstrapIcon';

import classNames from 'classnames';

import map from 'lodash/map';
import reduce from 'lodash/reduce';
import get from 'lodash/get';
import head from 'lodash/head';
import tail from 'lodash/tail';
import initial from 'lodash/initial';
import last from 'lodash/last';

import raw from 'raw.macro';

import content from './content';

const introText = raw('./intro.md');

export default function LogicLog() {
    const [showIntro, setShowIntro] = useState(false);

    const [SMLogic, setSMLogic] = useState('Normal');
    const [keyShuffle, setKeyShuffle] = useState('');

    const [tabHistory, setTabHistory] = useState({});

    const [bars, Content] = activeTabs();

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
                    {bars.map(({ path, active, tabs }, i) => (
                        <LevelCardHeader key={i} level={bars.length - i}>
                            <Nav card tabs>
                                {tabs.map(name => (
                                    <NavItem key={name}>
                                        <NavLink
                                            className={classNames({ active: name === active })}
                                            onClick={() => onTabClick([...path, name])}
                                        >
                                            {name}
                                        </NavLink>
                                    </NavItem>
                                ))}
                            </Nav>
                        </LevelCardHeader>
                    ))}
                    <CardBody>
                        <Content Markdown={LogicMarkdown} mode={{ logic: SMLogic, keysanity: keyShuffle }} />
                    </CardBody>
                </Card>
            </Col>
        </Row>
    </>;

    function activeTabs() {
        const path = construct(tabHistory);
        const paths = unfold(path);
        const bars = convert(paths);
        const Content = get(content, last(paths));
        return [bars, Content];

        function construct(history = {}, path = []) {
            const next = history.recent || get(content, [...path, 'tabs', 0])
            return next ? construct(history[next], [...path, next]) : path;
        }

        function unfold(path) {
            const [paths] = reduce(path,
                ([acc, prev], next) => {
                    next = [...prev, next];
                    return [[...acc, next], next];
                },
                [[], []]
            );
            return paths;
        }

        function convert(paths) {
            return map(paths, path => ({
                active: last(path),
                path: initial(path),
                tabs: get(content, initial(path), content).tabs,
            }));
        }
    }

    function onTabClick(path) {
        setTabHistory(update(tabHistory, path));

        function update(history, path) {
            const base = head(path);
            const top = last(path);
            return base !== top
                ? { ...history, [base]: update(history[base] || {}, tail(path)) }
                : { ...history, recent: top };
        }
    }
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
