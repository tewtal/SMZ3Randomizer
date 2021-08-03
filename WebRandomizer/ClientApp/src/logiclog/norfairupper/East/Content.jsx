import React from 'react';

import raw from 'raw.macro';
const CanEnter = {
    Normal: raw('./CanEnter/Normal.md'),
    NormalKs: raw('./CanEnter/NormalKs.md'),
    Hard: raw('./CanEnter/Hard.md'),
    HardKs: raw('./CanEnter/HardKs.md'),
};
const MissileBubbleNorfair = {
    '': raw('./MissileBubbleNorfair/Regular.md'),
    Ks: raw('./MissileBubbleNorfair/Ks.md'),
};
const BubbleCliff = {
    Normal: raw('./BubbleCliff/Normal.md'),
    NormalKs: raw('./BubbleCliff/NormalKs.md'),
    Hard: raw('./BubbleCliff/Hard.md'),
    HardKs: raw('./BubbleCliff/HardKs.md'),
};
const MissileBubbleNorfairGreenDoor = {
    Normal: raw('./MissileBubbleNorfairGreenDoor/Normal.md'),
    NormalKs: raw('./MissileBubbleNorfairGreenDoor/NormalKs.md'),
    Hard: raw('./MissileBubbleNorfairGreenDoor/Hard.md'),
    HardKs: raw('./MissileBubbleNorfairGreenDoor/HardKs.md'),
};
const SpeedBooster = {
    Normal: raw('./SpeedBooster/Normal.md'),
    NormalKs: raw('./SpeedBooster/NormalKs.md'),
    Hard: raw('./SpeedBooster/Hard.md'),
    HardKs: raw('./SpeedBooster/HardKs.md'),
};
const MissileWaveBeam = {
    Normal: raw('./MissileWaveBeam/Normal.md'),
    NormalKs: raw('./MissileWaveBeam/NormalKs.md'),
    Hard: raw('./MissileWaveBeam/Hard.md'),
    HardKs: raw('./MissileWaveBeam/HardKs.md'),
};
const WaveBeam = {
    Normal: raw('./WaveBeam/Normal.md'),
    NormalKs: raw('./WaveBeam/NormalKs.md'),
    Hard: raw('./WaveBeam/Hard.md'),
    HardKs: raw('./WaveBeam/HardKs.md'),
};

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter[logic + keysanity]} />
        <Markdown text={MissileBubbleNorfair[keysanity]} />
        <Markdown text={BubbleCliff[logic + keysanity]} />
        <Markdown text={MissileBubbleNorfairGreenDoor[logic + keysanity]} />
        <Markdown text={SpeedBooster[logic + keysanity]} />
        <Markdown text={MissileWaveBeam[logic + keysanity]} />
        <Markdown text={WaveBeam[logic + keysanity]} />
    </>;
}
