import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const MasterSwordPedestal = raw('./MasterSwordPedestal.md');
const LostWoods = raw('./LostWoods.md');
const LumberjackTree = raw('./LumberjackTree.md');
const PegasusRocks = raw('./PegasusRocks.md');
const GraveyardLedge = raw('./GraveyardLedge.md');
const KingsTomb = raw('./KingsTomb.md');
const KakarikoVillage = raw('./KakarikoVillage.md');
const SickKid = raw('./SickKid.md');
const MagicBat = raw('./MagicBat.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={MasterSwordPedestal} />
        <Markdown text={LostWoods} />
        <Markdown text={LumberjackTree} />
        <Markdown text={PegasusRocks} />
        <Markdown text={GraveyardLedge} />
        <Markdown text={KingsTomb} />
        <Markdown text={KakarikoVillage} />
        <Markdown text={SickKid} />
        <Markdown text={MagicBat} />
    </>;
}
