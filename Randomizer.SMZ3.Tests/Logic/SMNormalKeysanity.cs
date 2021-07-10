using System.Collections.Generic;

namespace Randomizer.SMZ3.Tests.Logic {

    public class SMNormalKeysanity : SMNormal {

        #region Crateria West

        protected override List<Case> EnergyTankGauntlet => new() {
            CaseWith(x => x.CardCrateriaL1.Morph.Bombs.ETank(1)),
            CaseWith(x => x.CardCrateriaL1.Morph.SpaceJump.TwoPowerBombs.ETank(1)),
            CaseWith(x => x.CardCrateriaL1.Morph.SpaceJump.ScrewAttack.ETank(1)),
            CaseWith(x => x.CardCrateriaL1.Morph.SpeedBooster.TwoPowerBombs.ETank(1)),
            CaseWith(x => x.CardCrateriaL1.Morph.SpeedBooster.ScrewAttack.ETank(1)),
        };

        protected override List<Case> MissileCrateriaGauntlet => new() {
            CaseWith(x => x.CardCrateriaL1.Morph.Bombs.ETank(2)),
            CaseWith(x => x.CardCrateriaL1.Morph.SpaceJump.TwoPowerBombs.ETank(2)),
            CaseWith(x => x.CardCrateriaL1.Morph.SpaceJump.ScrewAttack.PowerBomb.ETank(2)),
            CaseWith(x => x.CardCrateriaL1.Morph.SpeedBooster.TwoPowerBombs.ETank(2)),
            CaseWith(x => x.CardCrateriaL1.Morph.SpeedBooster.ScrewAttack.PowerBomb.ETank(2)),
        };

        #endregion

        #region Crateria Central

        protected override List<Case> PowerBombCrateriaSurface => new() {
            CaseWith(x => x.CardCrateriaL1.SpeedBooster),
            CaseWith(x => x.CardCrateriaL1.SpaceJump),
            CaseWith(x => x.CardCrateriaL1.Morph.Bombs),
        };

        protected override List<Case> Bombs => new() {
            CaseWith(x => x.CardCrateriaBoss.Morph.Bombs),
            CaseWith(x => x.CardCrateriaBoss.Morph.PowerBomb),
        };

        #endregion

        #region Crateria East

        protected override List<Case> CrateriaEast => new() {
            CaseWith(x => x.CardCrateriaL2.Super),
            CaseWith(x => x.CardCrateriaL2.Flute.Ice),
            CaseWith(x => x.CardCrateriaL2.Flute.HiJump),
            CaseWith(x => x.CardCrateriaL2.Flute.SpaceJump),
            CaseWith(x => x.CardCrateriaL2.Glove.Lamp.Ice),
            CaseWith(x => x.CardCrateriaL2.Glove.Lamp.HiJump),
            CaseWith(x => x.CardCrateriaL2.Glove.Lamp.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super.CardMaridiaL2.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super.CardMaridiaBoss.SpeedBooster.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super.CardMaridiaBoss.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super.CardMaridiaBoss.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super.CardMaridiaL2.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super.CardMaridiaBoss.SpeedBooster.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super.CardMaridiaBoss.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super.CardMaridiaBoss.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super.CardMaridiaL2.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super.CardMaridiaBoss.SpeedBooster.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super.CardMaridiaBoss.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super.CardMaridiaBoss.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Super.CardMaridiaL2.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Super.CardMaridiaL2.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Super.CardMaridiaBoss.SpeedBooster.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Super.CardMaridiaBoss.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.Super.CardMaridiaBoss.Bombs),
            CaseWith(x => x.Morph.PowerBomb.Super.Gravity),
        };

        protected override List<Case> MissileOutsideWreckedShipBottom => new() {
            CaseWith(x => x.Morph.SpeedBooster),
            CaseWith(x => x.Morph.Grapple),
            CaseWith(x => x.Morph.SpaceJump),
            CaseWith(x => x.Morph.Gravity.Bombs),
            CaseWith(x => x.Morph.Gravity.HiJump),
            CaseWith(x => x.Morph.Super.PowerBomb.Gravity),
            CaseWith(x => x.Morph.Super.MoonPearl.Flippers.Gravity.Sword.Cape.Lamp.KeyCT(2).ScrewAttack.CardMaridiaL2),
            CaseWith(x => x.Morph.Super.MoonPearl.Flippers.Gravity.MasterSword.Lamp.KeyCT(2).ScrewAttack.CardMaridiaL2),
            CaseWith(x => x.Morph.Super.MoonPearl.Flippers.Gravity.Hammer.Glove.ScrewAttack.CardMaridiaL2),
            CaseWith(x => x.Morph.Super.MoonPearl.Flippers.Gravity.Mitt.ScrewAttack.CardMaridiaL2),
        };

        protected override List<Case> MissilesOutsideWreckedShipTopHalf => new() {
            CaseWith(x => x.Super.CardCrateriaL2.SpeedBooster.CardWreckedShipBoss.Morph.Bombs),
            CaseWith(x => x.Super.CardCrateriaL2.SpeedBooster.CardWreckedShipBoss.Morph.PowerBomb),
            CaseWith(x => x.Super.CardCrateriaL2.Grapple.CardWreckedShipBoss.Morph.Bombs),
            CaseWith(x => x.Super.CardCrateriaL2.Grapple.CardWreckedShipBoss.Morph.PowerBomb),
            CaseWith(x => x.Super.CardCrateriaL2.SpaceJump.CardWreckedShipBoss.Morph.Bombs),
            CaseWith(x => x.Super.CardCrateriaL2.SpaceJump.CardWreckedShipBoss.Morph.PowerBomb),
            CaseWith(x => x.Super.CardCrateriaL2.Gravity.Morph.Bombs.CardWreckedShipBoss),
            CaseWith(x => x.Super.Morph.PowerBomb.Gravity.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Bombs.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Bombs.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Bombs.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Bombs.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Bombs.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.Bombs.CardMaridiaL2.CardWreckedShipBoss),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Bombs.CardWreckedShipBoss),
        };

        #endregion

        #region Wrecked Ship

        protected override List<Case> WreckedShip => new() {
            CaseWith(x => x.Super.CardCrateriaL2.SpeedBooster),
            CaseWith(x => x.Super.CardCrateriaL2.Grapple),
            CaseWith(x => x.Super.CardCrateriaL2.SpaceJump),
            CaseWith(x => x.Super.CardCrateriaL2.Gravity.Morph.Bombs),
            CaseWith(x => x.Super.CardCrateriaL2.Gravity.HiJump),
            CaseWith(x => x.Super.Morph.PowerBomb.Gravity),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Bombs.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).ScrewAttack.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.SpeedBooster.HiJump),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.SpaceJump),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Bombs.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).ScrewAttack.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.SpeedBooster.HiJump),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.SpaceJump),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Bombs.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.ScrewAttack.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.SpeedBooster.HiJump),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.SpaceJump),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Bombs),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.Bombs.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.ScrewAttack.CardMaridiaL2),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.SpeedBooster.HiJump),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.SpaceJump),
            CaseWith(x => x.Super.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Bombs),
        };

        protected override List<Case> ReserveTankWreckedShip => new() {
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.SpeedBooster.Grapple),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.SpeedBooster.SpaceJump),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.SpeedBooster.Varia.ETank(2)),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.SpeedBooster.ETank(3)),
        };

        protected override List<Case> MissileGravitySuit => new() {
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.CardWreckedShipL1.Grapple),
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.CardWreckedShipL1.SpaceJump),
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.CardWreckedShipL1.Varia.ETank(2)),
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.CardWreckedShipL1.ETank(3)),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.Grapple),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.SpaceJump),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.Varia.ETank(2)),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.ETank(3)),
        };

        protected override List<Case> MissileWreckedShipTop => new() {
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb),
        };

        protected override List<Case> EnergyTankWreckedShip => new() {
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.HiJump),
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.SpaceJump),
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.SpeedBooster),
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.Gravity),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.HiJump),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.SpeedBooster),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.Gravity),
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
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.CardWreckedShipL1.Grapple),
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.CardWreckedShipL1.SpaceJump),
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.CardWreckedShipL1.Varia.ETank(2)),
            CaseWith(x => x.CardWreckedShipBoss.Morph.Bombs.CardWreckedShipL1.ETank(3)),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.Grapple),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.SpaceJump),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.Varia.ETank(2)),
            CaseWith(x => x.CardWreckedShipBoss.Morph.PowerBomb.CardWreckedShipL1.ETank(3)),
        };

        #endregion

        #region Brinstar Blue

        protected override List<Case> MissileBlueBrinstarMiddle => new() {
            CaseWith(x => x.CardBrinstarL1.Morph),
        };
        
        protected override List<Case> EnergyTankBrinstarCeiling => new() {
            CaseWith(x => x.CardBrinstarL1.SpaceJump),
            CaseWith(x => x.CardBrinstarL1.Morph.Bombs),
            CaseWith(x => x.CardBrinstarL1.HiJump),
            CaseWith(x => x.CardBrinstarL1.SpeedBooster),
            CaseWith(x => x.CardBrinstarL1.Ice),
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
        };

        protected override List<Case> EnergyTankBrinstarGate => new() {
            CaseWith(x => x.CardBrinstarL2.Morph.PowerBomb.Wave.ETank(1)),
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
            CaseWith(x => x.Gravity.Morph.PowerBomb.Super),
            CaseWith(x => x.Gravity.Flute.Morph.PowerBomb),
            CaseWith(x => x.Gravity.Glove.Lamp.Morph.PowerBomb),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.ScrewAttack),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL1.CardMaridiaL2.ScrewAttack),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Hammer.Glove.CardMaridiaL1.CardMaridiaL2.ScrewAttack),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Mitt.CardMaridiaL1.CardMaridiaL2.Bombs),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Mitt.CardMaridiaL1.CardMaridiaL2.PowerBomb),
            CaseWith(x => x.Gravity.MoonPearl.Flippers.Morph.Mitt.CardMaridiaL1.CardMaridiaL2.ScrewAttack),
        };

        #endregion

        #region Maridia Inner

        protected override List<Case> YellowMaridiaWateringHole => new() {
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb),
        };
        protected override List<Case> MissileYellowMaridiaFalseWall => new() {
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs),
            CaseWith(x => x.CardMaridiaL1.Morph.PowerBomb),
        };

        protected override List<Case> PlasmaBeam => new() {
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.CardMaridiaBoss.Gravity.HiJump.ScrewAttack),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.CardMaridiaBoss.Gravity.HiJump.Plasma),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.CardMaridiaBoss.Gravity.SpaceJump.ScrewAttack),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.CardMaridiaBoss.Gravity.SpaceJump.Plasma),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.CardMaridiaBoss.Gravity.Morph.Bombs.ScrewAttack),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.CardMaridiaBoss.Gravity.Morph.Bombs.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.SpeedBooster.HiJump.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.SpeedBooster.HiJump.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.SpaceJump.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.SpaceJump.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Bombs.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Bombs.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.SpeedBooster.HiJump.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.SpeedBooster.HiJump.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.SpaceJump.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.SpaceJump.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Bombs.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Bombs.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.SpeedBooster.HiJump.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.SpeedBooster.HiJump.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.SpaceJump.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.SpaceJump.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Bombs.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Bombs.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.SpeedBooster.HiJump.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.SpeedBooster.HiJump.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.SpaceJump.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.SpaceJump.Plasma),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Bombs.ScrewAttack),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Bombs.Plasma),
        };

        protected override List<Case> LeftMaridiaSandPitRoom => new() {
            CaseWith(x => x.CardMaridiaL1.SpaceJump.Super.Morph.PowerBomb),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.Super),
            CaseWith(x => x.CardMaridiaL1.SpeedBooster.Super.Morph.PowerBomb),
            CaseWith(x => x.CardMaridiaL1.Grapple.Super.Morph.PowerBomb),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super.Bombs),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super.PowerBomb),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super.Bombs),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super.PowerBomb),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super.Bombs),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super.PowerBomb),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Mitt.Super.Bombs),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Mitt.Super.PowerBomb),
        };

        protected override List<Case> MissileRightMaridiaSandPitRoom => RightMaridiaSandPitRoom;
        protected override List<Case> PowerBombRightMaridiaSandPitRoom => RightMaridiaSandPitRoom;

        List<Case> RightMaridiaSandPitRoom => new() {
            CaseWith(x => x.CardMaridiaL1.SpaceJump.Super),
            CaseWith(x => x.CardMaridiaL1.Morph.Bombs.Super),
            CaseWith(x => x.CardMaridiaL1.SpeedBooster.Super),
            CaseWith(x => x.CardMaridiaL1.Grapple.Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.Super),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Mitt.Super),
        };

        protected override List<Case> PinkMaridia => new() {
            CaseWith(x => x.CardMaridiaL1.SpeedBooster),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).SpeedBooster),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).SpeedBooster),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.SpeedBooster),
            CaseWith(x => x.CardMaridiaL2.MoonPearl.Flippers.Gravity.Morph.Mitt.SpeedBooster),
        };

        protected override List<Case> MissileDraygon => new() {
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt),
        };

        protected override List<Case> EnergyTankBotwoon => new() {
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaL2),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaL2),
        };

        protected override List<Case> SpaceJump => new() {
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.CardMaridiaBoss.Gravity.HiJump),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.CardMaridiaBoss.Gravity.SpaceJump),
            CaseWith(x => x.CardMaridiaL1.CardMaridiaL2.SpeedBooster.CardMaridiaBoss.Gravity.Morph.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.SpeedBooster.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Sword.Cape.Lamp.KeyCT(2).CardMaridiaBoss.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.SpeedBooster.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.MasterSword.Lamp.KeyCT(2).CardMaridiaBoss.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.SpeedBooster.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Hammer.Glove.CardMaridiaBoss.Bombs),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.SpeedBooster.HiJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.SpaceJump),
            CaseWith(x => x.MoonPearl.Flippers.Gravity.Morph.Mitt.CardMaridiaBoss.Bombs),
        };

        #endregion

        #region Norfair Upper West

        // MisileLavaRoom is unchanged by virtue of Space, IBJ, HiJump, Speed already
        // satisfying crossing Cathedral which in turn shortcuts UN East access

        protected override List<Case> IceBeam => new() {
            CaseWith(x => x.CardNorfairL1.Morph.Bombs.Varia.SpeedBooster),
            CaseWith(x => x.CardNorfairL1.Morph.PowerBomb.Varia.SpeedBooster),
        };

        protected override List<Case> MissileBelowIceBeam => new() {
            CaseWith(x => x.CardNorfairL1.Morph.PowerBomb.Varia.SpeedBooster),
        };

        #endregion

        #region Norfair Upper East

        protected override List<Case> NorfairUpperEast => new() {
            CaseWith(x => x.Morph.Bombs.Super.Varia.CardNorfairL2),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.HiJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.CardNorfairL2.SpaceJump),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.CardNorfairL2.HiJump),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia.CardNorfairL2),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia.Wave.PowerBomb),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.HiJump),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.SpeedBooster),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.HiJump),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.SpeedBooster),
        };

        protected override List<Case> ReserveTankNorfair => new() {
            CaseWith(x => x.CardNorfairL2.Morph.SpaceJump),
            CaseWith(x => x.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.CardNorfairL2.Morph.Grapple.SpeedBooster),
            CaseWith(x => x.CardNorfairL2.Morph.Grapple.PowerBomb),
            CaseWith(x => x.CardNorfairL2.Morph.HiJump),
            CaseWith(x => x.CardNorfairL2.Morph.Ice),
        };
        protected override List<Case> MissileNorfairReserveTank => new() {
            CaseWith(x => x.CardNorfairL2.Morph.SpaceJump),
            CaseWith(x => x.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.CardNorfairL2.Morph.Grapple.SpeedBooster),
            CaseWith(x => x.CardNorfairL2.Morph.Grapple.PowerBomb),
            CaseWith(x => x.CardNorfairL2.Morph.HiJump),
            CaseWith(x => x.CardNorfairL2.Morph.Ice),
        };

        protected override List<Case> MissileBubbleNorfairGreenDoor => new() {
            CaseWith(x => x.CardNorfairL2.SpaceJump),
            CaseWith(x => x.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.CardNorfairL2.Grapple.Morph.SpeedBooster),
            CaseWith(x => x.CardNorfairL2.Grapple.Morph.PowerBomb),
            CaseWith(x => x.CardNorfairL2.HiJump),
            CaseWith(x => x.CardNorfairL2.Ice),
        };

        protected override List<Case> MissileBubbleNorfair => new() {
            CaseWith(x => x.CardNorfairL2),
        };

        protected override List<Case> MissileSpeedBooster => new() {
            CaseWith(x => x.CardNorfairL2.SpaceJump),
            CaseWith(x => x.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.CardNorfairL2.Morph.SpeedBooster),
            CaseWith(x => x.CardNorfairL2.Morph.PowerBomb),
            CaseWith(x => x.CardNorfairL2.HiJump),
            CaseWith(x => x.CardNorfairL2.Ice),
        };
        protected override List<Case> SpeedBooster => new() {
            CaseWith(x => x.CardNorfairL2.SpaceJump),
            CaseWith(x => x.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.CardNorfairL2.Morph.SpeedBooster),
            CaseWith(x => x.CardNorfairL2.Morph.PowerBomb),
            CaseWith(x => x.CardNorfairL2.HiJump),
            CaseWith(x => x.CardNorfairL2.Ice),
        };

        protected override List<Case> MissileWaveBeam => new() {
            CaseWith(x => x.CardNorfairL2.SpaceJump),
            CaseWith(x => x.CardNorfairL2.Morph.Bombs),
            CaseWith(x => x.CardNorfairL2.Morph.SpeedBooster),
            CaseWith(x => x.CardNorfairL2.Morph.PowerBomb),
            CaseWith(x => x.CardNorfairL2.HiJump),
            CaseWith(x => x.CardNorfairL2.Ice),
            CaseWith(x => x.SpeedBooster.Wave.Morph.Super),
        };

        protected override List<Case> WaveBeam => new() {
            CaseWith(x => x.Morph.CardNorfairL2.SpaceJump),
            CaseWith(x => x.Morph.CardNorfairL2.Bombs),
            CaseWith(x => x.Morph.CardNorfairL2.SpeedBooster),
            CaseWith(x => x.Morph.CardNorfairL2.PowerBomb),
            CaseWith(x => x.Morph.CardNorfairL2.HiJump),
            CaseWith(x => x.Morph.CardNorfairL2.Ice),
            CaseWith(x => x.Morph.SpeedBooster.Wave.Super),
        };

        #endregion

        #region Norfair Upper Crocomire

        protected override List<Case> NorfairUpperCrocomire => new() {
            CaseWith(x => x.Morph.Bombs.Super.Varia.CardNorfairL2.Wave),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.SpaceJump.Wave),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.CardNorfairL2.HiJump.Wave),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.CardNorfairL2.SpaceJump.Gravity.Wave),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.CardNorfairL2.HiJump.Gravity.Wave),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia.CardNorfairL1.PowerBomb),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia.Wave),
            CaseWith(x => x.Flute.Varia.CardNorfairL1.Morph.PowerBomb.SpeedBooster),
            CaseWith(x => x.Flute.Varia.SpeedBooster.Wave),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.SpaceJump.Morph.PowerBomb.Wave),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.SpaceJump.Gravity.Morph.Wave),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.Morph.Bombs.Wave),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.HiJump.Morph.PowerBomb.Wave),
            CaseWith(x => x.Flute.Varia.Missile.CardNorfairL2.HiJump.Gravity.Morph.Wave),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.SpaceJump.Gravity.Morph.Wave),
            CaseWith(x => x.Flute.Varia.Super.CardNorfairL2.HiJump.Gravity.Morph.Wave),
            CaseWith(x => x.Glove.Lamp.Varia.CardNorfairL1.Morph.PowerBomb.SpeedBooster),
            CaseWith(x => x.Glove.Lamp.Varia.SpeedBooster.Wave),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.SpaceJump.Morph.PowerBomb.Wave),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.SpaceJump.Gravity.Morph.Wave),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.Morph.Bombs.Wave),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.HiJump.Morph.PowerBomb.Wave),
            CaseWith(x => x.Glove.Lamp.Varia.Missile.CardNorfairL2.HiJump.Gravity.Morph.Wave),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.SpaceJump.Gravity.Morph.Wave),
            CaseWith(x => x.Glove.Lamp.Varia.Super.CardNorfairL2.HiJump.Gravity.Morph.Wave),
            CaseWith(x => x.Varia.Flute.Mitt.ScrewAttack.SpaceJump.Super.Gravity.Wave.CardNorfairL2),
            CaseWith(x => x.Varia.Flute.Mitt.ScrewAttack.SpaceJump.Super.Gravity.Wave.Morph),
        };

        protected override List<Case> EnergyTankCrocomire => new() {
            CaseWith(x => x.CardNorfairBoss.ETank(1)),
            CaseWith(x => x.CardNorfairBoss.SpaceJump),
            CaseWith(x => x.CardNorfairBoss.Grapple),
        };

        protected override List<Case> PowerBombCrocomire => new() {
            CaseWith(x => x.CardNorfairBoss.SpaceJump),
            CaseWith(x => x.CardNorfairBoss.Morph.Bombs),
            CaseWith(x => x.CardNorfairBoss.HiJump),
            CaseWith(x => x.CardNorfairBoss.Grapple),
        };

        protected override List<Case> MissileBelowCrocomire => new() {
            CaseWith(x => x.CardNorfairBoss.Morph),
        };

        protected override List<Case> MissileGrapplingBeam => new() {
            CaseWith(x => x.CardNorfairBoss.Morph.SpaceJump),
            CaseWith(x => x.CardNorfairBoss.Morph.Bombs),
            CaseWith(x => x.CardNorfairBoss.Morph.SpeedBooster.PowerBomb),
        };

        protected override List<Case> GrapplingBeam => new() {
            CaseWith(x => x.CardNorfairBoss.Morph.SpaceJump),
            CaseWith(x => x.CardNorfairBoss.Morph.Bombs),
            CaseWith(x => x.CardNorfairBoss.Morph.SpeedBooster.PowerBomb),
        };

        #endregion

        #region Norfair Lower West

        protected override List<Case> NorfairLowerWest => new() {
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.CardNorfairL2.SpaceJump.Gravity),
            CaseWith(x => x.Varia.SpeedBooster.Super.Morph.Wave.PowerBomb.SpaceJump.Gravity),
            CaseWith(x => x.Varia.Flute.Mitt.Morph.Bombs),
            CaseWith(x => x.Varia.Flute.Mitt.Morph.PowerBomb),
            CaseWith(x => x.Varia.Flute.Mitt.ScrewAttack),
        };

        protected override List<Case> MissileMickeyMouseRoom => new() {
            CaseWith(x => x.Morph.Super.SpaceJump.PowerBomb),
            CaseWith(x => x.Morph.Super.Bombs.PowerBomb.CardLowerNorfairL1.CardNorfairL2),
            CaseWith(x => x.Morph.Super.Bombs.PowerBomb.Gravity.CardNorfairL2),
            CaseWith(x => x.Morph.Super.Bombs.PowerBomb.Gravity.Wave.Grapple),
        };

        #endregion

        #region Norfair Lower East

        protected override List<Case> NorfairLowerEast => new() {
            CaseWith(x => x.Varia.CardLowerNorfairL1.Morph.PowerBomb.Super.CardNorfairL2.SpaceJump.Gravity),
            CaseWith(x => x.Varia.CardLowerNorfairL1.SpeedBooster.Super.Morph.Wave.PowerBomb.SpaceJump.Gravity),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.Mitt.Morph.Bombs.Super.PowerBomb),
            CaseWith(x => x.Varia.CardLowerNorfairL1.Flute.Mitt.Morph.PowerBomb.Super.SpaceJump),
        };

        protected override List<Case> MissileLowerNorfairAboveFireFleaRoom => new() {
            CaseWith(x => x.Morph.CardNorfairL2),
            CaseWith(x => x.Morph.Gravity.Wave.Grapple),
            CaseWith(x => x.Morph.Gravity.Wave.SpaceJump),
        };
        protected override List<Case> PowerBombLowerNorfairAboveFireFleaRoom => new() {
            CaseWith(x => x.Morph.CardNorfairL2),
            CaseWith(x => x.Morph.Gravity.Wave.Grapple),
            CaseWith(x => x.Morph.Gravity.Wave.SpaceJump),
        };

        protected override List<Case> PowerBombPowerBombsOfShame => new() {
            CaseWith(x => x.Morph.CardNorfairL2.PowerBomb),
            CaseWith(x => x.Morph.Gravity.Wave.Grapple.PowerBomb),
            CaseWith(x => x.Morph.Gravity.Wave.SpaceJump.PowerBomb),
        };

        protected override List<Case> MissileLowerNorfairNearWaveBeam => new() {
            CaseWith(x => x.Morph.CardNorfairL2),
            CaseWith(x => x.Morph.Gravity.Wave.Grapple),
            CaseWith(x => x.Morph.Gravity.Wave.SpaceJump),
        };

        protected override List<Case> EnergyTankRidley => new() {
            CaseWith(x => x.Morph.CardNorfairL2.CardLowerNorfairBoss.PowerBomb.Super),
            CaseWith(x => x.Morph.Gravity.Wave.Grapple.CardLowerNorfairBoss.PowerBomb.Super),
            CaseWith(x => x.Morph.Gravity.Wave.SpaceJump.CardLowerNorfairBoss.PowerBomb.Super),
        };

        protected override List<Case> EnergyTankFirefleas => new() {
            CaseWith(x => x.Morph.CardNorfairL2),
            CaseWith(x => x.Morph.Gravity.Wave.Grapple),
            CaseWith(x => x.Morph.Gravity.Wave.SpaceJump),
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
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
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
            CaseWith(x => x.Boots.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
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
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Mirror.MoonPearl.Hammer.Glove),
            CaseWith(x => x.Mirror.MoonPearl.Mitt),
        };

        protected override List<Case> CheckerboardCave => new() {
            CaseWith(x => x.Mirror.Flute.Mitt),
            CaseWith(x => x.Mirror.CardNorfairL2.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Glove),
            CaseWith(x => x.Mirror.SpeedBooster.Wave.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Glove),
        };

        protected override List<Case> BombosTablet => new() {
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Lamp.KeyCT(2).Hookshot.Flippers),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Lamp.KeyCT(2).Hookshot.Glove),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Hammer.Glove),
            CaseWith(x => x.Book.MasterSword.Mirror.MoonPearl.Mitt),
        };

        protected override List<Case> LakeHyliaIsland => new() {
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Hammer.Glove),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.Mitt),
            CaseWith(x => x.Flippers.MoonPearl.Mirror.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster),
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
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
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
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers),
        };

        protected override List<Case> PyramidFairy => new() {
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Flippers.Mirror),
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2).Hookshot.Glove.Mirror),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hammer),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Flippers.Mirror),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2).Hookshot.Glove.Mirror),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hammer),
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
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hammer),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt),
        };

        #endregion

        #region Dark World Mire

        protected override List<Case> DarkWorldMire => new() {
            CaseWith(x => x.Flute.Mitt),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb),
            CaseWith(x => x.SpeedBooster.Wave.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb),
        };

        #endregion

        #region Desert Palace

        protected override List<Case> DesertPalace => new() {
            CaseWith(x => x.Book),
            CaseWith(x => x.Mirror.Mitt.Flute),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror),
            CaseWith(x => x.SpeedBooster.Wave.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror),
        };

        protected override List<Case> DesertPalaceLanmolas => new() {
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Firerod),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Lamp.Sword),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Lamp.Hammer),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Lamp.Bow),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Lamp.Icerod),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Lamp.Byrna),
            CaseWith(x => x.Glove.BigKeyDP.KeyDP.Lamp.Somaria),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Firerod),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Sword),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Hammer),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Bow),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Icerod),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Byrna),
            CaseWith(x => x.CardNorfairL2.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Somaria),
            CaseWith(x => x.SpeedBooster.Wave.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Firerod),
            CaseWith(x => x.SpeedBooster.Wave.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Sword),
            CaseWith(x => x.SpeedBooster.Wave.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Hammer),
            CaseWith(x => x.SpeedBooster.Wave.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Bow),
            CaseWith(x => x.SpeedBooster.Wave.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Icerod),
            CaseWith(x => x.SpeedBooster.Wave.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Byrna),
            CaseWith(x => x.SpeedBooster.Wave.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb.Mirror.BigKeyDP.KeyDP.Lamp.Somaria),
        };

        #endregion

        #region Palace of Darkness

        protected override List<Case> PalaceOfDarkness => new() {
            CaseWith(x => x.MoonPearl.Sword.Cape.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.MasterSword.Lamp.KeyCT(2)),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt.Flippers),
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers),
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
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Hammer),
            CaseWith(x => x.MoonPearl.Mirror.Flippers.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Hookshot),
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
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
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
            CaseWith(x => x.MoonPearl.CardMaridiaL1.CardMaridiaL2.Morph.PowerBomb.Super.Gravity.SpeedBooster.Flippers.Hookshot),
            CaseWith(x => x.MoonPearl.Hammer.Glove),
            CaseWith(x => x.MoonPearl.Mitt),
        };

        #endregion

        #region Misery Mire

        protected override List<Case> MiseryMire => new() {
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.Flute.Mitt),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.CardNorfairL2.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Boots.SpeedBooster.Wave.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.Flute.Mitt),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.CardNorfairL2.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb),
            CaseWith(x => x.Ether.Sword.MoonPearl.Hookshot.SpeedBooster.Wave.Varia.Super.Gravity.SpaceJump.Morph.PowerBomb),
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
