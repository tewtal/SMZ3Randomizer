using System.Collections.Generic;

namespace Randomizer.SuperMetroid.Tests.Logic {

    public class TournamentCases : CasualCases {

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
            CaseWith(x => x.Morph.Missile),
            CaseWith(x => x.Morph.Super),
        };

        #endregion

        #region Crateria East

        protected override List<Case> MissileOutsideWreckedShipBottom => CaseWithNothing;

        protected override List<Case> MissilesOutsideWreckedShipTopHalf => new() {
            CaseWith(x => x.Super.Morph.Bombs),
            CaseWith(x => x.Super.Morph.PowerBomb),
        };

        #endregion

        #region Wrecked Ship

        protected override List<Case> WreckedShip => new() {
            CaseWith(x => x.Super.Morph.PowerBomb),
        };

        protected override List<Case> ReserveTankWreckedShip => new() {
            CaseWith(x => x.Morph.PowerBomb.SpeedBooster.Varia),
            CaseWith(x => x.Morph.PowerBomb.SpeedBooster.ETank(2)),
        };

        protected override List<Case> MissileGravitySuit => new() {
            CaseWith(x => x.Varia),
            CaseWith(x => x.ETank(1)),
        };

        protected override List<Case> EnergyTankWreckedShip => new() {
            CaseWith(x => x.Bombs),
            CaseWith(x => x.PowerBomb),
            CaseWith(x => x.Morph.SpringBall),
            CaseWith(x => x.HiJump),
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.SpeedBooster),
            CaseWith(x => x.Gravity),
        };

        protected override List<Case> GravitySuit => new() {
            CaseWith(x => x.Varia),
            CaseWith(x => x.ETank(1)),
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

        protected override List<Case> PowerBombPinkBrinstar => new() {
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        protected override List<Case> EnergyTankBrinstarGate => new() {
            CaseWith(x => x.Morph.PowerBomb.Wave),
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        #endregion

        #region Brinstar Red

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
        };

        protected override List<Case> MissileGreenMaridiaShinespark => new() {
            CaseWith(x => x.Gravity.SpeedBooster),
        };

        protected override List<Case> EnergyTankMamaTurtle => new() {
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.SpeedBooster),
            CaseWith(x => x.Grapple),
            CaseWith(x => x.Morph.SpringBall.Gravity),
            CaseWith(x => x.Morph.SpringBall.HiJump),
        };

        #endregion

        #region Maridia Inner

        protected override List<Case> MaridiaInner => new() {
            CaseWith(x => x.Morph.PowerBomb.Super.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.HiJump.Ice.Grapple),
            CaseWith(x => x.Morph.PowerBomb.Super.HiJump.SpringBall.Grapple),
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
        };

        protected override List<Case> LeftMaridiaSandPitRoom => new() {
            CaseWith(x => x.HiJump.SpaceJump),
            CaseWith(x => x.HiJump.Morph.SpringBall),
            CaseWith(x => x.Gravity),
        };

        protected override List<Case> MissileRightMaridiaSandPitRoom => new() {
            CaseWith(x => x.HiJump),
            CaseWith(x => x.Gravity),
        };
        protected override List<Case> PowerBombRightMaridiaSandPitRoom => new() {
            CaseWith(x => x.HiJump.Morph.SpringBall),
            CaseWith(x => x.Gravity),
        };

        protected override List<Case> PinkMaridia => new() {
            CaseWith(x => x.Gravity),
        };

        protected override List<Case> SpringBall => new() {
            CaseWith(x => x.Grapple.Morph.PowerBomb.Gravity.SpaceJump),
            CaseWith(x => x.Grapple.Morph.PowerBomb.Gravity.Bombs),
            CaseWith(x => x.Grapple.Morph.PowerBomb.Gravity.HiJump),
            CaseWith(x => x.Grapple.Morph.PowerBomb.Ice.HiJump.SpringBall.SpaceJump),
        };

        protected override List<Case> MissileDraygon => new() {
            CaseWith(x => x.Ice.Gravity),
            CaseWith(x => x.SpeedBooster.Gravity),
        };

        protected override List<Case> EnergyTankBotwoon => new() {
            CaseWith(x => x.Ice),
            CaseWith(x => x.SpeedBooster.Gravity),
        };

        protected override List<Case> SpaceJump => new() {
            CaseWith(x => x.Ice.Gravity.SpaceJump),
            CaseWith(x => x.Ice.Gravity.Morph.Bombs),
            CaseWith(x => x.SpeedBooster.Gravity.SpaceJump),
            CaseWith(x => x.SpeedBooster.Gravity.Morph.Bombs),
            CaseWith(x => x.SpeedBooster.Gravity.HiJump),
        };

        #endregion

        #region Norfair Upper West

        protected override List<Case> IceBeam => new() {
            CaseWith(x => x.Super.Morph.Varia),
            CaseWith(x => x.Super.Morph.ETank(3)),
        };

        protected override List<Case> MissileBelowIceBeam => new() {
            CaseWith(x => x.Super.Morph.PowerBomb.Varia),
            CaseWith(x => x.Super.Morph.PowerBomb.ETank(3)),
            CaseWith(x => x.Varia.SpeedBooster.Super),
        };

        #endregion

        #region Norfair Upper East

        protected override List<Case> NorfairUpperEast => new() {
            CaseWith(x => x.Morph.Bombs.Super.Varia),
            CaseWith(x => x.Morph.Bombs.Super.ETank(5)),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia),
            CaseWith(x => x.Morph.PowerBomb.Super.ETank(5)),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia),
            CaseWith(x => x.ScrewAttack.Super.Morph.ETank(5)),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia),
            CaseWith(x => x.SpeedBooster.Super.Morph.ETank(5)),
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
            CaseWith(x => x.Missile.HiJump.Varia),
            CaseWith(x => x.Missile.SpaceJump),
            CaseWith(x => x.Super.Morph),
            CaseWith(x => x.Super.Grapple),
            CaseWith(x => x.Super.HiJump.Varia),
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
        };

        protected override List<Case> EnergyTankCrocomire => CaseWithNothing;

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

        protected override List<Case> PowerBombCrocomire => CaseWithNothing;

        protected override List<Case> MissileGrapplingBeam => new() {
            CaseWith(x => x.SpeedBooster),
            CaseWith(x => x.Morph.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.Grapple),
        };

        protected override List<Case> GrapplingBeam => new() {
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Morph),
            CaseWith(x => x.Grapple),
            CaseWith(x => x.HiJump.SpeedBooster),
        };

        #endregion

        #region Norfair Lower West

        protected override List<Case> NorfairLowerWest => new() {
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.HiJump),
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.Gravity),
        };

        protected override List<Case> MissileGoldTorizo => new() {
            CaseWith(x => x.Morph.PowerBomb.SpaceJump.Varia.HiJump),
            CaseWith(x => x.Morph.PowerBomb.SpaceJump.Varia.Gravity),
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
            CaseWith(x => x.Morph.Bombs.Varia),
            CaseWith(x => x.Morph.PowerBomb.Varia),
            CaseWith(x => x.ScrewAttack.Varia),
        };

        #endregion

        #region Norfair Lower East

        protected override List<Case> NorfairLowerEast => new() {
            CaseWith(x => x.Varia.Morph.Bombs.Super.PowerBomb.Gravity),
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.HiJump),
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.Gravity.SpaceJump),
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.Gravity.SpringBall),
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.Gravity.Ice.Charge),
        };

        protected override List<Case> MissileMickeyMouseRoom => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
            CaseWith(x => x.Morph.ScrewAttack),
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

        #endregion

    }

}
