import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const FreeLocations = raw('./FreeLocations.md');
const BigChest = raw('./BigChest.md');
const PinballRoom = raw('./PinballRoom.md');
const BigKeyChest = raw('./BigKeyChest.md');
const BridgeRoom = raw('./BridgeRoom.md');
const Mothula = raw('./Mothula.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={FreeLocations} />
        <Markdown text={BigChest} />
        <Markdown text={PinballRoom} />
        <Markdown text={BigKeyChest} />
        <Markdown text={BridgeRoom} />
        <Markdown text={Mothula} />
    </>;
}
