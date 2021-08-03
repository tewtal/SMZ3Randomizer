import React from 'react';

import raw from 'raw.macro';
const CanEnter = {
    Normal: raw('./CanEnter/Normal.md'),
    NormalKs: raw('./CanEnter/NormalKs.md'),
    Hard: raw('./CanEnter/Hard.md'),
    HardKs: raw('./CanEnter/HardKs.md'),
};
const MissileWreckedShipMiddle = raw('./MissileWreckedShipMiddle.md');
const AfterShipUnlocked = raw('./AfterShipUnlocked.md');
const ReserveTankWreckedShip = {
    Normal: raw('./ReserveTankWreckedShip/Normal.md'),
    NormalKs: raw('./ReserveTankWreckedShip/NormalKs.md'),
    Hard: raw('./ReserveTankWreckedShip/Hard.md'),
    HardKs: raw('./ReserveTankWreckedShip/HardKs.md'),
};
const GravitySuit = {
    Normal: raw('./GravitySuit/Normal.md'),
    NormalKs: raw('./GravitySuit/NormalKs.md'),
    Hard: raw('./GravitySuit/Hard.md'),
    HardKs: raw('./GravitySuit/HardKs.md'),
};
const EnergyTankWreckedShip = {
    Normal: raw('./EnergyTankWreckedShip/Normal.md'),
    Hard: raw('./EnergyTankWreckedShip/Hard.md'),
};
const CanUnlockShip = {
    '': raw('./CanUnlockShip/Regular.md'),
    Ks: raw('./CanUnlockShip/Ks.md'),
};

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter[logic + keysanity]} />
        <Markdown text={MissileWreckedShipMiddle} />
        <Markdown text={AfterShipUnlocked} />
        <Markdown text={ReserveTankWreckedShip[logic + keysanity]} />
        <Markdown text={GravitySuit[logic + keysanity]} />
        <Markdown text={EnergyTankWreckedShip[logic]} />
        <Markdown text={CanUnlockShip[keysanity]} />
    </>;
}
