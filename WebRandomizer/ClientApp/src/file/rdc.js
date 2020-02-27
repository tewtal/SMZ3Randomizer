// [[rom addresses], length, entries = 1, entry offset = 0]

const link_manifest = [
    [[0x508000], 0x7000], // sprite
    [[0x5BD308], 4 * 30], // palette
    [[0x5BEDF5], 4]       // gloves
];

const samus_loader_offsets = [0x0, 0x24, 0x4F, 0x73, 0x9E, 0xC2, 0xED, 0x111, 0x139];
const samus_manifest = [
    [[0x440000], 0x8000],       // DMA bank 1
    [[0x450000], 0x8000],       // DMA bank 2
    [[0x460000], 0x8000],       // DMA bank 3
    [[0x470000], 0x8000],       // DMA bank 4
    [[0x480000], 0x8000],       // DMA bank 5
    [[0x490000], 0x8000],       // DMA bank 6
    [[0x4A0000], 0x8000],       // DMA bank 7
    [[0x4B0000], 0x8000],       // DMA bank 8
    [[0x540000], 0x8000],       // DMA bank 9
    [[0x550000], 0x8000],       // DMA bank 10
    [[0x560000], 0x8000],       // DMA bank 11
    [[0x570000], 0x8000],       // DMA bank 12
    [[0x580000], 0x8000],       // DMA bank 13
    [[0x590000], 0x7880],       // DMA bank 14
    [[0x5A0000], 0x3F60],       // Death left
    [[0x5A4000], 0x3F60],       // Death right
    [[0x1A9A00], 0x3C0],        // Gun port data
    [[0x36DA00], 0x600],        // File select sprites
    [[0x36D900], 0x20],         // File select missile
    [[0x36D980], 0x20],         // File select missile head
    [[0x1B9402], 30],           // Power Standard
    [[0x1B9522], 30],           // Varia Standard
    [[0x1B9802], 30],           // Gravity Standard
    [[0xDDB6D], 30, 9, samus_loader_offsets], // Power Loader
    [[0xDDCD3], 30, 9, samus_loader_offsets], // Varia Loader
    [[0xDDE39], 30, 9, samus_loader_offsets], // Gravity Loader
    [[0xDE468], 30, 16, 0x22],  // Power Heat
    [[0xDE694], 30, 16, 0x22],  // Varia Heat
    [[0xDE8C0], 30, 16, 0x22],  // Gravity Heat
    [[0x1B9822], 30, 8, 0x20],  // Power Charge
    [[0x1B9922], 30, 8, 0x20],  // Varia Charge
    [[0x1B9A22], 30, 8, 0x20],  // Gravity Charge
    [[0x1B9B22], 30, 4, 0x20],  // Power Speed boost
    [[0x1B9D22], 30, 4, 0x20],  // Varia Speed boost
    [[0x1B9F22], 30, 4, 0x20],  // Gravity Speed boost
    [[0x1B9BA2], 30, 4, 0x20],  // Power Speed squat
    [[0x1B9DA2], 30, 4, 0x20],  // Varia Speed squat
    [[0x1B9FA2], 30, 4, 0x20],  // Gravity Speed squat
    [[0x1B9C22], 30, 4, 0x20],  // Power Shinespark
    [[0x1B9E22], 30, 4, 0x20],  // Varia Shinespark
    [[0x1BA022], 30, 4, 0x20],  // Gravity Shinespark
    [[0x1B9CA2], 30, 4, 0x20],  // Power Screw attack
    [[0x1B9EA2], 30, 4, 0x20],  // Varia Screw attack
    [[0x1BA0A2], 30, 4, 0x20],  // Gravity Screw attack
    [[0x1B96C2], 30, 6, 0x20],  // Crystal flash
    [[0x1BA122], 30, 9, 0x20],  // Death
    [[0x1BA242], 30, 10, 0x20], // Hyper beam
    [[0x1BA3A2, 0xCE56B], 30],  // Sepia
    [[0x1BA382], 30],           // Sepia hurt
    [[0x1BA3C0, 0x1BA3C6], 6],  // Xray
    [[0x2E52C], 2],             // Door Visor
    [[0xEE5E2], 30],            // File select
    [[0xCE68B], 30],            // Ship Intro
    [[0xDD6C2], 30, 16, 0x24],  // Ship Outro
    [[0x22A5A0], 28],           // Ship Standard
    [[0xDCA54], 2, 14, 0x6]     // Ship Glow
];

const block_entries = {
    1: ['link_sprite', link_manifest],
    4: ['samus_sprite', samus_manifest]
};

export function parse_rdc(rdc) {
    const little = true;
    const utf8 = new TextDecoder();
    const version = 1;
    if (utf8.decode(rdc.slice(0, 18)) !== 'RETRODATACONTAINER')
        throw new Error("Could not find the RDC format header");
    if (rdc[18] !== version)
        throw new Error(`RDC version ${rdc[18]} is not supported, expected version ${version}`);

    let offset = 19;
    const view = new DataView(rdc.buffer);
    const block_list = [];
    let blocks = view.getUint32(offset, little);
    while (blocks > 0) {
        blocks -= 1;
        const block_type = view.getUint32(offset += 4, little);
        const block_offset = view.getUint32(offset += 4, little);
        block_list.push([block_type, block_offset]);
    }

    let field = new Uint8Array(rdc.buffer, offset += 4);
    let end = field.findIndex(x => x === 0);
    if (end < 0)
        throw new Error("Missing null terminator for the Author data field");
    const author = utf8.decode(field.slice(0, end));

    return [process_blocks(rdc, block_list), author];
}

function process_blocks(rdc, block_list) {
    const list = {};
    block_list.forEach(([type, offset]) => {
        const entry = block_entries[type];
        if (entry) {
            const [name, manifest] = entry;
            const block = new Uint8Array(rdc.buffer, offset);
            const content = parse_block(block, manifest);
            list[name] = (rom) => apply_block(rom, content, manifest);
        }
    });
    return list;
}

function parse_block(block, manifest) {
    let offset = 0;
    const content = [];
    for (const [, length, entries = 1] of manifest) {
        content.push(block.slice(offset, offset + entries * length));
        offset += entries * length;
    }
    return content;
}

function apply_block(rom, content, manifest) {
    let index = -1;
    for (const [addrs, length, entries = 1, offset = 0] of manifest) {
        const entry = content[index += 1];
        for (const addr of addrs) {
            for (let i = 0; i < entries; i += 1) {
                const dest = addr + (!Array.isArray(offset) ? offset * i : offset[i]);
                const src = length * i;
                rom.set(entry.slice(src, src + length), dest);
            }
        }
    }
}
