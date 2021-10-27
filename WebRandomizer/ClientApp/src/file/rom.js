import { readAsArrayBuffer, snesToPc } from './util';
import { parseRdc } from './rdc';
import { bigText } from '../snes/big_text_table';

import { inflate } from 'pako';
import localForage from 'localforage';

import each from 'lodash/each';
import range from 'lodash/range';
import defaultTo from 'lodash/defaultTo';
import isPlainObject from 'lodash/isPlainObject';

const legalCharacters = /[A-Z0-9]/;
const illegalCharacters = /[^A-Z0-9]/g;
const continousSpace = / +/g;

export async function prepareRom(worldPatch, settings, baseIps, game) {
    let rom = null;
    if (game.smz3) {
        const smRom = await readAsArrayBuffer(await localForage.getItem("baseRomSM"));
        const lttpRom = await readAsArrayBuffer(await localForage.getItem("baseRomLTTP"));
        rom = mergeRoms(new Uint8Array(smRom), new Uint8Array(lttpRom));
    } else {
        const baseRom = await readAsArrayBuffer(await localForage.getItem("baseRomSM"));
        /* extend to 4 mb to account for the base patch with custom sprites */
        rom = new Uint8Array(new ArrayBuffer(0x400000));
        rom.set(new Uint8Array(baseRom));
    }
    const basePatch = maybeCompressed(new Uint8Array(await (await fetch(baseIps, { cache: 'no-store' })).arrayBuffer()));
    worldPatch = Uint8Array.from(atob(worldPatch), c => c.charCodeAt(0));

    const { mapping } = game;
    applyIps(rom, basePatch);

    if (game.z3) {
        await applySprite(rom, mapping, 'link_sprite', settings.z3Sprite);
    }
    await applySprite(rom, mapping, 'samus_sprite', settings.smSprite);
    if (settings.smSpinjumps) {
        smSpinjumps(rom, mapping);
    }

    if (game.z3) {
        z3HeartColor(rom, mapping, settings.z3HeartColor);
        z3HeartBeep(rom, settings.z3HeartBeep);
    }
    if (!settings.smEnergyBeep) {
        smEnergyBeepOff(rom, mapping);
    }

    applySeed(rom, worldPatch);

    return rom;
}

async function applySprite(rom, mapping, block, sprite) {
    if (sprite.path) {
        const url = `${process.env.PUBLIC_URL}/sprites/${sprite.path}`;
        const rdc = maybeCompressed(new Uint8Array(await (await fetch(url)).arrayBuffer()));
        const [blocks, author] = parseRdc(rdc);
        blocks[block] && blocks[block](rom, mapping);
        applySpriteAuthor(rom, mapping, block, author);
    }
}

function applySpriteAuthor(rom, mapping, block, author) {
    author = author.toUpperCase();
    /* Author field that is empty or has no accepted characters */
    if (!author.match(legalCharacters))
        return;

    author = formatAuthor(author);
    const width = 32;
    const pad = (width - author.length) >> 1; /* shift => div + floor */

    const addrs = {
        link_sprite: [0xF47002, 0xFD1480],
        samus_sprite: { exhirom: [0xF47004, 0xFD1600], lorom: [0xCEFF02, 0xCEC740] },
    }[block];
    const [enable, tilemap] = isPlainObject(addrs) ? addrs[mapping] : addrs;

    rom[snesToPc(mapping, enable)] = 0x01;
    each(author, (char, i) => {
        const bytes = bigText[char];
        rom[snesToPc(mapping, tilemap + 2 * (pad + i))] = bytes[0];
        rom[snesToPc(mapping, tilemap + 2 * (pad + i + 32))] = bytes[1];
    });
}

function formatAuthor(author) {
    author = author.replace(illegalCharacters, ' ');
    author = author.replace(continousSpace, ' ');
    /* Keep at most 30 non-whitespace characters */
    /* A limit of 30 guarantee a margin at the edges */
    return author.trimStart().slice(0, 30).trimEnd();
}

/* Enables separate spinjump behavior */
function smSpinjumps(rom, mapping) {
    rom[snesToPc(mapping, 0x9B93FE)] = 0x01;
}

function z3HeartColor(rom, mapping, setting) {
    const values = {
        red:    [0x24, [0x18, 0x00]],
        yellow: [0x28, [0xBC, 0x02]],
        blue:   [0x2C, [0xC9, 0x69]],
        green:  [0x3C, [0x04, 0x17]]
    };
    const [hud, fileSelect] = defaultTo(values[setting], values.red);

    each(range(0, 20, 2), i => {
        rom[snesToPc(mapping, 0xDFA1E + i)] = hud;
    });

    rom.set(fileSelect, snesToPc(mapping, 0x1BD6AA));
}

function z3HeartBeep(rom, setting) {
    const values = {
        off: 0x00,
        double: 0x10,
        normal: 0x20,
        half: 0x40,
        quarter: 0x80
    };
    /* Redirected to low bank $40 in combo */
    rom[0x400033] = defaultTo(values[setting], values.half);
}

function smEnergyBeepOff(rom, mapping) {
    each([
        [0x90EA9B, 0x80],
        [0x90F337, 0x80],
        [0x91E6D5, 0x80]
    ],
        ([addr, value]) => rom[snesToPc(mapping, addr)] = value
    );
}

function maybeCompressed(data) {
    const big = false;
    const isGzip = new DataView(data.buffer).getUint16(0, big) === 0x1f8b;
    return isGzip ? inflate(data) : data;
}

function mergeRoms(smRom, z3Rom) {
    const rom = new Uint8Array(0x600000);

    let pos = 0;
    for (let i = 0; i < 0x40; i++) {
        let hiBank = smRom.slice((i * 0x8000), (i * 0x8000) + 0x8000);
        let loBank = smRom.slice(((i + 0x40) * 0x8000), ((i + 0x40) * 0x8000) + 0x8000);

        rom.set(loBank, pos);
        rom.set(hiBank, pos + 0x8000);
        pos += 0x10000;
    }

    pos = 0x400000;
    for (let i = 0; i < 0x20; i++) {
        let hiBank = z3Rom.slice((i * 0x8000), (i * 0x8000) + 0x8000);
        rom.set(hiBank, pos + 0x8000);
        pos += 0x10000;
    }

    return rom;
}

function applyIps(rom, patch) {
    const big = false;
    let offset = 5;
    const footer = 3;
    const view = new DataView(patch.buffer);
    while (offset + footer < patch.length) {
        const dest = (patch[offset] << 16) + view.getUint16(offset + 1, big);
        const length = view.getUint16(offset + 3, big);
        offset += 5;
        if (length > 0) {
            rom.set(patch.slice(offset, offset + length), dest);
            offset += length;
        } else {
            const rleLength = view.getUint16(offset, big);
            const rleByte = patch[offset + 2];
            rom.set(Uint8Array.from(new Array(rleLength), () => rleByte), dest);
            offset += 3;
        }
    }
}

function applySeed(rom, patch) {
    const little = true;
    let offset = 0;
    const view = new DataView(patch.buffer);
    while (offset < patch.length) {
        let dest = view.getUint32(offset, little);
        let length = view.getUint16(offset + 4, little);
        offset += 6;
        rom.set(patch.slice(offset, offset + length), dest);
        offset += length;
    }
}
