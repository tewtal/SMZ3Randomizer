import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const MireShed = raw('./MireShed.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={MireShed} />
    </>;
}
