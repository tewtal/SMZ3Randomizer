import React from 'react';

import raw from 'raw.macro';
const CanEnter = {
    Normal: raw('./CanEnter/Normal.md'),
    Hard: raw('./CanEnter/Hard.md'),
};
const SuperMissilePinkBrinstarRegular = raw('./SuperMissilePinkBrinstar/Regular.md')
const SuperMissilePinkBrinstar = {
    Normal: SuperMissilePinkBrinstarRegular,
    Hard: SuperMissilePinkBrinstarRegular,
    NormalKs: raw('./SuperMissilePinkBrinstar/NormalKs.md'),
    HardKs: raw('./SuperMissilePinkBrinstar/HardKs.md'),
};
const MissilePinkBrinstarRoom = raw('./MissilePinkBrinstarRoom.md');
const ChargeBeam = raw('./ChargeBeam.md');
const PowerBombPinkBrinstar = {
    Normal: raw('./PowerBombPinkBrinstar/Normal.md'),
    Hard: raw('./PowerBombPinkBrinstar/Hard.md'),
};
const MissileGreenBrinstarPipe = raw('./MissileGreenBrinstarPipe.md');
const EnergyTankWaterway = raw('./EnergyTankWaterway.md');
const EnergyTankBrinstarGate = {
    Normal: raw('./EnergyTankBrinstarGate/Normal.md'),
    NormalKs: raw('./EnergyTankBrinstarGate/NormalKs.md'),
    Hard: raw('./EnergyTankBrinstarGate/Hard.md'),
    HardKs: raw('./EnergyTankBrinstarGate/HardKs.md'),
};

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter[logic]} />
        <Markdown text={SuperMissilePinkBrinstar[logic + keysanity]} />
        <Markdown text={MissilePinkBrinstarRoom} />
        <Markdown text={ChargeBeam} />
        <Markdown text={PowerBombPinkBrinstar[logic]} />
        <Markdown text={MissileGreenBrinstarPipe} />
        <Markdown text={EnergyTankWaterway} />
        <Markdown text={EnergyTankBrinstarGate[logic + keysanity]} />
    </>;
}
