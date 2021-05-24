using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class WreckedShip : SMRegion, IReward {

        public override string Name => "Wrecked Ship";
        public override string Area => "Wrecked Ship";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public WreckedShip(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 128, 0x8FC265, LocationType.Visible, "Missile (Wrecked Ship middle)", Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages())
                }),
                new Location(this, 129, 0x8FC2E9, LocationType.Chozo, "Reserve Tank, Wrecked Ship", Logic switch {
                    Normal => items => CanUnlockShip(items) && items.CardWreckedShipL1 && items.CanUsePowerBombs() && items.SpeedBooster &&
                        (items.Grapple || items.SpaceJump || items.Varia && items.HasEnergyReserves(2) || items.HasEnergyReserves(3)),
                    _ => new Requirement(items => CanUnlockShip(items) && items.CardWreckedShipL1 && items.CanUsePowerBombs() && items.SpeedBooster &&
                        (items.Varia || items.HasEnergyReserves(2)))
                }),
                new Location(this, 130, 0x8FC2EF, LocationType.Visible, "Missile (Gravity Suit)", Logic switch {
                    Normal => items => CanUnlockShip(items) && items.CardWreckedShipL1 &&
                        (items.Grapple || items.SpaceJump || items.Varia && items.HasEnergyReserves(2) || items.HasEnergyReserves(3)),
                    _ => new Requirement(items => CanUnlockShip(items) && items.CardWreckedShipL1 && (items.Varia || items.HasEnergyReserves(1)))
                }),
                new Location(this, 131, 0x8FC319, LocationType.Visible, "Missile (Wrecked Ship top)",
                    items => CanUnlockShip(items)),
                new Location(this, 132, 0x8FC337, LocationType.Visible, "Energy Tank, Wrecked Ship", Logic switch {
                    Normal => items => CanUnlockShip(items) &&
                        (items.HiJump || items.SpaceJump || items.SpeedBooster || items.Gravity),
                    _ => new Requirement(items => CanUnlockShip(items) && (
                        items.Morph && (items.Bombs || items.PowerBomb) /* "OnceBJ" */ || items.CanSpringBallJump() ||
                        items.HiJump || items.SpaceJump || items.SpeedBooster || items.Gravity
                    )),
                }),
                new Location(this, 133, 0x8FC357, LocationType.Visible, "Super Missile (Wrecked Ship left)",
                    items => CanUnlockShip(items)),
                new Location(this, 134, 0x8FC365, LocationType.Visible, "Right Super, Wrecked Ship",
                    items => CanUnlockShip(items)),
                new Location(this, 135, 0x8FC36D, LocationType.Chozo, "Gravity Suit", Logic switch {
                    Normal => items => CanUnlockShip(items) && items.CardWreckedShipL1 &&
                        (items.Grapple || items.SpaceJump || items.Varia && items.HasEnergyReserves(2) || items.HasEnergyReserves(3)),
                    _ => new Requirement(items => CanUnlockShip(items) && items.CardWreckedShipL1 && (items.Varia || items.HasEnergyReserves(1)))
                })
            };
        }

        bool CanUnlockShip(Progression items) {
            return items.CardWreckedShipBoss && items.CanPassBombPassages();
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Normal =>
                    items.Super && (
                        /* Over the Moat */
                        (Config.Keysanity ? items.CardCrateriaL2 : items.CanUsePowerBombs()) && (
                            items.SpeedBooster || items.Grapple || items.SpaceJump ||
                            items.Gravity && (items.CanIbj() || items.HiJump)
                        ) ||
                        /* Through Maridia -> Forgotten Highway */
                        items.CanUsePowerBombs() && items.Gravity ||
                        /* From Maridia portal -> Forgotten Highway */
                        items.CanAccessMaridiaPortal(World) && items.Gravity && (
                            items.CanDestroyBombWalls() && items.CardMaridiaL2 ||
                            World.GetLocation("Space Jump").Available(items)
                        )
                    ),
                _ =>
                    items.Super && (
                        /* Over the Moat */
                        (Config.Keysanity ? items.CardCrateriaL2 : items.CanUsePowerBombs()) ||
                        /* Through Maridia -> Forgotten Highway */
                        items.CanUsePowerBombs() && (
                            items.Gravity ||
                            /* Climb Mt. Everest */
                            items.HiJump && (items.Ice || items.CanSpringBallJump()) && items.Grapple && items.CardMaridiaL1
                        ) ||
                        /* From Maridia portal -> Forgotten Highway */
                        items.CanAccessMaridiaPortal(World) && ( 
                            items.HiJump && items.CanPassBombPassages() && items.CardMaridiaL2 ||
                            items.Gravity && (
                                items.CanDestroyBombWalls() && items.CardMaridiaL2 ||
                                World.GetLocation("Space Jump").Available(items)
                            )
                        )
                    ),
            };
        }

        public bool CanComplete(Progression items) {
            return CanEnter(items) && CanUnlockShip(items);
        }

    }

}
