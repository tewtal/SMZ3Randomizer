import React from 'react';
import Markdown from './Markdown';

const homeMarkdown =
`
## Super Metroid and A Link to the Past Randomizer

To generate a Combo Rando seed, go to the [Configure SMZ3 Game](/configure/smz3) page.

To generate a Super Metroid seed, go to the [Configure SM Game](/configure/sm) page.

*Please note that this is still in beta and being under constant developement, so things might not work or the site might have some downtime.*

`;

export default function Home() {
    return (
        <Markdown text={homeMarkdown} />
    );
}
