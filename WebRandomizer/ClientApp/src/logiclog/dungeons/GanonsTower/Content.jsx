import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const BobsTorch = raw('./BobsTorch.md');
const DMsRoom = raw('./DMsRoom.md');
const MapChest = raw('./MapChest.md');
const FiresnakeRoom = raw('./FiresnakeRoom.md');
const RandomizerRoom = raw('./RandomizerRoom.md');
const HopeRoom = raw('./HopeRoom.md');
const TileRoom = raw('./TileRoom.md');
const CompassRoom = raw('./CompassRoom.md');
const BobsChest = raw('./BobsChest.md');
const BigChest = raw('./BigChest.md');
const BigKeyRoom = raw('./BigKeyRoom.md');
const Ascent = raw('./Ascent.md');
const MoldormChest = raw('./MoldormChest.md');
const CanBeatArmos = raw('./CanBeatArmos.md');
const CanBeatMoldorm = raw('./CanBeatMoldorm.md');

export default function ({ Markdown }) {
    return <>
        <p>Multiworld only fill locations with non-progression items from own world</p>
        <Markdown text={CanEnter} />
        <Markdown text={BobsTorch} />
        <Markdown text={DMsRoom} />
        <Markdown text={MapChest} />
        <Markdown text={FiresnakeRoom} />
        <Markdown text={RandomizerRoom} />
        <Markdown text={HopeRoom} />
        <Markdown text={TileRoom} />
        <Markdown text={CompassRoom} />
        <Markdown text={BobsChest} />
        <Markdown text={BigChest} />
        <Markdown text={BigKeyRoom} />
        <Markdown text={Ascent} />
        <Markdown text={MoldormChest} />
        <Markdown text={CanBeatArmos} />
        <Markdown text={CanBeatMoldorm} />
    </>;
}
