import raw from "raw.macro";
const AliasesNormal = raw('./aliases.md');
const AliasesMedium = raw('./aliasesMedium.md');
const AliasesHard = raw('./aliasesHard.md');
const PortalsNormal = raw('./portalsNormal.md');
const PortalsMedium = raw('./portalsMedium.md');
const PortalsHard = raw('./portalsHard.md');

export default [
    { name: 'Aliases', normal: Aliases, medium: AliasesMedium, hard: AliasesHard },
    { name: 'Portals', normal: PortalsNormal, medium: PortalsMedium, hard: PortalsHard },
];
