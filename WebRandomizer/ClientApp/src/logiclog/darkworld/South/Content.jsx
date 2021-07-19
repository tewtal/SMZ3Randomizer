import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const FreeLocations = raw('./FreeLocations.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={FreeLocations} />
    </>;
}
