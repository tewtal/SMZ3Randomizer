import React from 'react';

import raw from 'raw.macro';
const CanEnter = {
    Normal: raw('./CanEnter/Normal.md'),
    NormalKs: raw('./CanEnter/NormalKs.md'),
    Hard: raw('./CanEnter/Hard.md'),
    HardKs: raw('./CanEnter/HardKs.md'),
};
const MissileLowerNorfairNearWaveBeam = {
    Normal: raw('./MissileLowerNorfairNearWaveBeam/Normal.md'),
    Hard: raw('./MissileLowerNorfairNearWaveBeam/Hard.md'),
};
const MissileLowerNorfairAboveFireFleaRoom = raw('./MissileLowerNorfairAboveFireFleaRoom.md');
const PowerBombLowerNorfairAboveFireFleaRoom = {
    Normal: raw('./PowerBombLowerNorfairAboveFireFleaRoom/Normal.md'),
    Hard: raw('./PowerBombLowerNorfairAboveFireFleaRoom/Hard.md'),
};
const EnergyTankFirefleas = raw('./EnergyTankFirefleas.md');
const PowerBombPowerBombsOfShame = raw('./PowerBombPowerBombsOfShame.md');
const EnergyTankRidley = {
    '': raw('./EnergyTankRidley/Regular.md'),
    Ks: raw('./EnergyTankRidley/Ks.md'),
};
const CanExit = {
    Normal: raw('./CanExit/Normal.md'),
    NormalKs: raw('./CanExit/NormalKs.md'),
    Hard: raw('./CanExit/Hard.md'),
    HardKs: raw('./CanExit/HardKs.md'),
};

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter[logic + keysanity]} />
        <Markdown text={MissileLowerNorfairNearWaveBeam[logic]} />
        <Markdown text={MissileLowerNorfairAboveFireFleaRoom} />
        <Markdown text={PowerBombLowerNorfairAboveFireFleaRoom[logic]} />
        <Markdown text={EnergyTankFirefleas} />
        <Markdown text={PowerBombPowerBombsOfShame} />
        <Markdown text={EnergyTankRidley[keysanity]} />
        <Markdown text={CanExit[logic + keysanity]} />
    </>;
}
