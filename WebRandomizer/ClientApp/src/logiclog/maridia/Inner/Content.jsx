import React from 'react';

import raw from 'raw.macro';
const CanEnter = {
    Normal: raw('./CanEnter/Normal.md'),
    Hard: raw('./CanEnter/Hard.md'),
};
const YellowMaridia = {
    Normal: raw('./YellowMaridia/Normal.md'),
    NormalKs: raw('./YellowMaridia/NormalKs.md'),
    Hard: raw('./YellowMaridia/Hard.md'),
    HardKs: raw('./YellowMaridia/HardKs.md'),
};
const PlasmaBeam = {
    Normal: raw('./PlasmaBeam/Normal.md'),
    Hard: raw('./PlasmaBeam/Hard.md'),
};
const LeftMaridiaSandPitRoom = {
    Normal: raw('./LeftMaridiaSandPitRoom/Normal.md'),
    Hard: raw('./LeftMaridiaSandPitRoom/Hard.md'),
};
const MissileRightMaridiaSandPitRoom = {
    Normal: raw('./MissileRightMaridiaSandPitRoom/Normal.md'),
    Hard: raw('./MissileRightMaridiaSandPitRoom/Hard.md'),
};
const PowerBombRightMaridiaSandPitRoom = {
    Normal: raw('./PowerBombRightMaridiaSandPitRoom/Normal.md'),
    Hard: raw('./PowerBombRightMaridiaSandPitRoom/Hard.md'),
};
const PinkMaridia = {
    Normal: raw('./PinkMaridia/Normal.md'),
    Hard: raw('./PinkMaridia/Hard.md'),
};
const SpringBall = {
    Normal: raw('./SpringBall/Normal.md'),
    Hard: raw('./SpringBall/Hard.md'),
};
const MissileDraygon = {
    Normal: raw('./MissileDraygon/Normal.md'),
    NormalKs: raw('./MissileDraygon/NormalKs.md'),
    Hard: raw('./MissileDraygon/Hard.md'),
    HardKs: raw('./MissileDraygon/HardKs.md'),
};
const EnergyTankBotwoon = {
    '': raw('./EnergyTankBotwoon/Regular.md'),
    Ks: raw('./EnergyTankBotwoon/Ks.md'),
};
const SpaceJump = raw('./SpaceJump.md');
const CanReachAqueduct = {
    Normal: raw('./CanReachAqueduct/Normal.md'),
    NormalKs: raw('./CanReachAqueduct/NormalKs.md'),
    Hard: raw('./CanReachAqueduct/Hard.md'),
    HardKs: raw('./CanReachAqueduct/HardKs.md'),
};
const CanDefeatDraygon = {
    Normal: raw('./CanDefeatDraygon/Normal.md'),
    NormalKs: raw('./CanDefeatDraygon/NormalKs.md'),
    Hard: raw('./CanDefeatDraygon/Hard.md'),
    HardKs: raw('./CanDefeatDraygon/HardKs.md'),
};
const CanDefeatBotwoon = {
    Normal: raw('./CanDefeatBotwoon/Normal.md'),
    Hard: raw('./CanDefeatBotwoon/Hard.md'),
};

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter[logic]} />
        <Markdown text={YellowMaridia[logic + keysanity]} />
        <Markdown text={PlasmaBeam[logic]} />
        <Markdown text={LeftMaridiaSandPitRoom[logic]} />
        <Markdown text={MissileRightMaridiaSandPitRoom[logic]} />
        <Markdown text={PowerBombRightMaridiaSandPitRoom[logic]} />
        <Markdown text={PinkMaridia[logic]} />
        <Markdown text={SpringBall[logic]} />
        <Markdown text={MissileDraygon[logic + keysanity]} />
        <Markdown text={EnergyTankBotwoon[keysanity]} />
        <Markdown text={SpaceJump} />
        <Markdown text={CanReachAqueduct[logic + keysanity]} />
        <Markdown text={CanDefeatDraygon[logic + keysanity]} />
        <Markdown text={CanDefeatBotwoon[logic]} />
    </>;
}
