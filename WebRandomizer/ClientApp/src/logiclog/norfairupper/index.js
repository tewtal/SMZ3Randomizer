import raw from "raw.macro";
const NorfairUpperWestNormal = raw('./NorfairUpperWestNormal.md');
const NorfairUpperWestMedium = raw('./NorfairUpperWestMedium.md');
const NorfairUpperWestHard = raw('./NorfairUpperWestHard.md');
const NorfairUpperEastNormal = raw('./NorfairUpperEastNormal.md');
const NorfairUpperEastMedium = raw('./NorfairUpperEastMedium.md');
const NorfairUpperEastHard = raw('./NorfairUpperEastHard.md');
const NorfairUpperCrocomireNormal = raw('./NorfairUpperCrocomireNormal.md');
const NorfairUpperCrocomireMedium = raw('./NorfairUpperCrocomireMedium.md');
const NorfairUpperCrocomireHard = raw('./NorfairUpperCrocomireHard.md');

export default [
    { name: 'West', normal: NorfairUpperWestNormal, medium: NorfairUpperWestMedium, hard: NorfairUpperWestHard },
    { name: 'East', normal: NorfairUpperEastNormal, medium: NorfairUpperEastMedium, hard: NorfairUpperEastHard },
    { name: 'Crocomire', normal: NorfairUpperCrocomireNormal, medium: NorfairUpperCrocomireMedium, hard: NorfairUpperCrocomireHard }
];
