import { readAsArrayBuffer } from '../util';

let socket = null;
let busy = false;

export function webSocket() {
    return socket;
}

export function clearBusy() {
    busy = false;
}

export function createMessage(opcode, operands, space = "SNES") {
    return JSON.stringify({
        "Opcode": opcode,
        "Space": space,
        "Flags": null,
        "Operands": operands
    });
}

export function connect(url) {
    return new Promise(function (resolve, reject) {
        if (busy) {
            reject("BUSY");
        }

        busy = true;
        var client = new WebSocket(url);
        client.onopen = function () {
            socket = client;
            busy = false;
            resolve(client);
        };

        client.onerror = function (err) {
            busy = false;
            reject(err);
        };
    });
}

export function send(msg, noReply = false, timeOut = 1) {
    return new Promise(function (resolve, reject) {
        if (busy) {
            reject("BUSY");
        }

        busy = true;
        socket.send(msg);

        if (noReply) {
            busy = false;
            setTimeout(function () { resolve(true); }, timeOut);
            return;
        } else {
            setTimeout(function () {
                busy = false;
                reject(false);
            }, 10000);
        }

        socket.onmessage = function (event) {
            busy = false;
            resolve(event);
        };

        socket.onerror = function (err) {
            busy = false;
            reject(err);
        };
    });
}

export async function sendBin(msg, size) {
    return new Promise(async function (resolve, reject) {

        if (busy) {
            reject("BUSY");
            return;
        }

        busy = true;
        let outputBuffer = null;

        socket.onmessage = async (event) => {
            try {
                let buf = await readAsArrayBuffer(event.data);
                let arrayBuffer = new Uint8Array(buf);

                if (outputBuffer === null) {
                    outputBuffer = arrayBuffer;
                } else {
                    let tmpBuffer = new Uint8Array(outputBuffer.byteLength + arrayBuffer.byteLength);
                    for (let i = 0; i < tmpBuffer.byteLength; ++i) {
                        tmpBuffer[i] = (i < outputBuffer.byteLength) ? outputBuffer[i] : arrayBuffer[i - outputBuffer.byteLength];
                    }
                    outputBuffer = tmpBuffer;
                }

                if (outputBuffer.byteLength === size) {
                    busy = false;
                    resolve(outputBuffer);
                }
            } catch (err) {
                busy = false;
                reject(err);
            }
        };

        socket.onerror = function (err) {
            busy = false;
            reject(err);
        };

        socket.send(msg);
    });
}

export async function runCmd(data) {
    return new Promise(async function (resolve, reject) {
        try {
            let size = data.byteLength.toString(16);
            let message = createMessage("PutAddress", ["2C01", size, "2C00", "1"], "CMD");
            let ok = await send(message, true);
            if (ok) {
                let newArray = Array.from(data);
                newArray.push(0xEA);
                ok = await send(new Blob([new Uint8Array(newArray)]), true);
                if (ok) {
                    resolve(true);
                } else {
                    reject("Error while sending binary data for command");
                }
            } else {
                reject("Error during PutAddress for command");
            }
        } catch (err) {
            reject(err);
        }
    });
}

export async function readData(address, size) {
    return new Promise(async function (resolve, reject) {
        try {
            let message = createMessage("GetAddress", [address.toString(16), size.toString(16)]);
            let response = await sendBin(message, size);
            resolve(response);
        }
        catch (err) {
            reject("Could not read data from device", err);
        }
    });
}

export async function writeData(address, data) {
    return new Promise(async function (resolve, reject) {
        try {
            let size = data.byteLength.toString(16);
            let message = createMessage("PutAddress", [address.toString(16), size]);
            let ok = await send(message, true);
            if (ok) {
                try {
                    ok = await send(new Blob([data]), true, 10);
                    if (ok) {
                        resolve(true);
                    }
                    else {
                        reject("Error sending binary data");
                    }
                } catch (err) {
                    reject("Error sending binary data");
                }
            } else {
                reject("Error in PutAddress request");
            }
        }
        catch (err) {
            reject("Could not write data to usb2snes device", err);
        }
    });
}

/* Helper function for RAM writes, converts write to putCmd if on console */
export async function writeRam(address, data, snes = true) {
    return new Promise(async function (resolve, reject) {
        try {
            if (snes) {
                let opcodes = [0x48, 0x08, 0xC2, 0x30]; // PHA : PHP : REP #$30
                for (let i = 0; i < data.byteLength; i += 2) {
                    if (data.byteLength - i === 1) {
                        opcodes = opcodes.concat([0xE2, 0x20]);    // SEP #$20
                        opcodes = opcodes.concat([0xA9, data[i]]); // LDA #$xx
                        let target = (address + 0x7E0000) + i;
                        opcodes = opcodes.concat([0x8F, (target & 0xFF), (target >> 8) & 0xFF, (target >> 16) & 0xFF]); // STA.L $xxyyzz
                        opcodes = opcodes.concat([0xC2, 0x30]); // REP #$30
                    } else {
                        opcodes = opcodes.concat([0xA9, data[i], data[i + 1]]); //LDA #$xxyy
                        let target = (address + 0x7E0000) + i;
                        opcodes = opcodes.concat([0x8F, (target & 0xFF), (target >> 8) & 0xFF, (target >> 16) & 0xFF]); // STA.L $xxyyzz
                    }
                }
                opcodes = opcodes.concat([0x9C, 0x00, 0x2C, 0x28, 0x68, 0x6C, 0xEA, 0xFF]); // STZ $2C00 : PLA : PLP : JMP ($FFEA)
                console.log(new Uint8Array(opcodes));
                let ok = await runCmd(new Uint8Array(opcodes));
                resolve(ok);
            } else {
                let ok = await writeData(address + 0xF50000, data);
                resolve(ok);
            }
        }
        catch (err) {
            reject("Could not write data to usb2snes device:" + err);
        }
    });
}
