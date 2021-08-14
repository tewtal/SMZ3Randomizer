using System.Collections.Generic;

namespace Randomizer.SuperMetroid.Tests.Logic {
    
    public class CasualCases : LogicCases {

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
            CaseWith(x => x.Morph.Bombs.Missile),
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.Morph.PowerBomb.Missile),
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        #endregion

        #region Crateria East

        protected override List<Case> CrateriaEast => new() {
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        protected override List<Case> MissileOutsideWreckedShipBottom => new() {
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.SpeedBooster),
            CaseWith(x => x.Grapple),
            CaseWith(x => x.Gravity.Morph.Bombs),
            CaseWith(x => x.Gravity.HiJump),
        };

        protected override List<Case> MissilesOutsideWreckedShipTopHalf => new() {
            CaseWith(x => x.Super.Morph.Bombs.SpaceJump),
            CaseWith(x => x.Super.Morph.Bombs.SpeedBooster),
            CaseWith(x => x.Super.Morph.Bombs.Grapple),
            CaseWith(x => x.Super.Morph.Bombs.Gravity),
            CaseWith(x => x.Super.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.Super.Morph.PowerBomb.SpeedBooster),
            CaseWith(x => x.Super.Morph.PowerBomb.Grapple),
            CaseWith(x => x.Super.Morph.PowerBomb.Gravity.HiJump),
        };

        protected override List<Case> MissileCrateriaMoat => CaseWithNothing;

        #endregion

        #region Wrecked Ship

        protected override List<Case> WreckedShip => new() {
            CaseWith(x => x.Super.Morph.PowerBomb.SpeedBooster),
            CaseWith(x => x.Super.Morph.PowerBomb.Grapple),
            CaseWith(x => x.Super.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.Super.Morph.PowerBomb.Gravity.Bombs),
            CaseWith(x => x.Super.Morph.PowerBomb.Gravity.HiJump),
        };

        protected override List<Case> MissileWreckedShipMiddle => CaseWithNothing;

        protected override List<Case> ReserveTankWreckedShip => new() {
            CaseWith(x => x.SpeedBooster.Morph.PowerBomb.Grapple),
            CaseWith(x => x.SpeedBooster.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.SpeedBooster.Morph.PowerBomb.Varia.ETank(2)),
            CaseWith(x => x.SpeedBooster.Morph.PowerBomb.ETank(3)),
        };

        protected override List<Case> MissileGravitySuit => new() {
            CaseWith(x => x.Grapple),
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Varia.ETank(2)),
            CaseWith(x => x.ETank(3)),
        };

        protected override List<Case> MissileWreckedShipTop => CaseWithNothing;

        protected override List<Case> EnergyTankWreckedShip => new() {
            CaseWith(x => x.HiJump),
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.SpeedBooster),
            CaseWith(x => x.Gravity),
        };

        protected override List<Case> SuperMissileWreckedShipLeft => CaseWithNothing;
        protected override List<Case> SuperMissileWreckedShipRight => CaseWithNothing;

        protected override List<Case> GravitySuit => new() {
            CaseWith(x => x.Grapple),
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Varia.ETank(2)),
            CaseWith(x => x.ETank(3)),
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
            CaseWith(x => x.Missile),
            CaseWith(x => x.Super),
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
            CaseWith(x => x.Morph.Super),
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
            CaseWith(x => x.Morph.PowerBomb.Super.Gravity),
        };

        protected override List<Case> MissileGreenMaridiaShinespark => new() {
            CaseWith(x => x.SpeedBooster),
        };

        protected override List<Case> SuperMissileGreenMaridia => CaseWithNothing;

        protected override List<Case> EnergyTankMamaTurtle => new() {
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.SpeedBooster),
            CaseWith(x => x.Grapple),
        };

        protected override List<Case> MissileGreenMaridiaTatori => CaseWithNothing;

        #endregion

        #region Maridia Inner

        protected override List<Case> MaridiaInner => new() {
            CaseWith(x => x.Morph.Bombs.Super.PowerBomb.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.SpaceJump.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.SpeedBooster.Gravity),
            CaseWith(x => x.Morph.PowerBomb.Super.Grapple.Gravity),
        };

        protected override List<Case> YellowMaridiaWateringHole => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
        };
        protected override List<Case> MissileYellowMaridiaFalseWall => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> PlasmaBeam => new() {
            CaseWith(x => x.SpeedBooster.Gravity.HiJump.ScrewAttack),
            CaseWith(x => x.SpeedBooster.Gravity.HiJump.Plasma),
            CaseWith(x => x.SpeedBooster.Gravity.SpaceJump.ScrewAttack),
            CaseWith(x => x.SpeedBooster.Gravity.SpaceJump.Plasma),
            CaseWith(x => x.SpeedBooster.Gravity.Morph.Bombs.ScrewAttack),
            CaseWith(x => x.SpeedBooster.Gravity.Morph.Bombs.Plasma),
        };

        protected override List<Case> LeftMaridiaSandPitRoom => new() {
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> MissileRightMaridiaSandPitRoom => CaseWithNothing;
        protected override List<Case> PowerBombRightMaridiaSandPitRoom => CaseWithNothing;

        protected override List<Case> PinkMaridia => new() {
            CaseWith(x => x.SpeedBooster),
        };

        protected override List<Case> SpringBall => new() {
            CaseWith(x => x.Grapple.Morph.PowerBomb.SpaceJump),
            CaseWith(x => x.Grapple.Morph.PowerBomb.HiJump),
        };

        protected override List<Case> MissileDraygon => new() {
            CaseWith(x => x.SpeedBooster),
        };

        protected override List<Case> EnergyTankBotwoon => new() {
            CaseWith(x => x.SpeedBooster.Gravity),
            CaseWith(x => x.SpeedBooster.Ice),
        };

        protected override List<Case> SpaceJump => new() {
            CaseWith(x => x.SpeedBooster.Gravity.HiJump),
            CaseWith(x => x.SpeedBooster.Gravity.SpaceJump),
            CaseWith(x => x.SpeedBooster.Gravity.Morph.Bombs),
        };

        #endregion

        #region Norfair Upper West

        protected override List<Case> NorfairUpperWest => new() {
            CaseWith(x => x.Morph.Bombs.Super),
            CaseWith(x => x.Morph.PowerBomb.Super),
            CaseWith(x => x.ScrewAttack.Super.Morph),
            CaseWith(x => x.SpeedBooster.Super.Morph),
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
            CaseWith(x => x.Missile),
            CaseWith(x => x.Super),
        };

        protected override List<Case> EnergyTankHiJumpBoots => new() {
            CaseWith(x => x.Missile),
            CaseWith(x => x.Super),
        };

        #endregion

        #region Norfair Upper East

        protected override List<Case> NorfairUpperEast => new() {
            CaseWith(x => x.Morph.Bombs.Super.Morph.Varia),
            CaseWith(x => x.Morph.PowerBomb.Super.Morph.Varia),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia),
        };

        protected override List<Case> MissileLavaRoom => new() {
            CaseWith(x => x.Morph),
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
            CaseWith(x => x.Morph.Bombs.Super.Varia.Wave),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.SpaceJump.Wave),
            CaseWith(x => x.Morph.PowerBomb.Super.Varia.HiJump.Wave),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.SpaceJump.Wave),
            CaseWith(x => x.ScrewAttack.Super.Morph.Varia.HiJump.Wave),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia.PowerBomb),
            CaseWith(x => x.SpeedBooster.Super.Morph.Varia.Wave),
        };

        protected override List<Case> EnergyTankCrocomire => new() {
            CaseWith(x => x.ETank(1)),
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Grapple),
        };

        protected override List<Case> MissileAboveCrocomire => new() {
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Grapple),
            CaseWith(x => x.HiJump.SpeedBooster),
        };

        protected override List<Case> PowerBombCrocomire => new() {
            CaseWith(x => x.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.HiJump),
            CaseWith(x => x.Grapple),
        };

        protected override List<Case> MissileBelowCrocomire => new() {
            CaseWith(x => x.Morph),
        };

        protected override List<Case> MissileGrapplingBeam => new() {
            CaseWith(x => x.Morph.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.SpeedBooster.PowerBomb),
        };
        protected override List<Case> GrapplingBeam => new() {
            CaseWith(x => x.Morph.SpaceJump),
            CaseWith(x => x.Morph.Bombs),
            CaseWith(x => x.Morph.SpeedBooster.PowerBomb),
        };

        #endregion

        #region Norfair Lower West

        protected override List<Case> NorfairLowerWest => new() {
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.SpaceJump.Gravity),
        };

        protected override List<Case> MissileGoldTorizo => new() {
            CaseWith(x => x.Morph.PowerBomb.SpaceJump.Super),
        };

        protected override List<Case> SuperMissileGoldTorizo => new() {
            CaseWith(x => x.Morph.PowerBomb.Super.SpaceJump),
            CaseWith(x => x.Morph.PowerBomb.Charge.SpaceJump),
        };

        protected override List<Case> ScrewAttack => new() {
            CaseWith(x => x.Morph.PowerBomb.SpaceJump),
        };

        #endregion

        #region Norfair Lower East

        protected override List<Case> NorfairLowerEast => new() {
            CaseWith(x => x.Varia.Morph.PowerBomb.Super.SpaceJump.Gravity),
        };

        protected override List<Case> MissileMickeyMouseRoom => CaseWithNothing;

        protected override List<Case> MissileLowerNorfairAboveFireFleaRoom => CaseWithNothing;
        protected override List<Case> PowerBombLowerNorfairAboveFireFleaRoom => CaseWithNothing;

        protected override List<Case> PowerBombPowerBombsOfShame => new() {
            CaseWith(x => x.Morph.PowerBomb),
        };

        protected override List<Case> MissileLowerNorfairNearWaveBeam => CaseWithNothing;

        protected override List<Case> EnergyTankRidley => new() {
            CaseWith(x => x.Morph.PowerBomb.Super),
        };

        protected override List<Case> EnergyTankFirefleas => CaseWithNothing;

        #endregion

    }

}
