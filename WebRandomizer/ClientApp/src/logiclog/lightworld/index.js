import raw from "raw.macro";
const LightWorldDeathMountainWest = raw('./LightWorldDeathMountainWest.md');
const LightWorldDeathMountainEast = raw('./LightWorldDeathMountainEast.md');
const LightWorldNorthWest = raw('./LightWorldNorthWest.md');
const LightWorldNorthEast = raw('./LightWorldNorthEast.md');
const LightWorldSouth = raw('./LightWorldSouth.md');

export default [
    { name: 'Death Mountain West', normal: LightWorldDeathMountainWest },
    { name: 'Death Mountain East', normal: LightWorldDeathMountainEast },
    { name: 'North West', normal: LightWorldNorthWest },
    { name: 'North East', normal: LightWorldNorthEast },
    { name: 'South', normal: LightWorldSouth }
];
