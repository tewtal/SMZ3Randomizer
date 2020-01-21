import raw from "raw.macro";
const HyruleCastle = raw('./HyruleCastle.md');
const EasternPalace = raw('./EasternPalace.md');
const DesertPalace = raw('./DesertPalace.md');
const TowerOfHera = raw('./TowerOfHera.md');
const CastleTower = raw('./CastleTower.md');
const PalaceOfDarkness = raw('./PalaceOfDarkness.md');
const SwampPalace = raw('./SwampPalace.md');
const SkullWoods = raw('./SkullWoods.md');
const ThievesTown = raw('./ThievesTown.md');
const IcePalace = raw('./IcePalace.md');
const MiseryMire = raw('./MiseryMire.md');
const TurtleRock = raw('./TurtleRock.md');
const GanonsTower = raw('./GanonsTower.md');

export default [
    { name: 'Hyrule Castle', normal: HyruleCastle },
    { name: 'Eastern Palace', normal: EasternPalace },
    { name: 'Desert Palace', normal: DesertPalace },
    { name: 'Tower of Hera', normal: TowerOfHera },
    { name: 'Castle Tower', normal: CastleTower },
    { name: 'Palace of Darkness', normal: PalaceOfDarkness },
    { name: 'Swamp Palace', normal: SwampPalace },
    { name: 'Skull Woods', normal: SkullWoods },
    { name: "Thieves' Town", normal: ThievesTown },
    { name: 'Ice Palace', normal: IcePalace },
    { name: 'Misery Mire', normal: MiseryMire },
    { name: 'Turtle Rock', normal: TurtleRock },
    { name: "Ganon's Tower", normal: GanonsTower }
];
