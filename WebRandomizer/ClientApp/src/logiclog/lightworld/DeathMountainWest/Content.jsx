import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const EtherTablet = raw('./EtherTablet.md');
const SpectacleRock = raw('./SpectacleRock.md');
const SpectacleRockCave = raw('./SpectacleRockCave.md');
const OldMan = raw('./OldMan.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={EtherTablet} />
        <Markdown text={SpectacleRock} />
        <Markdown text={SpectacleRockCave} />
        <Markdown text={OldMan} />
    </>;
}
