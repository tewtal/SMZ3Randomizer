import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const BumperCave = raw('./BumperCave.md');
const VillageOfOutcasts = raw('./VillageOfOutcasts.md');
const HammerPegs = raw('./HammerPegs.md');
const BlacksmithChain = raw('./BlacksmithChain.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={BumperCave} />
        <Markdown text={VillageOfOutcasts} />
        <Markdown text={HammerPegs} />
        <Markdown text={BlacksmithChain} />
    </>;
}
