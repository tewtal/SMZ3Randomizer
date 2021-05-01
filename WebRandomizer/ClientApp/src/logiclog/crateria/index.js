import raw from "raw.macro";
const CrateriaWestNormal = raw('./CrateriaWestNormal.md');
const CrateriaWestMedium = raw('./CrateriaWestMedium.md');
const CrateriaWestHard = raw('./CrateriaWestHard.md');
const CrateriaCentralNormal = raw('./CrateriaCentralNormal.md');
const CrateriaCentralMedium = raw('./CrateriaCentralMedium.md');
const CrateriaCentralHard = raw('./CrateriaCentralHard.md');
const CrateriaEastNormal = raw('./CrateriaEastNormal.md');
const CrateriaEastMedium = raw('./CrateriaEastMedium.md');
const CrateriaEastHard = raw('./CrateriaEastHard.md');

export default [
    { name: 'West', normal: CrateriaWestNormal, medium: CarderaWestMedium,hard: CrateriaWestHard },
    { name: 'Central', normal: CrateriaCentralNormal, medium: CarderaCentralMedium, hard: CrateriaCentralHard },
    { name: 'East', normal: CrateriaEastNormal, medium: CarderaEastMedium, hard: CrateriaEastHard }
];
