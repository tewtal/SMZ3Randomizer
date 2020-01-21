import raw from "raw.macro";
const NorfairUpperWestNormal = raw('./NorfairUpperWestNormal.md');
const NorfairUpperWestHard = raw('./NorfairUpperWestHard.md');
const NorfairUpperEastNormal = raw('./NorfairUpperEastNormal.md');
const NorfairUpperEastHard = raw('./NorfairUpperEastHard.md');
const NorfairUpperCrocomireNormal = raw('./NorfairUpperCrocomireNormal.md');
const NorfairUpperCrocomireHard = raw('./NorfairUpperCrocomireHard.md');

export default [
    { name: 'West', normal: NorfairUpperWestNormal, hard: NorfairUpperWestHard },
    { name: 'East', normal: NorfairUpperEastNormal, hard: NorfairUpperEastHard },
    { name: 'Crocomire', normal: NorfairUpperCrocomireNormal, hard: NorfairUpperCrocomireHard }
];
