import React from 'react';

import raw from 'raw.macro';
const CanAccessDeathMountainPortal = raw('./CanAccessDeathMountainPortal.md');
const CanAccessDarkWorldPortal = {
    Normal: raw('./CanAccessDarkWorldPortal/Normal.md'),
    NormalKs: raw('./CanAccessDarkWorldPortal/NormalKs.md'),
    Hard: raw('./CanAccessDarkWorldPortal/Hard.md'),
    HardKs: raw('./CanAccessDarkWorldPortal/HardKs.md'),
};
const CanAccessMiseryMirePortal = {
    Normal: raw('./CanAccessMiseryMirePortal/Normal.md'),
    NormalKs: raw('./CanAccessMiseryMirePortal/NormalKs.md'),
    Hard: raw('./CanAccessMiseryMirePortal/Hard.md'),
    HardKs: raw('./CanAccessMiseryMirePortal/HardKs.md'),
};
const CanAccessNorfairUpperPortal = raw('./CanAccessNorfairUpperPortal.md');
const CanAccessNorfairLowerPortal = raw('./CanAccessNorfairLowerPortal.md');
const CanAccessMaridiaPortal = {
    Normal: raw('./CanAccessMaridiaPortal/Normal.md'),
    Hard: raw('./CanAccessMaridiaPortal/Hard.md'),
};

export default function ({ Markdown, mode: { logic, keysanity } }) {
    return <>
        <h2>Entering ALttP</h2>
        <Markdown text={CanAccessDeathMountainPortal} />
        <Markdown text={CanAccessDarkWorldPortal[logic + keysanity]} />
        <Markdown text={CanAccessMiseryMirePortal[logic + keysanity]} />

        <h2>Entering SM</h2>
        <Markdown text={CanAccessNorfairUpperPortal} />
        <Markdown text={CanAccessNorfairLowerPortal} />
        <Markdown text={CanAccessMaridiaPortal[logic]} />
    </>;
}
