import raw from "raw.macro";
const BrinstarBlueNormal = raw('./BrinstarBlueNormal.md');
const BrinstarBlueHard = raw('./BrinstarBlueHard.md');
const BrinstarGreenNormal = raw('./BrinstarGreenNormal.md');
const BrinstarGreenHard = raw('./BrinstarGreenHard.md');
const BrinstarPinkNormal = raw('./BrinstarPinkNormal.md');
const BrinstarPinkHard = raw('./BrinstarPinkHard.md');
const BrinstarRedNormal = raw('./BrinstarRedNormal.md');
const BrinstarRedHard = raw('./BrinstarRedHard.md');
const BrinstarKraid = raw('./BrinstarKraid.md');

export default [
    { name: 'Blue', normal: BrinstarBlueNormal, hard: BrinstarBlueHard },
    { name: 'Green', normal: BrinstarGreenNormal, hard: BrinstarGreenHard },
    { name: 'Pink', normal: BrinstarPinkNormal, hard: BrinstarPinkHard },
    { name: 'Red', normal: BrinstarRedNormal, hard: BrinstarRedHard },
    { name: 'Kraid', normal: BrinstarKraid }
];
