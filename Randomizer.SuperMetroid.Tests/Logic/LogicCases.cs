using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Randomizer.SuperMetroid.Tests.Logic {
    
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
