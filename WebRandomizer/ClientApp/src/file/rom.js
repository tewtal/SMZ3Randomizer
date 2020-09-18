import { readAsArrayBuffer, snes_to_pc } from './util';
import { parse_rdc } from './rdc';
import { bigText } from '../snes/big_text_table';

import { inflate } from 'pako';
import localForage from 'localforage';

import each from 'lodash/each';
import range from 'lodash/range';
import defaultTo from 'lodash/defaultTo';
import isPlainObject from 'lodash/isPlainObject';

export async function prepareRom(world_patch, settings, baseIps, game) {
    let rom = null;
    if (game.smz3) {
        const smRom = await readAsArrayBuffer(await localForage.getItem("baseRomSM"));
        const lttpRom = await readAsArrayBuffer(await localForage.getItem("baseRomLTTP"));
        rom = mergeRoms(new Uint8Array(smRom), new Uint8Array(lttpRom));
    } else {
        const base_rom = await readAsArrayBuffer(await localForage.getItem("baseRomSM"));
        /* extend to 4 mb to account for the base patch with custom sprites */
        rom = new Uint8Array(new ArrayBuffer(0x400000));
        rom.set(new Uint8Array(base_rom));
    }
    const base_patch = maybeCompressed(new Uint8Array(await (await fetch(baseIps, { cache: 'no-store' })).arrayBuffer()));
    world_patch = Uint8Array.from(atob(world_patch), c => c.charCodeAt(0));

    const { mapping } = game;
    applyIps(rom, base_patch);

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

    applySeed(rom, world_patch);

    return rom;
}

async function applySprite(rom, mapping, block, sprite) {
    if (sprite.path) {
        const url = `${process.env.PUBLIC_URL}/sprites/${sprite.path}`;
        const rdc = maybeCompressed(new Uint8Array(await (await fetch(url)).arrayBuffer()));
        const [blocks, author] = parse_rdc(rdc);
        blocks[block] && blocks[block](rom, mapping);
        applySpriteAuthor(rom, mapping, block, author);
    }
}

function applySpriteAuthor(rom, mapping, block, author) {
    author = author.toUpperCase();
    /* Author field that is empty or has no accepted characters */
    if (!author.match(/[A-Z0-9]/))
        return;

    author = formatAuthor(author);
    const center = 16 - ((author.length + 1) >> 1);

    const addrs = {
        link_sprite: [0xF47002, 0xFD1480],
        samus_sprite: { exhirom: [0xF47004, 0xFD1600], lorom: [0xCEFF02, 0xCEC740] },
    }[block];
    const [enable, tilemap] = isPlainObject(addrs) ? addrs[mapping] : addrs;

    rom[snes_to_pc(mapping, enable)] = 0x01;
    each(author, (char, i) => {
        const bytes = bigText[char];
        rom[snes_to_pc(mapping, tilemap + 2 * (center + i))] = bytes[0];
        rom[snes_to_pc(mapping, tilemap + 2 * (center + i + 32))] = bytes[1];
    });
}

function formatAuthor(author) {
    /* Replace non-alphanum with space */
    author = author.replace(/[^A-Z0-9]/g, ' ');
    /* Normalize spaces */
    author = author.replace(/ +/g, ' ');
    /* Keep at most 30 non-whitespace characters */
    /* A limit of 30 guarantee a margin at the edges */
    return author.trimStart().slice(0, 30).trimEnd();
}

/* Enables separate spinjump behavior */
function smSpinjumps(rom, mapping) {
    rom[snes_to_pc(mapping, 0x9B93FE)] = 0x01;
}

function z3HeartColor(rom, mapping, setting) {
    const values = {
        red:    [0x24, [0x18, 0x00]],
        yellow: [0x28, [0xBC, 0x02]],
        blue:   [0x2C, [0xC9, 0x69]],
        green:  [0x3C, [0x04, 0x17]]
    };
    const [hud, file_select] = defaultTo(values[setting], values.red);

    each(range(0, 20, 2), i => {
        rom[snes_to_pc(mapping, 0xDFA1E + i)] = hud;
    });

    rom.set(file_select, snes_to_pc(mapping, 0x1BD6AA));
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
        ([addr, value]) => rom[snes_to_pc(mapping, addr)] = value
    );
}

function maybeCompressed(data) {
    const big = false;
    const isGzip = new DataView(data.buffer).getUint16(0, big) === 0x1f8b;
    return isGzip ? inflate(data) : data;
}

function mergeRoms(sm_rom, z3_rom) {
    const rom = new Uint8Array(0x600000);

    let pos = 0;
    for (let i = 0; i < 0x40; i++) {
        let hi_bank = sm_rom.slice((i * 0x8000), (i * 0x8000) + 0x8000);
        let lo_bank = sm_rom.slice(((i + 0x40) * 0x8000), ((i + 0x40) * 0x8000) + 0x8000);

        rom.set(lo_bank, pos);
        rom.set(hi_bank, pos + 0x8000);
        pos += 0x10000;
    }

    pos = 0x400000;
    for (let i = 0; i < 0x20; i++) {
        let hi_bank = z3_rom.slice((i * 0x8000), (i * 0x8000) + 0x8000);
        rom.set(hi_bank, pos + 0x8000);
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
            const rle_length = view.getUint16(offset, big);
            const rle_byte = patch[offset + 2];
            rom.set(Uint8Array.from(new Array(rle_length), () => rle_byte), dest);
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
