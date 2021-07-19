import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const FreeLocations = raw('./FreeLocations.md');
const BigChest = raw('./BigChest.md');
const BigKeyChest = raw('./BigKeyChest.md');
const ArmosKnights = raw('./ArmosKnights.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={FreeLocations} />
        <Markdown text={BigChest} />
        <Markdown text={BigKeyChest} />
        <Markdown text={ArmosKnights} />
    </>;
}
