import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const CastleTowerFoyer = raw('./CastleTowerFoyer.md');
const CastleTowerDarkMaze = raw('./CastleTowerDarkMaze.md');
const Agahnim = raw('./Agahnim.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={CastleTowerFoyer} />
        <Markdown text={CastleTowerDarkMaze} />
        <Markdown text={Agahnim} />
    </>;
}
