export async function readAsArrayBuffer(blob) {
    const fileReader = new FileReader();
    return new Promise((resolve, reject) => {
        fileReader.onerror = () => {
            fileReader.abort();
            reject(new DOMException('Error parsing data'));
        };

        fileReader.onload = (e) => {
            resolve(e.target.result);
        };

        fileReader.readAsArrayBuffer(blob);
    });
}

export function snes_to_pc(mode = {}, addr) {
    if (mode.exhirom) {
        const ex = addr < 0x800000 ? 0x400000 : 0;
        const pc = addr & 0x3FFFFF;
        return ex | pc;
    }
    if (mode.lorom) {
        return ((addr & 0x7F0000) >>> 1) | (addr & 0x7FFF);
    }
    throw new Error('No known addressing mode supplied');
}
