import raw from "raw.macro";
const DarkWorldDeathMountainWest = raw('./DarkWorldDeathMountainWest.md');
const DarkWorldDeathMountainEastNormal = raw('./DarkWorldDeathMountainEast.md');
const DarkWorldDeathMountainEastNMG = raw('./DarkWorldDeathMountainEastNMG.md');
const DarkWorldNorthWestNormal = raw('./DarkWorldNorthWest.md');
const DarkWorldNorthWestNMG = raw('./DarkWorldNorthWestNMG.md');
const DarkWorldNorthEast = raw('./DarkWorldNorthEast.md');
const DarkWorldSouth = raw('./DarkWorldSouth.md');
const DarkWorldMire = raw('./DarkWorldMire.md');

export default [
    { name: 'Death Mountain West', normal: DarkWorldDeathMountainWest },
    { name: 'Death Mountain East', normal: DarkWorldDeathMountainEastNormal, nmg: DarkWorldDeathMountainEastNMG },
    { name: 'North West', normal: DarkWorldNorthWestNormal, nmg: DarkWorldNorthWestNMG },
    { name: 'North East', normal: DarkWorldNorthEast },
    { name: 'South', normal: DarkWorldSouth },
    { name: 'Mire', normal: DarkWorldMire }
];
