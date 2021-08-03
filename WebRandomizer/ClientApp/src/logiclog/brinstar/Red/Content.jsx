import React from 'react';

import raw from 'raw.macro';
const CanEnter = {
    Normal: raw('./CanEnter/Normal.md'),
    Hard: raw('./CanEnter/Hard.md'),
};
const XRayScope = {
    Normal: raw('./XRayScope/Normal.md'),
    Hard: raw('./XRayScope/Hard.md'),
};
const RedBrinstarAlpha = raw('./RedBrinstarAlpha.md');
const RedBrinstarBeta = {
    Normal: raw('./RedBrinstarBeta/Normal.md'),
    Hard: raw('./RedBrinstarBeta/Hard.md'),
};
const Spazer = raw('./Spazer.md');

export default function ({ Markdown, mode: { logic } }) {
    return <>
        <Markdown text={CanEnter[logic]} />
        <Markdown text={XRayScope[logic]} />
        <Markdown text={RedBrinstarAlpha} />
        <Markdown text={RedBrinstarBeta[logic]} />
        <Markdown text={Spazer} />
    </>;
}
