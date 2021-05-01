﻿import raw from "raw.macro";
const NorfairLowerWestNormal = raw('./NorfairLowerWestNormal.md');
const NorfairLowerWestMedium = raw('./NorfairLowerWestMedium.md');
const NorfairLowerWestHard = raw('./NorfairLowerWestHard.md');
const NorfairLowerEastNormal = raw('./NorfairLowerEastNormal.md');
const NorfairLowerEastMedium = raw('./NorfairLowerEastMedium.md');
const NorfairLowerEastHard = raw('./NorfairLowerEastHard.md');

export default [
    { name: 'West', normal: NorfairLowerWestNormal, medium: NorfairLowerWestMedium, hard: NorfairLowerWestHard },
    { name: 'East', normal: NorfairLowerEastNormal, medium: NorfairLowerEastMedium, hard: NorfairLowerEastHard }
];
