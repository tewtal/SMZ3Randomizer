import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const MissileGreenBrinstarBelowSuperMissile = raw('./MissileGreenBrinstarBelowSuperMissile.md');
const GreenBrinstarTopPassedGates = {
    Normal: raw('./GreenBrinstarTopPassedGates/Normal.md'),
    Hard: raw('./GreenBrinstarTopPassedGates/Hard.md'),
};
const MissileGreenBrinstarBehindReserveTank = {
    Normal: raw('./MissileGreenBrinstarBehindReserveTank/Normal.md'),
    Hard: raw('./MissileGreenBrinstarBehindReserveTank/Hard.md'),
};
const MissileGreenBrinstarBehindMissile = {
    Normal: raw('./MissileGreenBrinstarBehindMissile/Normal.md'),
    Hard: raw('./MissileGreenBrinstarBehindMissile/Hard.md'),
};
const GreenBrinstarBottomCorridors = {
    '': raw('./GreenBrinstarBottomCorridors/Regular.md'),
    Ks: raw('./GreenBrinstarBottomCorridors/Ks.md'),
}
const SuperMissileGreenBrinstarBottom = {
    '': raw('./SuperMissileGreenBrinstarBottom/Regular.md'),
    Ks: raw('./SuperMissileGreenBrinstarBottom/Ks.md'),
};

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={MissileGreenBrinstarBelowSuperMissile} />
        <Markdown text={GreenBrinstarTopPassedGates[logic]} />
        <Markdown text={MissileGreenBrinstarBehindReserveTank[logic]} />
        <Markdown text={MissileGreenBrinstarBehindMissile[logic]} />
        <Markdown text={GreenBrinstarBottomCorridors[keysanity]} />
        <Markdown text={SuperMissileGreenBrinstarBottom[keysanity]} />
    </>;
}
