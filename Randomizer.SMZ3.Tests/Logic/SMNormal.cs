using System.Collections.Generic;

namespace Randomizer.SMZ3.Tests.Logic {

    public class SMNormal : LogicCases {

        #region Crateria West

        protected override List<Case> CrateriaWest => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
            CaseWith(x => x.ScrewAttack),
            CaseWith(x => x.SpeedBooster),
        };

        protected override List<Case> EnergyTankTerminator => CaseWithNothing;

        protected override List<Case> EnergyTankGauntlet => new() {
            CaseWith(x => x.Morph.Bombs.ETank(1)),
            CaseWith(x => x.Morph.SpaceJump.TwoPowerBombs.ETank(1)),
            CaseWith(x => x.Morph.SpaceJump.ScrewAttack.ETank(1)),
            CaseWith(x => x.Morph.SpeedBooster.TwoPowerBombs.ETank(1)),
            CaseWith(x => x.Morph.SpeedBooster.ScrewAttack.ETank(1)),
        };

        protected override List<Case> MissileCrateriaGauntlet => new() {
            CaseWith(x => x.Morph.Bombs.ETank(2)),
            CaseWith(x => x.Morph.SpaceJump.TwoPowerBombs.ETank(2)),
            CaseWith(x => x.Morph.SpaceJump.ScrewAttack.PowerBomb.ETank(2)),
            CaseWith(x => x.Morph.SpeedBooster.TwoPowerBombs.ETank(2)),
            CaseWith(x => x.Morph.SpeedBooster.ScrewAttack.PowerBomb.ETank(2)),
        };

        #endregion

        #region Crateria Central

        protected override List<Case> CrateriaCentral => CaseWithNothing;

        protected override List<Case> PowerBombCrateriaSurface => new() {
            CaseWith(x => x.Morph.PowerBomb.SpeedBooster),
            CaseWith(x => x.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Bombs),
        };

        protected override List<Case> MissileCrateriaMiddle => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> MissileCrateriaBottom => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
            CaseWith(x => x.ScrewAttack),
        };

        protected override List<Case> SuperMissileCrateria => new() {
            CaseWith(x => x.Morph.PowerBomb.ETank(2).SpeedBooster),
        };

        protected override List<Case> Bombs => new() {
            CaseWith(x => x.Missile.Morph.Bombs),
            CaseWith(x => x.Missile.Morph.PowerBomb),
            CaseWith(x => x.Super.Morph.Bombs),
            CaseWith(x => x.Super.Morph.PowerBomb),
        };

        #endregion

        #region Crateria East

        protected override List<Case> CrateriaEast => new() {
            CaseWith(x => x.Morph.PowerBomb.Super),
            CaseWith(x => x.Morph.PowerBomb.Flute.Ice),
            CaseWith(x => x.Morph.PowerBomb.Flute.HiJump),
            CaseWith(x => x.Morph.PowerBomb.Flute.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Glove.Lamp.Ice),
            CaseWith(x => x.Morph.PowerBomb.Glove.Lamp.HiJump),
            CaseWith(x => x.Morph.PowerBomb.Glove.Lamp.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super.SpeedBooster.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super.SpeedBooster.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super.SpeedBooster.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Super.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Super.SpeedBooster.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Super.SpaceJump),
        };

        protected override List<Case> MissileOutsideWreckedShipBottom => new() {
            CaseWith(x => x.Morph.SpeedBooster),
            CaseWith(x => x.Morph.Grapple),
            CaseWith(x => x.Morph.SpaceJump),
            CaseWith(x => x.Morph.Gravity.Bombs),
            CaseWith(x => x.Morph.Gravity.HiJump),
            CaseWith(x => x.Morph.Super.PowerBomb.Gravity),
            CaseWith(x => x.Morph.MoonPearl.Flippers.Gravity.Sword.Cape.Lamp.KeyCT(2).Super.ScrewAttack),
            CaseWith(x => x.Morph.MoonPearl.Flippers.Gravity.MasterSword.Lamp.KeyCT(2).Super.ScrewAttack),
            CaseWith(x => x.Morph.MoonPearl.Flippers.Gravity.Hammer.Glove.Super.ScrewAttack),
            CaseWith(x => x.Morph.MoonPearl.Flippers.Gravity.Mitt.Super.ScrewAttack),
        };

        protected override List<Case> MissilesOutsideWreckedShipTopHalf => new() {
            CaseWith(x => x.Super.Morph.PowerBomb.SpeedBooster),
            CaseWith(x => x.Super.Morph.PowerBomb.Grapple),
            CaseWith(x => x.Super.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.Super.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.Bombs),
        };

        protected override List<Case> MissileCrateriaMoat => CaseWithNothing;

        #endregion

        #region Wrecked Ship

        protected override List<Case> WreckedShip => new() {
            CaseWith(x => x.Super.Morph.PowerBomb.SpeedBooster),
            CaseWith(x => x.Super.Morph.PowerBomb.Grapple),
            CaseWith(x => x.Super.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.Super.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.Sword.Cape.Lamp.KeyCT(2).ScrewAttack),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.Sword.Cape.Lamp.KeyCT(2).SpeedBooster.HiJump),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.Sword.Cape.Lamp.KeyCT(2).SpaceJump),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.MasterSword.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.MasterSword.Lamp.KeyCT(2).ScrewAttack),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.MasterSword.Lamp.KeyCT(2).SpeedBooster.HiJump),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.MasterSword.Lamp.KeyCT(2).SpaceJump),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.Hammer.Glove.Bombs),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.Hammer.Glove.ScrewAttack),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.Hammer.Glove.SpeedBooster.HiJump),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.Hammer.Glove.SpaceJump),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.Mitt.Bombs),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.Mitt.ScrewAttack),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.Mitt.SpeedBooster.HiJump),
            CaseWith(x => x.Super.Gravity.MoonPearl.Flippers.Morph.Mitt.SpaceJump),
        };

        protected override List<Case> MissileWreckedShipMiddle => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> ReserveTankWreckedShip => new() {
            CaseWith(x => x.Morph.PowerBomb.SpeedBooster.Grapple),
            CaseWith(x => x.Morph.PowerBomb.SpeedBooster.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.SpeedBooster.Varia.ETank(2)),
            CaseWith(x => x.Morph.PowerBomb.SpeedBooster.ETank(3)),
        };

        protected override List<Case> MissileGravitySuit => new() {
            CaseWith(x => x.Morph.Bombs.Grapple),
            CaseWith(x => x.Morph.Bombs.SpaceJump),
            CaseWith(x => x.Morph.Bombs.Varia.ETank(2)),
            CaseWith(x => x.Morph.Bombs.ETank(3)),
            CaseWith(x => x.Morph.PowerBomb.Grapple),
            CaseWith(x => x.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Varia.ETank(2)),
            CaseWith(x => x.Morph.PowerBomb.ETank(3)),
        };

        protected override List<Case> MissileWreckedShipTop => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> EnergyTankWreckedShip => new() {
            CaseWith(x => x.Morph.Bombs.HiJump),
            CaseWith(x => x.Morph.Bombs.SpaceJump),
            CaseWith(x => x.Morph.Bombs.SpeedBooster),
            CaseWith(x => x.Morph.Bombs.Gravity),
            CaseWith(x => x.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.SpeedBooster),
            CaseWith(x => x.Morph.PowerBomb.Gravity),
        };

        protected override List<Case> SuperMissileWreckedShipLeft => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
        };
        protected override List<Case> SuperMissileWreckedShipRight => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> GravitySuit => new() {
            CaseWith(x => x.Morph.Bombs.Grapple),
            CaseWith(x => x.Morph.Bombs.SpaceJump),
            CaseWith(x => x.Morph.Bombs.Varia.ETank(2)),
            CaseWith(x => x.Morph.Bombs.ETank(3)),
            CaseWith(x => x.Morph.PowerBomb.Grapple),
            CaseWith(x => x.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Varia.ETank(2)),
            CaseWith(x => x.Morph.PowerBomb.ETank(3)),
        };

        #endregion

        #region Brinstar Blue

        protected override List<Case> BrinstarBlue => CaseWithNothing;

        protected override List<Case> MorphingBall => CaseWithNothing;

        protected override List<Case> PowerBombBlueBrinstar => new() {
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> MissileBlueBrinstarMiddle => new() {
            CaseWith(x => x.Morph),
        };

        protected override List<Case> EnergyTankBrinstarCeiling => new() {
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.HiJump),
            CaseWith(x => x.SpeedBooster),
            CaseWith(x => x.Ice),
        };

        protected override List<Case> MissileBlueBrinstarBottom => new() {
            CaseWith(x => x.Morph),
        };

        protected override List<Case> MissileBlueBrinstarBillyMays => new() {
            CaseWith(x => x.Morph.PowerBomb),
        };

        #endregion

        #region Brinstar Green

        protected override List<Case> BrinstarGreen => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
            CaseWith(x => x.ScrewAttack),
            CaseWith(x => x.SpeedBooster),
        };

        protected override List<Case> PowerBombGreenBrinstarBottom => new() {
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> MissileGreenBrinstarBelowSuperMissile => new() {
            CaseWith(x => x.Morph.Bombs.Missile),
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.Morph.PowerBomb.Missile),
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        protected override List<Case> SuperMissileGreenBrinstarTop => new() {
            CaseWith(x => x.Missile.SpeedBooster),
            CaseWith(x => x.Super.SpeedBooster),
        };
        protected override List<Case> ReserveTankBrinstar => new() {
            CaseWith(x => x.Missile.SpeedBooster),
            CaseWith(x => x.Super.SpeedBooster),
        };

        protected override List<Case> MissileGreenBrinstarBehindMissile => new() {
            CaseWith(x => x.SpeedBooster.Morph.Bombs.Missile),
            CaseWith(x => x.SpeedBooster.Morph.Bombs.Super),
            CaseWith(x => x.SpeedBooster.Morph.PowerBomb.Missile),
            CaseWith(x => x.SpeedBooster.Morph.PowerBomb.Super),
        };

        protected override List<Case> MissileGreenBrinstarBehindReserveTank => new() {
            CaseWith(x => x.SpeedBooster.Missile.Morph),
            CaseWith(x => x.SpeedBooster.Super.Morph),
        };

        protected override List<Case> EnergyTankEtecoons => new() {
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> SuperMissileGreenBrinstarBottom => new() {
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        #endregion

        #region Brinstar Pink

        protected override List<Case> BrinstarPink => new() {
            CaseWith(x => x.Missile.Morph.Bombs),
            CaseWith(x => x.Missile.ScrewAttack),
            CaseWith(x => x.Missile.SpeedBooster),
            CaseWith(x => x.Super.Morph.Bombs),
            CaseWith(x => x.Super.ScrewAttack),
            CaseWith(x => x.Super.SpeedBooster),
            CaseWith(x => x.Morph.PowerBomb),
            CaseWith(x => x.Flute.Morph.Wave.Ice),
            CaseWith(x => x.Flute.Morph.Wave.HiJump),
            CaseWith(x => x.Flute.Morph.Wave.SpaceJump),
            CaseWith(x => x.Glove.Lamp.Morph.Wave.Ice),
            CaseWith(x => x.Glove.Lamp.Morph.Wave.HiJump),
            CaseWith(x => x.Glove.Lamp.Morph.Wave.SpaceJump),
        };

        protected override List<Case> SuperMissilePinkBrinstar => new() {
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        protected override List<Case> MissilePinkBrinstarRoom => CaseWithNothing;

        protected override List<Case> ChargeBeam => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> PowerBombPinkBrinstar => new() {
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(1)),
        };

        protected override List<Case> MissileGreenBrinstarPipe => new() {
            CaseWith(x => x.Morph.PowerBomb),
            CaseWith(x => x.Morph.Super),
            CaseWith(x => x.Morph.Flute),
            CaseWith(x => x.Morph.Glove.Lamp),
        };

        protected override List<Case> EnergyTankWaterway => new() {
            CaseWith(x => x.Morph.PowerBomb.Missile.SpeedBooster.ETank(1)),
            CaseWith(x => x.Morph.PowerBomb.Missile.SpeedBooster.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.SpeedBooster.ETank(1)),
            CaseWith(x => x.Morph.PowerBomb.Super.SpeedBooster.Gravity),
        };

        protected override List<Case> EnergyTankBrinstarGate => new() {
            CaseWith(x => x.Morph.PowerBomb.Wave.ETank(1)),
        };

        #endregion

        #region Brinstar Red

        protected override List<Case> BrinstarRed => new() {
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.Morph.PowerBomb.Super),
            CaseWith(x => x.ScrewAttack.Super.Morph),
            CaseWith(x => x.SpeedBooster.Super.Morph),
            CaseWith(x => x.Flute.Ice),
            CaseWith(x => x.Flute.HiJump),
            CaseWith(x => x.Flute.SpaceJump),
            CaseWith(x => x.Glove.Lamp.Ice),
            CaseWith(x => x.Glove.Lamp.HiJump),
            CaseWith(x => x.Glove.Lamp.SpaceJump),
        };

        protected override List<Case> XRayScope => new() {
            CaseWith(x => x.Morph.PowerBomb.Missile.Grapple),
            CaseWith(x => x.Morph.PowerBomb.Missile.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Grapple),
            CaseWith(x => x.Morph.PowerBomb.Super.SpaceJump),
        };

        protected override List<Case> PowerBombRedBrinstarSidehopperRoom => new() {
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        protected override List<Case> PowerBombRedBrinstarSpikeRoom => new() {
            CaseWith(x => x.Morph.PowerBomb.Super),
            CaseWith(x => x.Ice.Super),
        };

        protected override List<Case> MissileRedBrinstarSpikeRoom => new() {
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        protected override List<Case> Spazer => new() {
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        #endregion

        #region Brinstar Kraid

        protected override List<Case> BrinstarKraid => new() {
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        protected override List<Case> EnergyTankKraid => CaseWithNothing;
        protected override List<Case> VariaSuit => CaseWithNothing;

        protected override List<Case> MissileKraid => new() {
            CaseWith(x => x.Morph.PowerBomb),
        };

        #endregion

        #region Maridia Outer

        protected override List<Case> MaridiaOuter => new() {
            CaseWith(x => x.Gravity.Morph.PowerBomb.Super),
            CaseWith(x => x.Gravity.Flute.Morph.PowerBomb),
            CaseWith(x => x.Gravity.Glove.Lamp.Morph.PowerBomb),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Sword.Cape.Lamp.KeyCT(2).PowerBomb),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Sword.Cape.Lamp.KeyCT(2).ScrewAttack),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.MasterSword.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.MasterSword.Lamp.KeyCT(2).PowerBomb),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.MasterSword.Lamp.KeyCT(2).ScrewAttack),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Hammer.Glove.Bombs),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Hammer.Glove.PowerBomb),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Hammer.Glove.ScrewAttack),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Mitt.Bombs),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Mitt.PowerBomb),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Mitt.ScrewAttack),
        };

        protected override List<Case> MissileGreenMaridiaShinespark => new() {
            CaseWith(x => x.SpeedBooster),
        };

        protected override List<Case> SuperMissileGreenMaridia => CaseWithNothing;

        protected override List<Case> EnergyTankMamaTurtle => new() {
            CaseWith(x => x.Missile.SpaceJump),
            CaseWith(x => x.Missile.Morph.Bombs),
            CaseWith(x => x.Missile.SpeedBooster),
            CaseWith(x => x.Missile.Grapple),
            CaseWith(x => x.Super.SpaceJump),
            CaseWith(x => x.Super.Morph.Bombs),
            CaseWith(x => x.Super.SpeedBooster),
            CaseWith(x => x.Super.Grapple),
        };

        protected override List<Case> MissileGreenMaridiaTatori => new() {
            CaseWith(x => x.Missile),
            CaseWith(x => x.Super),
        };

        #endregion

        #region Maridia Inner

        protected override List<Case> MaridiaInner => new() {
            CaseWith(x => x.Gravity.Morph.PowerBomb.Super.SpaceJump),
            CaseWith(x => x.Gravity.Morph.PowerBomb.Super.Bombs),
            CaseWith(x => x.Gravity.Morph.PowerBomb.Super.SpeedBooster),
            CaseWith(x => x.Gravity.Morph.PowerBomb.Super.Grapple),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Hammer.Glove),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Mitt),
        };

        protected override List<Case> YellowMaridiaWateringHole => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.SpeedBooster),
            CaseWith(x => x.Morph.PowerBomb.Grapple),
            CaseWith(x => x.Morph.PowerBomb.MoonPearl.Flippers.Gravity.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.Morph.PowerBomb.MoonPearl.Flippers.Gravity.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.Morph.PowerBomb.MoonPearl.Flippers.Gravity.Hammer.Glove),
            CaseWith(x => x.Morph.PowerBomb.MoonPearl.Flippers.Gravity.Mitt),
        };
        protected override List<Case> MissileYellowMaridiaFalseWall => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.SpeedBooster),
            CaseWith(x => x.Morph.PowerBomb.Grapple),
            CaseWith(x => x.Morph.PowerBomb.MoonPearl.Flippers.Gravity.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.Morph.PowerBomb.MoonPearl.Flippers.Gravity.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.Morph.PowerBomb.MoonPearl.Flippers.Gravity.Hammer.Glove),
            CaseWith(x => x.Morph.PowerBomb.MoonPearl.Flippers.Gravity.Mitt),
        };

        protected override List<Case> PlasmaBeam => new() {
            CaseWith(x => x.SpeedBooster.Gravity.HiJump.ScrewAttack),
            CaseWith(x => x.SpeedBooster.Gravity.HiJump.Plasma),
            CaseWith(x => x.SpeedBooster.Gravity.SpaceJump.ScrewAttack),
            CaseWith(x => x.SpeedBooster.Gravity.SpaceJump.Plasma),
            CaseWith(x => x.SpeedBooster.Gravity.Morph.Bombs.ScrewAttack),
            CaseWith(x => x.SpeedBooster.Gravity.Morph.Bombs.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).SpaceJump.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).SpaceJump.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).SpaceJump.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).SpaceJump.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Bombs.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Bombs.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.SpaceJump.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.SpaceJump.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Bombs.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Bombs.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.SpaceJump.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.SpaceJump.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Bombs.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Bombs.Plasma),
        };

        protected override List<Case> LeftMaridiaSandPitRoom => new() {
            CaseWith(x => x.SpaceJump.Super.Morph.PowerBomb),
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.SpeedBooster.Super.Morph.PowerBomb),
            CaseWith(x => x.Grapple.Super.Morph.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Super.PowerBomb),
        };

        protected override List<Case> MissileRightMaridiaSandPitRoom => RightMaridiaSandPitRoom;
        protected override List<Case> PowerBombRightMaridiaSandPitRoom => RightMaridiaSandPitRoom;

        List<Case> RightMaridiaSandPitRoom => new() {
            CaseWith(x => x.SpaceJump.Super),
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.SpeedBooster.Super),
            CaseWith(x => x.Grapple.Super),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Super),
        };

        protected override List<Case> PinkMaridia => new() {
            CaseWith(x => x.SpeedBooster),
        };

        protected override List<Case> SpringBall => new() {
            CaseWith(x => x.Super.Grapple.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.Super.Grapple.Morph.PowerBomb.HiJump),
        };

        protected override List<Case> MissileDraygon => new() {
            CaseWith(x => x.SpeedBooster),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt),
        };

        protected override List<Case> EnergyTankBotwoon => new() {
            CaseWith(x => x.SpeedBooster),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt),
        };

        protected override List<Case> SpaceJump => new() {
            CaseWith(x => x.SpeedBooster.Gravity.HiJump),
            CaseWith(x => x.SpeedBooster.Gravity.SpaceJump),
            CaseWith(x => x.SpeedBooster.Gravity.Morph.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Bombs),
        };

        #endregion

        #region Norfair Upper West

        protected override List<Case> NorfairUpperWest => new() {
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.Morph.PowerBomb.Super),
            CaseWith(x => x.ScrewAttack.Super.Morph),
            CaseWith(x => x.SpeedBooster.Super.Morph),
            CaseWith(x => x.Flute),
            CaseWith(x => x.Glove.Lamp),
        };

        protected override List<Case> MissileLavaRoom => new() {
            CaseWith(x => x.Varia.Missile.SpaceJump.Morph),
            CaseWith(x => x.Varia.Missile.Morph.Bombs),
            CaseWith(x => x.Varia.Missile.HiJump.Morph),
            CaseWith(x => x.Varia.Missile.SpeedBooster.Morph),
            CaseWith(x => x.Varia.Super.SpaceJump.Morph),
            CaseWith(x => x.Varia.Super.Morph.Bombs),
            CaseWith(x => x.Varia.Super.HiJump.Morph),
            CaseWith(x => x.Varia.Super.SpeedBooster.Morph),
        };

        protected override List<Case> IceBeam => new() {
            CaseWith(x => x.Super.Morph.Bombs.Varia.SpeedBooster),
            CaseWith(x => x.Super.Morph.PowerBomb.Varia.SpeedBooster),
        };

        protected override List<Case> MissileBelowIceBeam => new() {
            CaseWith(x => x.Super.Morph.PowerBomb.Varia.SpeedBooster),
        };

        protected override List<Case> HiJumpBoots => new() {
            CaseWith(x => x.Missile.Morph.Bombs),
            CaseWith(x => x.Missile.Morph.PowerBomb),
            CaseWith(x => x.Super.Morph.Bombs),
            CaseWith(x => x.Super.Morph.PowerBomb),
        };

        protected override List<Case> MissileHiJumpBoots => new() {
            CaseWith(x => x.Missile.Morph),
            CaseWith(x => x.Super.Morph),
        };

        protected override List<Case> EnergyTankHiJumpBoots => new() {
            CaseWith(x => x.Missile),
            CaseWith(x => x.Super),
        };

        #endregion

        #region Norfair Upper East

        protected override List<Case> NorfairUpperEast => new() {
            CaseWith(x => x.Morph.Bombs.Super.Varia),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.HiJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.SpaceJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.HiJump),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia),
            CaseWith(x => x.Flute.Varia.Super.SpaceJump),
            CaseWith(x => x.Flute.Varia.Super.HiJump),
            CaseWith(x => x.Flute.Varia.Super.SpeedBooster),
            CaseWith(x => x.Glove.Lamp.Varia.Super.SpaceJump),
            CaseWith(x => x.Glove.Lamp.Varia.Super.HiJump),
            CaseWith(x => x.Glove.Lamp.Varia.Super.SpeedBooster),
        };

        protected override List<Case> ReserveTankNorfair => new() {
            CaseWith(x => x.Morph.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.Grapple.SpeedBooster),
            CaseWith(x => x.Morph.Grapple.PowerBomb),
            CaseWith(x => x.Morph.HiJump),
            CaseWith(x => x.Morph.Ice),
        };
        protected override List<Case> MissileNorfairReserveTank => new() {
            CaseWith(x => x.Morph.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.Grapple.SpeedBooster),
            CaseWith(x => x.Morph.Grapple.PowerBomb),
            CaseWith(x => x.Morph.HiJump),
            CaseWith(x => x.Morph.Ice),
        };

        protected override List<Case> MissileBubbleNorfairGreenDoor => new() {
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Grapple.Morph.SpeedBooster),
            CaseWith(x => x.Grapple.Morph.PowerBomb),
            CaseWith(x => x.HiJump),
            CaseWith(x => x.Ice),
        };

        protected override List<Case> MissileBubbleNorfair => CaseWithNothing;

        protected override List<Case> MissileSpeedBooster => new() {
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.SpeedBooster),
            CaseWith(x => x.Morph.PowerBomb),
            CaseWith(x => x.HiJump),
            CaseWith(x => x.Ice),
        };
        protected override List<Case> SpeedBooster => new() {
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.SpeedBooster),
            CaseWith(x => x.Morph.PowerBomb),
            CaseWith(x => x.HiJump),
            CaseWith(x => x.Ice),
        };

        protected override List<Case> MissileWaveBeam => new() {
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.SpeedBooster),
            CaseWith(x => x.Morph.PowerBomb),
            CaseWith(x => x.HiJump),
            CaseWith(x => x.Ice),
        };

        protected override List<Case> WaveBeam => new() {
            CaseWith(x => x.Morph.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.SpeedBooster),
            CaseWith(x => x.Morph.PowerBomb),
            CaseWith(x => x.Morph.HiJump),
            CaseWith(x => x.Morph.Ice),
        };

        #endregion

        #region Norfair Upper Crocomire

        protected override List<Case> NorfairUpperCrocomire => new() {
            CaseWith(x => x.Varia.Morph.Bombs.Super.Wave),
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.SpaceJump.Wave),
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.HiJump.Wave),
            CaseWith(x => x.Varia.ScrewAttack.Super.Morph.SpaceJump.Gravity.Wave),
            CaseWith(x => x.Varia.ScrewAttack.Super.Morph.HiJump.Gravity.Wave),
            CaseWith(x => x.Varia.SpeedBooster.Super.Morph.PowerBomb),
            CaseWith(x => x.Varia.SpeedBooster.Super.Morph.Wave),
            CaseWith(x => x.Varia.Flute.SpeedBooster.Wave),
            CaseWith(x => x.Varia.Flute.Super.SpaceJump.Gravity.Morph.Wave),
            CaseWith(x => x.Varia.Flute.Super.HiJump.Gravity.Morph.Wave),
            CaseWith(x => x.Varia.Glove.Lamp.SpeedBooster.Wave),
            CaseWith(x => x.Varia.Glove.Lamp.Super.SpaceJump.Gravity.Morph.Wave),
            CaseWith(x => x.Varia.Glove.Lamp.Super.HiJump.Gravity.Morph.Wave),
            CaseWith(x => x.Varia.Flute.Mitt.ScrewAttack.SpaceJump.Super.Gravity.Wave),
        };

        protected override List<Case> EnergyTankCrocomire => new() {
            CaseWith(x => x.Super.ETank(1)),
            CaseWith(x => x.Super.SpaceJump),
            CaseWith(x => x.Super.Grapple),
        };

        protected override List<Case> MissileAboveCrocomire => new() {
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Grapple),
            CaseWith(x => x.HiJump.SpeedBooster),
        };

        protected override List<Case> PowerBombCrocomire => new() {
            CaseWith(x => x.Super.SpaceJump),
            CaseWith(x => x.Super.Morph.Bombs),
            CaseWith(x => x.Super.HiJump),
            CaseWith(x => x.Super.Grapple),
        };

        protected override List<Case> MissileBelowCrocomire => new() {
            CaseWith(x => x.Super.Morph),
        };

        protected override List<Case> MissileGrapplingBeam => new() {
            CaseWith(x => x.Super.Morph.SpaceJump),
            CaseWith(x => x.Super.Morph.Bombs),
            CaseWith(x => x.Super.Morph.SpeedBooster.PowerBomb),
        };
        protected override List<Case> GrapplingBeam => new() {
            CaseWith(x => x.Super.Morph.SpaceJump),
            CaseWith(x => x.Super.Morph.Bombs),
            CaseWith(x => x.Super.Morph.SpeedBooster.PowerBomb),
        };

        #endregion

        #region Norfair Lower West

        protected override List<Case> NorfairLowerWest => new() {
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.SpaceJump.Gravity),
            CaseWith(x => x.Varia.Flute.Mitt.Morph.Bombs),
            CaseWith(x => x.Varia.Flute.Mitt.Morph.PowerBomb),
            CaseWith(x => x.Varia.Flute.Mitt.ScrewAttack),
        };

        protected override List<Case> MissileGoldTorizo => new() {
            CaseWith(x => x.Morph.PowerBomb.SpaceJump.Super),
        };

        protected override List<Case> SuperMissileGoldTorizo => new() {
            CaseWith(x => x.Morph.Bombs.Super.Flute.Mitt),
            CaseWith(x => x.Morph.Bombs.Charge.Flute.Mitt),
            CaseWith(x => x.Morph.PowerBomb.Super.Flute.Mitt),
            CaseWith(x => x.Morph.PowerBomb.Super.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Charge.Flute.Mitt),
            CaseWith(x => x.Morph.PowerBomb.Charge.SpaceJump),
            CaseWith(x => x.ScrewAttack.Super.Flute.Mitt),
            CaseWith(x => x.ScrewAttack.Charge.Flute.Mitt),
        };

        protected override List<Case> ScrewAttack => new() {
            CaseWith(x => x.Morph.Bombs.Flute.Mitt),
            CaseWith(x => x.Morph.PowerBomb.Flute.Mitt),
            CaseWith(x => x.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.ScrewAttack.Flute.Mitt),
        };

        protected override List<Case> MissileMickeyMouseRoom => new() {
            CaseWith(x => x.Morph.Super.SpaceJump.PowerBomb),
            CaseWith(x => x.Morph.Super.Bombs.PowerBomb),
        };

        #endregion

        #region Norfair Lower East

        protected override List<Case> NorfairLowerEast => new() {
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.SpaceJump.Gravity),
            CaseWith(x => x.Varia.Flute.Mitt.Morph.PowerBomb.Super.SpaceJump),
            CaseWith(x => x.Varia.Flute.Mitt.Morph.PowerBomb.Super.Bombs),
        };

        protected override List<Case> MissileLowerNorfairAboveFireFleaRoom => new() {
            CaseWith(x => x.Morph),
        };
        protected override List<Case> PowerBombLowerNorfairAboveFireFleaRoom => new() {
            CaseWith(x => x.Morph),
        };

        protected override List<Case> PowerBombPowerBombsOfShame => new() {
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> MissileLowerNorfairNearWaveBeam => new() {
            CaseWith(x => x.Morph),
        };

        protected override List<Case> EnergyTankRidley => new() {
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        protected override List<Case> EnergyTankFirefleas => new() {
            CaseWith(x => x.Morph),
        };

        #endregion

        #region Light World Death Mountain West

        protected override List<Case> LightWorldDeathMountainWest => new() {
            CaseWith(x => x.Flute),
            CaseWith(x => x.Glove.Lamp),
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.Morph.PowerBomb.Super),
            CaseWith(x => x.ScrewAttack.Super.Morph),
            CaseWith(x => x.SpeedBooster.Super.Morph),
        };

        protected override List<Case> EtherTablet => new() {
            CaseWith(x => x.Book.MasterSword.Mirror),
            CaseWith(x => x.Book.MasterSword.Hammer.Hookshot),
        };

        protected override List<Case> SpectacleRock => new() {
            CaseWith(x => x.Mirror),
        };

        protected override List<Case> SpectacleRockCave => CaseWithNothing;

        protected override List<Case> OldMan => new() {
            CaseWith(x => x.Lamp),
        };

        #endregion

        #region Light World Death Mountain East

        protected override List<Case> LightWorldDeathMountainEast => new() {
            CaseWith(x => x.Flute.Hammer.Mirror),
            CaseWith(x => x.Flute.Hookshot),
            CaseWith(x => x.Glove.Lamp.Hammer.Mirror),
            CaseWith(x => x.Glove.Lamp.Hookshot),
            CaseWith(x => x.Morph.Bombs.Super.Hammer.Mirror),
            CaseWith(x => x.Morph.Bombs.Super.Hookshot),
            CaseWith(x => x.Morph.PowerBomb.Super.Hammer.Mirror),
            CaseWith(x => x.Morph.PowerBomb.Super.Hookshot),
            CaseWith(x => x.ScrewAttack.Super.Morph.Hammer.Mirror),
            CaseWith(x => x.ScrewAttack.Super.Morph.Hookshot),
            CaseWith(x => x.SpeedBooster.Super.Morph.Hammer.Mirror),
            CaseWith(x => x.SpeedBooster.Super.Morph.Hookshot),
        };

        protected override List<Case> FloatingIsland => new() {
            CaseWith(x => x.Mirror.MoonPearl.Mitt),
        };

        protected override List<Case> SpiralCave => CaseWithNothing;
        protected override List<Case> ParadoxCave => CaseWithNothing;

        protected override List<Case> MimicCave => new() {
            CaseWith(x => x.Mirror.KeyTR(2).Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.Flute),
            CaseWith(x => x.Mirror.KeyTR(2).Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.Lamp),
            CaseWith(x => x.Mirror.KeyTR(2).Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.Morph.Bombs.Super),
            CaseWith(x => x.Mirror.KeyTR(2).Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.Morph.PowerBomb.Super),
            CaseWith(x => x.Mirror.KeyTR(2).Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.ScrewAttack.Super.Morph),
            CaseWith(x => x.Mirror.KeyTR(2).Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.SpeedBooster.Super.Morph),
        };

        #endregion

        #region Light World North West

        protected override List<Case> LightWorldNorthWest => CaseWithNothing;

        // Assumes prizes can be acquired
        protected override List<Case> MasterSwordPedestal => CaseWithNothing;

        protected override List<Case> Mushroom => CaseWithNothing;
        protected override List<Case> LostWoodsHideout => CaseWithNothing;

        protected override List<Case> LumberjackTree => new() {
            CaseWith(x => x.Sword.Cape.Lamp.KeyCT(2).Boots),
            CaseWith(x => x.MasterSword.Lamp.KeyCT(2).Boots),
        };

        protected override List<Case> PegasusRocks => new() {
            CaseWith(x => x.Boots),
        };

        protected override List<Case> GraveyardLedge => new() {
            CaseWith(x => x.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Hammer.Glove),
            CaseWith(x => x.Mirror.MoonPearl.Mitt),
        };

        protected override List<Case> KingsTomb => new() {
            CaseWith(x => x.Boots.Mitt),
            CaseWith(x => x.Boots.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Boots.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Boots.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Hammer),
            CaseWith(x => x.Boots.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Boots.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Boots.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Hammer),
            CaseWith(x => x.Boots.Mirror.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Boots.Mirror.MoonPearl.Hammer.Glove),
        };

        protected override List<Case> KakarikoWell => CaseWithNothing;
        protected override List<Case> BlindsHideout => CaseWithNothing;
        protected override List<Case> BottleMerchant => CaseWithNothing;
        protected override List<Case> ChickenHouse => CaseWithNothing;

        protected override List<Case> SickKid => new() {
            CaseWith(x => x.Bottle),
        };

        protected override List<Case> KakarikoTavern => CaseWithNothing;

        protected override List<Case> MagicBat => new() {
            CaseWith(x => x.Powder.Hammer),
            CaseWith(x => x.Powder.MoonPearl.Mirror.Mitt),
        };

        #endregion

        #region Light World North East

        protected override List<Case> LightWorldNorthEast => CaseWithNothing;

        protected override List<Case> KingZora => new() {
            CaseWith(x => x.Glove),
            CaseWith(x => x.Flippers),
        };

        protected override List<Case> ZorasLedge => new() {
            CaseWith(x => x.Flippers),
        };
        protected override List<Case> WaterfallFairy => new() {
            CaseWith(x => x.Flippers),
        };

        protected override List<Case> PotionShop => new() {
            CaseWith(x => x.Mushroom),
        };

        protected override List<Case> SahasrahlasHut => CaseWithNothing;

        // Assumes prize can be acquired
        protected override List<Case> Sahasrahla => CaseWithNothing;

        #endregion

        #region Light World South

        protected override List<Case> LightWorldSouth => CaseWithNothing;

        protected override List<Case> MazeRace => CaseWithNothing;

        protected override List<Case> Library => new() {
            CaseWith(x => x.Boots),
        };

        protected override List<Case> FluteSpot => new() {
            CaseWith(x => x.Shovel),
        };

        protected override List<Case> SouthOfGrove => new() {
            CaseWith(x => x.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Hammer.Glove),
            CaseWith(x => x.Mirror.MoonPearl.Mitt),
        };

        protected override List<Case> LinksHouse => CaseWithNothing;
        protected override List<Case> AginahsCave => CaseWithNothing;
        protected override List<Case> MiniMoldormCave => CaseWithNothing;

        protected override List<Case> DesertLedge => DesertPalace;

        protected override List<Case> CheckerboardCave => new() {
            CaseWith(x => x.Mirror.Flute.Mitt),
            CaseWith(x => x.Mirror.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Glove),
        };

        protected override List<Case> BombosTablet => new() {
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Hammer.Glove),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Mitt),
        };

        protected override List<Case> FloodgateChest => CaseWithNothing;
        protected override List<Case> SunkenTreasure => CaseWithNothing;

        protected override List<Case> LakeHyliaIsland => new() {
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Hammer.Glove),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Mitt),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Morph.PowerBomb.Super.Gravity.SpeedBooster),
        };

        protected override List<Case> Hobo => new() {
            CaseWith(x => x.Flippers),
        };

        protected override List<Case> IceRodCave => CaseWithNothing;

        #endregion

        #region Dark World Death Mountain West

        protected override List<Case> DarkWorldDeathMountainWest => CaseWithNothing;

        protected override List<Case> SpikeCave => new() {
            CaseWith(x => x.MoonPearl.Hammer.Glove.HalfMagic.Cape.Flute),
            CaseWith(x => x.MoonPearl.Hammer.Glove.HalfMagic.Cape.Lamp),
            CaseWith(x => x.MoonPearl.Hammer.Glove.HalfMagic.Cape.Morph.Bombs.Super),
            CaseWith(x => x.MoonPearl.Hammer.Glove.HalfMagic.Cape.Morph.PowerBomb.Super),
            CaseWith(x => x.MoonPearl.Hammer.Glove.HalfMagic.Cape.ScrewAttack.Super.Morph),
            CaseWith(x => x.MoonPearl.Hammer.Glove.HalfMagic.Cape.SpeedBooster.Super.Morph),
            CaseWith(x => x.MoonPearl.Hammer.Glove.Bottle.Cape.Flute),
            CaseWith(x => x.MoonPearl.Hammer.Glove.Bottle.Cape.Lamp),
            CaseWith(x => x.MoonPearl.Hammer.Glove.Bottle.Cape.Morph.Bombs.Super),
            CaseWith(x => x.MoonPearl.Hammer.Glove.Bottle.Cape.Morph.PowerBomb.Super),
            CaseWith(x => x.MoonPearl.Hammer.Glove.Bottle.Cape.ScrewAttack.Super.Morph),
            CaseWith(x => x.MoonPearl.Hammer.Glove.Bottle.Cape.SpeedBooster.Super.Morph),
            CaseWith(x => x.MoonPearl.Hammer.Glove.Byrna.Flute),
            CaseWith(x => x.MoonPearl.Hammer.Glove.Byrna.Lamp),
            CaseWith(x => x.MoonPearl.Hammer.Glove.Byrna.Morph.Bombs.Super),
            CaseWith(x => x.MoonPearl.Hammer.Glove.Byrna.Morph.PowerBomb.Super),
            CaseWith(x => x.MoonPearl.Hammer.Glove.Byrna.ScrewAttack.Super.Morph),
            CaseWith(x => x.MoonPearl.Hammer.Glove.Byrna.SpeedBooster.Super.Morph),
        };

        #endregion

        #region Dark World Death Mountain East

        protected override List<Case> DarkWorldDeathMountainEast => new() {
            CaseWith(x => x.Mitt.Flute.Hammer.Mirror),
            CaseWith(x => x.Mitt.Flute.Hookshot),
            CaseWith(x => x.Mitt.Lamp.Hammer.Mirror),
            CaseWith(x => x.Mitt.Lamp.Hookshot),
            CaseWith(x => x.Mitt.Morph.Bombs.Super.Hammer.Mirror),
            CaseWith(x => x.Mitt.Morph.Bombs.Super.Hookshot),
            CaseWith(x => x.Mitt.Morph.PowerBomb.Super.Hammer.Mirror),
            CaseWith(x => x.Mitt.Morph.PowerBomb.Super.Hookshot),
            CaseWith(x => x.Mitt.ScrewAttack.Super.Morph.Hammer.Mirror),
            CaseWith(x => x.Mitt.ScrewAttack.Super.Morph.Hookshot),
            CaseWith(x => x.Mitt.SpeedBooster.Super.Morph.Hammer.Mirror),
            CaseWith(x => x.Mitt.SpeedBooster.Super.Morph.Hookshot),
        };

        protected override List<Case> HookshotCave => new() {
            CaseWith(x => x.MoonPearl.Hookshot),
        };

        protected override List<Case> HookshotCaveBottomRight => new() {
            CaseWith(x => x.MoonPearl.Hookshot),
            CaseWith(x => x.MoonPearl.Boots),
        };

        protected override List<Case> SuperbunnyCave => new() {
            CaseWith(x => x.MoonPearl),
        };

        #endregion

        #region Dark World North West

        protected override List<Case> DarkWorldNorthWest => new() {
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Hammer),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt),
        };

        protected override List<Case> BumperCave => new() {
            CaseWith(x => x.Glove.Cape),
        };

        protected override List<Case> VillageOfOutcasts => CaseWithNothing;

        protected override List<Case> HammerPegs => new() {
            CaseWith(x => x.Mitt.Hammer),
        };

        protected override List<Case> Blacksmith => new() {
            CaseWith(x => x.Mitt),
        };
        protected override List<Case> PurpleChest => new() {
            CaseWith(x => x.Mitt),
        };

        #endregion

        #region Dark World North East

        protected override List<Case> DarkWorldNorthEast => new() {
            CaseWith(x => x.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers),
        };

        protected override List<Case> Catfish => new() {
            CaseWith(x => x.MoonPearl.Glove),
        };

        protected override List<Case> Pyramid => CaseWithNothing;

        // Assumes prizes can be acquired
        protected override List<Case> PyramidFairy => new() {
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers.Mirror),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove.Mirror),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers.Mirror),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove.Mirror),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt.Mirror.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Mitt.Mirror.MasterSword.Lamp.KeyCT(2)),
        };

        #endregion

        #region Dark World South

        protected override List<Case> DarkWorldSouth => new() {
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt),
        };

        protected override List<Case> DiggingGame => CaseWithNothing;
        protected override List<Case> Stumpy => CaseWithNothing;
        protected override List<Case> HypeCave => CaseWithNothing;

        #endregion

        #region Dark World Mire

        protected override List<Case> DarkWorldMire => new() {
            CaseWith(x => x.Flute.Mitt),
            CaseWith(x => x.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb),
        };

        protected override List<Case> MireShed => new() {
            CaseWith(x => x.MoonPearl),
        };

        #endregion

        #region Hyrule Castle

        protected override List<Case> HyruleCastle => CaseWithNothing;

        protected override List<Case> Sanctuary => CaseWithNothing;

        protected override List<Case> SewersSecretRoom => new() {
            CaseWith(x => x.Glove),
            CaseWith(x => x.Lamp.KeyHC(1)),
        };

        protected override List<Case> SewersDarkCross => new() {
            CaseWith(x => x.Lamp),
        };

        protected override List<Case> HyruleCastleMapChest => CaseWithNothing;

        protected override List<Case> HyruleCastleBoomerangChest => new() {
            CaseWith(x => x.KeyHC(1)),
        };
        protected override List<Case> HyruleCastleZeldasCell => new() {
            CaseWith(x => x.KeyHC(1)),
        };

        protected override List<Case> LinksUncle => CaseWithNothing;
        protected override List<Case> SecretPassage => CaseWithNothing;

        #endregion

        #region Castle Tower

        protected override List<Case> CastleTower => new() {
            CaseWith(x => x.MasterSword),
            CaseWith(x => x.Sword.Cape),
            CaseWith(x => x.Hammer.Cape),
            CaseWith(x => x.Bow.Cape),
            CaseWith(x => x.Firerod.Cape),
            CaseWith(x => x.Somaria.Cape),
            CaseWith(x => x.HalfMagic.Byrna.Cape),
            CaseWith(x => x.Bottle.Byrna.Cape),
        };

        protected override List<Case> CastleTowerFoyer => CaseWithNothing;

        protected override List<Case> CastleTowerDarkMaze => new() {
            CaseWith(x => x.Lamp.KeyCT(1)),
        };

        #endregion

        #region Eastern Palace

        protected override List<Case> EasternPalace => CaseWithNothing;

        protected override List<Case> EasternPalaceCannonballChest => CaseWithNothing;
        protected override List<Case> EasternPalaceMapChest => CaseWithNothing;
        protected override List<Case> EasternPalaceCompassChest => CaseWithNothing;

        protected override List<Case> EasternPalaceBigChest => new() {
            CaseWith(x => x.BigKeyEP),
        };

        protected override List<Case> EasternPalaceBigKeyChest => new() {
            CaseWith(x => x.Lamp),
        };

        protected override List<Case> EasternPalaceArmosKnights => new() {
            CaseWith(x => x.BigKeyEP.Bow.Lamp),
        };

        #endregion

        #region Desert Palace

        protected override List<Case> DesertPalace => new() {
            CaseWith(x => x.Book),
            CaseWith(x => x.Mirror.Mitt.Flute),
            CaseWith(x => x.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror),
        };

        protected override List<Case> DesertPalaceBigChest => new() {
            CaseWith(x => x.BigKeyDP),
        };

        protected override List<Case> DesertPalaceTorch => new() {
            CaseWith(x => x.Boots),
        };

        protected override List<Case> DesertPalaceMapChest => CaseWithNothing;

        protected override List<Case> DesertPalaceBigKeyChest => new() {
            CaseWith(x => x.KeyDP(1)),
        };
        protected override List<Case> DesertPalaceCompassChest => new() {
            CaseWith(x => x.KeyDP(1)),
        };

        protected override List<Case> DesertPalaceLanmolas => new() {
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Firerod),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Lamp.Sword),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Lamp.Hammer),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Lamp.Bow),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Lamp.Icerod),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Lamp.Byrna),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Lamp.Somaria),
            CaseWith(x => x.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Firerod),
            CaseWith(x => x.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Sword),
            CaseWith(x => x.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Hammer),
            CaseWith(x => x.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Bow),
            CaseWith(x => x.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Icerod),
            CaseWith(x => x.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Byrna),
            CaseWith(x => x.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Somaria),
        };

        #endregion

        #region Tower of Hera

        protected override List<Case> TowerOfHera => new() {
            CaseWith(x => x.Mirror.Flute),
            CaseWith(x => x.Mirror.Glove.Lamp),
            CaseWith(x => x.Mirror.Morph.Bombs.Super),
            CaseWith(x => x.Mirror.Morph.PowerBomb.Super),
            CaseWith(x => x.Mirror.ScrewAttack.Super.Morph),
            CaseWith(x => x.Mirror.SpeedBooster.Super.Morph),
            CaseWith(x => x.Hookshot.Hammer.Flute),
            CaseWith(x => x.Hookshot.Hammer.Glove.Lamp),
            CaseWith(x => x.Hookshot.Hammer.Morph.Bombs.Super),
            CaseWith(x => x.Hookshot.Hammer.Morph.PowerBomb.Super),
            CaseWith(x => x.Hookshot.Hammer.ScrewAttack.Super.Morph),
            CaseWith(x => x.Hookshot.Hammer.SpeedBooster.Super.Morph),
        };

        protected override List<Case> TowerOfHeraBasementCage => CaseWithNothing;
        protected override List<Case> TowerOfHeraMapChest => CaseWithNothing;

        protected override List<Case> TowerOfHeraBigKeyChest => new() {
            CaseWith(x => x.KeyTH(1).Firerod),
            CaseWith(x => x.KeyTH(1).Lamp),
        };

        protected override List<Case> TowerOfHeraCompassChest => new() {
            CaseWith(x => x.BigKeyTH),
        };
        protected override List<Case> TowerOfHeraBigChest => new() {
            CaseWith(x => x.BigKeyTH),
        };

        protected override List<Case> TowerOfHeraMoldorm => new() {
            CaseWith(x => x.BigKeyTH.Sword),
            CaseWith(x => x.BigKeyTH.Hammer),
        };

        #endregion

        #region Palace of Darkness

        protected override List<Case> PalaceOfDarkness => new() {
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers),
        };

        protected override List<Case> PalaceOfDarknessShooterRoom => CaseWithNothing;

        protected override List<Case> PalaceOfDarknessBigKeyChest => new() {
            CaseWith(x => x.Has.KeyPD.KeyPD(1)),
            CaseWith(x => x.IfSkipping.Hammer.Bow.Lamp.AlsoSkip.KeyPD(2).KeyPD(6)),
            CaseWith(x => x.IfSkipping.Hammer.Bow.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.IfSkipping.Hammer.Lamp.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.IfSkipping.Hammer.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.IfSkipping.Bow.Lamp.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.IfSkipping.Bow.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.IfSkipping.Lamp.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.KeyPD(5)),
        };

        protected override List<Case> PalaceOfDarknessStalfosBasement => new() {
            CaseWith(x => x.KeyPD(1)),
            CaseWith(x => x.Bow.Hammer),
        };
        protected override List<Case> PalaceOfDarknessTheArenaBridge => new() {
            CaseWith(x => x.KeyPD(1)),
            CaseWith(x => x.Bow.Hammer),
        };

        protected override List<Case> PalaceOfDarknessTheArenaLedge => new() {
            CaseWith(x => x.Bow),
        };
        protected override List<Case> PalaceOfDarknessMapChest => new() {
            CaseWith(x => x.Bow),
        };

        protected override List<Case> PalaceOfDarknessCompassChest => new() {
            CaseWith(x => x.IfSkipping.Hammer.Bow.Lamp.AlsoSkip.KeyPD(2).KeyPD(4)),
            CaseWith(x => x.IfSkipping.Hammer.Bow.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.IfSkipping.Hammer.Lamp.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.IfSkipping.Hammer.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.IfSkipping.Bow.Lamp.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.IfSkipping.Bow.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.IfSkipping.Lamp.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.KeyPD(3)),
        };

        protected override List<Case> PalaceOfDarknessHarmlessHellway => new() {
            CaseWith(x => x.Has.KeyPD.IfSkipping.Hammer.Bow.Lamp.AlsoSkip.KeyPD(2).KeyPD(4)),
            CaseWith(x => x.Has.KeyPD.IfSkipping.Hammer.Bow.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.Has.KeyPD.IfSkipping.Hammer.Lamp.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.Has.KeyPD.IfSkipping.Bow.Lamp.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.Has.KeyPD.IfSkipping.Hammer.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.Has.KeyPD.IfSkipping.Bow.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.Has.KeyPD.IfSkipping.Lamp.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.Has.KeyPD.KeyPD(3)),
            CaseWith(x => x.IfSkipping.Hammer.Bow.Lamp.AlsoSkip.KeyPD(2).KeyPD(6)),
            CaseWith(x => x.IfSkipping.Hammer.Bow.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.IfSkipping.Hammer.Lamp.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.IfSkipping.Bow.Lamp.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.IfSkipping.Hammer.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.IfSkipping.Bow.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.IfSkipping.Lamp.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.KeyPD(5)),
        };

        protected override List<Case> PalaceOfDarknessDarkBasement => new() {
            CaseWith(x => x.Lamp.IfSkipping.Hammer.Bow.AlsoSkip.KeyPD(2).KeyPD(4)),
            CaseWith(x => x.Lamp.IfSkipping.Hammer.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.Lamp.IfSkipping.Bow.AlsoSkip.KeyPD(1).KeyPD(3)),
            CaseWith(x => x.Lamp.KeyPD(3)),
        };

        protected override List<Case> PalaceOfDarknessDarkMaze => new() {
            CaseWith(x => x.Lamp.IfSkipping.Hammer.Bow.AlsoSkip.KeyPD(2).KeyPD(6)),
            CaseWith(x => x.Lamp.IfSkipping.Hammer.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.Lamp.IfSkipping.Bow.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.Lamp.KeyPD(5)),
        };

        protected override List<Case> PalaceOfDarknessBigChest => new() {
            CaseWith(x => x.BigKeyPD.Lamp.IfSkipping.Hammer.Bow.AlsoSkip.KeyPD(2).KeyPD(6)),
            CaseWith(x => x.BigKeyPD.Lamp.IfSkipping.Hammer.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.BigKeyPD.Lamp.IfSkipping.Bow.AlsoSkip.KeyPD(1).KeyPD(5)),
            CaseWith(x => x.BigKeyPD.Lamp.KeyPD(5)),
        };

        protected override List<Case> PalaceOfDarknessHelmasaurKing => new() {
            CaseWith(x => x.Lamp.Hammer.Bow.BigKeyPD.KeyPD(6)),
        };

        #endregion

        #region Swamp Palace

        protected override List<Case> SwampPalace => new() {
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Sword.Cape.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Sword.Cape.Lamp.KeyCT(2).Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.MasterSword.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.MasterSword.Lamp.KeyCT(2).Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Gravity.SpeedBooster.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Gravity.SpeedBooster.Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Mitt),
        };

        protected override List<Case> SwampPalaceEntrance => CaseWithNothing;

        protected override List<Case> SwampPalaceMapChest => new() {
            CaseWith(x => x.KeySP(1)),
        };

        protected override List<Case> SwampPalaceBigChest => new() {
            CaseWith(x => x.BigKeySP.KeySP(1).Hammer),
        };

        protected override List<Case> SwampPalaceCompassChest => new() {
            CaseWith(x => x.KeySP(1).Hammer),
        };
        protected override List<Case> SwampPalaceWestWing => new() {
            CaseWith(x => x.KeySP(1).Hammer),
        };

        protected override List<Case> SwampPalaceFloodedRoom => new() {
            CaseWith(x => x.KeySP(1).Hammer.Hookshot),
        };
        protected override List<Case> SwampPalaceWaterfallRoom => new() {
            CaseWith(x => x.KeySP(1).Hammer.Hookshot),
        };

        protected override List<Case> SwampPalaceArrghus => new() {
            CaseWith(x => x.KeySP(1).Hammer.Hookshot),
        };

        #endregion

        #region Skull Woods

        protected override List<Case> SkullWoods => new() {
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Hammer),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt),
        };

        protected override List<Case> SkullWoodsPotPrison => CaseWithNothing;
        protected override List<Case> SkullWoodsCompassChest => CaseWithNothing;

        protected override List<Case> SkullWoodsBigChest => new() {
            CaseWith(x => x.BigKeySW),
        };

        protected override List<Case> SkullWoodsMapChest => CaseWithNothing;
        // Skipping "Skull Woods - Pinball Room" since it is always a key
        protected override List<Case> SkullWoodsBigKeyChest => CaseWithNothing;

        protected override List<Case> SkullWoodsBridgeRoom => new() {
            CaseWith(x => x.Firerod),
        };

        protected override List<Case> SkullWoodsMothula => new() {
            CaseWith(x => x.Firerod.Sword.KeySW(3)),
        };

        #endregion

        #region Thieves' Town

        protected override List<Case> ThievesTown => new() {
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Hammer),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt),
        };

        protected override List<Case> ThievesTownCourtyard => CaseWithNothing;

        protected override List<Case> ThievesTownAttic => new() {
            CaseWith(x => x.BigKeyTT.KeyTT(1)),
        };

        protected override List<Case> ThievesTownBlindsCell => new() {
            CaseWith(x => x.BigKeyTT),
        };

        protected override List<Case> ThievesTownBigChest => new() {
            CaseWith(x => x.BigKeyTT.Hammer.Has.KeyTT),
            CaseWith(x => x.BigKeyTT.Hammer.KeyTT(1)),
        };

        protected override List<Case> ThievesTownBlind => new() {
            CaseWith(x => x.BigKeyTT.KeyTT(1).Sword),
            CaseWith(x => x.BigKeyTT.KeyTT(1).Hammer),
            CaseWith(x => x.BigKeyTT.KeyTT(1).Somaria),
            CaseWith(x => x.BigKeyTT.KeyTT(1).Byrna),
        };

        #endregion

        #region Ice Palace

        protected override List<Case> IcePalace => new() {
            CaseWith(x => x.MoonPearl.Flippers.Mitt.Firerod),
            CaseWith(x => x.MoonPearl.Flippers.Mitt.Bombos.Sword),
        };

        protected override List<Case> IcePalaceCompassChest => CaseWithNothing;

        protected override List<Case> IcePalaceSpikeRoom => new() {
            CaseWith(x => x.Hookshot),
            // Todo: These cases are ignored for now because the Playthrough does not handle progress backtracking
            //CaseWith(x => x.KeyIP(1)),
            //CaseWith(x => x.KeyIP(1).Assume.BigKeyIP.HasAt("Ice Palace - Map Chest").BigKeyIP),
            //CaseWith(x => x.KeyIP(1).Assume.BigKeyIP.HasAt("Ice Palace - Big Key Chest").BigKeyIP),
        };

        protected override List<Case> IcePalaceMapChest => new() {
            CaseWith(x => x.Hammer.Glove.Hookshot),
            // Todo: These cases are ignored for now because the Playthrough does not handle progress backtracking
            //CaseWith(x => x.Hammer.Glove.KeyIP(1)),
            //CaseWith(x => x.Hammer.Glove.KeyIP(1).Assume.BigKeyIP.HasAt("Ice Palace - Spike Room").BigKeyIP),
            //CaseWith(x => x.Hammer.Glove.KeyIP(1).Assume.BigKeyIP.HasAt("Ice Palace - Big Key Chest").BigKeyIP),
        };

        protected override List<Case> IcePalaceBigKeyChest => new() {
            CaseWith(x => x.Hammer.Glove.Hookshot),
            // Todo: These cases are ignored for now because the Playthrough does not handle progress backtracking
            //CaseWith(x => x.Hammer.Glove.KeyIP(1)),
            //CaseWith(x => x.Hammer.Glove.KeyIP(1).Assume.BigKeyIP.HasAt("Ice Palace - Spike Room").BigKeyIP),
            //CaseWith(x => x.Hammer.Glove.KeyIP(1).Assume.BigKeyIP.HasAt("Ice Palace - Map Chest").BigKeyIP),
        };

        protected override List<Case> IcePalaceIcedTRoom => CaseWithNothing;
        protected override List<Case> IcePalaceFreezorChest => CaseWithNothing;

        protected override List<Case> IcePalaceBigChest => new() {
            CaseWith(x => x.BigKeyIP),
        };

        protected override List<Case> IcePalaceKholdstare => new() {
            CaseWith(x => x.BigKeyIP.Hammer.Glove.Somaria.KeyIP(1)),
            CaseWith(x => x.BigKeyIP.Hammer.Glove.KeyIP(2)),
        };

        #endregion

        #region Misery Mire

        protected override List<Case> MiseryMire => new() {
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.Flute.Mitt),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.Flute.Mitt),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb),
        };

        protected override List<Case> MiseryMireMainLobby => new() {
            CaseWith(x => x.BigKeyMM),
            CaseWith(x => x.KeyMM(1)),
        };
        protected override List<Case> MiseryMireMapChest => new() {
            CaseWith(x => x.BigKeyMM),
            CaseWith(x => x.KeyMM(1)),
        };

        protected override List<Case> MiseryMireBridgeChest => CaseWithNothing;
        protected override List<Case> MiseryMireSpikeChest => CaseWithNothing;

        protected override List<Case> MiseryMireCompassChest => new() {
            CaseWith(x => x.Firerod.KeyMM(2).HasAt("Misery Mire - Big Key Chest").BigKeyMM),
            CaseWith(x => x.Firerod.KeyMM(3)),
            CaseWith(x => x.Lamp.KeyMM(2).HasAt("Misery Mire - Big Key Chest").BigKeyMM),
            CaseWith(x => x.Lamp.KeyMM(3)),
        };

        protected override List<Case> MiseryMireBigKeyChest => new() {
            CaseWith(x => x.Firerod.KeyMM(2).HasAt("Misery Mire - Compass Chest").BigKeyMM),
            CaseWith(x => x.Firerod.KeyMM(3)),
            CaseWith(x => x.Lamp.KeyMM(2).HasAt("Misery Mire - Compass Chest").BigKeyMM),
            CaseWith(x => x.Lamp.KeyMM(3)),
        };

        protected override List<Case> MiseryMireBigChest => new() {
            CaseWith(x => x.BigKeyMM),
        };

        protected override List<Case> MiseryMireVitreous => new() {
            CaseWith(x => x.BigKeyMM.Lamp.Somaria),
        };

        #endregion

        #region Turtle Rock

        protected override List<Case> TurtleRock => new() {
            CaseWith(x => x.Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.Flute.Mirror),
            CaseWith(x => x.Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.Flute.Hookshot),
            CaseWith(x => x.Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.Lamp.Mirror),
            CaseWith(x => x.Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.Lamp.Hookshot),
            CaseWith(x => x.Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.Morph.Bombs.Super.Mirror),
            CaseWith(x => x.Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.Morph.Bombs.Super.Hookshot),
            CaseWith(x => x.Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.Morph.PowerBomb.Super.Mirror),
            CaseWith(x => x.Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.Morph.PowerBomb.Super.Hookshot),
            CaseWith(x => x.Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.ScrewAttack.Super.Morph.Mirror),
            CaseWith(x => x.Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.ScrewAttack.Super.Morph.Hookshot),
            CaseWith(x => x.Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.SpeedBooster.Super.Morph.Mirror),
            CaseWith(x => x.Quake.Sword.MoonPearl.Mitt.Hammer.Somaria.SpeedBooster.Super.Morph.Hookshot),
        };

        protected override List<Case> TurtleRockCompassChest => CaseWithNothing;

        protected override List<Case> TurtleRockRollerRoom => new() {
            CaseWith(x => x.Firerod),
        };

        protected override List<Case> TurtleRockChainChomps => new() {
            CaseWith(x => x.KeyTR(1)),
        };

        protected override List<Case> TurtleRockBigKeyChest => new() {
            CaseWith(x => x.KeyTR(2)),
        };

        protected override List<Case> TurtleRockBigChest => new() {
            CaseWith(x => x.BigKeyTR.KeyTR(2)),
        };
        protected override List<Case> TurtleRockCrystarollerRoom => new() {
            CaseWith(x => x.BigKeyTR.KeyTR(2)),
        };

        protected override List<Case> TurtleRockEyeBridge => new() {
            CaseWith(x => x.BigKeyTR.KeyTR(3).Lamp.Cape),
            CaseWith(x => x.BigKeyTR.KeyTR(3).Lamp.Byrna),
            CaseWith(x => x.BigKeyTR.KeyTR(3).Lamp.MirrorShield),
        };

        protected override List<Case> TurtleRockTrinexx => new() {
            CaseWith(x => x.BigKeyTR.KeyTR(4).Lamp.Firerod.Icerod),
        };

        #endregion

        #region Ganon's Tower

        // Assumes prizes can be acquired
        protected override List<Case> GanonsTower => new() {
            CaseWith(x => x.MoonPearl.Mitt.Flute.Hammer.Mirror),
            CaseWith(x => x.MoonPearl.Mitt.Flute.Hookshot),
            CaseWith(x => x.MoonPearl.Mitt.Lamp.Hammer.Mirror),
            CaseWith(x => x.MoonPearl.Mitt.Lamp.Hookshot),
            CaseWith(x => x.MoonPearl.Mitt.Morph.Bombs.Super.Hammer.Mirror),
            CaseWith(x => x.MoonPearl.Mitt.Morph.Bombs.Super.Hookshot),
            CaseWith(x => x.MoonPearl.Mitt.Morph.PowerBomb.Super.Hammer.Mirror),
            CaseWith(x => x.MoonPearl.Mitt.Morph.PowerBomb.Super.Hookshot),
            CaseWith(x => x.MoonPearl.Mitt.ScrewAttack.Super.Morph.Hammer.Mirror),
            CaseWith(x => x.MoonPearl.Mitt.ScrewAttack.Super.Morph.Hookshot),
            CaseWith(x => x.MoonPearl.Mitt.SpeedBooster.Super.Morph.Hammer.Mirror),
            CaseWith(x => x.MoonPearl.Mitt.SpeedBooster.Super.Morph.Hookshot),
        };

        protected override List<Case> GanonsTowerBobsTorch => new() {
            CaseWith(x => x.Boots),
        };

        protected override List<Case> GanonsTowerDMsRoom => new() {
            CaseWith(x => x.Hammer.Hookshot),
        };

        protected override List<Case> GanonsTowerMapChest => new() {
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).Has.BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).Has.KeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(4)),
            CaseWith(x => x.Hammer.Boots.KeyGT(3).Has.BigKeyGT),
            CaseWith(x => x.Hammer.Boots.KeyGT(3).Has.KeyGT),
            CaseWith(x => x.Hammer.Boots.KeyGT(4)),
        };

        protected override List<Case> GanonsTowerFiresnakeRoom => new() {
            CaseWith(x => x.Hammer.Hookshot.KeyGT(2).HasAt("Ganon's Tower - Randomizer Room - Top Left").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(2).HasAt("Ganon's Tower - Randomizer Room - Top Right").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(2).HasAt("Ganon's Tower - Randomizer Room - Bottom Left").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(2).HasAt("Ganon's Tower - Randomizer Room - Bottom Right").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(2).Has.KeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3)),
        };

        protected override List<Case> GanonsTowerRandomizerRoomTopLeft => new() {
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).HasAt("Ganon's Tower - Randomizer Room - Top Right").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).HasAt("Ganon's Tower - Randomizer Room - Bottom Left").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).HasAt("Ganon's Tower - Randomizer Room - Bottom Right").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(4)),
        };
        protected override List<Case> GanonsTowerRandomizerRoomTopRight => new() {
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).HasAt("Ganon's Tower - Randomizer Room - Top Left").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).HasAt("Ganon's Tower - Randomizer Room - Bottom Left").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).HasAt("Ganon's Tower - Randomizer Room - Bottom Right").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(4)),
        };
        protected override List<Case> GanonsTowerRandomizerRoomBottomLeft => new() {
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).HasAt("Ganon's Tower - Randomizer Room - Top Right").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).HasAt("Ganon's Tower - Randomizer Room - Top Left").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).HasAt("Ganon's Tower - Randomizer Room - Bottom Right").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(4)),
        };
        protected override List<Case> GanonsTowerRandomizerRoomBottomRight => new() {
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).HasAt("Ganon's Tower - Randomizer Room - Top Left").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).HasAt("Ganon's Tower - Randomizer Room - Top Right").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(3).HasAt("Ganon's Tower - Randomizer Room - Bottom Left").BigKeyGT),
            CaseWith(x => x.Hammer.Hookshot.KeyGT(4)),
        };

        protected override List<Case> GanonsTowerHopeRoom => CaseWithNothing;

        protected override List<Case> GanonsTowerTileRoom => new() {
            CaseWith(x => x.Somaria),
        };

        protected override List<Case> GanonsTowerCompassRoomTopLeft => new() {
            CaseWith(x => x.Somaria.Firerod.KeyGT(3).HasAt("Ganon's Tower - Compass Room - Top Right").BigKeyGT),
            CaseWith(x => x.Somaria.Firerod.KeyGT(3).HasAt("Ganon's Tower - Compass Room - Bottom Left").BigKeyGT),
            CaseWith(x => x.Somaria.Firerod.KeyGT(3).HasAt("Ganon's Tower - Compass Room - Bottom Right").BigKeyGT),
            CaseWith(x => x.Somaria.Firerod.KeyGT(4)),
        };
        protected override List<Case> GanonsTowerCompassRoomTopRight => new() {
            CaseWith(x => x.Somaria.Firerod.KeyGT(3).HasAt("Ganon's Tower - Compass Room - Top Left").BigKeyGT),
            CaseWith(x => x.Somaria.Firerod.KeyGT(3).HasAt("Ganon's Tower - Compass Room - Bottom Left").BigKeyGT),
            CaseWith(x => x.Somaria.Firerod.KeyGT(3).HasAt("Ganon's Tower - Compass Room - Bottom Right").BigKeyGT),
            CaseWith(x => x.Somaria.Firerod.KeyGT(4)),
        };
        protected override List<Case> GanonsTowerCompassRoomBottomLeft => new() {
            CaseWith(x => x.Somaria.Firerod.KeyGT(3).HasAt("Ganon's Tower - Compass Room - Top Left").BigKeyGT),
            CaseWith(x => x.Somaria.Firerod.KeyGT(3).HasAt("Ganon's Tower - Compass Room - Top Right").BigKeyGT),
            CaseWith(x => x.Somaria.Firerod.KeyGT(3).HasAt("Ganon's Tower - Compass Room - Bottom Right").BigKeyGT),
            CaseWith(x => x.Somaria.Firerod.KeyGT(4)),
        };
        protected override List<Case> GanonsTowerCompassRoomBottomRight => new() {
            CaseWith(x => x.Somaria.Firerod.KeyGT(3).HasAt("Ganon's Tower - Compass Room - Top Left").BigKeyGT),
            CaseWith(x => x.Somaria.Firerod.KeyGT(3).HasAt("Ganon's Tower - Compass Room - Top Right").BigKeyGT),
            CaseWith(x => x.Somaria.Firerod.KeyGT(3).HasAt("Ganon's Tower - Compass Room - Bottom Left").BigKeyGT),
            CaseWith(x => x.Somaria.Firerod.KeyGT(4)),
        };

        protected override List<Case> GanonsTowerBobsChest => new() {
            CaseWith(x => x.KeyGT(3).Hammer.Hookshot),
            CaseWith(x => x.KeyGT(3).Somaria.Firerod),
        };

        protected override List<Case> GanonsTowerBigChest => new() {
            CaseWith(x => x.BigKeyGT.KeyGT(3).Hammer.Hookshot),
            CaseWith(x => x.BigKeyGT.KeyGT(3).Somaria.Firerod),
        };

        protected override List<Case> GanonsTowerBigKeyRoom => new() {
            CaseWith(x => x.KeyGT(3).Hammer.Hookshot),
            CaseWith(x => x.KeyGT(3).Firerod.Somaria.Sword),
            CaseWith(x => x.KeyGT(3).Firerod.Somaria.Hammer),
            CaseWith(x => x.KeyGT(3).Firerod.Somaria.Bow),
            CaseWith(x => x.KeyGT(3).Firerod.Somaria.HalfMagic),
            CaseWith(x => x.KeyGT(3).Firerod.Somaria.Bottle),
        };

        protected override List<Case> GanonsTowerAscent => new() {
            CaseWith(x => x.BigKeyGT.KeyGT(3).Bow.Firerod),
            CaseWith(x => x.BigKeyGT.KeyGT(3).Bow.Lamp),
        };

        protected override List<Case> GanonsTowerMoldormChest => new() {
            CaseWith(x => x.BigKeyGT.KeyGT(4).Bow.Firerod.Sword.Hookshot),
            CaseWith(x => x.BigKeyGT.KeyGT(4).Bow.Firerod.Hammer.Hookshot),
            CaseWith(x => x.BigKeyGT.KeyGT(4).Bow.Lamp.Sword.Hookshot),
            CaseWith(x => x.BigKeyGT.KeyGT(4).Bow.Lamp.Hammer.Hookshot),
        };

        #endregion

    }

}
