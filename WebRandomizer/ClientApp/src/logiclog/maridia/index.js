import raw from "raw.macro";
const MaridiaOuterNormal = raw('./MaridiaOuterNormal.md');
const MaridiaOuterHard = raw('./MaridiaOuterHard.md');
const MaridiaInnerNormal = raw('./MaridiaInnerNormal.md');
const MaridiaInnerHard = raw('./MaridiaInnerHard.md');

export default [
    { name: 'Outer', normal: MaridiaOuterNormal, hard: MaridiaOuterHard },
    { name: 'Inner', normal: MaridiaInnerNormal, hard: MaridiaInnerHard }
];
