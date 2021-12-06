using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairLower {

    class East : SMRegion, IReward {

        public override string Name => "Norfair Lower East";
        public override string Area => "Norfair Lower";

        public RewardType Reward { get; set; } = RewardType.None;

        public East(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 74, 0x8F8FCA, LocationType.Visible, "Missile (lower Norfair above fire flea room)", Logic switch {
                    _ => new Requirement(items => CanExit(items))
                }),
                new Location(this, 75, 0x8F8FD2, LocationType.Visible, "Power Bomb (lower Norfair above fire flea room)", Logic switch {
                    Normal => new Requirement(items => CanExit(items)),
                    _ => items => CanExit(items) && items.CanPassBombPassages()
                }),
                new Location(this, 76, 0x8F90C0, LocationType.Visible, "Power Bomb (Power Bombs of shame)", Logic switch {
                    _ => new Requirement(items => CanExit(items) && items.CanUsePowerBombs())
                }),
                new Location(this, 77, 0x8F9100, LocationType.Visible, "Missile (lower Norfair near Wave Beam)", Logic switch {
                    Normal => new Requirement(items => CanExit(items)),
                    _ => items => CanExit(items) && items.Morph && items.CanDestroyBombWalls()
                }),
                new Location(this, 78, 0x8F9108, LocationType.Hidden, "Energy Tank, Ridley", Logic switch {
                    _ => new Requirement(items => CanExit(items) && items.CardLowerNorfairBoss && items.CanUsePowerBombs() && items.Super)
                }),
                new Location(this, 80, 0x8F9184, LocationType.Visible, "Energy Tank, Firefleas", Logic switch {
                    _ => new Requirement(items => CanExit(items))
                })
            };
        }

        bool CanExit(Progression items) {
            return Logic switch {
                Normal => /* Intended LN Escape */
                    items.Morph && (
                        items.CardNorfairL2 /* Bubble Mountain */ ||
                        items.Gravity && items.Wave /* Volcano Room, Blue Gate */ &&
                            (items.Grapple || items.SpaceJump) /* Spikey Acid Snakes -> Croc Escape (this shortcuts Frog Speedway) */
                    ),
                _ => /* Intended LN Escape */
                    items.Morph && (
                        items.CardNorfairL2 /* Bubble Mountain */ ||
                        (items.Missile || items.Super || items.Wave /* Blue Gate */) && (
                            items.SpeedBooster || items.CanFly() || items.Grapple ||
                            items.HiJump && (items.CanSpringBallJump() || items.Ice) /* Frog Speedway / Croc Escape */
                        )
                    ) ||
                    /* Reverse Amphitheater */
                    items.HasEnergyReserves(5),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Normal =>
                    items.Varia && items.CardLowerNorfairL1 && (
                        World.CanEnter("Norfair Upper East", items) && items.CanUsePowerBombs() && items.SpaceJump && items.Gravity ||
                        items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls() && items.Super && items.CanUsePowerBombs() && items.CanFly()
                    ),
                _ =>
                    items.Varia && items.CardLowerNorfairL1 && (
                        World.CanEnter("Norfair Upper East", items) && items.CanUsePowerBombs() && (items.HiJump || items.Gravity) ||
                        items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls() && items.Super && (items.CanFly() || items.CanSpringBallJump() || items.SpeedBooster)
                    ) &&
                    (items.CanFly() || items.HiJump || items.CanSpringBallJump() || items.Ice && items.Charge) &&
                    (items.CanPassBombPassages() || items.ScrewAttack && items.SpaceJump)
            };
        }

        public bool CanComplete(Progression items) {
            return GetLocation("Energy Tank, Ridley").Available(items);
        }

    }

}
