import React from 'react';
import Markdown from './Markdown';

const homeMarkdown =
`
## Super Metroid Randomizer

To generate a Super Metroid seed, go to the [Generate game](/configure) page.

`;

export default function SmHome() {
    return (
        <Markdown text={homeMarkdown} />
    );
}
