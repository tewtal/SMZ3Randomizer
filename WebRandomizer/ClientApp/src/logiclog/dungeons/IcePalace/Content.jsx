import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const CompassChest = raw('./CompassChest.md');
const SpikeRoom = raw('./SpikeRoom.md');
const MapChest = raw('./MapChest.md');
const BigKeyChest = raw('./BigKeyChest.md');
const FreeLocations = raw('./FreeLocations.md');
const BigChest = raw('./BigChest.md');
const Kholdstare = raw('./Kholdstare.md');
const CanNotWasteKeysBeforeAccessible = raw('./CanNotWasteKeysBeforeAccessible.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={CompassChest} />
        <Markdown text={SpikeRoom} />
        <Markdown text={MapChest} />
        <Markdown text={BigKeyChest} />
        <Markdown text={FreeLocations} />
        <Markdown text={BigChest} />
        <Markdown text={Kholdstare} />
        <Markdown text={CanNotWasteKeysBeforeAccessible} />
    </>;
}
