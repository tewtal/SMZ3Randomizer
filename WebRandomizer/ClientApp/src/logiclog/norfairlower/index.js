import raw from "raw.macro";
const NorfairLowerWestNormal = raw('./NorfairLowerWestNormal.md');
const NorfairLowerWestHard = raw('./NorfairLowerWestHard.md');
const NorfairLowerEastNormal = raw('./NorfairLowerEastNormal.md');
const NorfairLowerEastHard = raw('./NorfairLowerEastHard.md');

export default [
    { name: 'West', normal: NorfairLowerWestNormal, hard: NorfairLowerWestHard },
    { name: 'East', normal: NorfairLowerEastNormal, hard: NorfairLowerEastHard }
];
