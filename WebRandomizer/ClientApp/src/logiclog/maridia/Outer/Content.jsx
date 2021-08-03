import React from 'react';

import raw from 'raw.macro';
const CanEnter = {
    Normal: raw('./CanEnter/Normal.md'),
    NormalKs: raw('./CanEnter/NormalKs.md'),
    Hard: raw('./CanEnter/Hard.md'),
    HardKs: raw('./CanEnter/HardKs.md'),
};
const MissileGreenMaridiaShinespark = {
    Normal: raw('./MissileGreenMaridiaShinespark/Normal.md'),
    Hard: raw('./MissileGreenMaridiaShinespark/Hard.md'),
};
const SuperMissileGreenMaridia = raw('./SuperMissileGreenMaridia.md');
const EnergyTankMamaTurtle = {
    Normal: raw('./EnergyTankMamaTurtle/Normal.md'),
    Hard: raw('./EnergyTankMamaTurtle/Hard.md'),
};
const MissileGreenMaridiaTatori = raw('./MissileGreenMaridiaTatori.md');

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter[logic + keysanity]} />
        <Markdown text={MissileGreenMaridiaShinespark[logic]} />
        <Markdown text={SuperMissileGreenMaridia} />
        <Markdown text={EnergyTankMamaTurtle[logic]} />
        <Markdown text={MissileGreenMaridiaTatori} />
    </>;
}
