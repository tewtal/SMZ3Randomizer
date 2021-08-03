import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const HookshotCave = raw('./HookshotCave.md');
const HookshotCaveBottomRight = raw('./HookshotCaveBottomRight.md');
const SuperbunnyCave = raw('./SuperbunnyCave.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={HookshotCave} />
        <Markdown text={HookshotCaveBottomRight} />
        <Markdown text={SuperbunnyCave} />
    </>;
}
