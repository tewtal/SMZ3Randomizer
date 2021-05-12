import raw from "raw.macro";
const LightWorldDeathMountainWestNormal = raw('./LightWorldDeathMountainWest.md');
const LightWorldDeathMountainWestNMG = raw('./LightWorldDeathMountainWestNMG.md');
const LightWorldDeathMountainEast = raw('./LightWorldDeathMountainEast.md');
const LightWorldNorthWestNormal = raw('./LightWorldNorthWest.md');
const LightWorldNorthWestNMG = raw('./LightWorldNorthWestNMG.md');
const LightWorldNorthEastNormal = raw('./LightWorldNorthEast.md');
const LightWorldNorthEastNMG = raw('./LightWorldNorthEastNMG.md');
const LightWorldSouthNormal = raw('./LightWorldSouth.md');
const LightWorldSouthNMG = raw('./LightWorldSouthNMG.md');

export default [
    { name: 'Death Mountain West', normal: LightWorldDeathMountainWestNormal, nmg: LightWorldDeathMountainWestNMG },
    { name: 'Death Mountain East', normal: LightWorldDeathMountainEast },
    { name: 'North West', normal: LightWorldNorthWestNormal, nmg: LightWorldNorthWestNMG },
    { name: 'North East', normal: LightWorldNorthEastNormal, nmg: LightWorldNorthEastNMG },
    { name: 'South', normal: LightWorldSouthNormal, nmg: LightWorldSouthNMG }
];
