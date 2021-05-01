import raw from "raw.macro";
const MaridiaOuterNormal = raw('./MaridiaOuterNormal.md');
const MaridiaOuterMedium = raw('./MaridiaOuterMedium.md');
const MaridiaOuterHard = raw('./MaridiaOuterHard.md');
const MaridiaInnerNormal = raw('./MaridiaInnerNormal.md');
const MaridiaInnerMedium = raw('./MaridiaInnerMedium.md');
const MaridiaInnerHard = raw('./MaridiaInnerHard.md');

export default [
    { name: 'Outer', normal: MaridiaOuterNormal, medium: MaridiaOuterMedioum, hard: MaridiaOuterHard },
    { name: 'Inner', normal: MaridiaInnerNormal, medium: MaridiaInnerMedioum, hard: MaridiaInnerHard }
];
