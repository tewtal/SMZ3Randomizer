import raw from "raw.macro";
const DarkWorldDeathMountainWest = raw('./DarkWorldDeathMountainWest.md');
const DarkWorldDeathMountainEast = raw('./DarkWorldDeathMountainEast.md');
const DarkWorldNorthWest = raw('./DarkWorldNorthWest.md');
const DarkWorldNorthEast = raw('./DarkWorldNorthEast.md');
const DarkWorldSouth = raw('./DarkWorldSouth.md');
const DarkWorldMire = raw('./DarkWorldMire.md');

export default [
    { name: 'Death Mountain West', normal: DarkWorldDeathMountainWest },
    { name: 'Death Mountain East', normal: DarkWorldDeathMountainEast },
    { name: 'North West', normal: DarkWorldNorthWest },
    { name: 'North East', normal: DarkWorldNorthEast },
    { name: 'South', normal: DarkWorldSouth },
    { name: 'Mire', normal: DarkWorldMire }
];
