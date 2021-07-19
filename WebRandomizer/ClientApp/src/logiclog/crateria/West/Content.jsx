import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const EnergyTankTerminator = raw('./EnergyTankTerminator.md');
const EnergyTankGauntlet = {
    Normal: raw('./EnergyTankGauntlet/Normal.md'),
    Hard: raw('./EnergyTankGauntlet/Hard.md'),
};
const MissileCrateriaGauntlet = {
    Normal: raw('./MissileCrateriaGauntlet/Normal.md'),
    Hard: raw('./MissileCrateriaGauntlet/Hard.md'),
};
const EnterAndLeaveGauntlet = {
    Normal: raw('./EnterAndLeaveGauntlet/Normal.md'),
    NormalKs: raw('./EnterAndLeaveGauntlet/NormalKs.md'),
    Hard: raw('./EnterAndLeaveGauntlet/Hard.md'),
    HardKs: raw('./EnterAndLeaveGauntlet/HardKs.md'),
};

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={EnergyTankTerminator} />
        <Markdown text={EnergyTankGauntlet[logic]} />
        <Markdown text={MissileCrateriaGauntlet[logic]} />
        <Markdown text={EnterAndLeaveGauntlet[logic + keysanity]} />
    </>;
}
