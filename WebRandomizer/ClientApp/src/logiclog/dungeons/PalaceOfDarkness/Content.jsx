import React from 'react';

import raw from 'raw.macro';
const CanEnter = raw('./CanEnter.md');
const ShooterRoom = raw('./ShooterRoom.md');
const BigKeyChest = {
    '': raw('./BigKeyChest/Regular.md'),
    Ks: raw('./BigKeyChest/Ks.md'),
};
const CenterRooms = raw('./CenterRooms.md');
const EastWing = raw('./EastWing.md');
const CompassChest = {
    '': raw('./CompassChest/Regular.md'),
    Ks: raw('./CompassChest/Ks.md'),
};
const HarmlessHellway = {
    '': raw('./HarmlessHellway/Regular.md'),
    Ks: raw('./HarmlessHellway/Ks.md'),
};
const DarkBasement = {
    '': raw('./DarkBasement/Regular.md'),
    Ks: raw('./DarkBasement/Ks.md'),
};
const DarkMaze = {
    '': raw('./DarkMaze/Regular.md'),
    Ks: raw('./DarkMaze/Ks.md'),
};
const BigChest = {
    '': raw('./BigChest/Regular.md'),
    Ks: raw('./BigChest/Ks.md'),
};
const HelmasaurKing = raw('./HelmasaurKing.md');

export default function ({ Markdown, mode: { keysanity } }) {
    return <>
        <Markdown text={CanEnter} />
        <Markdown text={ShooterRoom} />
        <Markdown text={BigKeyChest[keysanity]} />
        <Markdown text={CenterRooms} />
        <Markdown text={EastWing} />
        <Markdown text={CompassChest[keysanity]} />
        <Markdown text={HarmlessHellway[keysanity]} />
        <Markdown text={DarkBasement[keysanity]} />
        <Markdown text={DarkMaze[keysanity]} />
        <Markdown text={BigChest[keysanity]} />
        <Markdown text={HelmasaurKing} />
    </>;
}
