export const readAsArrayBuffer = async (blob) => {
    const fileReader = new FileReader();
    return new Promise((resolve, reject) => {
        fileReader.onerror = () => {
            fileReader.abort();
            reject(new DOMException("Error parsing data"));
        };

        fileReader.onload = (e) => {
            resolve(e.target.result);
        };

        fileReader.readAsArrayBuffer(blob);
    });
};

export const mergeRoms = (sm_rom, z3_rom) => {
    const data = new Uint8Array(0x600000);

    let pos = 0;
    for (let i = 0; i < 0x40; i++) {
        let hi_bank = sm_rom.slice((i * 0x8000), (i * 0x8000) + 0x8000);
        let lo_bank = sm_rom.slice(((i + 0x40) * 0x8000), ((i + 0x40) * 0x8000) + 0x8000);

        data.set(lo_bank, pos);
        data.set(hi_bank, pos + 0x8000);
        pos += 0x10000;
    }

    pos = 0x400000;
    for (let i = 0; i < 0x20; i++) {
        let hi_bank = z3_rom.slice((i * 0x8000), (i * 0x8000) + 0x8000);
        data.set(hi_bank, pos + 0x8000);
        pos += 0x10000;
    }

    return new Blob([data]);
};

export const applyIps = (rom, patch) => {
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
};

export const applySeed = (rom, patch) => {
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
};
