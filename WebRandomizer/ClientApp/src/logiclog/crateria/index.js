import raw from "raw.macro";
const CrateriaWestNormal = raw('./CrateriaWestNormal.md');
const CrateriaWestHard = raw('./CrateriaWestHard.md');
const CrateriaCentralNormal = raw('./CrateriaCentralNormal.md');
const CrateriaCentralHard = raw('./CrateriaCentralHard.md');
const CrateriaEastNormal = raw('./CrateriaEastNormal.md');
const CrateriaEastHard = raw('./CrateriaEastHard.md');

export default [
    { name: 'West', normal: CrateriaWestNormal, hard: CrateriaWestHard },
    { name: 'Central', normal: CrateriaCentralNormal, hard: CrateriaCentralHard },
    { name: 'East', normal: CrateriaEastNormal, hard: CrateriaEastHard }
];
