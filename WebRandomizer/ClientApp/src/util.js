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

export function tryParseJson(text) {
    try {
        return JSON.parse(text);
    } catch (syntaxerror) {
        return null;
    }
}
