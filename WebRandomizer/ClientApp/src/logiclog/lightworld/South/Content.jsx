import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const MazeRace = raw('./MazeRace.md');
const Library = raw('./Library.md');
const FluteSpot = raw('./FluteSpot.md');
const SouthOfGrove = raw('./SouthOfGrove.md');
const FreeLocations = raw('./FreeLocations.md');
const DesertLedge = raw('./DesertLedge.md');
const CheckerboardCave = raw('./CheckerboardCave.md');
const BombosTablet = raw('./BombosTablet.md');
const Floodgate = raw('./Floodgate.md');
const LakeHyliaIsland = raw('./LakeHyliaIsland.md');
const Hobo = raw('./Hobo.md');
const IceRodCave = raw('./IceRodCave.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={MazeRace} />
        <Markdown text={Library} />
        <Markdown text={FluteSpot} />
        <Markdown text={SouthOfGrove} />
        <Markdown text={FreeLocations} />
        <Markdown text={DesertLedge} />
        <Markdown text={CheckerboardCave} />
        <Markdown text={BombosTablet} />
        <Markdown text={Floodgate} />
        <Markdown text={LakeHyliaIsland} />
        <Markdown text={Hobo} />
        <Markdown text={IceRodCave} />
    </>;
}
