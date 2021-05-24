using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Randomizer.SMZ3.Tests.Logic {
    
    public abstract class LogicCases {

        protected readonly List<Case> CaseWithNothing = new() { Case.WithNothing };
        protected Case CaseWith(Func<dynamic, Case> func) => func(new Case.Builder());

        public static IEnumerable<Locality> ForRegionsUsing<TLogic>() where TLogic : LogicCases, new()
            => LocalitiesUsing<TLogic, RegionAttribute>();

        public static IEnumerable<Locality> ForLocationsUsing<TLogic>() where TLogic : LogicCases, new()
            => LocalitiesUsing<TLogic, LocationAttribute>();

        static IEnumerable<Locality> LocalitiesUsing<TLogic, TLocality>()
            where TLogic : LogicCases, new()
            where TLocality : LocalityAttribute
        {
            var source = new TLogic();
            return from property in PropertiesOf<LogicCases>()
                   from locality in LocalitiesOf(property)
                   let cases = CasesFrom(source, property)
                   select new Locality(locality.Name, cases);

            static PropertyInfo[] PropertiesOf<T>()
                => typeof(T).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            static IEnumerable<TLocality> LocalitiesOf(PropertyInfo property)
                => Attribute.GetCustomAttributes(property, typeof(TLocality)).Cast<TLocality>();
            static List<Case> CasesFrom(TLogic source, PropertyInfo property)
                => property.GetValue(source) as List<Case>;
        }

        public record Locality(string Name, IList<Case> Cases);

        #region Locality declarations

        [Region("Crateria West")]
        protected abstract List<Case> CrateriaWest { get; }
        [Location("Energy Tank, Terminator")]
        protected abstract List<Case> EnergyTankTerminator { get; }
        [Location("Energy Tank, Gauntlet")]
        protected abstract List<Case> EnergyTankGauntlet { get; }
        [Location("Missile (Crateria gauntlet right)")]
        [Location("Missile (Crateria gauntlet left)")]
        protected abstract List<Case> MissileCrateriaGauntlet { get; }

        [Region("Crateria Central")]
        protected abstract List<Case> CrateriaCentral { get; }
        [Location("Power Bomb (Crateria surface)")]
        protected abstract List<Case> PowerBombCrateriaSurface { get; }
        [Location("Missile (Crateria middle)")]
        protected abstract List<Case> MissileCrateriaMiddle { get; }
        [Location("Missile (Crateria bottom)")]
        protected abstract List<Case> MissileCrateriaBottom { get; }
        [Location("Super Missile (Crateria)")]
        protected abstract List<Case> SuperMissileCrateria { get; }
        [Location("Bombs")]
        protected abstract List<Case> Bombs { get; }

        [Region("Crateria East")]
        protected abstract List<Case> CrateriaEast { get; }
        [Location("Missile (outside Wrecked Ship bottom)")]
        protected abstract List<Case> MissileOutsideWreckedShipBottom { get; }
        [Location("Missile (outside Wrecked Ship top)")]
        [Location("Missile (outside Wrecked Ship middle)")]
        protected abstract List<Case> MissilesOutsideWreckedShipTopHalf { get; }
        [Location("Missile (Crateria moat)")]
        protected abstract List<Case> MissileCrateriaMoat { get; }

        [Region("Wrecked Ship")]
        protected abstract List<Case> WreckedShip { get; }
        [Location("Missile (Wrecked Ship middle)")]
        protected abstract List<Case> MissileWreckedShipMiddle { get; }
        [Location("Reserve Tank, Wrecked Ship")]
        protected abstract List<Case> ReserveTankWreckedShip { get; }
        [Location("Missile (Gravity Suit)")]
        protected abstract List<Case> MissileGravitySuit { get; }
        [Location("Missile (Wrecked Ship top)")]
        protected abstract List<Case> MissileWreckedShipTop { get; }
        [Location("Energy Tank, Wrecked Ship")]
        protected abstract List<Case> EnergyTankWreckedShip { get; }
        [Location("Super Missile (Wrecked Ship left)")]
        protected abstract List<Case> SuperMissileWreckedShipLeft { get; }
        [Location("Right Super, Wrecked Ship")]
        protected abstract List<Case> SuperMissileWreckedShipRight { get; }
        [Location("Gravity Suit")]
        protected abstract List<Case> GravitySuit { get; }

        [Region("Brinstar Blue")]
        protected abstract List<Case> BrinstarBlue { get; }
        [Location("Morphing Ball")]
        protected abstract List<Case> MorphingBall { get; }
        [Location("Power Bomb (blue Brinstar)")]
        protected abstract List<Case> PowerBombBlueBrinstar { get; }
        [Location("Missile (blue Brinstar middle)")]
        protected abstract List<Case> MissileBlueBrinstarMiddle { get; }
        [Location("Energy Tank, Brinstar Ceiling")]
        protected abstract List<Case> EnergyTankBrinstarCeiling { get; }
        [Location("Missile (blue Brinstar bottom)")]
        protected abstract List<Case> MissileBlueBrinstarBottom { get; }
        [Location("Missile (blue Brinstar top)")]
        [Location("Missile (blue Brinstar behind missile)")]
        protected abstract List<Case> MissileBlueBrinstarBillyMays { get; }

        [Region("Brinstar Green")]
        protected abstract List<Case> BrinstarGreen { get; }
        [Location("Power Bomb (green Brinstar bottom)")]
        protected abstract List<Case> PowerBombGreenBrinstarBottom { get; }
        [Location("Missile (green Brinstar below super missile)")]
        protected abstract List<Case> MissileGreenBrinstarBelowSuperMissile { get; }
        [Location("Super Missile (green Brinstar top)")]
        protected abstract List<Case> SuperMissileGreenBrinstarTop { get; }
        [Location("Reserve Tank, Brinstar")]
        protected abstract List<Case> ReserveTankBrinstar { get; }
        [Location("Missile (green Brinstar behind missile)")]
        protected abstract List<Case> MissileGreenBrinstarBehindMissile { get; }
        [Location("Missile (green Brinstar behind reserve tank)")]
        protected abstract List<Case> MissileGreenBrinstarBehindReserveTank { get; }
        [Location("Energy Tank, Etecoons")]
        protected abstract List<Case> EnergyTankEtecoons { get; }
        [Location("Super Missile (green Brinstar bottom)")]
        protected abstract List<Case> SuperMissileGreenBrinstarBottom { get; }

        [Region("Brinstar Pink")]
        protected abstract List<Case> BrinstarPink { get; }
        [Location("Super Missile (pink Brinstar)")]
        protected abstract List<Case> SuperMissilePinkBrinstar { get; }
        [Location("Missile (pink Brinstar top)")]
        [Location("Missile (pink Brinstar bottom)")]
        protected abstract List<Case> MissilePinkBrinstarRoom { get; }
        [Location("Charge Beam")]
        protected abstract List<Case> ChargeBeam { get; }
        [Location("Power Bomb (pink Brinstar)")]
        protected abstract List<Case> PowerBombPinkBrinstar { get; }
        [Location("Missile (green Brinstar pipe)")]
        protected abstract List<Case> MissileGreenBrinstarPipe { get; }
        [Location("Energy Tank, Waterway")]
        protected abstract List<Case> EnergyTankWaterway { get; }
        [Location("Energy Tank, Brinstar Gate")]
        protected abstract List<Case> EnergyTankBrinstarGate { get; }

        [Region("Brinstar Red")]
        protected abstract List<Case> BrinstarRed { get; }
        [Location("X-Ray Scope")]
        protected abstract List<Case> XRayScope { get; }
        [Location("Power Bomb (red Brinstar sidehopper room)")]
        protected abstract List<Case> PowerBombRedBrinstarSidehopperRoom { get; }
        [Location("Power Bomb (red Brinstar spike room)")]
        protected abstract List<Case> PowerBombRedBrinstarSpikeRoom { get; }
        [Location("Missile (red Brinstar spike room)")]
        protected abstract List<Case> MissileRedBrinstarSpikeRoom { get; }
        [Location("Spazer")]
        protected abstract List<Case> Spazer { get; }

        [Region("Brinstar Kraid")]
        protected abstract List<Case> BrinstarKraid { get; }
        [Location("Energy Tank, Kraid")]
        protected abstract List<Case> EnergyTankKraid { get; }
        [Location("Varia Suit")]
        protected abstract List<Case> VariaSuit { get; }
        [Location("Missile (Kraid)")]
        protected abstract List<Case> MissileKraid { get; }

        [Region("Maridia Outer")]
        protected abstract List<Case> MaridiaOuter { get; }
        [Location("Missile (green Maridia shinespark)")]
        protected abstract List<Case> MissileGreenMaridiaShinespark { get; }
        [Location("Super Missile (green Maridia)")]
        protected abstract List<Case> SuperMissileGreenMaridia { get; }
        [Location("Energy Tank, Mama turtle")]
        protected abstract List<Case> EnergyTankMamaTurtle { get; }
        [Location("Missile (green Maridia tatori)")]
        protected abstract List<Case> MissileGreenMaridiaTatori { get; }

        [Region("Maridia Inner")]
        protected abstract List<Case> MaridiaInner { get; }
        [Location("Super Missile (yellow Maridia)")]
        [Location("Missile (yellow Maridia super missile)")]
        protected abstract List<Case> YellowMaridiaWateringHole { get; }
        [Location("Missile (yellow Maridia false wall)")]
        protected abstract List<Case> MissileYellowMaridiaFalseWall { get; }
        [Location("Plasma Beam")]
        protected abstract List<Case> PlasmaBeam { get; }
        [Location("Missile (left Maridia sand pit room)")]
        [Location("Reserve Tank, Maridia")]
        protected abstract List<Case> LeftMaridiaSandPitRoom { get; }
        [Location("Missile (right Maridia sand pit room)")]
        protected abstract List<Case> MissileRightMaridiaSandPitRoom { get; }
        [Location("Power Bomb (right Maridia sand pit room)")]
        protected abstract List<Case> PowerBombRightMaridiaSandPitRoom { get; }
        [Location("Missile (pink Maridia)")]
        [Location("Super Missile (pink Maridia)")]
        protected abstract List<Case> PinkMaridia { get; }
        [Location("Spring Ball")]
        protected abstract List<Case> SpringBall { get; }
        [Location("Missile (Draygon)")]
        protected abstract List<Case> MissileDraygon { get; }
        [Location("Energy Tank, Botwoon")]
        protected abstract List<Case> EnergyTankBotwoon { get; }
        [Location("Space Jump")]
        protected abstract List<Case> SpaceJump { get; }

        [Region("Norfair Upper West")]
        protected abstract List<Case> NorfairUpperWest { get; }
        [Location("Missile (lava room)")]
        protected abstract List<Case> MissileLavaRoom { get; }
        [Location("Ice Beam")]
        protected abstract List<Case> IceBeam { get; }
        [Location("Missile (below Ice Beam)")]
        protected abstract List<Case> MissileBelowIceBeam { get; }
        [Location("Hi-Jump Boots")]
        protected abstract List<Case> HiJumpBoots { get; }
        [Location("Missile (Hi-Jump Boots)")]
        protected abstract List<Case> MissileHiJumpBoots { get; }
        [Location("Energy Tank (Hi-Jump Boots)")]
        protected abstract List<Case> EnergyTankHiJumpBoots { get; }

        [Region("Norfair Upper East")]
        protected abstract List<Case> NorfairUpperEast { get; }
        [Location("Reserve Tank, Norfair")]
        protected abstract List<Case> ReserveTankNorfair { get; }
        [Location("Missile (Norfair Reserve Tank)")]
        protected abstract List<Case> MissileNorfairReserveTank { get; }
        [Location("Missile (bubble Norfair green door)")]
        protected abstract List<Case> MissileBubbleNorfairGreenDoor { get; }
        [Location("Missile (bubble Norfair)")]
        protected abstract List<Case> MissileBubbleNorfair { get; }
        [Location("Missile (Speed Booster)")]
        protected abstract List<Case> MissileSpeedBooster { get; }
        [Location("Speed Booster")]
        protected abstract List<Case> SpeedBooster { get; }
        [Location("Missile (Wave Beam)")]
        protected abstract List<Case> MissileWaveBeam { get; }
        [Location("Wave Beam")]
        protected abstract List<Case> WaveBeam { get; }

        [Region("Norfair Upper Crocomire")]
        protected abstract List<Case> NorfairUpperCrocomire { get; }
        [Location("Energy Tank, Crocomire")]
        protected abstract List<Case> EnergyTankCrocomire { get; }
        [Location("Missile (above Crocomire)")]
        protected abstract List<Case> MissileAboveCrocomire { get; }
        [Location("Power Bomb (Crocomire)")]
        protected abstract List<Case> PowerBombCrocomire { get; }
        [Location("Missile (below Crocomire)")]
        protected abstract List<Case> MissileBelowCrocomire { get; }
        [Location("Missile (Grappling Beam)")]
        protected abstract List<Case> MissileGrapplingBeam { get; }
        [Location("Grappling Beam")]
        protected abstract List<Case> GrapplingBeam { get; }

        [Region("Norfair Lower West")]
        protected abstract List<Case> NorfairLowerWest { get; }
        [Location("Missile (Gold Torizo)")]
        protected abstract List<Case> MissileGoldTorizo { get; }
        [Location("Super Missile (Gold Torizo)")]
        protected abstract List<Case> SuperMissileGoldTorizo { get; }
        [Location("Screw Attack")]
        protected abstract List<Case> ScrewAttack { get; }
        [Location("Missile (Mickey Mouse room)")]
        protected abstract List<Case> MissileMickeyMouseRoom { get; }

        [Region("Norfair Lower East")]
        protected abstract List<Case> NorfairLowerEast { get; }
        [Location("Missile (lower Norfair above fire flea room)")]
        protected abstract List<Case> MissileLowerNorfairAboveFireFleaRoom { get; }
        [Location("Power Bomb (lower Norfair above fire flea room)")]
        protected abstract List<Case> PowerBombLowerNorfairAboveFireFleaRoom { get; }
        [Location("Power Bomb (Power Bombs of shame)")]
        protected abstract List<Case> PowerBombPowerBombsOfShame { get; }
        [Location("Missile (lower Norfair near Wave Beam)")]
        protected abstract List<Case> MissileLowerNorfairNearWaveBeam { get; }
        [Location("Energy Tank, Ridley")]
        protected abstract List<Case> EnergyTankRidley { get; }
        [Location("Energy Tank, Firefleas")]
        protected abstract List<Case> EnergyTankFirefleas { get; }

        [Region("Light World Death Mountain West")]
        protected abstract List<Case> LightWorldDeathMountainWest { get; }
        [Location("Ether Tablet")]
        protected abstract List<Case> EtherTablet { get; }
        [Location("Spectacle Rock")]
        protected abstract List<Case> SpectacleRock { get; }
        [Location("Spectacle Rock Cave")]
        protected abstract List<Case> SpectacleRockCave { get; }
        [Location("Old Man")]
        protected abstract List<Case> OldMan { get; }

        [Region("Light World Death Mountain East")]
        protected abstract List<Case> LightWorldDeathMountainEast { get; }
        [Location("Floating Island")]
        protected abstract List<Case> FloatingIsland { get; }
        [Location("Spiral Cave")]
        protected abstract List<Case> SpiralCave { get; }
        [Location("Paradox Cave Upper - Left")]
        [Location("Paradox Cave Upper - Right")]
        [Location("Paradox Cave Lower - Far Left")]
        [Location("Paradox Cave Lower - Left")]
        [Location("Paradox Cave Lower - Middle")]
        [Location("Paradox Cave Lower - Right")]
        [Location("Paradox Cave Lower - Far Right")]
        protected abstract List<Case> ParadoxCave { get; }
        [Location("Mimic Cave")]
        protected abstract List<Case> MimicCave { get; }

        [Region("Light World North West")]
        protected abstract List<Case> LightWorldNorthWest { get; }
        [Location("Master Sword Pedestal")]
        protected abstract List<Case> MasterSwordPedestal { get; }
        [Location("Mushroom")]
        protected abstract List<Case> Mushroom { get; }
        [Location("Lost Woods Hideout")]
        protected abstract List<Case> LostWoodsHideout { get; }
        [Location("Lumberjack Tree")]
        protected abstract List<Case> LumberjackTree { get; }
        [Location("Pegasus Rocks")]
        protected abstract List<Case> PegasusRocks { get; }
        [Location("Graveyard Ledge")]
        protected abstract List<Case> GraveyardLedge { get; }
        [Location("King's Tomb")]
        protected abstract List<Case> KingsTomb { get; }
        [Location("Kakariko Well - Top")]
        [Location("Kakariko Well - Left")]
        [Location("Kakariko Well - Middle")]
        [Location("Kakariko Well - Right")]
        [Location("Kakariko Well - Bottom")]
        protected abstract List<Case> KakarikoWell { get; }
        [Location("Blind's Hideout - Top")]
        [Location("Blind's Hideout - Far Left")]
        [Location("Blind's Hideout - Left")]
        [Location("Blind's Hideout - Right")]
        [Location("Blind's Hideout - Far Right")]
        protected abstract List<Case> BlindsHideout { get; }
        [Location("Bottle Merchant")]
        protected abstract List<Case> BottleMerchant { get; }
        [Location("Chicken House")]
        protected abstract List<Case> ChickenHouse { get; }
        [Location("Sick Kid")]
        protected abstract List<Case> SickKid { get; }
        [Location("Kakariko Tavern")]
        protected abstract List<Case> KakarikoTavern { get; }
        [Location("Magic Bat")]
        protected abstract List<Case> MagicBat { get; }

        [Region("Light World North East")]
        protected abstract List<Case> LightWorldNorthEast { get; }
        [Location("King Zora")]
        protected abstract List<Case> KingZora { get; }
        [Location("Zora's Ledge")]
        protected abstract List<Case> ZorasLedge { get; }
        [Location("Waterfall Fairy - Left")]
        [Location("Waterfall Fairy - Right")]
        protected abstract List<Case> WaterfallFairy { get; }
        [Location("Potion Shop")]
        protected abstract List<Case> PotionShop { get; }
        [Location("Sahasrahla's Hut - Left")]
        [Location("Sahasrahla's Hut - Middle")]
        [Location("Sahasrahla's Hut - Right")]
        protected abstract List<Case> SahasrahlasHut { get; }
        [Location("Sahasrahla")]
        protected abstract List<Case> Sahasrahla { get; }

        [Region("Light World South")]
        protected abstract List<Case> LightWorldSouth { get; }
        [Location("Maze Race")]
        protected abstract List<Case> MazeRace { get; }
        [Location("Library")]
        protected abstract List<Case> Library { get; }
        [Location("Flute Spot")]
        protected abstract List<Case> FluteSpot { get; }
        [Location("South of Grove")]
        protected abstract List<Case> SouthOfGrove { get; }
        [Location("Link's House")]
        protected abstract List<Case> LinksHouse { get; }
        [Location("Aginah's Cave")]
        protected abstract List<Case> AginahsCave { get; }
        [Location("Mini Moldorm Cave - Far Left")]
        [Location("Mini Moldorm Cave - Left")]
        [Location("Mini Moldorm Cave - NPC")]
        [Location("Mini Moldorm Cave - Right")]
        [Location("Mini Moldorm Cave - Far Right")]
        protected abstract List<Case> MiniMoldormCave { get; }
        [Location("Desert Ledge")]
        protected abstract List<Case> DesertLedge { get; }
        [Location("Checkerboard Cave")]
        protected abstract List<Case> CheckerboardCave { get; }
        [Location("Bombos Tablet")]
        protected abstract List<Case> BombosTablet { get; }
        [Location("Floodgate Chest")]
        protected abstract List<Case> FloodgateChest { get; }
        [Location("Sunken Treasure")]
        protected abstract List<Case> SunkenTreasure { get; }
        [Location("Lake Hylia Island")]
        protected abstract List<Case> LakeHyliaIsland { get; }
        [Location("Hobo")]
        protected abstract List<Case> Hobo { get; }
        [Location("Ice Rod Cave")]
        protected abstract List<Case> IceRodCave { get; }

        [Region("Dark World Death Mountain West")]
        protected abstract List<Case> DarkWorldDeathMountainWest { get; }
        [Location("Spike Cave")]
        protected abstract List<Case> SpikeCave { get; }

        [Region("Dark World Death Mountain East")]
        protected abstract List<Case> DarkWorldDeathMountainEast { get; }
        [Location("Hookshot Cave - Top Right")]
        [Location("Hookshot Cave - Top Left")]
        [Location("Hookshot Cave - Bottom Left")]
        protected abstract List<Case> HookshotCave { get; }
        [Location("Hookshot Cave - Bottom Right")]
        protected abstract List<Case> HookshotCaveBottomRight { get; }
        [Location("Superbunny Cave - Top")]
        [Location("Superbunny Cave - Bottom")]
        protected abstract List<Case> SuperbunnyCave { get; }

        [Region("Dark World North West")]
        protected abstract List<Case> DarkWorldNorthWest { get; }
        [Location("Bumper Cave")]
        protected abstract List<Case> BumperCave { get; }
        [Location("Chest Game")]
        [Location("C-Shaped House")]
        [Location("Brewery")]
        protected abstract List<Case> VillageOfOutcasts { get; }
        [Location("Hammer Pegs")]
        protected abstract List<Case> HammerPegs { get; }
        [Location("Blacksmith")]
        protected abstract List<Case> Blacksmith { get; }
        [Location("Purple Chest")]
        protected abstract List<Case> PurpleChest { get; }

        [Region("Dark World North East")]
        protected abstract List<Case> DarkWorldNorthEast { get; }
        [Location("Catfish")]
        protected abstract List<Case> Catfish { get; }
        [Location("Pyramid")]
        protected abstract List<Case> Pyramid { get; }
        [Location("Pyramid Fairy - Left")]
        [Location("Pyramid Fairy - Right")]
        protected abstract List<Case> PyramidFairy { get; }

        [Region("Dark World South")]
        protected abstract List<Case> DarkWorldSouth { get; }
        [Location("Digging Game")]
        protected abstract List<Case> DiggingGame { get; }
        [Location("Stumpy")]
        protected abstract List<Case> Stumpy { get; }
        [Location("Hype Cave - Top")]
        [Location("Hype Cave - Middle Right")]
        [Location("Hype Cave - Middle Left")]
        [Location("Hype Cave - Bottom")]
        [Location("Hype Cave - NPC")]
        protected abstract List<Case> HypeCave { get; }

        [Region("Dark World Mire")]
        protected abstract List<Case> DarkWorldMire { get; }
        [Location("Mire Shed - Left")]
        [Location("Mire Shed - Right")]
        protected abstract List<Case> MireShed { get; }

        [Region("Hyrule Castle")]
        protected abstract List<Case> HyruleCastle { get; }
        [Location("Sanctuary")]
        protected abstract List<Case> Sanctuary { get; }
        [Location("Sewers - Secret Room - Left")]
        [Location("Sewers - Secret Room - Middle")]
        [Location("Sewers - Secret Room - Right")]
        protected abstract List<Case> SewersSecretRoom { get; }
        [Location("Sewers - Dark Cross")]
        protected abstract List<Case> SewersDarkCross { get; }
        [Location("Hyrule Castle - Map Chest")]
        protected abstract List<Case> HyruleCastleMapChest { get; }
        [Location("Hyrule Castle - Boomerang Chest")]
        protected abstract List<Case> HyruleCastleBoomerangChest { get; }
        [Location("Hyrule Castle - Zelda's Cell")]
        protected abstract List<Case> HyruleCastleZeldasCell { get; }
        [Location("Link's Uncle")]
        protected abstract List<Case> LinksUncle { get; }
        [Location("Secret Passage")]
        protected abstract List<Case> SecretPassage { get; }

        [Region("Castle Tower")]
        protected abstract List<Case> CastleTower { get; }
        [Location("Castle Tower - Foyer")]
        protected abstract List<Case> CastleTowerFoyer { get; }
        [Location("Castle Tower - Dark Maze")]
        protected abstract List<Case> CastleTowerDarkMaze { get; }

        [Region("Eastern Palace")]
        protected abstract List<Case> EasternPalace { get; }
        [Location("Eastern Palace - Cannonball Chest")]
        protected abstract List<Case> EasternPalaceCannonballChest { get; }
        [Location("Eastern Palace - Map Chest")]
        protected abstract List<Case> EasternPalaceMapChest { get; }
        [Location("Eastern Palace - Compass Chest")]
        protected abstract List<Case> EasternPalaceCompassChest { get; }
        [Location("Eastern Palace - Big Chest")]
        protected abstract List<Case> EasternPalaceBigChest { get; }
        [Location("Eastern Palace - Big Key Chest")]
        protected abstract List<Case> EasternPalaceBigKeyChest { get; }
        [Location("Eastern Palace - Armos Knights")]
        protected abstract List<Case> EasternPalaceArmosKnights { get; }

        [Region("Desert Palace")]
        protected abstract List<Case> DesertPalace { get; }
        [Location("Desert Palace - Big Chest")]
        protected abstract List<Case> DesertPalaceBigChest { get; }
        [Location("Desert Palace - Torch")]
        protected abstract List<Case> DesertPalaceTorch { get; }
        [Location("Desert Palace - Map Chest")]
        protected abstract List<Case> DesertPalaceMapChest { get; }
        [Location("Desert Palace - Big Key Chest")]
        protected abstract List<Case> DesertPalaceBigKeyChest { get; }
        [Location("Desert Palace - Compass Chest")]
        protected abstract List<Case> DesertPalaceCompassChest { get; }
        [Location("Desert Palace - Lanmolas")]
        protected abstract List<Case> DesertPalaceLanmolas { get; }

        [Region("Tower of Hera")]
        protected abstract List<Case> TowerOfHera { get; }
        [Location("Tower of Hera - Basement Cage")]
        protected abstract List<Case> TowerOfHeraBasementCage { get; }
        [Location("Tower of Hera - Map Chest")]
        protected abstract List<Case> TowerOfHeraMapChest { get; }
        [Location("Tower of Hera - Big Key Chest")]
        protected abstract List<Case> TowerOfHeraBigKeyChest { get; }
        [Location("Tower of Hera - Compass Chest")]
        protected abstract List<Case> TowerOfHeraCompassChest { get; }
        [Location("Tower of Hera - Big Chest")]
        protected abstract List<Case> TowerOfHeraBigChest { get; }
        [Location("Tower of Hera - Moldorm")]
        protected abstract List<Case> TowerOfHeraMoldorm { get; }

        [Region("Palace of Darkness")]
        protected abstract List<Case> PalaceOfDarkness { get; }
        [Location("Palace of Darkness - Shooter Room")]
        protected abstract List<Case> PalaceOfDarknessShooterRoom { get; }
        [Location("Palace of Darkness - Big Key Chest")]
        protected abstract List<Case> PalaceOfDarknessBigKeyChest { get; }
        [Location("Palace of Darkness - Stalfos Basement")]
        protected abstract List<Case> PalaceOfDarknessStalfosBasement { get; }
        [Location("Palace of Darkness - The Arena - Bridge")]
        protected abstract List<Case> PalaceOfDarknessTheArenaBridge { get; }
        [Location("Palace of Darkness - The Arena - Ledge")]
        protected abstract List<Case> PalaceOfDarknessTheArenaLedge { get; }
        [Location("Palace of Darkness - Map Chest")]
        protected abstract List<Case> PalaceOfDarknessMapChest { get; }
        [Location("Palace of Darkness - Compass Chest")]
        protected abstract List<Case> PalaceOfDarknessCompassChest { get; }
        [Location("Palace of Darkness - Harmless Hellway")]
        protected abstract List<Case> PalaceOfDarknessHarmlessHellway { get; }
        [Location("Palace of Darkness - Dark Basement - Left")]
        [Location("Palace of Darkness - Dark Basement - Right")]
        protected abstract List<Case> PalaceOfDarknessDarkBasement { get; }
        [Location("Palace of Darkness - Dark Maze - Top")]
        [Location("Palace of Darkness - Dark Maze - Bottom")]
        protected abstract List<Case> PalaceOfDarknessDarkMaze { get; }
        [Location("Palace of Darkness - Big Chest")]
        protected abstract List<Case> PalaceOfDarknessBigChest { get; }
        [Location("Palace of Darkness - Helmasaur King")]
        protected abstract List<Case> PalaceOfDarknessHelmasaurKing { get; }

        [Region("Swamp Palace")]
        protected abstract List<Case> SwampPalace { get; }
        [Location("Swamp Palace - Entrance")]
        protected abstract List<Case> SwampPalaceEntrance { get; }
        [Location("Swamp Palace - Map Chest")]
        protected abstract List<Case> SwampPalaceMapChest { get; }
        [Location("Swamp Palace - Big Chest")]
        protected abstract List<Case> SwampPalaceBigChest { get; }
        [Location("Swamp Palace - Compass Chest")]
        protected abstract List<Case> SwampPalaceCompassChest { get; }
        [Location("Swamp Palace - West Chest")]
        [Location("Swamp Palace - Big Key Chest")]
        protected abstract List<Case> SwampPalaceWestWing { get; }
        [Location("Swamp Palace - Flooded Room - Left")]
        [Location("Swamp Palace - Flooded Room - Right")]
        protected abstract List<Case> SwampPalaceFloodedRoom { get; }
        [Location("Swamp Palace - Waterfall Room")]
        protected abstract List<Case> SwampPalaceWaterfallRoom { get; }
        [Location("Swamp Palace - Arrghus")]
        protected abstract List<Case> SwampPalaceArrghus { get; }

        [Region("Skull Woods")]
        protected abstract List<Case> SkullWoods { get; }
        [Location("Skull Woods - Pot Prison")]
        protected abstract List<Case> SkullWoodsPotPrison { get; }
        [Location("Skull Woods - Compass Chest")]
        protected abstract List<Case> SkullWoodsCompassChest { get; }
        [Location("Skull Woods - Big Chest")]
        protected abstract List<Case> SkullWoodsBigChest { get; }
        [Location("Skull Woods - Map Chest")]
        protected abstract List<Case> SkullWoodsMapChest { get; }
        // Skipping "Skull Woods - Pinball Room" since it is always a key
        [Location("Skull Woods - Big Key Chest")]
        protected abstract List<Case> SkullWoodsBigKeyChest { get; }
        [Location("Skull Woods - Bridge Room")]
        protected abstract List<Case> SkullWoodsBridgeRoom { get; }
        [Location("Skull Woods - Mothula")]
        protected abstract List<Case> SkullWoodsMothula { get; }

        [Region("Thieves' Town")]
        protected abstract List<Case> ThievesTown { get; }
        [Location("Thieves' Town - Map Chest")]
        [Location("Thieves' Town - Ambush Chest")]
        [Location("Thieves' Town - Compass Chest")]
        [Location("Thieves' Town - Big Key Chest")]
        protected abstract List<Case> ThievesTownCourtyard { get; }
        [Location("Thieves' Town - Attic")]
        protected abstract List<Case> ThievesTownAttic { get; }
        [Location("Thieves' Town - Blind's Cell")]
        protected abstract List<Case> ThievesTownBlindsCell { get; }
        [Location("Thieves' Town - Big Chest")]
        protected abstract List<Case> ThievesTownBigChest { get; }

        [Location("Thieves' Town - Blind")]
        protected abstract List<Case> ThievesTownBlind { get; }

        [Region("Ice Palace")]
        protected abstract List<Case> IcePalace { get; }
        [Location("Ice Palace - Compass Chest")]
        protected abstract List<Case> IcePalaceCompassChest { get; }
        [Location("Ice Palace - Spike Room")]
        protected abstract List<Case> IcePalaceSpikeRoom { get; }
        [Location("Ice Palace - Map Chest")]
        protected abstract List<Case> IcePalaceMapChest { get; }
        [Location("Ice Palace - Big Key Chest")]
        protected abstract List<Case> IcePalaceBigKeyChest { get; }
        [Location("Ice Palace - Iced T Room")]
        protected abstract List<Case> IcePalaceIcedTRoom { get; }
        [Location("Ice Palace - Freezor Chest")]
        protected abstract List<Case> IcePalaceFreezorChest { get; }
        [Location("Ice Palace - Big Chest")]
        protected abstract List<Case> IcePalaceBigChest { get; }
        [Location("Ice Palace - Kholdstare")]
        protected abstract List<Case> IcePalaceKholdstare { get; }

        [Region("Misery Mire")]
        protected abstract List<Case> MiseryMire { get; }
        [Location("Misery Mire - Main Lobby")]
        protected abstract List<Case> MiseryMireMainLobby { get; }
        [Location("Misery Mire - Map Chest")]
        protected abstract List<Case> MiseryMireMapChest { get; }
        [Location("Misery Mire - Bridge Chest")]
        protected abstract List<Case> MiseryMireBridgeChest { get; }
        [Location("Misery Mire - Spike Chest")]
        protected abstract List<Case> MiseryMireSpikeChest { get; }
        [Location("Misery Mire - Compass Chest")]
        protected abstract List<Case> MiseryMireCompassChest { get; }
        [Location("Misery Mire - Big Key Chest")]
        protected abstract List<Case> MiseryMireBigKeyChest { get; }
        [Location("Misery Mire - Big Chest")]
        protected abstract List<Case> MiseryMireBigChest { get; }
        [Location("Misery Mire - Vitreous")]
        protected abstract List<Case> MiseryMireVitreous { get; }

        [Region("Turtle Rock")]
        protected abstract List<Case> TurtleRock { get; }
        [Location("Turtle Rock - Compass Chest")]
        protected abstract List<Case> TurtleRockCompassChest { get; }
        [Location("Turtle Rock - Roller Room - Left")]
        [Location("Turtle Rock - Roller Room - Right")]
        protected abstract List<Case> TurtleRockRollerRoom { get; }
        [Location("Turtle Rock - Chain Chomps")]
        protected abstract List<Case> TurtleRockChainChomps { get; }
        [Location("Turtle Rock - Big Key Chest")]
        protected abstract List<Case> TurtleRockBigKeyChest { get; }
        [Location("Turtle Rock - Big Chest")]
        protected abstract List<Case> TurtleRockBigChest { get; }
        [Location("Turtle Rock - Crystaroller Room")]
        protected abstract List<Case> TurtleRockCrystarollerRoom { get; }
        [Location("Turtle Rock - Eye Bridge - Top Right")]
        [Location("Turtle Rock - Eye Bridge - Top Left")]
        [Location("Turtle Rock - Eye Bridge - Bottom Right")]
        [Location("Turtle Rock - Eye Bridge - Bottom Left")]
        protected abstract List<Case> TurtleRockEyeBridge { get; }
        [Location("Turtle Rock - Trinexx")]
        protected abstract List<Case> TurtleRockTrinexx { get; }

        [Region("Ganon's Tower")]
        protected abstract List<Case> GanonsTower { get; }
        [Location("Ganon's Tower - Bob's Torch")]
        protected abstract List<Case> GanonsTowerBobsTorch { get; }
        [Location("Ganon's Tower - DMs Room - Top Left")]
        [Location("Ganon's Tower - DMs Room - Top Right")]
        [Location("Ganon's Tower - DMs Room - Bottom Left")]
        [Location("Ganon's Tower - DMs Room - Bottom Right")]
        protected abstract List<Case> GanonsTowerDMsRoom { get; }
        [Location("Ganon's Tower - Map Chest")]
        protected abstract List<Case> GanonsTowerMapChest { get; }
        [Location("Ganon's Tower - Firesnake Room")]
        protected abstract List<Case> GanonsTowerFiresnakeRoom { get; }
        [Location("Ganon's Tower - Randomizer Room - Top Left")]
        protected abstract List<Case> GanonsTowerRandomizerRoomTopLeft { get; }
        [Location("Ganon's Tower - Randomizer Room - Top Right")]
        protected abstract List<Case> GanonsTowerRandomizerRoomTopRight { get; }
        [Location("Ganon's Tower - Randomizer Room - Bottom Left")]
        protected abstract List<Case> GanonsTowerRandomizerRoomBottomLeft { get; }
        [Location("Ganon's Tower - Randomizer Room - Bottom Right")]
        protected abstract List<Case> GanonsTowerRandomizerRoomBottomRight { get; }
        [Location("Ganon's Tower - Hope Room - Left")]
        [Location("Ganon's Tower - Hope Room - Right")]
        protected abstract List<Case> GanonsTowerHopeRoom { get; }
        [Location("Ganon's Tower - Tile Room")]
        protected abstract List<Case> GanonsTowerTileRoom { get; }
        [Location("Ganon's Tower - Compass Room - Top Left")]
        protected abstract List<Case> GanonsTowerCompassRoomTopLeft { get; }
        [Location("Ganon's Tower - Compass Room - Top Right")]
        protected abstract List<Case> GanonsTowerCompassRoomTopRight { get; }
        [Location("Ganon's Tower - Compass Room - Bottom Left")]
        protected abstract List<Case> GanonsTowerCompassRoomBottomLeft { get; }
        [Location("Ganon's Tower - Compass Room - Bottom Right")]
        protected abstract List<Case> GanonsTowerCompassRoomBottomRight { get; }
        [Location("Ganon's Tower - Bob's Chest")]
        protected abstract List<Case> GanonsTowerBobsChest { get; }
        [Location("Ganon's Tower - Big Chest")]
        protected abstract List<Case> GanonsTowerBigChest { get; }
        [Location("Ganon's Tower - Big Key Chest")]
        [Location("Ganon's Tower - Big Key Room - Left")]
        [Location("Ganon's Tower - Big Key Room - Right")]
        protected abstract List<Case> GanonsTowerBigKeyRoom { get; }
        [Location("Ganon's Tower - Mini Helmasaur Room - Left")]
        [Location("Ganon's Tower - Mini Helmasaur Room - Right")]
        [Location("Ganon's Tower - Pre-Moldorm Chest")]
        protected abstract List<Case> GanonsTowerAscent { get; }
        [Location("Ganon's Tower - Moldorm Chest")]
        protected abstract List<Case> GanonsTowerMoldormChest { get; }

        #endregion

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
        abstract class LocalityAttribute : Attribute {
            public string Name { get; init; }
            public LocalityAttribute(string name) { Name = name; }
        }

        class RegionAttribute : LocalityAttribute {
            public RegionAttribute(string name) : base(name) { }
        }

        class LocationAttribute : LocalityAttribute {
            public LocationAttribute(string name) : base(name) { }
        }

    }

}
