import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const FreeLocations = raw('./FreeLocations.md');
const BigKeyChest = raw('./BigKeyChest.md');
const AfterBigKeyDoor = raw('./AfterBigKeyDoor.md');
const Moldorm = raw('./Moldorm.md');
const CanBeatBoss = raw('./CanBeatBoss.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={FreeLocations} />
        <Markdown text={BigKeyChest} />
        <Markdown text={AfterBigKeyDoor} />
        <Markdown text={Moldorm} />
        <Markdown text={CanBeatBoss} />
    </>;
}
