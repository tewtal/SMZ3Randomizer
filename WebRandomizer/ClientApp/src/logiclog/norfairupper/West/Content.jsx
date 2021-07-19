import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const MissileLavaRoom = {
    Normal: raw('./MissileLavaRoom/Normal.md'),
    NormalKs: raw('./MissileLavaRoom/NormalKs.md'),
    Hard: raw('./MissileLavaRoom/Hard.md'),
    HardKs: raw('./MissileLavaRoom/HardKs.md'),
};
const IceBeam = {
    Normal: raw('./IceBeam/Normal.md'),
    NormalKs: raw('./IceBeam/NormalKs.md'),
    Hard: raw('./IceBeam/Hard.md'),
    HardKs: raw('./IceBeam/HardKs.md'),
};
const MissileBelowIceBeam = {
    Normal: raw('./MissileBelowIceBeam/Normal.md'),
    NormalKs: raw('./MissileBelowIceBeam/NormalKs.md'),
    Hard: raw('./MissileBelowIceBeam/Hard.md'),
    HardKs: raw('./MissileBelowIceBeam/HardKs.md'),
};
const HiJumpBoots = raw('./HiJumpBoots.md');
const MissileHiJumpBoots = raw('./MissileHiJumpBoots.md');
const EnergyTankHiJumpBoots = raw('./EnergyTankHiJumpBoots.md');

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={MissileLavaRoom[logic + keysanity]} />
        <Markdown text={IceBeam[logic + keysanity]} />
        <Markdown text={MissileBelowIceBeam[logic + keysanity]} />
        <Markdown text={HiJumpBoots} />
        <Markdown text={MissileHiJumpBoots} />
        <Markdown text={EnergyTankHiJumpBoots} />
    </>;
}
