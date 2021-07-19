import React from 'react';

import raw from 'raw.macro';
const CanEnter = {
    Normal: raw('./CanEnter/Normal.md'),
    NormalKs: raw('./CanEnter/NormalKs.md'),
    Hard: raw('./CanEnter/Hard.md'),
    HardKs: raw('./CanEnter/HardKs.md'),
};
const MissileOutsideWreckedShipBottom = {
    Normal: raw('./MissileOutsideWreckedShipBottom/Normal.md'),
    Hard: raw('./MissileOutsideWreckedShipBottom/Hard.md'),
};
const MissilesOutsideWreckedShipTopHalf = {
    '': raw('./MissilesOutsideWreckedShipTopHalf/Regular.md'),
    Ks: raw('./MissilesOutsideWreckedShipTopHalf/Ks.md'),
};
const MissileCrateriaMoat = raw('./MissileCrateriaMoat.md');

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <Markdown text={CanEnter[logic + keysanity]} />
        <Markdown text={MissileOutsideWreckedShipBottom[logic]} />
        <Markdown text={MissilesOutsideWreckedShipTopHalf[keysanity]} />
        <Markdown text={MissileCrateriaMoat} />
    </>;
}
