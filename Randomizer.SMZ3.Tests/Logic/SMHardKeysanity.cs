using System.Collections.Generic;

namespace Randomizer.SMZ3.Tests.Logic {

    public class SMHardKeysanity : SMHard {

        #region Crateria West

        protected override List<Case> EnergyTankGauntlet => new() {
            CaseWith(x => x.CardCrateriaL1.Morph.Bombs),
            CaseWith(x => x.CardCrateriaL1.Morph.TwoPowerBombs),
            CaseWith(x => x.CardCrateriaL1.ScrewAttack),
            CaseWith(x => x.CardCrateriaL1.SpeedBooster.Morph.PowerBomb.ETank(2)),
        };

        protected override List<Case> MissileCrateriaGauntlet => new() {
            CaseWith(x => x.CardCrateriaL1.Morph.Bombs),
            CaseWith(x => x.CardCrateriaL1.Morph.TwoPowerBombs),
            CaseWith(x => x.CardCrateriaL1.ScrewAttack.Morph.PowerBomb),
            CaseWith(x => x.CardCrateriaL1.SpeedBooster.Morph.PowerBomb.ETank(2)),
        };

        #endregion

        #region Crateria Central

        protected override List<Case> PowerBombCrateriaSurface => new() {
            CaseWith(x => x.CardCrateriaL1.SpeedBooster),
            CaseWith(x => x.CardCrateriaL1.SpaceJump),
            CaseWith(x => x.CardCrateriaL1.Morph.Bombs),
        };

        protected override List<Case> Bombs => new() {
            CaseWith(x => x.CardCrateriaBoss.Morph),
        };

        #endregion

        #region Crateria East

        protected override List<Case> CrateriaEast => new() {
            CaseWith(x => x.CardCrateriaL2.Super),
            CaseWith(x => x.CardCrateriaL2.Flute.Ice),
            CaseWith(x => x.CardCrateriaL2.Flute.HiJump),
            CaseWith(x => x.CardCrateriaL2.Flute.SpaceJump),
            CaseWith(x => x.CardCrateriaL2.Flute.Morph.Bombs),
            CaseWith(x => x.CardCrateriaL2.Flute.Morph.SpringBall),
            CaseWith(x => x.CardCrateriaL2.Glove.Lamp.Ice),
            CaseWith(x => x.CardCrateriaL2.Glove.Lamp.HiJump),
            CaseWith(x => x.CardCrateriaL2.Glove.Lamp.SpaceJump),
            CaseWith(x => x.CardCrateriaL2.Glove.Lamp.Morph.Bombs),
            CaseWith(x => x.CardCrateriaL2.Glove.Lamp.Morph.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL2.Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL2.Super.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL2.Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL2.Super.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.CardMaridiaL2.Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.CardMaridiaL2.Super.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.CardMaridiaL2.Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.CardMaridiaL2.Super.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL2.Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL2.Super.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL2.Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL2.Super.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaL2.Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaL2.Super.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaL2.Super.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaL2.Super.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss),
            CaseWith(x => x.Morph.PowerBomb.Super.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.HiJump.Ice.Grapple.CardMaridiaL1),
            CaseWith(x => x.Morph.PowerBomb.Super.HiJump.SpringBall.Grapple.CardMaridiaL1),
        };

        protected override List<Case> MissilesOutsideWreckedShipTopHalf => new() {
            CaseWith(x => x.Super.CardCrateriaL2.CardWreckedShipBoss.Morph.Bombs),
            CaseWith(x => x.Super.CardCrateriaL2.CardWreckedShipBoss.Morph.PowerBomb),
            CaseWith(x => x.Super.Morph.PowerBomb.Gravity.CardWreckedShipBoss),
            CaseWith(x => x.Super.Morph.PowerBomb.HiJump.Ice.Grapple.CardMaridiaL1.CardWreckedShipBoss),
            CaseWith(x => x.Super.Morph.PowerBomb.HiJump.Morph.SpringBall.Grapple.CardMaridiaL1.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).PowerBomb.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).Bombs.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).PowerBomb.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.Bombs.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.PowerBomb.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Mitt.Bombs.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Mitt.PowerBomb.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.CardWreckedShipBoss.Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Bombs.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.CardWreckedShipBoss.Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Bombs.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.CardWreckedShipBoss.Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.Bombs.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.CardWreckedShipBoss.Bombs),
        };

        #endregion

        #region Wrecked Ship

        protected override List<Case> WreckedShip => new() {
            CaseWith(x => x.Super.CardCrateriaL2),
            CaseWith(x => x.Super.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Super.Morph.PowerBomb.HiJump.Ice.Grapple.CardMaridiaL1),
            CaseWith(x => x.Super.Morph.PowerBomb.HiJump.Morph.SpringBall.Grapple.CardMaridiaL1),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).PowerBomb.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).Bombs.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).PowerBomb.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.Bombs.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.PowerBomb.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Mitt.Bombs.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.HiJump.Morph.Mitt.PowerBomb.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).ScrewAttack.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Bombs.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).ScrewAttack.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Bombs.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.ScrewAttack.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.Bombs.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.ScrewAttack.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss),
        };

        protected override List<Case> ReserveTankWreckedShip => new() {
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.SpeedBooster.Varia),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.SpeedBooster.ETank(2)),
        };

        protected override List<Case> MissileGravitySuit => new() {
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.CardWreckedShipL1.Varia),
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.CardWreckedShipL1.ETank(1)),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.Varia),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.ETank(1)),
        };

        protected override List<Case> MissileWreckedShipTop => new() {
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb),
        };

        protected override List<Case> EnergyTankWreckedShip => new() {
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb),
        };

        protected override List<Case> SuperMissileWreckedShipLeft => new() {
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb),
        };

        protected override List<Case> SuperMissileWreckedShipRight => new() {
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb),
        };

        protected override List<Case> GravitySuit => new() {
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.CardWreckedShipL1.Varia),
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.CardWreckedShipL1.ETank(1)),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.Varia),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.ETank(1)),
        };

        #endregion

        #region Brinstar Blue

        protected override List<Case> MissileBlueBrinstarMiddle => new() {
            CaseWith(x => x.CardBrinstarL1.Morph),
        };
        
        protected override List<Case> EnergyTankBrinstarCeiling => new() {
            CaseWith(x => x.CardBrinstarL1),
        };

        protected override List<Case> MissileBlueBrinstarBillyMays => new() {
            CaseWith(x => x.CardBrinstarL1.Morph.PowerBomb),
        };

        #endregion

        #region Brinstar Green

        protected override List<Case> PowerBombGreenBrinstarBottom => new() {
            CaseWith(x => x.CardBrinstarL2.Morph.PowerBomb),
        };

        protected override List<Case> EnergyTankEtecoons => new() {
            CaseWith(x => x.CardBrinstarL2.Morph.PowerBomb),
        };

        protected override List<Case> SuperMissileGreenBrinstarBottom => new() {
            CaseWith(x => x.CardBrinstarL2.Morph.PowerBomb.Super),
        };

        #endregion

        #region Brinstar Pink

        protected override List<Case> SuperMissilePinkBrinstar => new() {
            CaseWith(x => x.CardBrinstarBoss.Morph.Bombs.Super),
            CaseWith(x => x.CardBrinstarBoss.Morph.PowerBomb.Super),
            CaseWith(x => x.CardBrinstarL2.Morph.Bombs.Super),
            CaseWith(x => x.CardBrinstarL2.Morph.PowerBomb.Super),
        };

        protected override List<Case> EnergyTankBrinstarGate => new() {
            CaseWith(x => x.CardBrinstarL2.Morph.PowerBomb.Wave),
            CaseWith(x => x.CardBrinstarL2.Morph.PowerBomb.Super),
        };

        #endregion

        #region Brinstar Kraid

        protected override List<Case> EnergyTankKraid => new() {
            CaseWith(x => x.CardBrinstarBoss),
        };
        protected override List<Case> VariaSuit => new() {
            CaseWith(x => x.CardBrinstarBoss),
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
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Mitt.CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Mitt.CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Super.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Super.Ice),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Super.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Super.Ice),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.Super.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.Super.Ice),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.CardMaridiaL1.CardMaridiaL2.Super.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.CardMaridiaL1.CardMaridiaL2.Super.Ice),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Super),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Super),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.Super),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaL1.CardMaridiaL2.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaL1.CardMaridiaL2.Super),
        };

        #endregion

        #region Maridia Inner

        protected override List<Case> YellowMaridiaWateringHole => new() {
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.Gravity),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.HiJump.Ice.Grapple),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.HiJump.SpringBall.Grapple),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.SpringBall.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.SpringBall.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.SpringBall.Hammer.Glove),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.SpringBall.Mitt),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.HiJump.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.HiJump.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.HiJump.Hammer.Glove),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.HiJump.Mitt),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.Gravity),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.HiJump.Ice.Grapple),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.HiJump.SpringBall.Grapple),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.SpringBall.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.SpringBall.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.SpringBall.Hammer.Glove),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.SpringBall.Mitt),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.HiJump.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.HiJump.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.HiJump.Hammer.Glove),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.HiJump.Mitt),
        };
        protected override List<Case> MissileYellowMaridiaFalseWall => new() {
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.Gravity),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.HiJump.Ice.Grapple),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.HiJump.SpringBall.Grapple),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.SpringBall.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.SpringBall.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.SpringBall.Hammer.Glove),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.SpringBall.Mitt),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.HiJump.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.HiJump.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.HiJump.Hammer.Glove),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.CardMaridiaL2.MoonPearl.Flippers.HiJump.Mitt),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.Gravity),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.HiJump.Ice.Grapple),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.HiJump.SpringBall.Grapple),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.SpringBall.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.SpringBall.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.SpringBall.Hammer.Glove),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.SpringBall.Mitt),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.HiJump.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.HiJump.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.HiJump.Hammer.Glove),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb.CardMaridiaL2.MoonPearl.Flippers.HiJump.Mitt),
        };

        protected override List<Case> PlasmaBeam => new() {
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity.Charge.ETank(3).HiJump),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity.Charge.ETank(3).Morph.SpringBall),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity.Charge.ETank(3).SpaceJump),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity.Charge.ETank(3).Morph.Bombs),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity.ScrewAttack.HiJump),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity.ScrewAttack.Morph.SpringBall),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity.ScrewAttack.SpaceJump),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity.ScrewAttack.Morph.Bombs),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity.Plasma.HiJump),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity.Plasma.Morph.SpringBall),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity.Plasma.SpaceJump),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity.Plasma.Morph.Bombs),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.Gravity.CardMaridiaBoss),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Charge.ETank(3).HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Charge.ETank(3).SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Charge.ETank(3).SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Charge.ETank(3).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.ScrewAttack.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.ScrewAttack.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.ScrewAttack.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.ScrewAttack.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Plasma.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Plasma.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Plasma.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Plasma.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.SpeedBooster),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Charge.ETank(3).HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Charge.ETank(3).SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Charge.ETank(3).SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Charge.ETank(3).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.ScrewAttack.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.ScrewAttack.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.ScrewAttack.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.ScrewAttack.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Plasma.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Plasma.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Plasma.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Plasma.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.SpeedBooster),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Charge.ETank(3).HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Charge.ETank(3).SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Charge.ETank(3).SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Charge.ETank(3).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.ScrewAttack.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.ScrewAttack.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.ScrewAttack.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.ScrewAttack.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Plasma.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Plasma.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Plasma.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Plasma.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.SpeedBooster),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Charge.ETank(3).HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Charge.ETank(3).SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Charge.ETank(3).SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Charge.ETank(3).Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.ScrewAttack.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.ScrewAttack.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.ScrewAttack.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.ScrewAttack.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Plasma.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Plasma.SpringBall),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Plasma.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Plasma.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.SpeedBooster),
        };

        protected override List<Case> LeftMaridiaSandPitRoom => new() {
            CaseWith(x => x.CardMaridiaL1.Gravity.Super),
            CaseWith(x => x.CardMaridiaL1.HiJump.Ice.Grapple.Super.SpaceJump),
            CaseWith(x => x.CardMaridiaL1.HiJump.Morph.SpringBall.Grapple.Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Morph.SpringBall.Sword.Cape.Lamp.KeyCT(2).Super.HiJump),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Morph.SpringBall.MasterSword.Lamp.KeyCT(2).Super.HiJump),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Morph.SpringBall.Hammer.Glove.Super.HiJump),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Morph.SpringBall.Mitt.Super.HiJump),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).Super.SpaceJump),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).Super.SpaceJump),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.Super.SpaceJump),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.HiJump.Morph.Mitt.Super.SpaceJump),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Mitt.Super),
        };

        protected override List<Case> MissileRightMaridiaSandPitRoom => new() {
            CaseWith(x => x.CardMaridiaL1.Gravity.Super),
            CaseWith(x => x.CardMaridiaL1.HiJump.Ice.Grapple.Super),
            CaseWith(x => x.CardMaridiaL1.HiJump.Morph.SpringBall.Grapple.Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.HiJump.Morph.Mitt.Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Mitt.Super),
        };

        protected override List<Case> PowerBombRightMaridiaSandPitRoom => new() {
            CaseWith(x => x.CardMaridiaL1.Gravity.Super),
            CaseWith(x => x.CardMaridiaL1.HiJump.Morph.SpringBall.Grapple.Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Morph.SpringBall.Sword.Cape.Lamp.KeyCT(2).Super.HiJump),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Morph.SpringBall.MasterSword.Lamp.KeyCT(2).Super.HiJump),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Morph.SpringBall.Hammer.Glove.Super.HiJump),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Morph.SpringBall.Mitt.Super.HiJump),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Mitt.Super),
        };

        protected override List<Case> PinkMaridia => new() {
            CaseWith(x => x.CardMaridiaL1.Gravity),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Gravity),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Gravity),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Gravity),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Mitt.Gravity),
        };


        protected override List<Case> MissileDraygon => new() {
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.Gravity),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.Gravity),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt),
        };

        protected override List<Case> EnergyTankBotwoon => new() {
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.Gravity),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.MasterSword.Lamp.KeyCT(2).CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Hammer.Glove.CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.Morph.SpringBall.Mitt.CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Hammer.Glove.CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.HiJump.Morph.Mitt.CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaL2),
        };

        protected override List<Case> SpaceJump => new() {
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.Ice.CardMaridiaBoss.Gravity),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.Gravity.CardMaridiaBoss),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss),
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
            CaseWith(x => x.Varia.Flute.SpeedBooster.CardNorfairL2.Morph.PowerBomb),
            CaseWith(x => x.Varia.Glove.Lamp.SpeedBooster.CardNorfairL2.Morph.PowerBomb),
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
            CaseWith(x => x.ETank(5).Flute.SpeedBooster.CardNorfairL2.Morph.PowerBomb),
            CaseWith(x => x.ETank(5).Glove.Lamp.SpeedBooster.CardNorfairL2.Morph.PowerBomb),
        };

        protected override List<Case> IceBeam => new() {
            CaseWith(x => x.CardNorfairL1.Morph.Varia),
            CaseWith(x => x.CardNorfairL1.Morph.ETank(3)),
        };
        
        protected override List<Case> MissileBelowIceBeam => new() {
            CaseWith(x => x.CardNorfairL1.Morph.PowerBomb.Varia),
            CaseWith(x => x.CardNorfairL1.Morph.PowerBomb.ETank(3)),
            CaseWith(x => x.Missile.Varia.SpeedBooster.CardNorfairBoss.CardNorfairL1),
            CaseWith(x => x.Super.Varia.SpeedBooster.CardNorfairBoss.CardNorfairL1),
            CaseWith(x => x.Wave.Varia.SpeedBooster.CardNorfairBoss.CardNorfairL1),
        };

        #endregion

        #region Norfair Upper East

        protected override List<Case> NorfairUpperEast => new() {
            CaseWith(x => x.Morph.Bombs.Super.Varia.CardNorfairL2),
            CaseWith(x => x.Morph.Bombs.Super.ETank(5).CardNorfairL2),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.HiJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.SpringBall),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.Ice),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5).CardNorfairL2.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5).CardNorfairL2.HiJump),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5).CardNorfairL2.SpringBall),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.CardNorfairL2.SpaceJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.CardNorfairL2.HiJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.CardNorfairL2.SpringBall),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.CardNorfairL2.Ice),
            CaseWith(x => x.ScrewAttack.Super.Morph.ETank(5).CardNorfairL2.SpaceJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.ETank(5).CardNorfairL2.HiJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.ETank(5).CardNorfairL2.SpringBall),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia.CardNorfairL2),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia.PowerBomb),
            CaseWith(x => x.SpeedBooster.Super.Morph.ETank(5).CardNorfairL2),
            CaseWith(x => x.SpeedBooster.Super.Morph.ETank(5).PowerBomb),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.HiJump),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.SpeedBooster),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.Morph.SpringBall),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.Ice),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.HiJump),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.SpeedBooster),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.Morph.SpringBall),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.Ice),
            CaseWith(x => x.Flute.Varia.SpeedBooster.CardNorfairL2.Morph.PowerBomb),
            CaseWith(x => x.Flute.Varia.SpeedBooster.Missile.Morph.PowerBomb),
            CaseWith(x => x.Flute.Varia.SpeedBooster.Wave.Morph.PowerBomb),
            CaseWith(x => x.Flute.ETank(5).Missile.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Flute.ETank(5).Missile.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.Flute.ETank(5).Missile.CardNorfairL2.HiJump),
            CaseWith(x => x.Flute.ETank(5).Missile.CardNorfairL2.SpeedBooster),
            CaseWith(x => x.Flute.ETank(5).Missile.CardNorfairL2.Morph.SpringBall),
            CaseWith(x => x.Flute.ETank(5).Super.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Flute.ETank(5).Super.CardNorfairL2.HiJump),
            CaseWith(x => x.Flute.ETank(5).Super.CardNorfairL2.SpeedBooster),
            CaseWith(x => x.Flute.ETank(5).Super.CardNorfairL2.Morph.SpringBall),
            CaseWith(x => x.Flute.ETank(5).SpeedBooster.CardNorfairL2.Morph.PowerBomb),
            CaseWith(x => x.Flute.ETank(5).SpeedBooster.Missile.Morph.PowerBomb),
            CaseWith(x => x.Flute.ETank(5).SpeedBooster.Wave.Morph.PowerBomb),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.HiJump),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.SpeedBooster),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.Morph.SpringBall),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.Ice),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.HiJump),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.SpeedBooster),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.Morph.SpringBall),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.Ice),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.CardNorfairL2.Morph.PowerBomb),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.Missile.Morph.PowerBomb),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.Wave.Morph.PowerBomb),
            CaseWith(x => x.Glove.Lamp.ETank(5).Missile.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Glove.Lamp.ETank(5).Missile.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.Glove.Lamp.ETank(5).Missile.CardNorfairL2.HiJump),
            CaseWith(x => x.Glove.Lamp.ETank(5).Missile.CardNorfairL2.SpeedBooster),
            CaseWith(x => x.Glove.Lamp.ETank(5).Missile.CardNorfairL2.Morph.SpringBall),
            CaseWith(x => x.Glove.Lamp.ETank(5).Super.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Glove.Lamp.ETank(5).Super.CardNorfairL2.HiJump),
            CaseWith(x => x.Glove.Lamp.ETank(5).Super.CardNorfairL2.SpeedBooster),
            CaseWith(x => x.Glove.Lamp.ETank(5).Super.CardNorfairL2.Morph.SpringBall),
            CaseWith(x => x.Glove.Lamp.ETank(5).SpeedBooster.CardNorfairL2.Morph.PowerBomb),
            CaseWith(x => x.Glove.Lamp.ETank(5).SpeedBooster.Missile.Morph.PowerBomb),
            CaseWith(x => x.Glove.Lamp.ETank(5).SpeedBooster.Wave.Morph.PowerBomb),
        };

        protected override List<Case> ReserveTankNorfair => new() {
            CaseWith(x => x.CardNorfairL2.Morph.Super),
        };
        protected override List<Case> MissileNorfairReserveTank => new() {
            CaseWith(x => x.CardNorfairL2.Morph.Super),
        };
        
        protected override List<Case> MissileBubbleNorfairGreenDoor => new() {
            CaseWith(x => x.CardNorfairL2.Super),
        };

        protected override List<Case> MissileBubbleNorfair => new() {
            CaseWith(x => x.CardNorfairL2),
        };

        protected override List<Case> MissileSpeedBooster => new() {
            CaseWith(x => x.CardNorfairL2.Super),
        };
        protected override List<Case> SpeedBooster => new() {
            CaseWith(x => x.CardNorfairL2.Super),
        };

        protected override List<Case> MissileWaveBeam => new() {
            CaseWith(x => x.CardNorfairL2),
            CaseWith(x => x.Varia),
        };

        protected override List<Case> WaveBeam => new() {
            CaseWith(x => x.Missile.CardNorfairL2.Morph),
            CaseWith(x => x.Missile.CardNorfairL2.Grapple),
            CaseWith(x => x.Missile.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Missile.Varia.Morph),
            CaseWith(x => x.Missile.Varia.Grapple),
            CaseWith(x => x.Missile.Varia.HiJump),
            CaseWith(x => x.Missile.Varia.SpaceJump),
            CaseWith(x => x.Super.CardNorfairL2.Morph),
            CaseWith(x => x.Super.CardNorfairL2.Grapple),
            CaseWith(x => x.Super.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Super.Varia.Morph),
            CaseWith(x => x.Super.Varia.Grapple),
            CaseWith(x => x.Super.Varia.HiJump),
            CaseWith(x => x.Super.Varia.SpaceJump),
        };

        #endregion

        #region Norfair Upper Crocomire

        protected override List<Case> NorfairUpperCrocomire => new() {
            CaseWith(x => x.Morph.Bombs.Super.Varia.CardNorfairL2),
            CaseWith(x => x.Morph.Bombs.Super.ETank(5).CardNorfairL2),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.HiJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.SpringBall),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.Ice),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5).CardNorfairL2.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5).CardNorfairL2.HiJump),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5).CardNorfairL2.SpringBall),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.CardNorfairL2.SpaceJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.CardNorfairL2.HiJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.CardNorfairL2.SpringBall),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.CardNorfairL2.Ice),
            CaseWith(x => x.SpeedBooster.Super.Morph.ETank(2)),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia),
            CaseWith(x => x.Flute.CardNorfairL1.Morph.PowerBomb.SpeedBooster.ETank(3)),
            CaseWith(x => x.Flute.CardNorfairL1.Morph.PowerBomb.SpeedBooster.Varia),
            CaseWith(x => x.Flute.SpeedBooster.ETank(2).Missile),
            CaseWith(x => x.Flute.SpeedBooster.ETank(2).Super),
            CaseWith(x => x.Flute.SpeedBooster.ETank(2).Wave),
            CaseWith(x => x.Flute.SpeedBooster.Varia.Missile),
            CaseWith(x => x.Flute.SpeedBooster.Varia.Super),
            CaseWith(x => x.Flute.SpeedBooster.Varia.Wave),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.SpaceJump.Morph),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.HiJump.Morph),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.Morph.SpringBall),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.Ice.Morph),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.SpaceJump.Morph),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.HiJump.Morph),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.Morph.SpringBall),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.Ice.Morph),
            CaseWith(x => x.Flute.ETank(5).Missile.CardNorfairL2.SpaceJump.Morph.PowerBomb),
            CaseWith(x => x.Flute.ETank(5).Missile.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.Flute.ETank(5).Missile.CardNorfairL2.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Flute.ETank(5).Missile.CardNorfairL2.Morph.SpringBall.PowerBomb),
            CaseWith(x => x.Glove.Lamp.CardNorfairL1.Morph.PowerBomb.SpeedBooster.ETank(3)),
            CaseWith(x => x.Glove.Lamp.CardNorfairL1.Morph.PowerBomb.SpeedBooster.Varia),
            CaseWith(x => x.Glove.Lamp.SpeedBooster.ETank(2).Missile),
            CaseWith(x => x.Glove.Lamp.SpeedBooster.ETank(2).Super),
            CaseWith(x => x.Glove.Lamp.SpeedBooster.ETank(2).Wave),
            CaseWith(x => x.Glove.Lamp.SpeedBooster.Varia.Missile),
            CaseWith(x => x.Glove.Lamp.SpeedBooster.Varia.Super),
            CaseWith(x => x.Glove.Lamp.SpeedBooster.Varia.Wave),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.SpaceJump.Morph),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.HiJump.Morph),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.Morph.SpringBall),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.Ice.Morph),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.SpaceJump.Morph),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.HiJump.Morph),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.Morph.SpringBall),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.Ice.Morph),
            CaseWith(x => x.Glove.Lamp.ETank(5).Missile.CardNorfairL2.SpaceJump.Morph.PowerBomb),
            CaseWith(x => x.Glove.Lamp.ETank(5).Missile.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.Glove.Lamp.ETank(5).Missile.CardNorfairL2.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Glove.Lamp.ETank(5).Missile.CardNorfairL2.Morph.SpringBall.PowerBomb),
            CaseWith(x => x.Varia.Flute.Mitt.ScrewAttack.SpaceJump.Super.ETank(2).CardNorfairL2),
            CaseWith(x => x.Varia.Flute.Mitt.ScrewAttack.SpaceJump.Super.ETank(2).Morph),
        };

        protected override List<Case> EnergyTankCrocomire => new() {
            CaseWith(x => x.CardNorfairBoss),
        };

        protected override List<Case> PowerBombCrocomire => new() {
            CaseWith(x => x.CardNorfairBoss),
        };

        protected override List<Case> MissileBelowCrocomire => new() {
            CaseWith(x => x.CardNorfairBoss.Morph),
        };

        protected override List<Case> MissileGrapplingBeam => new() {
            CaseWith(x => x.CardNorfairBoss.SpeedBooster),
            CaseWith(x => x.CardNorfairBoss.Morph.SpaceJump),
            CaseWith(x => x.CardNorfairBoss.Morph.Bombs),
            CaseWith(x => x.CardNorfairBoss.Morph.Grapple),
        };

        protected override List<Case> GrapplingBeam => new() {
            CaseWith(x => x.CardNorfairBoss.SpaceJump),
            CaseWith(x => x.CardNorfairBoss.Morph),
            CaseWith(x => x.CardNorfairBoss.Grapple),
            CaseWith(x => x.CardNorfairBoss.HiJump.SpeedBooster),
        };

        #endregion

        #region Norfair Lower West

        protected override List<Case> NorfairLowerWest => new() {
            CaseWith(x => x.Morph.Bombs.Super.Varia.CardNorfairL2.PowerBomb.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.SpaceJump.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.HiJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.SpringBall.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.Ice.Gravity),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia.PowerBomb.HiJump),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia.PowerBomb.Gravity),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.SpaceJump.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.Morph.Bombs.PowerBomb.Gravity),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.Morph.SpringBall.PowerBomb.Gravity),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.Ice.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Flute.Varia.SpeedBooster.CardNorfairL2.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Flute.Varia.SpeedBooster.CardNorfairL2.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Flute.Varia.SpeedBooster.Missile.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Flute.Varia.SpeedBooster.Missile.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Flute.Varia.SpeedBooster.Wave.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Flute.Varia.SpeedBooster.Wave.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.SpaceJump.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.Morph.Bombs.PowerBomb.Gravity),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.Morph.SpringBall.PowerBomb.Gravity),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.Ice.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.CardNorfairL2.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.CardNorfairL2.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.Missile.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.Missile.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.Wave.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.Wave.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Flute.Mitt.Morph.Bombs),
            CaseWith(x => x.Flute.Mitt.Morph.PowerBomb),
            CaseWith(x => x.Flute.Mitt.ScrewAttack),
        };

        // MissileMickeyMouseRoom is unchanged by virtue of IBJ, PBs, Space already
        // satisfying the exit condition, thus shortcuting UN2 keycard access

        #endregion

        #region Norfair Lower East

        protected override List<Case> NorfairLowerEast => new() {
            CaseWith(x => x.Varia.CardLowerNorfairL1.Morph.Bombs.Super.CardNorfairL2.PowerBomb.Gravity),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Morph.PowerBomb.Super.CardNorfairL2.SpaceJump.Gravity),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Morph.PowerBomb.Super.CardNorfairL2.HiJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Morph.PowerBomb.Super.CardNorfairL2.SpringBall.Gravity),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Morph.PowerBomb.Super.CardNorfairL2.Ice.Gravity.Charge),
            CaseWith(x => x.Varia.CardLowerNorfairL1.SpeedBooster.Super.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.SpeedBooster.Super.Morph.PowerBomb.Gravity.SpaceJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.SpeedBooster.Super.Morph.PowerBomb.Gravity.Bombs),
            CaseWith(x => x.Varia.CardLowerNorfairL1.SpeedBooster.Super.Morph.PowerBomb.Gravity.SpringBall),
            CaseWith(x => x.Varia.CardLowerNorfairL1.SpeedBooster.Super.Morph.PowerBomb.Gravity.Ice.Charge),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.Missile.CardNorfairL2.SpaceJump.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.Missile.CardNorfairL2.Morph.Bombs.PowerBomb.Gravity),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.Missile.CardNorfairL2.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.Missile.CardNorfairL2.Morph.SpringBall.PowerBomb.Gravity),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.Missile.CardNorfairL2.Ice.Morph.PowerBomb.Gravity.Charge),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.CardNorfairL2.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.CardNorfairL2.Morph.PowerBomb.Gravity.SpaceJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.CardNorfairL2.Morph.PowerBomb.Gravity.Bombs),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.CardNorfairL2.Morph.PowerBomb.Gravity.SpringBall),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.CardNorfairL2.Morph.PowerBomb.Gravity.Ice.Charge),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.Missile.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.Missile.Morph.PowerBomb.Gravity.SpaceJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.Missile.Morph.PowerBomb.Gravity.Bombs),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.Missile.Morph.PowerBomb.Gravity.SpringBall),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.Missile.Morph.PowerBomb.Gravity.Ice.Charge),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.Wave.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.Wave.Morph.PowerBomb.Gravity.SpaceJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.Wave.Morph.PowerBomb.Gravity.Bombs),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.Wave.Morph.PowerBomb.Gravity.SpringBall),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.SpeedBooster.Wave.Morph.PowerBomb.Gravity.Ice.Charge),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.Missile.CardNorfairL2.SpaceJump.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.Missile.CardNorfairL2.Morph.Bombs.PowerBomb.Gravity),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.Missile.CardNorfairL2.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.Missile.CardNorfairL2.Morph.SpringBall.PowerBomb.Gravity),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.Missile.CardNorfairL2.Ice.Morph.PowerBomb.Gravity.Charge),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.CardNorfairL2.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.CardNorfairL2.Morph.PowerBomb.Gravity.SpaceJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.CardNorfairL2.Morph.PowerBomb.Gravity.Bombs),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.CardNorfairL2.Morph.PowerBomb.Gravity.SpringBall),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.CardNorfairL2.Morph.PowerBomb.Gravity.Ice.Charge),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.Missile.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.Missile.Morph.PowerBomb.Gravity.SpaceJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.Missile.Morph.PowerBomb.Gravity.Bombs),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.Missile.Morph.PowerBomb.Gravity.SpringBall),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.Missile.Morph.PowerBomb.Gravity.Ice.Charge),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.Wave.Morph.PowerBomb.HiJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.Wave.Morph.PowerBomb.Gravity.SpaceJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.Wave.Morph.PowerBomb.Gravity.Bombs),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.Wave.Morph.PowerBomb.Gravity.SpringBall),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Glove.Lamp.SpeedBooster.Wave.Morph.PowerBomb.Gravity.Ice.Charge),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.Mitt.Morph.Bombs.Super),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.Mitt.Morph.PowerBomb.Super.SpaceJump),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.Mitt.Morph.PowerBomb.Super.SpringBall),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.Mitt.Morph.PowerBomb.Super.SpeedBooster.Ice.Charge),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.Mitt.ScrewAttack.Super.SpaceJump),
        };

        protected override List<Case> MissileLowerNorfairAboveFireFleaRoom => NorfairLowerEastCanExit;

        protected override List<Case> PowerBombLowerNorfairAboveFireFleaRoom => new() {
            CaseWith(x => x.Morph.CardNorfairL2.Bombs),
            CaseWith(x => x.Morph.CardNorfairL2.PowerBomb),
            CaseWith(x => x.Morph.Missile.SpeedBooster.PowerBomb),
            CaseWith(x => x.Morph.Missile.SpaceJump.PowerBomb),
            CaseWith(x => x.Morph.Missile.Bombs),
            CaseWith(x => x.Morph.Missile.Grapple.PowerBomb),
            CaseWith(x => x.Morph.Missile.HiJump.SpringBall.PowerBomb),
            CaseWith(x => x.Morph.Missile.HiJump.Ice.PowerBomb),
            CaseWith(x => x.Morph.Super.SpeedBooster.PowerBomb),
            CaseWith(x => x.Morph.Super.SpaceJump.PowerBomb),
            CaseWith(x => x.Morph.Super.Bombs),
            CaseWith(x => x.Morph.Super.Grapple.PowerBomb),
            CaseWith(x => x.Morph.Super.HiJump.SpringBall.PowerBomb),
            CaseWith(x => x.Morph.Super.HiJump.Ice.PowerBomb),
            CaseWith(x => x.Morph.Wave.SpeedBooster.PowerBomb),
            CaseWith(x => x.Morph.Wave.SpaceJump.PowerBomb),
            CaseWith(x => x.Morph.Wave.Bombs),
            CaseWith(x => x.Morph.Wave.Grapple.PowerBomb),
            CaseWith(x => x.Morph.Wave.HiJump.SpringBall.PowerBomb),
            CaseWith(x => x.Morph.Wave.HiJump.Ice.PowerBomb),
            CaseWith(x => x.ETank(5).Morph.Bombs),
            CaseWith(x => x.ETank(5).Morph.PowerBomb),
        };

        protected override List<Case> PowerBombPowerBombsOfShame => new() {
            CaseWith(x => x.Morph.CardNorfairL2.PowerBomb),
            CaseWith(x => x.Morph.Missile.SpeedBooster.PowerBomb),
            CaseWith(x => x.Morph.Missile.SpaceJump.PowerBomb),
            CaseWith(x => x.Morph.Missile.Bombs.PowerBomb),
            CaseWith(x => x.Morph.Missile.Grapple.PowerBomb),
            CaseWith(x => x.Morph.Missile.HiJump.SpringBall.PowerBomb),
            CaseWith(x => x.Morph.Missile.HiJump.Ice.PowerBomb),
            CaseWith(x => x.Morph.Super.SpeedBooster.PowerBomb),
            CaseWith(x => x.Morph.Super.SpaceJump.PowerBomb),
            CaseWith(x => x.Morph.Super.Bombs.PowerBomb),
            CaseWith(x => x.Morph.Super.Grapple.PowerBomb),
            CaseWith(x => x.Morph.Super.HiJump.SpringBall.PowerBomb),
            CaseWith(x => x.Morph.Super.HiJump.Ice.PowerBomb),
            CaseWith(x => x.Morph.Wave.SpeedBooster.PowerBomb),
            CaseWith(x => x.Morph.Wave.SpaceJump.PowerBomb),
            CaseWith(x => x.Morph.Wave.Bombs.PowerBomb),
            CaseWith(x => x.Morph.Wave.Grapple.PowerBomb),
            CaseWith(x => x.Morph.Wave.HiJump.SpringBall.PowerBomb),
            CaseWith(x => x.Morph.Wave.HiJump.Ice.PowerBomb),
            CaseWith(x => x.ETank(5).Morph.PowerBomb),
        };

        protected override List<Case> MissileLowerNorfairNearWaveBeam => new() {
            CaseWith(x => x.Morph.CardNorfairL2.Bombs),
            CaseWith(x => x.Morph.CardNorfairL2.PowerBomb),
            CaseWith(x => x.Morph.CardNorfairL2.ScrewAttack),
            CaseWith(x => x.Morph.Missile.SpeedBooster.PowerBomb),
            CaseWith(x => x.Morph.Missile.SpeedBooster.ScrewAttack),
            CaseWith(x => x.Morph.Missile.SpaceJump.PowerBomb),
            CaseWith(x => x.Morph.Missile.SpaceJump.ScrewAttack),
            CaseWith(x => x.Morph.Missile.Bombs),
            CaseWith(x => x.Morph.Missile.Grapple.PowerBomb),
            CaseWith(x => x.Morph.Missile.Grapple.ScrewAttack),
            CaseWith(x => x.Morph.Missile.HiJump.SpringBall.PowerBomb),
            CaseWith(x => x.Morph.Missile.HiJump.SpringBall.ScrewAttack),
            CaseWith(x => x.Morph.Missile.HiJump.Ice.PowerBomb),
            CaseWith(x => x.Morph.Missile.HiJump.Ice.ScrewAttack),
            CaseWith(x => x.Morph.Super.SpeedBooster.PowerBomb),
            CaseWith(x => x.Morph.Super.SpeedBooster.ScrewAttack),
            CaseWith(x => x.Morph.Super.SpaceJump.PowerBomb),
            CaseWith(x => x.Morph.Super.SpaceJump.ScrewAttack),
            CaseWith(x => x.Morph.Super.Bombs),
            CaseWith(x => x.Morph.Super.Grapple.PowerBomb),
            CaseWith(x => x.Morph.Super.Grapple.ScrewAttack),
            CaseWith(x => x.Morph.Super.HiJump.SpringBall.PowerBomb),
            CaseWith(x => x.Morph.Super.HiJump.SpringBall.ScrewAttack),
            CaseWith(x => x.Morph.Super.HiJump.Ice.PowerBomb),
            CaseWith(x => x.Morph.Super.HiJump.Ice.ScrewAttack),
            CaseWith(x => x.Morph.Wave.SpeedBooster.PowerBomb),
            CaseWith(x => x.Morph.Wave.SpeedBooster.ScrewAttack),
            CaseWith(x => x.Morph.Wave.SpaceJump.PowerBomb),
            CaseWith(x => x.Morph.Wave.SpaceJump.ScrewAttack),
            CaseWith(x => x.Morph.Wave.Bombs),
            CaseWith(x => x.Morph.Wave.Grapple.PowerBomb),
            CaseWith(x => x.Morph.Wave.Grapple.ScrewAttack),
            CaseWith(x => x.Morph.Wave.HiJump.SpringBall.PowerBomb),
            CaseWith(x => x.Morph.Wave.HiJump.SpringBall.ScrewAttack),
            CaseWith(x => x.Morph.Wave.HiJump.Ice.PowerBomb),
            CaseWith(x => x.Morph.Wave.HiJump.Ice.ScrewAttack),
            CaseWith(x => x.ETank(5).Morph.Bombs),
            CaseWith(x => x.ETank(5).Morph.PowerBomb),
            CaseWith(x => x.ETank(5).Morph.ScrewAttack),
        };

        protected override List<Case> EnergyTankRidley => new() {
            CaseWith(x => x.Morph.CardNorfairL2.CardLowerNorfairBoss.PowerBomb.Super),
            CaseWith(x => x.Morph.Super.SpeedBooster.CardLowerNorfairBoss.PowerBomb),
            CaseWith(x => x.Morph.Super.SpaceJump.CardLowerNorfairBoss.PowerBomb),
            CaseWith(x => x.Morph.Super.Bombs.CardLowerNorfairBoss.PowerBomb),
            CaseWith(x => x.Morph.Super.Grapple.CardLowerNorfairBoss.PowerBomb),
            CaseWith(x => x.Morph.Super.HiJump.SpringBall.CardLowerNorfairBoss.PowerBomb),
            CaseWith(x => x.Morph.Super.HiJump.Ice.CardLowerNorfairBoss.PowerBomb),
            CaseWith(x => x.ETank(5).CardLowerNorfairBoss.Morph.PowerBomb.Super),
        };
        
        protected override List<Case> EnergyTankFirefleas => NorfairLowerEastCanExit;

        List<Case> NorfairLowerEastCanExit => new() {
            CaseWith(x => x.Morph.CardNorfairL2),
            CaseWith(x => x.Morph.Missile.SpeedBooster),
            CaseWith(x => x.Morph.Missile.SpaceJump),
            CaseWith(x => x.Morph.Missile.Bombs),
            CaseWith(x => x.Morph.Missile.Grapple),
            CaseWith(x => x.Morph.Missile.HiJump.SpringBall),
            CaseWith(x => x.Morph.Missile.HiJump.Ice),
            CaseWith(x => x.Morph.Super.SpeedBooster),
            CaseWith(x => x.Morph.Super.SpaceJump),
            CaseWith(x => x.Morph.Super.Bombs),
            CaseWith(x => x.Morph.Super.Grapple),
            CaseWith(x => x.Morph.Super.HiJump.SpringBall),
            CaseWith(x => x.Morph.Super.HiJump.Ice),
            CaseWith(x => x.Morph.Wave.SpeedBooster),
            CaseWith(x => x.Morph.Wave.SpaceJump),
            CaseWith(x => x.Morph.Wave.Bombs),
            CaseWith(x => x.Morph.Wave.Grapple),
            CaseWith(x => x.Morph.Wave.HiJump.SpringBall),
            CaseWith(x => x.Morph.Wave.HiJump.Ice),
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
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
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
            CaseWith(x => x.Boots.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Boots.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Boots.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Boots.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Boots.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Boots.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
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
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Hammer.Glove),
            CaseWith(x => x.Mirror.MoonPearl.Mitt),
        };

        protected override List<Case> CheckerboardCave => new() {
            CaseWith(x => x.Mirror.Flute.Mitt),
            CaseWith(x => x.Mirror.CardNorfairL2.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Glove),
            CaseWith(x => x.Mirror.CardNorfairL2.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Glove),
            CaseWith(x => x.Mirror.CardNorfairL2.Varia.Super.HiJump.Morph.PowerBomb.Glove),
            CaseWith(x => x.Mirror.CardNorfairL2.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Glove),
            CaseWith(x => x.Mirror.CardNorfairL2.Varia.Super.Ice.Gravity.Morph.PowerBomb.Glove),
            CaseWith(x => x.Mirror.SpeedBooster.Varia.Super.HiJump.Morph.PowerBomb.Glove),
            CaseWith(x => x.Mirror.SpeedBooster.Varia.Super.Gravity.Morph.PowerBomb.Glove),
        };

        protected override List<Case> BombosTablet => new() {
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Hammer.Glove),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Mitt),
        };

        protected override List<Case> LakeHyliaIsland => new() {
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Hammer.Glove),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Mitt),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple),
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
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
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
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers),
        };

        protected override List<Case> PyramidFairy => new() {
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers.Mirror),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove.Mirror),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers.Mirror),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove.Mirror),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hammer),
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
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt),
        };

        #endregion

        #region Dark World Mire

        protected override List<Case> DarkWorldMire => new() {
            CaseWith(x => x.Flute.Mitt),
            CaseWith(x => x.CardNorfairL2.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.Bombs.Gravity.PowerBomb),
            CaseWith(x => x.CardNorfairL2.Varia.Super.HiJump.Morph.PowerBomb),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.SpringBall.Gravity.PowerBomb),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Ice.Gravity.Morph.PowerBomb),
            CaseWith(x => x.SpeedBooster.Varia.Super.HiJump.Morph.PowerBomb),
            CaseWith(x => x.SpeedBooster.Varia.Super.Gravity.Morph.PowerBomb),
        };

        #endregion

        #region Desert Palace

        protected override List<Case> DesertPalace => new() {
            CaseWith(x => x.Book),
            CaseWith(x => x.Mirror.Mitt.Flute),
            CaseWith(x => x.CardNorfairL2.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror),
            CaseWith(x => x.CardNorfairL2.Varia.Super.HiJump.Morph.PowerBomb.Mirror),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror),
            CaseWith(x => x.SpeedBooster.Varia.Super.HiJump.Morph.PowerBomb.Mirror),
            CaseWith(x => x.SpeedBooster.Varia.Super.Gravity.Morph.PowerBomb.Mirror),
        };

        protected override List<Case> DesertPalaceLanmolas => new() {
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Firerod),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Lamp.Sword),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Lamp.Hammer),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Lamp.Bow),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Lamp.Icerod),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Lamp.Byrna),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Lamp.Somaria),
            CaseWith(x => x.CardNorfairL2.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Firerod),
            CaseWith(x => x.CardNorfairL2.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Sword),
            CaseWith(x => x.CardNorfairL2.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Hammer),
            CaseWith(x => x.CardNorfairL2.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Bow),
            CaseWith(x => x.CardNorfairL2.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Icerod),
            CaseWith(x => x.CardNorfairL2.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Byrna),
            CaseWith(x => x.CardNorfairL2.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Somaria),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Firerod),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Sword),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Hammer),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Bow),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Icerod),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Byrna),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.Bombs.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Somaria),
            CaseWith(x => x.CardNorfairL2.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Firerod),
            CaseWith(x => x.CardNorfairL2.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Sword),
            CaseWith(x => x.CardNorfairL2.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Hammer),
            CaseWith(x => x.CardNorfairL2.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Bow),
            CaseWith(x => x.CardNorfairL2.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Icerod),
            CaseWith(x => x.CardNorfairL2.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Byrna),
            CaseWith(x => x.CardNorfairL2.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Somaria),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Firerod),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Sword),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Hammer),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Bow),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Icerod),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Byrna),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Morph.SpringBall.Gravity.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Somaria),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Firerod),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Sword),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Hammer),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Bow),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Icerod),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Byrna),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Ice.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Somaria),
            CaseWith(x => x.SpeedBooster.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Firerod),
            CaseWith(x => x.SpeedBooster.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Sword),
            CaseWith(x => x.SpeedBooster.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Hammer),
            CaseWith(x => x.SpeedBooster.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Bow),
            CaseWith(x => x.SpeedBooster.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Icerod),
            CaseWith(x => x.SpeedBooster.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Byrna),
            CaseWith(x => x.SpeedBooster.Varia.Super.HiJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Somaria),
            CaseWith(x => x.SpeedBooster.Varia.Super.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Firerod),
            CaseWith(x => x.SpeedBooster.Varia.Super.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Sword),
            CaseWith(x => x.SpeedBooster.Varia.Super.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Hammer),
            CaseWith(x => x.SpeedBooster.Varia.Super.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Bow),
            CaseWith(x => x.SpeedBooster.Varia.Super.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Icerod),
            CaseWith(x => x.SpeedBooster.Varia.Super.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Byrna),
            CaseWith(x => x.SpeedBooster.Varia.Super.Gravity.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Somaria),
        };

        #endregion

        #region Palace of Darkness

        protected override List<Case> PalaceOfDarkness => new() {
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt.Flippers),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers),
        };

        protected override List<Case> PalaceOfDarknessBigKeyChest => new() {
            CaseWith(x => x.Has.KeyPD.KeyPD(1)),
            CaseWith(x => x.KeyPD(6)),
        };

        protected override List<Case> PalaceOfDarknessCompassChest => new() {
            CaseWith(x => x.KeyPD(4)),
        };

        protected override List<Case> PalaceOfDarknessHarmlessHellway => new() {
            CaseWith(x => x.Has.KeyPD.KeyPD(4)),
            CaseWith(x => x.KeyPD(6)),
        };

        protected override List<Case> PalaceOfDarknessDarkBasement => new() {
            CaseWith(x => x.Lamp.KeyPD(4)),
        };

        protected override List<Case> PalaceOfDarknessDarkMaze => new() {
            CaseWith(x => x.Lamp.KeyPD(6)),
        };

        protected override List<Case> PalaceOfDarknessBigChest => new() {
            CaseWith(x => x.BigKeyPD.Lamp.KeyPD(6)),
        };

        #endregion

        #region Swamp Palace

        protected override List<Case> SwampPalace => new() {
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Sword.Cape.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.Sword.Cape.Lamp.KeyCT(2).Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.MasterSword.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.MasterSword.Lamp.KeyCT(2).Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Hookshot),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Hookshot),
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
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
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
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Charge.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.Ice.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Missile.HiJump.Ice.Grapple.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt),
        };

        #endregion

        #region Misery Mire

        protected override List<Case> MiseryMire => new() {
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.Flute.Mitt),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.CardNorfairL2.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.CardNorfairL2.Varia.Super.Morph.Bombs.Gravity.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.CardNorfairL2.Varia.Super.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.CardNorfairL2.Varia.Super.Morph.SpringBall.Gravity.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.CardNorfairL2.Varia.Super.Ice.Gravity.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.SpeedBooster.Varia.Super.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.SpeedBooster.Varia.Super.Gravity.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.Flute.Mitt),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.CardNorfairL2.Varia.Super.SpaceJump.Gravity.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.CardNorfairL2.Varia.Super.Morph.Bombs.Gravity.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.CardNorfairL2.Varia.Super.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.CardNorfairL2.Varia.Super.Morph.SpringBall.Gravity.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.CardNorfairL2.Varia.Super.Ice.Gravity.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.SpeedBooster.Varia.Super.HiJump.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.SpeedBooster.Varia.Super.Gravity.Morph.PowerBomb),
        };

        #endregion

        #region Turtle Rock

        protected override List<Case> TurtleRockBigKeyChest => new() {
            CaseWith(x => x.Has.BigKeyTR.KeyTR(2)),
            CaseWith(x => x.Has.KeyTR.KeyTR(3)),
            CaseWith(x => x.KeyTR(4)),
        };

        #endregion

    }

}
