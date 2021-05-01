import raw from "raw.macro";
const BrinstarBlueNormal = raw('./BrinstarBlueNormal.md');
const BrinstarBlueMedium = raw('./BrinstarBlueMedium.md');
const BrinstarBlueHard = raw('./BrinstarBlueHard.md');
const BrinstarGreenNormal = raw('./BrinstarGreenNormal.md');
const BrinstarGreenMedium = raw('./BrinstarGreenMedium.md');
const BrinstarGreenHard = raw('./BrinstarGreenHard.md');
const BrinstarPinkNormal = raw('./BrinstarPinkNormal.md');
const BrinstarPinkMedium = raw('./BrinstarPinkMedium.md');
const BrinstarPinkHard = raw('./BrinstarPinkHard.md');
const BrinstarRedNormal = raw('./BrinstarRedNormal.md');
const BrinstarRedMedium = raw('./BrinstarRedMedium.md');
const BrinstarRedHard = raw('./BrinstarRedHard.md');
const BrinstarKraid = raw('./BrinstarKraid.md');

export default [
    { name: 'Blue', normal: BrinstarBlueNormal, medium: BrinstarBlueMedium, hard: BrinstarBlueHard },
    { name: 'Green', normal: BrinstarGreenNormal, medium: BrinstarGreenMedium, hard: BrinstarGreenHard },
    { name: 'Pink', normal: BrinstarPinkNormal, medium: BrinstarPinkMedium, hard: BrinstarPinkHard },
    { name: 'Red', normal: BrinstarRedNormal, medium: BrinstarRedMedium, hard: BrinstarRedHard },
    { name: 'Kraid', normal: BrinstarKraid }
];
