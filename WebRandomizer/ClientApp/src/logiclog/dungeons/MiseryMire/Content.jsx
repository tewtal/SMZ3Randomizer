import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const MainLobby = raw('./MainLobby.md');
const FreeLocations = raw('./FreeLocations.md');
const CompassChest = raw('./CompassChest.md');
const BigKeyChest = raw('./BigKeyChest.md');
const BigChest = raw('./BigChest.md');
const Vitreous = raw('./Vitreous.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={MainLobby} />
        <Markdown text={FreeLocations} />
        <Markdown text={CompassChest} />
        <Markdown text={BigKeyChest} />
        <Markdown text={BigChest} />
        <Markdown text={Vitreous} />
    </>;
}
