import raw from "raw.macro";
const HyruleCastleNormal = raw('./HyruleCastle.md');
const HyruleCastleNMG = raw('./HyruleCastleNMG.md');
const EasternPalaceNormal = raw('./EasternPalace.md');
const EasternPalaceNMG = raw('./EasternPalaceNMG.md');
const DesertPalace = raw('./DesertPalace.md');
const TowerOfHeraNormal = raw('./TowerOfHera.md');
const TowerOfHeraNMG = raw('./TowerOfHeraNMG.md');
const CastleTowerNormal = raw('./CastleTower.md');
const CastleTowerNMG = raw('./CastleTowerNMG.md');
const PalaceOfDarknessNormal = raw('./PalaceOfDarkness.md');
const PalaceOfDarknessNMG = raw('./PalaceOfDarknessNMG.md');
const SwampPalace = raw('./SwampPalace.md');
const SkullWoods = raw('./SkullWoods.md');
const ThievesTown = raw('./ThievesTown.md');
const IcePalaceNormal = raw('./IcePalace.md');
const IcePalaceNMG = raw('./IcePalaceNMG.md');
const MiseryMireNormal = raw('./MiseryMire.md');
const MiseryMireNMG = raw('./MiseryMireNMG.md');
const TurtleRockNormal = raw('./TurtleRock.md');
const TurtleRockNMG = raw('./TurtleRockNMG.md');
const GanonsTowerNormal = raw('./GanonsTower.md');
const GanonsTowerNMG = raw('./GanonsTowerNMG.md');

export default [
    { name: 'Hyrule Castle', normal: HyruleCastleNormal, nmg: HyruleCastleNMG },
    { name: 'Eastern Palace', normal: EasternPalaceNormal, nmg: EasternPalaceNMG },
    { name: 'Desert Palace', normal: DesertPalace },
    { name: 'Tower of Hera', normal: TowerOfHeraNormal, nmg: TowerOfHeraNMG },
    { name: 'Castle Tower', normal: CastleTowerNormal, nmg: CastleTowerNMG },
    { name: 'Palace of Darkness', normal: PalaceOfDarknessNormal, nmg: PalaceOfDarknessNMG },
    { name: 'Swamp Palace', normal: SwampPalace },
    { name: 'Skull Woods', normal: SkullWoods },
    { name: "Thieves' Town", normal: ThievesTown },
    { name: 'Ice Palace', normal: IcePalaceNormal, nmg: IcePalaceNMG },
    { name: 'Misery Mire', normal: MiseryMireNormal, nmg: MiseryMireNMG },
    { name: 'Turtle Rock', normal: TurtleRockNormal, nmg: TurtleRockNMG },
    { name: "Ganon's Tower", normal: GanonsTowerNormal, nmg: GanonsTowerNMG }
];
