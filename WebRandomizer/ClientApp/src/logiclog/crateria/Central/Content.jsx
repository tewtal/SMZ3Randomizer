import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const PowerBombCrateriaSurface = {
    '': raw('./PowerBombCrateriaSurface/Regular.md'),
    'Ks': raw('./PowerBombCrateriaSurface/Ks.md'),
};
const MissileCrateriaMiddle = raw('./MissileCrateriaMiddle.md');
const MissileCrateriaBottom = raw('./MissileCrateriaBottom.md');
const SuperMissileCrateria = raw('./SuperMissileCrateria.md');
const Bombs = {
    Normal: raw('./Bombs/Normal.md'),
    NormalKs: raw('./Bombs/NormalKs.md'),
    Hard: raw('./Bombs/Hard.md'),
    HardKs: raw('./Bombs/HardKs.md'),
};

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={PowerBombCrateriaSurface[keysanity]} />
        <Markdown text={MissileCrateriaMiddle} />
        <Markdown text={MissileCrateriaBottom} />
        <Markdown text={SuperMissileCrateria} />
        <Markdown text={Bombs[logic + keysanity]} />
    </>;
}
