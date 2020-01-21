import CrateriaWestNormal from '!!raw-loader!./CrateriaWestNormal.md';
import CrateriaWestHard from '!!raw-loader!./CrateriaWestHard.md';
import CrateriaCentralNormal from '!!raw-loader!./CrateriaCentralNormal.md';
import CrateriaCentralHard from '!!raw-loader!./CrateriaCentralHard.md';
import CrateriaEastNormal from '!!raw-loader!./CrateriaEastNormal.md';
import CrateriaEastHard from '!!raw-loader!./CrateriaEastHard.md';

export default [
    { name: 'West', normal: CrateriaWestNormal, hard: CrateriaWestHard },
    { name: 'Central', normal: CrateriaCentralNormal, hard: CrateriaCentralHard },
    { name: 'East', normal: CrateriaEastNormal, hard: CrateriaEastHard }
];
