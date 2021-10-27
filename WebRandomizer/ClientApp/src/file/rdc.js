import { snesToPc } from './util';

import isPlainObject from 'lodash/isPlainObject';
import isArray from 'lodash/isArray';

/* [{ address mode: [snes addresses], .. } | [snes addresses], length, entries = 1, entry offset = 0] */

const linkManifest = [
    [[0x108000], 0x7000], // sprite
    [[0x1BD308], 4 * 30], // palette
    [[0x1BEDF5], 4]       // gloves
];

const samusLoaderOffsets = [0x0, 0x24, 0x4F, 0x73, 0x9E, 0xC2, 0xED, 0x111, 0x139];
const samusManifest = [
    [{ exhirom: [0x440000], lorom: [0x9C8000] }, 0x8000], // DMA bank 1
    [{ exhirom: [0x450000], lorom: [0x9D8000] }, 0x8000], // DMA bank 2
    [{ exhirom: [0x460000], lorom: [0x9E8000] }, 0x8000], // DMA bank 3
    [{ exhirom: [0x470000], lorom: [0x9F8000] }, 0x8000], // DMA bank 4
    [{ exhirom: [0x480000], lorom: [0xF58000] }, 0x8000], // DMA bank 5
    [{ exhirom: [0x490000], lorom: [0xF68000] }, 0x8000], // DMA bank 6
    [{ exhirom: [0x4A0000], lorom: [0xF78000] }, 0x8000], // DMA bank 7
    [{ exhirom: [0x4B0000], lorom: [0xF88000] }, 0x8000], // DMA bank 8
    [{ exhirom: [0x540000], lorom: [0xF98000] }, 0x8000], // DMA bank 9
    [{ exhirom: [0x550000], lorom: [0xFA8000] }, 0x8000], // DMA bank 10
    [{ exhirom: [0x560000], lorom: [0xFB8000] }, 0x8000], // DMA bank 11
    [{ exhirom: [0x570000], lorom: [0xFC8000] }, 0x8000], // DMA bank 12
    [{ exhirom: [0x580000], lorom: [0xFD8000] }, 0x8000], // DMA bank 13
    [{ exhirom: [0x590000], lorom: [0xFE8000] }, 0x7880], // DMA bank 14
    [{ exhirom: [0x5A0000], lorom: [0xFF8000] }, 0x3F60], // Death DMA left
    [{ exhirom: [0x5A4000], lorom: [0xFFC000] }, 0x3F60], // Death DMA right
    [[0x9A9A00], 0x3C0],        // Gun port data
    [[0xB6DA00], 0x600],        // File select sprites
    [[0xB6D900], 0x20],         // File select missile
    [[0xB6D980], 0x20],         // File select missile head
    [[0x9B9402], 30],           // Power Standard
    [[0x9B9522], 30],           // Varia Standard
    [[0x9B9802], 30],           // Gravity Standard
    [[0x8DDB6D], 30, 9, samusLoaderOffsets], // Power Loader
    [[0x8DDCD3], 30, 9, samusLoaderOffsets], // Varia Loader
    [[0x8DDE39], 30, 9, samusLoaderOffsets], // Gravity Loader
    [[0x8DE468], 30, 16, 0x22], // Power Heat
    [[0x8DE694], 30, 16, 0x22], // Varia Heat
    [[0x8DE8C0], 30, 16, 0x22], // Gravity Heat
    [[0x9B9822], 30, 8, 0x20],  // Power Charge
    [[0x9B9922], 30, 8, 0x20],  // Varia Charge
    [[0x9B9A22], 30, 8, 0x20],  // Gravity Charge
    [[0x9B9B22], 30, 4, 0x20],  // Power Speed boost
    [[0x9B9D22], 30, 4, 0x20],  // Varia Speed boost
    [[0x9B9F22], 30, 4, 0x20],  // Gravity Speed boost
    [[0x9B9BA2], 30, 4, 0x20],  // Power Speed squat
    [[0x9B9DA2], 30, 4, 0x20],  // Varia Speed squat
    [[0x9B9FA2], 30, 4, 0x20],  // Gravity Speed squat
    [[0x9B9C22], 30, 4, 0x20],  // Power Shinespark
    [[0x9B9E22], 30, 4, 0x20],  // Varia Shinespark
    [[0x9BA022], 30, 4, 0x20],  // Gravity Shinespark
    [[0x9B9CA2], 30, 4, 0x20],  // Power Screw attack
    [[0x9B9EA2], 30, 4, 0x20],  // Varia Screw attack
    [[0x9BA0A2], 30, 4, 0x20],  // Gravity Screw attack
    [[0x9B96C2], 30, 6, 0x20],  // Crystal flash
    [[0x9BA122], 30, 9, 0x20],  // Death
    [[0x9BA242], 30, 10, 0x20], // Hyper beam
    [[0x9BA3A2, 0x8CE56B], 30], // Sepia
    [[0x9BA382], 30],           // Sepia hurt
    [[0x9BA3C0, 0x9BA3C6], 6],  // Xray
    [[0x82E52C], 2],            // Door Visor
    [[0x8EE5E2], 30],           // File select
    [[0x8CE68B], 30],           // Ship Intro
    [[0x8DD6C2], 30, 16, 0x24], // Ship Outro
    [[0xA2A5A0], 28],           // Ship Standard
    [[0x8DCA54], 2, 14, 0x6]    // Ship Glow
];

const blockEntries = {
    1: ['link_sprite', linkManifest],
    4: ['samus_sprite', samusManifest]
};

export function parseRdc(rdc) {
    const little = true;
    const utf8 = new TextDecoder();
    const version = 1;
    if (utf8.decode(rdc.slice(0, 18)) !== 'RETRODATACONTAINER')
        throw new Error("Could not find the RDC format header");
    if (rdc[18] !== version)
        throw new Error(`RDC version ${rdc[18]} is not supported, expected version ${version}`);

    let offset = 19;
    const view = new DataView(rdc.buffer);
    const blockList = [];
    let blocks = view.getUint32(offset, little);
    while (blocks > 0) {
        blocks -= 1;
        const blockType = view.getUint32(offset += 4, little);
        const blockOffset = view.getUint32(offset += 4, little);
        blockList.push([blockType, blockOffset]);
    }

    let field = new Uint8Array(rdc.buffer, offset += 4);
    let end = field.findIndex(x => x === 0);
    if (end < 0)
        throw new Error("Missing null terminator for the Author data field");
    const author = utf8.decode(field.slice(0, end));

    return [processBlocks(rdc, blockList), author];
}

function processBlocks(rdc, blockList) {
    const list = {};
    blockList.forEach(([type, offset]) => {
        const entry = blockEntries[type];
        if (entry) {
            const [name, manifest] = entry;
            const block = new Uint8Array(rdc.buffer, offset);
            const content = parseBlock(block, manifest);
            list[name] = (rom, mode) => applyBlock(rom, mode, content, manifest);
        }
    });
    return list;
}

function parseBlock(block, manifest) {
    let offset = 0;
    const content = [];
    for (const [, length, entries = 1] of manifest) {
        content.push(block.slice(offset, offset + entries * length));
        offset += entries * length;
    }
    return content;
}

function applyBlock(rom, mapping, content, manifest) {
    let index = -1;
    for (const [addrs, length, entries = 1, offset = 0] of manifest) {
        const _addrs = isPlainObject(addrs) ? addrs[mapping] : addrs;
        const entry = content[index += 1];
        for (const addr of _addrs) {
            for (let i = 0; i < entries; i += 1) {
                const dest = snesToPc(mapping, addr + (isArray(offset) ? offset[i] : offset * i));
                const src = length * i;
                rom.set(entry.slice(src, src + length), dest);
            }
        }
    }
}
