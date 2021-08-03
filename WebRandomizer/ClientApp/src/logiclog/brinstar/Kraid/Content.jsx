import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const BehindKraid = {
    '': raw('./BehindKraid/Regular.md'),
    Ks: raw('./BehindKraid/Ks.md'),
};
const MissileKraid = raw('./MissileKraid.md');

export default function ({ Markdown, mode: { keysanity } }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={BehindKraid[keysanity]} />
        <Markdown text={MissileKraid} />
    </>;
}
