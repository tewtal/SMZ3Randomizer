import crateria from './crateria';
import wreckedship from './wreckedship';
import brinstar from './brinstar';
import maridia from './maridia';
import norfairupper from './norfairupper';
import norfairlower from './norfairlower';
import lightworld from './lightworld';
import darkworld from './darkworld';
import dungeons from './dungeons';
import common from './common';

export default {
    tabs: [
        { name: 'Crateria', tabs: crateria },
        { name: 'Wrecked Ship', markdown: wreckedship },
        { name: 'Brinstar', tabs: brinstar },
        { name: 'Maridia', tabs: maridia },
        { name: 'Upper Norfair', tabs: norfairupper },
        { name: 'Lower Norfair', tabs: norfairlower },
        { name: 'Light World', tabs: lightworld },
        { name: 'Dark World', tabs: darkworld },
        { name: 'Dungeons', tabs: dungeons },
        { name: 'Common', tabs: common }
    ]
};
