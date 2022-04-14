import indexOf from 'lodash/indexOf';

const ordering = [
    'Light World Death Mountain West',
    'Light World Death Mountain East',
    'Light World North West',
    'Light World North East',
    'Light World South',
    'Hyrule Castle',
    'Dark World Death Mountain West',
    'Dark World Death Mountain East',
    'Dark World North West',
    'Dark World North East',
    'Dark World South',
    'Dark World Mire',

    'Castle Tower',

    'Eastern Palace',
    'Desert Palace',
    'Tower of Hera',
    'Palace of Darkness',
    'Swamp Palace',
    'Skull Woods',
    "Thieves' Town",
    'Ice Palace',
    'Misery Mire',
    'Turtle Rock',
    "Ganon's Tower",

    'Crateria West',
    'Crateria Central',
    'Crateria East',
    'Brinstar Blue',
    'Brinstar Green',
    'Brinstar Pink',
    'Brinstar Red',
    'Brinstar Kraid',
    'Wrecked Ship',
    'Maridia Outer',
    'Maridia Inner',
    'Norfair Upper West',
    'Norfair Upper East',
    'Norfair Upper Crocomire',
    'Norfair Lower West',
    'Norfair Lower East',
];

export function regionOrdering(name) {
    return indexOf(ordering, name);
}
