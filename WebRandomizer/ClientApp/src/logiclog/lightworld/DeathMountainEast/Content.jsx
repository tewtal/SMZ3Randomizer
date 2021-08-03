import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const FloatingIsland = raw('./FloatingIsland.md');
const FreeLocations = raw('./FreeLocations.md');
const MimicCave = raw('./MimicCave.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={FloatingIsland} />
        <Markdown text={FreeLocations} />
        <Markdown text={MimicCave} />
    </>;
}
