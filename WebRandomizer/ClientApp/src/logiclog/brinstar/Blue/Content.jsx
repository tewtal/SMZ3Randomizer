import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const MorphingBall = raw('./MorphingBall.md');
const PowerBombBlueBrinstar = raw('./PowerBombBlueBrinstar.md');
const MissileBlueBrinstarBottom = raw('./MissileBlueBrinstarBottom.md');
const EnergyTankBrinstarCeiling = {
    Normal: raw('./EnergyTankBrinstarCeiling/Normal.md'),
    NormalKs: raw('./EnergyTankBrinstarCeiling/NormalKs.md'),
    Hard: raw('./EnergyTankBrinstarCeiling/Hard.md'),
    HardKs: raw('./EnergyTankBrinstarCeiling/HardKs.md'),
};
const MissileBlueBrinstarMiddle = {
    '': raw('./MissileBlueBrinstarMiddle/Regular.md'),
    Ks: raw('./MissileBlueBrinstarMiddle/Ks.md'),
};
const MissileBlueBrinstarBillyMays = {
    '': raw('./MissileBlueBrinstarBillyMays/Regular.md'),
    Ks: raw('./MissileBlueBrinstarBillyMays/Ks.md'),
};

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={MorphingBall} />
        <Markdown text={PowerBombBlueBrinstar} />
        <Markdown text={MissileBlueBrinstarBottom} />
        <Markdown text={EnergyTankBrinstarCeiling[logic + keysanity]} />
        <Markdown text={MissileBlueBrinstarMiddle[keysanity]} />
        <Markdown text={MissileBlueBrinstarBillyMays[keysanity]} />
    </>;
}
