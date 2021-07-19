import React from 'react';

import raw from 'raw.macro';
const CanEnter = {
    Normal: raw('./CanEnter/Normal.md'),
    NormalKs: raw('./CanEnter/NormalKs.md'),
    Hard: raw('./CanEnter/Hard.md'),
    HardKs: raw('./CanEnter/HardKs.md'),
};
const MissileGoldTorizo = {
    Normal: raw('./MissileGoldTorizo/Normal.md'),
    Hard: raw('./MissileGoldTorizo/Hard.md'),
};
const SuperMissileGoldTorizo = {
    Normal: raw('./SuperMissileGoldTorizo/Normal.md'),
    Hard: raw('./SuperMissileGoldTorizo/Hard.md'),
};
const ScrewAttack = {
    Normal: raw('./ScrewAttack/Normal.md'),
    Hard: raw('./ScrewAttack/Hard.md'),
};
const MissileMickeyMouseRoom = {
    Normal: raw('./MissileMickeyMouseRoom/Normal.md'),
    NormalKs: raw('./MissileMickeyMouseRoom/NormalKs.md'),
    Hard: raw('./MissileMickeyMouseRoom/Hard.md'),
    HardKs: raw('./MissileMickeyMouseRoom/HardKs.md'),
};

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter[logic + keysanity]} />
        <Markdown text={MissileGoldTorizo[logic]} />
        <Markdown text={SuperMissileGoldTorizo[logic]} />
        <Markdown text={ScrewAttack[logic]} />
        <Markdown text={MissileMickeyMouseRoom[logic + keysanity]} />
    </>;
}
