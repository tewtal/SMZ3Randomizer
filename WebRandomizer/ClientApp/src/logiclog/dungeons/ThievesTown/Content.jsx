import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const Courtyard = raw('./Courtyard.md');
const Attic = raw('./Attic.md');
const BlindsCell = raw('./BlindsCell.md');
const BigChest = raw('./BigChest.md');
const Blind = raw('./Blind.md');
const CanBeatBoss = raw('./CanBeatBoss.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={Courtyard} />
        <Markdown text={Attic} />
        <Markdown text={BlindsCell} />
        <Markdown text={BigChest} />
        <Markdown text={Blind} />
        <Markdown text={CanBeatBoss} />
    </>;
}
