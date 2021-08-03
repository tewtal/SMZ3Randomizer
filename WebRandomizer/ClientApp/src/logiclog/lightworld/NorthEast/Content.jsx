import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const KingZora = raw('./KingZora.md');
const ZorasDomain = raw('./ZorasDomain.md');
const PotionShop = raw('./PotionShop.md');
const SahasrahlasHut = raw('./SahasrahlasHut.md');
const Sahasrahla = raw('./Sahasrahla.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={KingZora} />
        <Markdown text={ZorasDomain} />
        <Markdown text={PotionShop} />
        <Markdown text={SahasrahlasHut} />
        <Markdown text={Sahasrahla} />
    </>;
}
