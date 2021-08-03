import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const BigChest = raw('./BigChest.md');
const Torch = raw('./Torch.md');
const MapChest = raw('./MapChest.md');
const EastWing = raw('./EastWing.md');
const Lanmolas = raw('./Lanmolas.md');
const CanBeatBoss = raw('./CanBeatBoss.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={BigChest} />
        <Markdown text={Torch} />
        <Markdown text={MapChest} />
        <Markdown text={EastWing} />
        <Markdown text={Lanmolas} />
        <Markdown text={CanBeatBoss} />
    </>;
}
