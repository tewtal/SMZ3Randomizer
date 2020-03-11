import { readAsArrayBuffer } from '../file/util';
import { parse_rdc } from '../file/rdc';
import { bigText } from '../snes/big_text_table';
import { inflate } from 'pako';
import each from 'lodash/each';
import localForage from 'localforage';

export async function prepareRom(world_patch, settings, baseIps, gameId) {
    let rom = null;
    if (gameId === "sm") {
        const base_rom = await readAsArrayBuffer(await localForage.getItem("baseRomSM"));
        /* extend to 4 mb to account for the base patch with custom sprites */
        rom = new Uint8Array(new ArrayBuffer(0x400000));
        rom.set(new Uint8Array(base_rom));
    } else {
        const smRom = await readAsArrayBuffer(await localForage.getItem("baseRomSM"));
        const lttpRom = await readAsArrayBuffer(await localForage.getItem("baseRomLTTP"));
        rom = mergeRoms(new Uint8Array(smRom), new Uint8Array(lttpRom));
    }
    const base_patch = maybeCompressed(new Uint8Array(await (await fetch(baseIps, { cache: 'no-store' })).arrayBuffer()));
    world_patch = Uint8Array.from(atob(world_patch), c => c.charCodeAt(0));

    applyIps(rom, base_patch);
    if (gameId === "smz3") {
        await applySprite(rom, 'link_sprite', settings.z3Sprite);
        await applySprite(rom, 'samus_sprite', settings.smSprite);
        if (settings.spinjumps) {
            enableSeparateSpinjumps(rom);
        }
    }
    applySeed(rom, world_patch);

    return rom;
}

function enableSeparateSpinjumps(rom) {
    rom[0x34F500] = 0x01;
}

async function applySprite(rom, block, sprite) {
    if (sprite.path) {
        const url = `${process.env.PUBLIC_URL}/sprites/${sprite.path}`;
        const rdc = maybeCompressed(new Uint8Array(await (await fetch(url)).arrayBuffer()));
        // Todo: do something with the author field
        const [blocks, author] = parse_rdc(rdc);
        blocks[block] && blocks[block](rom);
        applySpriteAuthor(rom, block, author);
    }
}

function applySpriteAuthor(rom, block, author) {
    author = author.toUpperCase();
    /* Replace non-alphanum with space */
    author = author.replace(/[^A-Z0-9]/, ' ');
    /* Author field that is empty or has no accepted characters */
    if (!author.match(/[A-Z0-9]/))
        return;
    /* Normalize spaces */
    author = author.replace(/ +/, ' ');
    /* Keep at most 30 non-whitespace characters */
    /* A limit of 30 guarantee a margin at the edges */
    author = author.trimStart().slice(0, 30).trimEnd();

    const center = 16 - ((author.length + 1) >> 1);
    const [enable, tilemap] = {
        link_sprite: [0x347002, 0x3D1480],
        samus_sprite: [0x347004, 0x3D1600],
    }[block];

    rom[enable] = 0x01;
    each(author, (char, i) => {
        const bytes = bigText[char];
        rom[tilemap + 2 * (center + i)] = bytes[0];
        rom[tilemap + 2 * (center + i + 32)] = bytes[1];
    });
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

function applySeed (rom, patch) {
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
