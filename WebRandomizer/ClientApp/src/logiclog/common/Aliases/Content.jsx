import React from 'react';

import raw from 'raw.macro';
const CanLiftLight = raw('./CanLiftLight.md');
const CanLiftHeavy = raw('./CanLiftHeavy.md');
const CanLightTorches = raw('./CanLightTorches.md');
const CanMeltFreezors = raw('./CanMeltFreezors.md');
const CanExtendMagic = raw('./CanExtendMagic.md');
const CanKillManyEnemies = raw('./CanKillManyEnemies.md');
const CanIbj = raw('./CanIbj.md');
const CanFly = raw('./CanFly.md');
const CanUsePowerBombs = raw('./CanUsePowerBombs.md');
const CanPassBombPassages = raw('./CanPassBombPassages.md');
const CanDestroyBombWalls = raw('./CanDestroyBombWalls.md');
const CanSpringBallJump = raw('./CanSpringBallJump.md');
const CanHellRun = raw('./CanHellRun.md');
const CanOpenRedDoors = raw('./CanOpenRedDoors.md');

export default function ({ Markdown }) {
    return <>
        <h2>A Link to the Past</h2>
        <Markdown text={CanLiftLight} />
        <Markdown text={CanLiftHeavy} />
        <Markdown text={CanLightTorches} />
        <Markdown text={CanMeltFreezors} />
        <Markdown text={CanExtendMagic} />
        <Markdown text={CanKillManyEnemies} />

        <h2>Super Metroid</h2>
        <Markdown text={CanIbj} />
        <Markdown text={CanFly} />
        <Markdown text={CanUsePowerBombs} />
        <Markdown text={CanPassBombPassages} />
        <Markdown text={CanDestroyBombWalls} />
        <Markdown text={CanSpringBallJump} />
        <Markdown text={CanHellRun} />
        <Markdown text={CanOpenRedDoors} />
    </>;
}
