import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const Sanctuary = raw('./Sanctuary.md');
const SewersSecretRoom = raw('./SewersSecretRoom.md');
const SewersDarkCross = raw('./SewersDarkCross.md');
const HyruleCastleMapChest = raw('./HyruleCastleMapChest.md');
const HyruleCastleFront = raw('./HyruleCastleFront.md');
const SecretPassage = raw('./SecretPassage.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={Sanctuary} />
        <Markdown text={SewersSecretRoom} />
        <Markdown text={SewersDarkCross} />
        <Markdown text={HyruleCastleMapChest} />
        <Markdown text={HyruleCastleFront} />
        <Markdown text={SecretPassage} />
    </>;
}
