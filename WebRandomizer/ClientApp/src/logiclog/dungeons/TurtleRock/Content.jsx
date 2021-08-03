import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const CompassChest = raw('./CompassChest.md');
const RollerRoom = raw('./RollerRoom.md');
const ChainChomps = raw('./ChainChomps.md');
const BigKeyChest = {
    '': raw('./BigKeyChest/Regular.md'),
    Ks: raw('./BigKeyChest/Ks.md'),
};
const AfterSecondEntrance = raw('./AfterSecondEntrance.md');
const EyeBridge = raw('./EyeBridge.md');
const Trinexx = raw('./Trinexx.md');
const CanBeatBoss = raw('./CanBeatBoss.md');

export default function ({ Markdown, mode: { keysanity } }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={CompassChest} />
        <Markdown text={RollerRoom} />
        <Markdown text={ChainChomps} />
        <Markdown text={BigKeyChest[keysanity]} />
        <Markdown text={AfterSecondEntrance} />
        <Markdown text={EyeBridge} />
        <Markdown text={Trinexx} />
        <Markdown text={CanBeatBoss} />
    </>;
}
