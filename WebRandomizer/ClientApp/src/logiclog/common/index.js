import raw from "raw.macro";
const Aliases = raw('./aliases.md');
const PortalsNormal = raw('./portalsNormal.md');
const PortalsHard = raw('./portalsHard.md');

export default [
    { name: 'Aliases', normal: Aliases },
    { name: 'Portals', normal: PortalsNormal, hard: PortalsHard },
];
