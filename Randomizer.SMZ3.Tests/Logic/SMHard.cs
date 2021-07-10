using System.Collections.Generic;

namespace Randomizer.SMZ3.Tests.Logic {

    public class SMHard : SMNormal {

        #region Crateria West

        protected override List<Case> EnergyTankGauntlet => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.TwoPowerBombs),
            CaseWith(x => x.ScrewAttack),
            CaseWith(x => x.SpeedBooster.Morph.PowerBomb.ETank(2)),
        };

        protected override List<Case> MissileCrateriaGauntlet => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.TwoPowerBombs),
            CaseWith(x => x.ScrewAttack.Morph.PowerBomb),
            CaseWith(x => x.SpeedBooster.Morph.PowerBomb.ETank(2)),
        };

        #endregion

        #region Crateria Central

        protected override List<Case> Bombs => new() {
            CaseWith(x => x.Missile.Morph),
            CaseWith(x => x.Super.Morph),
        };

        #endregion

        #region Crateria East

        protected override List<Case> CrateriaEast => new() {
            CaseWith(x => x.Morph.PowerBomb.Super),
            CaseWith(x => x.Morph.PowerBomb.Flute.Ice),
            CaseWith(x => x.Morph.PowerBomb.Flute.HiJump),
            CaseWith(x => x.Morph.PowerBomb.Flute.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Flute.Bombs),
            CaseWith(x => x.Morph.PowerBomb.Flute.SpringBall),
            CaseWith(x => x.Morph.PowerBomb.Glove.Lamp.Ice),
            CaseWith(x => x.Morph.PowerBomb.Glove.Lamp.HiJump),
            CaseWith(x => x.Morph.PowerBomb.Glove.Lamp.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Glove.Lamp.Bombs),
            CaseWith(x => x.Morph.PowerBomb.Glove.Lamp.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt),
        };

        protected override List<Case> MissileOutsideWreckedShipBottom => new() {
            CaseWith(x => x.Morph),
        };

        protected override List<Case> MissilesOutsideWreckedShipTopHalf => new() {
            CaseWith(x => x.Super.Morph.PowerBomb),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Mitt.Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.Bombs),
        };

        #endregion

        #region Wrecked Ship

        protected override List<Case> WreckedShip => new() {
            CaseWith(x => x.Super.Morph.PowerBomb),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Mitt.Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt),
        };

        protected override List<Case> ReserveTankWreckedShip => new() {
            CaseWith(x => x.Morph.PowerBomb.SpeedBooster.Varia),
            CaseWith(x => x.Morph.PowerBomb.SpeedBooster.ETank(2)),
        };

        protected override List<Case> MissileGravitySuit => new() {
            CaseWith(x => x.Morph.Bombs.Varia),
            CaseWith(x => x.Morph.Bombs.ETank(1)),
            CaseWith(x => x.Morph.PowerBomb.Varia),
            CaseWith(x => x.Morph.PowerBomb.ETank(1)),
        };

        protected override List<Case> EnergyTankWreckedShip => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> GravitySuit => new() {
            CaseWith(x => x.Morph.Bombs.Varia),
            CaseWith(x => x.Morph.Bombs.ETank(1)),
            CaseWith(x => x.Morph.PowerBomb.Varia),
            CaseWith(x => x.Morph.PowerBomb.ETank(1)),
        };

        #endregion

        #region Brinstar Blue

        protected override List<Case> EnergyTankBrinstarCeiling => CaseWithNothing;

        #endregion

        #region Brinstar Green

        protected override List<Case> SuperMissileGreenBrinstarTop => new() {
            CaseWith(x => x.Missile.Morph),
            CaseWith(x => x.Missile.SpeedBooster),
            CaseWith(x => x.Super.Morph),
            CaseWith(x => x.Super.SpeedBooster),
        };
        protected override List<Case> ReserveTankBrinstar => new() {
            CaseWith(x => x.Missile.Morph),
            CaseWith(x => x.Missile.SpeedBooster),
            CaseWith(x => x.Super.Morph),
            CaseWith(x => x.Super.SpeedBooster),
        };

        protected override List<Case> MissileGreenBrinstarBehindMissile => new() {
            CaseWith(x => x.Morph.Bombs.Missile),
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.Morph.PowerBomb.Missile),
            CaseWith(x => x.Morph.PowerBomb.Super),
            CaseWith(x => x.Morph.ScrewAttack.Missile),
            CaseWith(x => x.Morph.ScrewAttack.Super),
        };

        protected override List<Case> MissileGreenBrinstarBehindReserveTank => new() {
            CaseWith(x => x.Missile.Morph),
            CaseWith(x => x.Super.Morph),
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
            CaseWith(x => x.Flute.Morph.Missile.Ice),
            CaseWith(x => x.Flute.Morph.Missile.HiJump),
            CaseWith(x => x.Flute.Morph.Missile.SpringBall),
            CaseWith(x => x.Flute.Morph.Missile.SpaceJump),
            CaseWith(x => x.Flute.Morph.Super.Ice),
            CaseWith(x => x.Flute.Morph.Super.HiJump),
            CaseWith(x => x.Flute.Morph.Super.SpringBall),
            CaseWith(x => x.Flute.Morph.Super.SpaceJump),
            CaseWith(x => x.Flute.Morph.Wave.Ice),
            CaseWith(x => x.Flute.Morph.Wave.HiJump),
            CaseWith(x => x.Flute.Morph.Wave.SpringBall),
            CaseWith(x => x.Flute.Morph.Wave.SpaceJump),
            CaseWith(x => x.Flute.Morph.Wave.Bombs),
            CaseWith(x => x.Glove.Lamp.Morph.Missile.Ice),
            CaseWith(x => x.Glove.Lamp.Morph.Missile.HiJump),
            CaseWith(x => x.Glove.Lamp.Morph.Missile.SpringBall),
            CaseWith(x => x.Glove.Lamp.Morph.Missile.SpaceJump),
            CaseWith(x => x.Glove.Lamp.Morph.Super.Ice),
            CaseWith(x => x.Glove.Lamp.Morph.Super.HiJump),
            CaseWith(x => x.Glove.Lamp.Morph.Super.SpringBall),
            CaseWith(x => x.Glove.Lamp.Morph.Super.SpaceJump),
            CaseWith(x => x.Glove.Lamp.Morph.Wave.Ice),
            CaseWith(x => x.Glove.Lamp.Morph.Wave.HiJump),
            CaseWith(x => x.Glove.Lamp.Morph.Wave.SpringBall),
            CaseWith(x => x.Glove.Lamp.Morph.Wave.SpaceJump),
            CaseWith(x => x.Glove.Lamp.Morph.Wave.Bombs),
        };

        protected override List<Case> PowerBombPinkBrinstar => new() {
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        protected override List<Case> EnergyTankBrinstarGate => new() {
            CaseWith(x => x.Morph.PowerBomb.Wave),
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        #endregion

        #region Brinstar Red

        protected override List<Case> BrinstarRed => new() {
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.Morph.PowerBomb.Super),
            CaseWith(x => x.ScrewAttack.Super.Morph),
            CaseWith(x => x.SpeedBooster.Super.Morph),
            CaseWith(x => x.Flute.Ice),
            CaseWith(x => x.Flute.Morph.SpringBall),
            CaseWith(x => x.Flute.HiJump),
            CaseWith(x => x.Flute.SpaceJump),
            CaseWith(x => x.Flute.Morph.Bombs),
            CaseWith(x => x.Glove.Lamp.Ice),
            CaseWith(x => x.Glove.Lamp.Morph.SpringBall),
            CaseWith(x => x.Glove.Lamp.HiJump),
            CaseWith(x => x.Glove.Lamp.SpaceJump),
            CaseWith(x => x.Glove.Lamp.Morph.Bombs),
        };

        protected override List<Case> XRayScope => new() {
            CaseWith(x => x.Morph.PowerBomb.Missile.Grapple),
            CaseWith(x => x.Morph.PowerBomb.Missile.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Missile.Bombs.Varia.ETank(3)),
            CaseWith(x => x.Morph.PowerBomb.Missile.Bombs.ETank(5)),
            CaseWith(x => x.Morph.PowerBomb.Missile.HiJump.SpeedBooster.Varia.ETank(3)),
            CaseWith(x => x.Morph.PowerBomb.Missile.HiJump.SpeedBooster.ETank(5)),
            CaseWith(x => x.Morph.PowerBomb.Missile.SpringBall.Varia.ETank(3)),
            CaseWith(x => x.Morph.PowerBomb.Missile.SpringBall.ETank(5)),
            CaseWith(x => x.Morph.PowerBomb.Super.Grapple),
            CaseWith(x => x.Morph.PowerBomb.Super.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Bombs.Varia.ETank(3)),
            CaseWith(x => x.Morph.PowerBomb.Super.Bombs.ETank(5)),
            CaseWith(x => x.Morph.PowerBomb.Super.HiJump.SpeedBooster.Varia.ETank(3)),
            CaseWith(x => x.Morph.PowerBomb.Super.HiJump.SpeedBooster.ETank(5)),
            CaseWith(x => x.Morph.PowerBomb.Super.SpringBall.Varia.ETank(3)),
            CaseWith(x => x.Morph.PowerBomb.Super.SpringBall.ETank(5)),
        };

        protected override List<Case> PowerBombRedBrinstarSpikeRoom => new() {
            CaseWith(x => x.Super),
        };

        #endregion

        #region Maridia Outer

        protected override List<Case> MaridiaOuter => new() {
            CaseWith(x => x.Morph.PowerBomb.Super.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.HiJump.SpringBall),
            CaseWith(x => x.Morph.PowerBomb.Super.HiJump.Ice),
            CaseWith(x => x.Flute.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Flute.Morph.PowerBomb.HiJump.SpringBall),
            CaseWith(x => x.Flute.Morph.PowerBomb.HiJump.Ice),
            CaseWith(x => x.Glove.Lamp.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Glove.Lamp.Morph.PowerBomb.HiJump.SpringBall),
            CaseWith(x => x.Glove.Lamp.Morph.PowerBomb.HiJump.Ice),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Sword.Cape.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Sword.Cape.Lamp.KeyCT(2).PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.MasterSword.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.MasterSword.Lamp.KeyCT(2).PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Hammer.Glove.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Hammer.Glove.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Mitt.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Mitt.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).Super.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).Super.Ice),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).Super.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).Super.Ice),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.Super.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.Super.Ice),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.Super.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.Super.Ice),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Super),
        };

        protected override List<Case> MissileGreenMaridiaShinespark => new() {
            CaseWith(x => x.Gravity.SpeedBooster),
        };

        protected override List<Case> EnergyTankMamaTurtle => new() {
            CaseWith(x => x.Missile.SpaceJump),
            CaseWith(x => x.Missile.Morph.Bombs),
            CaseWith(x => x.Missile.SpeedBooster),
            CaseWith(x => x.Missile.Grapple),
            CaseWith(x => x.Missile.Morph.SpringBall.Gravity),
            CaseWith(x => x.Missile.Morph.SpringBall.HiJump),
            CaseWith(x => x.Super.SpaceJump),
            CaseWith(x => x.Super.Morph.Bombs),
            CaseWith(x => x.Super.SpeedBooster),
            CaseWith(x => x.Super.Grapple),
            CaseWith(x => x.Super.Morph.SpringBall.Gravity),
            CaseWith(x => x.Super.Morph.SpringBall.HiJump),
        };

        #endregion

        #region Maridia Inner

        protected override List<Case> MaridiaInner => new() {
            CaseWith(x => x.Super.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Super.Morph.PowerBomb.HiJump.Ice.Grapple),
            CaseWith(x => x.Super.Morph.PowerBomb.HiJump.SpringBall.Grapple),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Mitt),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt),
        };

        protected override List<Case> YellowMaridiaWateringHole => new() {
            CaseWith(x => x.Morph.Bombs.Gravity),
            CaseWith(x => x.Morph.Bombs.Ice),
            CaseWith(x => x.Morph.Bombs.HiJump.SpringBall),
            CaseWith(x => x.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Ice),
            CaseWith(x => x.Morph.PowerBomb.HiJump.SpringBall),
        };
        protected override List<Case> MissileYellowMaridiaFalseWall => new() {
            CaseWith(x => x.Morph.Bombs.Gravity),
            CaseWith(x => x.Morph.Bombs.Ice),
            CaseWith(x => x.Morph.Bombs.HiJump.SpringBall),
            CaseWith(x => x.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Ice),
            CaseWith(x => x.Morph.PowerBomb.HiJump.SpringBall),
        };

        protected override List<Case> PlasmaBeam => new() {
            CaseWith(x => x.Ice.Gravity.Charge.ETank(3).HiJump),
            CaseWith(x => x.Ice.Gravity.Charge.ETank(3).Morph.SpringBall),
            CaseWith(x => x.Ice.Gravity.Charge.ETank(3).SpaceJump),
            CaseWith(x => x.Ice.Gravity.Charge.ETank(3).Morph.Bombs),
            CaseWith(x => x.Ice.Gravity.ScrewAttack.HiJump),
            CaseWith(x => x.Ice.Gravity.ScrewAttack.Morph.SpringBall),
            CaseWith(x => x.Ice.Gravity.ScrewAttack.SpaceJump),
            CaseWith(x => x.Ice.Gravity.ScrewAttack.Morph.Bombs),
            CaseWith(x => x.Ice.Gravity.Plasma.HiJump),
            CaseWith(x => x.Ice.Gravity.Plasma.Morph.SpringBall),
            CaseWith(x => x.Ice.Gravity.Plasma.SpaceJump),
            CaseWith(x => x.Ice.Gravity.Plasma.Morph.Bombs),
            CaseWith(x => x.SpeedBooster.Gravity),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Charge.ETank(3).HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Charge.ETank(3).SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Charge.ETank(3).SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Charge.ETank(3).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).ScrewAttack.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).ScrewAttack.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).ScrewAttack.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).ScrewAttack.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Plasma.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Plasma.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Plasma.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Plasma.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Charge.ETank(3).HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Charge.ETank(3).SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Charge.ETank(3).SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Charge.ETank(3).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).ScrewAttack.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).ScrewAttack.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).ScrewAttack.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).ScrewAttack.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Plasma.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Plasma.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Plasma.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Plasma.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Charge.ETank(3).HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Charge.ETank(3).SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Charge.ETank(3).SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Charge.ETank(3).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.ScrewAttack.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.ScrewAttack.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.ScrewAttack.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.ScrewAttack.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Plasma.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Plasma.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Plasma.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Plasma.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Charge.ETank(3).HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Charge.ETank(3).SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Charge.ETank(3).SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Charge.ETank(3).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.ScrewAttack.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.ScrewAttack.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.ScrewAttack.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.ScrewAttack.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Plasma.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Plasma.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Plasma.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Plasma.Bombs),
        };

        protected override List<Case> LeftMaridiaSandPitRoom => new() {
            CaseWith(x => x.Gravity.Super),
            CaseWith(x => x.HiJump.Ice.Grapple.Super.SpaceJump),
            CaseWith(x => x.HiJump.Morph.SpringBall.Grapple.Super),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Sword.Cape.Lamp.KeyCT(2).Super.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.MasterSword.Lamp.KeyCT(2).Super.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Hammer.Glove.Super.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Mitt.Super.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).Super.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).Super.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.Super.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.Super.SpaceJump),
        };

        protected override List<Case> MissileRightMaridiaSandPitRoom => new() {
            CaseWith(x => x.Gravity.Super),
            CaseWith(x => x.HiJump.Ice.Grapple.Super),
            CaseWith(x => x.HiJump.Morph.SpringBall.Grapple.Super),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).Super),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).Super),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.Super),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.Super),
        };

        protected override List<Case> PowerBombRightMaridiaSandPitRoom => new() {
            CaseWith(x => x.Gravity.Super),
            CaseWith(x => x.HiJump.Morph.SpringBall.Grapple.Super),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Sword.Cape.Lamp.KeyCT(2).Super.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.MasterSword.Lamp.KeyCT(2).Super.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Hammer.Glove.Super.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Mitt.Super.HiJump),
        };

        protected override List<Case> PinkMaridia => new() {
            CaseWith(x => x.Gravity),
        };

        protected override List<Case> SpringBall => new() {
            CaseWith(x => x.Super.Grapple.Morph.PowerBomb.Gravity.SpaceJump),
            CaseWith(x => x.Super.Grapple.Morph.PowerBomb.Gravity.Bombs),
            CaseWith(x => x.Super.Grapple.Morph.PowerBomb.Gravity.HiJump),
            CaseWith(x => x.Super.Grapple.Morph.PowerBomb.Ice.HiJump.SpringBall.SpaceJump),
        };

        protected override List<Case> MissileDraygon => new() {
            CaseWith(x => x.Ice.Gravity),
            CaseWith(x => x.SpeedBooster.Gravity),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt),
        };

        protected override List<Case> EnergyTankBotwoon => new() {
            CaseWith(x => x.Ice),
            CaseWith(x => x.SpeedBooster.Gravity),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Mitt),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt),
        };

        protected override List<Case> SpaceJump => new() {
            CaseWith(x => x.Ice.Gravity),
            CaseWith(x => x.SpeedBooster.Gravity),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt),
        };

        #endregion

        #region Norfair Upper West

        protected override List<Case> MissileLavaRoom => new() {
            CaseWith(x => x.Varia.Missile.SpaceJump.Morph),
            CaseWith(x => x.Varia.Missile.Morph.Bombs),
            CaseWith(x => x.Varia.Missile.HiJump.Morph),
            CaseWith(x => x.Varia.Missile.SpeedBooster.Morph),
            CaseWith(x => x.Varia.Missile.Morph.SpringBall),
            CaseWith(x => x.Varia.Missile.Ice.Morph),
            CaseWith(x => x.Varia.Super.SpaceJump.Morph),
            CaseWith(x => x.Varia.Super.Morph.Bombs),
            CaseWith(x => x.Varia.Super.HiJump.Morph),
            CaseWith(x => x.Varia.Super.SpeedBooster.Morph),
            CaseWith(x => x.Varia.Super.Morph.SpringBall),
            CaseWith(x => x.Varia.Super.Ice.Morph),
            CaseWith(x => x.Varia.Flute.SpeedBooster.Morph.PowerBomb),
            CaseWith(x => x.Varia.Glove.Lamp.SpeedBooster.Morph.PowerBomb),
            CaseWith(x => x.ETank(5).Missile.SpaceJump.Morph),
            CaseWith(x => x.ETank(5).Missile.Morph.Bombs),
            CaseWith(x => x.ETank(5).Missile.HiJump.Morph),
            CaseWith(x => x.ETank(5).Missile.SpeedBooster.Morph),
            CaseWith(x => x.ETank(5).Missile.Morph.SpringBall),
            CaseWith(x => x.ETank(5).Super.SpaceJump.Morph),
            CaseWith(x => x.ETank(5).Super.Morph.Bombs),
            CaseWith(x => x.ETank(5).Super.HiJump.Morph),
            CaseWith(x => x.ETank(5).Super.SpeedBooster.Morph),
            CaseWith(x => x.ETank(5).Super.Morph.SpringBall),
            CaseWith(x => x.ETank(5).Flute.SpeedBooster.Morph.PowerBomb),
            CaseWith(x => x.ETank(5).Glove.Lamp.SpeedBooster.Morph.PowerBomb),
        };

        protected override List<Case> IceBeam => new() {
            CaseWith(x => x.Super.Morph.Varia),
            CaseWith(x => x.Super.Morph.ETank(3)),
        };

        protected override List<Case> MissileBelowIceBeam => new() {
            CaseWith(x => x.Super.Morph.PowerBomb.Varia),
            CaseWith(x => x.Super.Morph.PowerBomb.ETank(3)),
            CaseWith(x => x.Super.Varia.SpeedBooster),
        };

        #endregion

        #region Norfair Upper East

        protected override List<Case> NorfairUpperEast => new() {
            CaseWith(x => x.Morph.Bombs.Super.Varia),
            CaseWith(x => x.Morph.Bombs.Super.ETank(5)),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.HiJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.SpringBall),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.Ice),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5).SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5).HiJump),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5).SpringBall),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.SpaceJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.HiJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.SpringBall),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.Ice),
            CaseWith(x => x.ScrewAttack.Super.Morph.ETank(5).SpaceJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.ETank(5).HiJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.ETank(5).SpringBall),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia),
            CaseWith(x => x.SpeedBooster.Super.Morph.ETank(5)),
            CaseWith(x => x.Flute.Varia.Super.SpaceJump),
            CaseWith(x => x.Flute.Varia.Super.HiJump),
            CaseWith(x => x.Flute.Varia.Super.SpeedBooster),
            CaseWith(x => x.Flute.Varia.Super.Morph.SpringBall),
            CaseWith(x => x.Flute.Varia.Super.Ice),
            CaseWith(x => x.Flute.Varia.SpeedBooster.Morph.PowerBomb),
            CaseWith(x => x.Flute.ETank(5).Super.SpaceJump),
            CaseWith(x => x.Flute.ETank(5).Super.HiJump),
            CaseWith(x => x.Flute.ETank(5).Super.SpeedBooster),
            CaseWith(x => x.Flute.ETank(5).Super.Morph.SpringBall),
            CaseWith(x => x.Flute.ETank(5).SpeedBooster.Morph.PowerBomb),
            CaseWith(x => x.Glove.Lamp.Varia.Super.SpaceJump),
            CaseWith(x => x.Glove.Lamp.Varia.Super.HiJump),
            CaseWith(x => x.Glove.Lamp.Varia.Super.SpeedBooster),
            CaseWith(x => x.Glove.Lamp.Varia.Super.Morph.SpringBall),
            CaseWith(x => x.Glove.Lamp.Varia.Super.Ice),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.Morph.PowerBomb),
            CaseWith(x => x.Glove.Lamp.ETank(5).Super.SpaceJump),
            CaseWith(x => x.Glove.Lamp.ETank(5).Super.HiJump),
            CaseWith(x => x.Glove.Lamp.ETank(5).Super.SpeedBooster),
            CaseWith(x => x.Glove.Lamp.ETank(5).Super.Morph.SpringBall),
            CaseWith(x => x.Glove.Lamp.ETank(5).SpeedBooster.Morph.PowerBomb),
        };

        protected override List<Case> ReserveTankNorfair => new() {
            CaseWith(x => x.Morph.Super),
        };
        protected override List<Case> MissileNorfairReserveTank => new() {
            CaseWith(x => x.Morph.Super),
        };

        protected override List<Case> MissileBubbleNorfairGreenDoor => new() {
            CaseWith(x => x.Super),
        };
        protected override List<Case> MissileSpeedBooster => new() {
            CaseWith(x => x.Super),
        };
        protected override List<Case> SpeedBooster => new() {
            CaseWith(x => x.Super),
        };

        protected override List<Case> MissileWaveBeam => CaseWithNothing;

        protected override List<Case> WaveBeam => new() {
            CaseWith(x => x.Missile.Morph),
            CaseWith(x => x.Missile.Grapple),
            CaseWith(x => x.Missile.Varia.HiJump),
            CaseWith(x => x.Missile.SpaceJump),
            CaseWith(x => x.Super.Morph),
            CaseWith(x => x.Super.Grapple),
            CaseWith(x => x.Super.Varia.HiJump),
            CaseWith(x => x.Super.SpaceJump),
        };

        #endregion

        #region Norfair Upper Crocomire

        protected override List<Case> NorfairUpperCrocomire => new() {
            CaseWith(x => x.Morph.Bombs.Super.Varia),
            CaseWith(x => x.Morph.Bombs.Super.ETank(5)),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.HiJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.SpringBall),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.Ice),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5).SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5).HiJump),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5).SpringBall),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.SpaceJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.HiJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.SpringBall),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.Ice),
            CaseWith(x => x.SpeedBooster.Super.Morph.ETank(2)),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia),
            CaseWith(x => x.Flute.SpeedBooster.ETank(2).Missile),
            CaseWith(x => x.Flute.SpeedBooster.ETank(2).Super),
            CaseWith(x => x.Flute.SpeedBooster.ETank(2).Wave),
            CaseWith(x => x.Flute.SpeedBooster.Varia.Missile),
            CaseWith(x => x.Flute.SpeedBooster.Varia.Super),
            CaseWith(x => x.Flute.SpeedBooster.Varia.Wave),
            CaseWith(x => x.Flute.Varia.Super.SpaceJump.Morph),
            CaseWith(x => x.Flute.Varia.Super.HiJump.Morph),
            CaseWith(x => x.Flute.Varia.Super.Morph.SpringBall),
            CaseWith(x => x.Flute.Varia.Super.Ice.Morph),
            CaseWith(x => x.Glove.Lamp.SpeedBooster.ETank(2).Missile),
            CaseWith(x => x.Glove.Lamp.SpeedBooster.ETank(2).Super),
            CaseWith(x => x.Glove.Lamp.SpeedBooster.ETank(2).Wave),
            CaseWith(x => x.Glove.Lamp.SpeedBooster.Varia.Missile),
            CaseWith(x => x.Glove.Lamp.SpeedBooster.Varia.Super),
            CaseWith(x => x.Glove.Lamp.SpeedBooster.Varia.Wave),
            CaseWith(x => x.Glove.Lamp.Varia.Super.SpaceJump.Morph),
            CaseWith(x => x.Glove.Lamp.Varia.Super.HiJump.Morph),
            CaseWith(x => x.Glove.Lamp.Varia.Super.Morph.SpringBall),
            CaseWith(x => x.Glove.Lamp.Varia.Super.Ice.Morph),
            CaseWith(x => x.Varia.Flute.Mitt.ScrewAttack.SpaceJump.Super.ETank(2)),
        };

        protected override List<Case> EnergyTankCrocomire => new() {
            CaseWith(x => x.Super),
        };

        protected override List<Case> MissileAboveCrocomire => new() {
            CaseWith(x => x.SpaceJump.Varia),
            CaseWith(x => x.SpaceJump.ETank(5)),
            CaseWith(x => x.Morph.Bombs.Varia),
            CaseWith(x => x.Morph.Bombs.ETank(5)),
            CaseWith(x => x.Grapple.Varia),
            CaseWith(x => x.Grapple.ETank(5)),
            CaseWith(x => x.HiJump.SpeedBooster.Varia),
            CaseWith(x => x.HiJump.SpeedBooster.ETank(5)),
            CaseWith(x => x.HiJump.Morph.SpringBall.Varia),
            CaseWith(x => x.HiJump.Morph.SpringBall.ETank(5)),
            CaseWith(x => x.HiJump.Varia.Ice),
        };

        protected override List<Case> PowerBombCrocomire => new() {
            CaseWith(x => x.Super),
        };

        protected override List<Case> MissileGrapplingBeam => new() {
            CaseWith(x => x.Super.SpeedBooster),
            CaseWith(x => x.Super.Morph.SpaceJump),
            CaseWith(x => x.Super.Morph.Bombs),
            CaseWith(x => x.Super.Morph.Grapple),
        };

        protected override List<Case> GrapplingBeam => new() {
            CaseWith(x => x.Super.SpaceJump),
            CaseWith(x => x.Super.Morph),
            CaseWith(x => x.Super.Grapple),
            CaseWith(x => x.Super.HiJump.SpeedBooster),
        };

        #endregion

        #region Norfair Lower West

        protected override List<Case> NorfairLowerWest => new() {
            CaseWith(x => x.Morph.Bombs.Super.Varia.PowerBomb.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.SpaceJump.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.HiJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.SpringBall.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.Ice.Gravity),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia.PowerBomb.Gravity),
            CaseWith(x => x.Flute.Varia.SpeedBooster.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Flute.Varia.SpeedBooster.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Flute.Mitt.Morph.Bombs),
            CaseWith(x => x.Flute.Mitt.Morph.PowerBomb),
            CaseWith(x => x.Flute.Mitt.ScrewAttack),
        };

        protected override List<Case> MissileGoldTorizo => new() {
            CaseWith(x => x.Morph.PowerBomb.SpaceJump.Varia.HiJump),
            CaseWith(x => x.Morph.PowerBomb.SpaceJump.Varia.Gravity),
            CaseWith(x => x.Morph.PowerBomb.SpaceJump.Varia.Flute.Mitt.Super),
        };

        protected override List<Case> SuperMissileGoldTorizo => new() {
            CaseWith(x => x.Morph.Bombs.Varia.Super),
            CaseWith(x => x.Morph.Bombs.Varia.Charge),
            CaseWith(x => x.Morph.PowerBomb.Varia.Super),
            CaseWith(x => x.Morph.PowerBomb.Varia.Charge),
            CaseWith(x => x.ScrewAttack.Varia.Super),
            CaseWith(x => x.ScrewAttack.Varia.Charge),
        };

        protected override List<Case> ScrewAttack => new() {
            CaseWith(x => x.Morph.Bombs.Flute.Mitt),
            CaseWith(x => x.Morph.Bombs.Varia),
            CaseWith(x => x.Morph.PowerBomb.Flute.Mitt),
            CaseWith(x => x.Morph.PowerBomb.Varia),
            CaseWith(x => x.ScrewAttack.Flute.Mitt),
            CaseWith(x => x.ScrewAttack.Varia),
        };

        protected override List<Case> MissileMickeyMouseRoom => new() {
            CaseWith(x => x.Varia.Morph.Super.SpaceJump.PowerBomb),
            CaseWith(x => x.Varia.Morph.Super.SpaceJump.ScrewAttack),
            CaseWith(x => x.Varia.Morph.Super.Bombs),
            CaseWith(x => x.Varia.Morph.Super.HiJump.PowerBomb),
            CaseWith(x => x.Varia.Morph.Super.SpringBall.PowerBomb),
            CaseWith(x => x.Varia.Morph.Super.Ice.Charge.PowerBomb),
        };

        #endregion

        #region Norfair Lower East

        protected override List<Case> NorfairLowerEast => new() {
            CaseWith(x => x.Varia.Morph.Bombs.Super.PowerBomb.Gravity),
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.SpaceJump.Gravity),
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.HiJump),
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.SpringBall.Gravity),
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.Ice.Gravity.Charge),
            CaseWith(x => x.Varia.Flute.SpeedBooster.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Varia.Flute.SpeedBooster.Morph.PowerBomb.Gravity.SpaceJump),
            CaseWith(x => x.Varia.Flute.SpeedBooster.Morph.PowerBomb.Gravity.Bombs),
            CaseWith(x => x.Varia.Flute.SpeedBooster.Morph.PowerBomb.Gravity.SpringBall),
            CaseWith(x => x.Varia.Flute.SpeedBooster.Morph.PowerBomb.Gravity.Ice.Charge),
            CaseWith(x => x.Varia.Glove.Lamp.SpeedBooster.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Varia.Glove.Lamp.SpeedBooster.Morph.PowerBomb.Gravity.SpaceJump),
            CaseWith(x => x.Varia.Glove.Lamp.SpeedBooster.Morph.PowerBomb.Gravity.Bombs),
            CaseWith(x => x.Varia.Glove.Lamp.SpeedBooster.Morph.PowerBomb.Gravity.SpringBall),
            CaseWith(x => x.Varia.Glove.Lamp.SpeedBooster.Morph.PowerBomb.Gravity.Ice.Charge),
            CaseWith(x => x.Varia.Flute.Mitt.Morph.Bombs.Super),
            CaseWith(x => x.Varia.Flute.Mitt.Morph.PowerBomb.Super.SpaceJump),
            CaseWith(x => x.Varia.Flute.Mitt.Morph.PowerBomb.Super.SpringBall),
            CaseWith(x => x.Varia.Flute.Mitt.Morph.PowerBomb.Super.SpeedBooster.Ice.Charge),
            CaseWith(x => x.Varia.Flute.Mitt.ScrewAttack.Super.SpaceJump),
        };

        protected override List<Case> MissileLowerNorfairAboveFireFleaRoom => new() {
            CaseWith(x => x.Morph),
            CaseWith(x => x.ETank(5)),
        };

        protected override List<Case> PowerBombLowerNorfairAboveFireFleaRoom => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> MissileLowerNorfairNearWaveBeam => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
            CaseWith(x => x.Morph.ScrewAttack),
        };

        protected override List<Case> EnergyTankFirefleas => new() {
            CaseWith(x => x.Morph),
            CaseWith(x => x.ETank(5)),
        };

        #endregion

        #region Light World North West

        protected override List<Case> GraveyardLedge => new() {
            CaseWith(x => x.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
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
            CaseWith(x => x.Boots.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Boots.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Boots.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Boots.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Boots.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Boots.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Boots.Mirror.MoonPearl.Hammer.Glove),
        };

        #endregion

        #region Light World South

        protected override List<Case> SouthOfGrove => new() {
            CaseWith(x => x.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Mirror.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Mirror.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Hammer.Glove),
            CaseWith(x => x.Mirror.MoonPearl.Mitt),
        };

        protected override List<Case> CheckerboardCave => new() {
            CaseWith(x => x.Mirror.Flute.Mitt),
            CaseWith(x => x.Mirror.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Glove),
            CaseWith(x => x.Mirror.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Glove),
            CaseWith(x => x.Mirror.Varia.Super.HiJump.Morph.PowerBomb.Glove),
            CaseWith(x => x.Mirror.Varia.Super.SpeedBooster.Gravity.Morph.PowerBomb.Glove),
            CaseWith(x => x.Mirror.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Glove),
            CaseWith(x => x.Mirror.Varia.Super.Ice.Gravity.Morph.PowerBomb.Glove),
        };

        protected override List<Case> BombosTablet => new() {
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Hammer.Glove),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Mitt),
        };

        protected override List<Case> LakeHyliaIsland => new() {
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Hammer.Glove),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Mitt),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Morph.PowerBomb.Super.Charge.Gravity.Ice),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Morph.PowerBomb.Super.Missile.Gravity.Ice),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple),
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
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt),
        };

        #endregion

        #region Dark World North East

        protected override List<Case> DarkWorldNorthEast => new() {
            CaseWith(x => x.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers),
        };

        protected override List<Case> PyramidFairy => new() {
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers.Mirror),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove.Mirror),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers.Mirror),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove.Mirror),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hammer),
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
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt),
        };

        #endregion

        #region Dark World Mire

        protected override List<Case> DarkWorldMire => new() {
            CaseWith(x => x.Flute.Mitt),
            CaseWith(x => x.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb),
            CaseWith(x => x.Varia.Super.Morph.Bombs.Gravity.PowerBomb),
            CaseWith(x => x.Varia.Super.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Varia.Super.SpeedBooster.Gravity.Morph.PowerBomb),
            CaseWith(x => x.Varia.Super.Morph.SpringBall.Gravity.PowerBomb),
            CaseWith(x => x.Varia.Super.Ice.Gravity.Morph.PowerBomb),
        };

        #endregion

        #region Desert Palace

        protected override List<Case> DesertPalace => new() {
            CaseWith(x => x.Book),
            CaseWith(x => x.Mirror.Mitt.Flute),
            CaseWith(x => x.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror),
            CaseWith(x => x.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror),
            CaseWith(x => x.Varia.Super.HiJump.Morph.PowerBomb.Mirror),
            CaseWith(x => x.Varia.Super.SpeedBooster.Gravity.Morph.PowerBomb.Mirror),
            CaseWith(x => x.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror),
            CaseWith(x => x.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror),
        };

        protected override List<Case> DesertPalaceLanmolas => new() {
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Firerod),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Lamp.Sword),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Lamp.Hammer),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Lamp.Bow),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Lamp.Icerod),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Lamp.Byrna),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP(1).Lamp.Somaria),
            CaseWith(x => x.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Firerod),
            CaseWith(x => x.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Sword),
            CaseWith(x => x.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Hammer),
            CaseWith(x => x.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Bow),
            CaseWith(x => x.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Icerod),
            CaseWith(x => x.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Byrna),
            CaseWith(x => x.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Somaria),
            CaseWith(x => x.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Firerod),
            CaseWith(x => x.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Sword),
            CaseWith(x => x.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Hammer),
            CaseWith(x => x.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Bow),
            CaseWith(x => x.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Icerod),
            CaseWith(x => x.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Byrna),
            CaseWith(x => x.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Somaria),
            CaseWith(x => x.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Firerod),
            CaseWith(x => x.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Sword),
            CaseWith(x => x.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Hammer),
            CaseWith(x => x.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Bow),
            CaseWith(x => x.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Icerod),
            CaseWith(x => x.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Byrna),
            CaseWith(x => x.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Somaria),
            CaseWith(x => x.Varia.Super.SpeedBooster.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Firerod),
            CaseWith(x => x.Varia.Super.SpeedBooster.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Sword),
            CaseWith(x => x.Varia.Super.SpeedBooster.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Hammer),
            CaseWith(x => x.Varia.Super.SpeedBooster.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Bow),
            CaseWith(x => x.Varia.Super.SpeedBooster.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Icerod),
            CaseWith(x => x.Varia.Super.SpeedBooster.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Byrna),
            CaseWith(x => x.Varia.Super.SpeedBooster.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Somaria),
            CaseWith(x => x.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Firerod),
            CaseWith(x => x.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Sword),
            CaseWith(x => x.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Hammer),
            CaseWith(x => x.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Bow),
            CaseWith(x => x.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Icerod),
            CaseWith(x => x.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Byrna),
            CaseWith(x => x.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Somaria),
            CaseWith(x => x.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Firerod),
            CaseWith(x => x.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Sword),
            CaseWith(x => x.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Hammer),
            CaseWith(x => x.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Bow),
            CaseWith(x => x.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Icerod),
            CaseWith(x => x.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Byrna),
            CaseWith(x => x.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP(1).Lamp.Somaria),
        };

        #endregion

        #region Palace of Darkness

        protected override List<Case> PalaceOfDarkness => new() {
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers),
        };

        #endregion

        #region Swamp Palace

        protected override List<Case> SwampPalace => new() {
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Sword.Cape.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Sword.Cape.Lamp.KeyCT(2).Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.MasterSword.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.MasterSword.Lamp.KeyCT(2).Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Charge.Gravity.Ice.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Charge.Gravity.Ice.Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Missile.Gravity.Ice.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Missile.Gravity.Ice.Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Mitt),
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
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt),
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
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt),
        };

        #endregion

        #region Misery Mire

        protected override List<Case> MiseryMire => new() {
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.Flute.Mitt),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.Varia.Super.Morph.Bombs.Gravity.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.Varia.Super.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.Varia.Super.SpeedBooster.Gravity.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.Varia.Super.Morph.SpringBall.Gravity.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.Varia.Super.Ice.Gravity.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.Flute.Mitt),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.Varia.Super.Morph.Bombs.Gravity.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.Varia.Super.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.Varia.Super.SpeedBooster.Gravity.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.Varia.Super.Morph.SpringBall.Gravity.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.Varia.Super.Ice.Gravity.Morph.PowerBomb),
        };

        #endregion

    }

}
