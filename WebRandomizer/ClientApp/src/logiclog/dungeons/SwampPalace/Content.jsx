import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const Entrance = raw('./Entrance.md');
const MapChest = raw('./MapChest.md');
const BigChest = raw('./BigChest.md');
const WestWing = raw('./WestWing.md');
const NorthWing = raw('./NorthWing.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={Entrance} />
        <Markdown text={MapChest} />
        <Markdown text={BigChest} />
        <Markdown text={WestWing} />
        <Markdown text={NorthWing} />
    </>;
}
