import React from 'react';

import raw from 'raw.macro';
const CanEnter = {
    Normal: raw('./CanEnter/Normal.md'),
    NormalKs: raw('./CanEnter/NormalKs.md'),
    Hard: raw('./CanEnter/Hard.md'),
    HardKs: raw('./CanEnter/HardKs.md'),
};
const MissileAboveCrocomire = {
    Normal: raw('./MissileAboveCrocomire/Normal.md'),
    Hard: raw('./MissileAboveCrocomire/Hard.md'),
};
const EnergyTankCrocomire = {
    Normal: raw('./EnergyTankCrocomire/Normal.md'),
    Hard: raw('./EnergyTankCrocomire/Hard.md'),
};
const PowerBombCrocomire = {
    Normal: raw('./PowerBombCrocomire/Normal.md'),
    Hard: raw('./PowerBombCrocomire/Hard.md'),
};
const MissileBelowCrocomire = raw('./MissileBelowCrocomire.md');
const MissileGrapplingBeam = {
    Normal: raw('./MissileGrapplingBeam/Normal.md'),
    Hard: raw('./MissileGrapplingBeam/Hard.md'),
};
const GrapplingBeam = {
    Normal: raw('./GrapplingBeam/Normal.md'),
    Hard: raw('./GrapplingBeam/Hard.md'),
};
const CanAccessCrocomire = {
    '': raw('./CanAccessCrocomire/Regular.md'),
    Ks: raw('./CanAccessCrocomire/Ks.md'),
};

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter[logic + keysanity]} />
        <Markdown text={MissileAboveCrocomire[logic]} />
        <Markdown text={EnergyTankCrocomire[logic]} />
        <Markdown text={PowerBombCrocomire[logic]} />
        <Markdown text={MissileBelowCrocomire} />
        <Markdown text={MissileGrapplingBeam[logic]} />
        <Markdown text={GrapplingBeam[logic]} />
        <Markdown text={CanAccessCrocomire[keysanity]} />
    </>;
}
