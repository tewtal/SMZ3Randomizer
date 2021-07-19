import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const Catfish = raw('./Catfish.md');
const Pyramid = raw('./Pyramid.md');
const PyramidFairy = raw('./PyramidFairy.md');

export default function ({ Markdown }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={Catfish} />
        <Markdown text={Pyramid} />
        <Markdown text={PyramidFairy} />
    </>;
}
