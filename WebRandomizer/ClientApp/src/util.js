import sortBy from 'lodash/sortBy';
import groupBy from 'lodash/groupBy';
import toPairs from 'lodash/toPairs';

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

export function sortGroupBy(collection, iteratee) {
    const groups = groupBy(collection, iteratee);
    return sortBy(toPairs(groups), ([key]) => key);
}
