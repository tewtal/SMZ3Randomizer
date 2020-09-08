const domainPattern = /^([^.]*).?/;
/* Does not include smz3 because in that case the root domain is used directly */
const domains = ['sm'];
const domainBy = { sm: 'sm.', smz3: '' };

export function resolveGameId(href) {
    const url = new URL(href);
    const hostname = url.hostname;
    const [,id] = hostname.match(domainPattern);
    return domains.includes(id) ? id : 'smz3';
}

export function adjustHostname(href, id) {
    const url = new URL(href);
    const hostname = url.hostname;
    url.hostname = `${domainBy[id]}${hostname.replace(domainPattern, removeGameDomain)}`;
    return url.href;
}

function removeGameDomain(match, id) {
    return domains.includes(id) ? '' : match;
}

export function gameServiceHost(href) {
    const url = new URL(href);
    const hostname = url.hostname;
    return hostname.includes('localhost') ? 'localhost:5101' : `svc.${hostname}`;
}
