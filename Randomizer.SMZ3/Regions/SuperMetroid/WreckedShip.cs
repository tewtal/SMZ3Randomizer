using System.Collections.Generic;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class WreckedShip : SMRegion, IReward {

        public override string Name => "Wrecked Ship";
        public override string Area => "Wrecked Ship";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public WreckedShip(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 128, 0xC7C265, LocationType.Visible, "Missile (Wrecked Ship middle)"),
                new Location(this, 129, 0xC7C2E9, LocationType.Chozo, "Reserve Tank, Wrecked Ship", Config.Logic switch {
                    Casual => items => CanUnlockShip(items) && items.SpeedBooster && items.CanUsePowerBombs() &&
                        (items.Grapple || items.SpaceJump || items.Varia && items.HasEnergyReserves(2) || items.HasEnergyReserves(3)),
                    _ => new Requirement(items => CanUnlockShip(items) && items.CanUsePowerBombs() && items.SpeedBooster &&
                        (items.Varia || items.HasEnergyReserves(2)))
                }),
                new Location(this, 130, 0xC7C2EF, LocationType.Visible, "Missile (Gravity Suit)", Config.Logic switch {
                    Casual => items => CanUnlockShip(items) &&
                        (items.Grapple || items.SpaceJump || items.Varia && items.HasEnergyReserves(2) || items.HasEnergyReserves(3)),
                    _ => new Requirement(items => CanUnlockShip(items) && (items.Varia || items.HasEnergyReserves(1)))
                }),
                new Location(this, 131, 0xC7C319, LocationType.Visible, "Missile (Wrecked Ship top)",
                    items => CanUnlockShip(items)),
                new Location(this, 132, 0xC7C337, LocationType.Visible, "Energy Tank, Wrecked Ship", Config.Logic switch {
                    Casual => items => CanUnlockShip(items) &&
                        (items.HiJump || items.SpaceJump || items.SpeedBooster || items.Gravity),
                    _ => new Requirement(items => CanUnlockShip(items) && (items.Bombs || items.PowerBomb || items.CanSpringBallJump() ||
                        items.HiJump || items.SpaceJump || items.SpeedBooster || items.Gravity))
                }),
                new Location(this, 133, 0xC7C357, LocationType.Visible, "Super Missile (Wrecked Ship left)",
                    items => CanUnlockShip(items)),
                new Location(this, 134, 0xC7C365, LocationType.Visible, "Right Super, Wrecked Ship",
                    items => CanUnlockShip(items)),
                new Location(this, 135, 0xC7C36D, LocationType.Chozo, "Gravity Suit", Config.Logic switch {
                    Casual => items => CanUnlockShip(items) &&
                        (items.Grapple || items.SpaceJump || items.Varia && items.HasEnergyReserves(2) || items.HasEnergyReserves(3)),
                    _ => new Requirement(items => CanUnlockShip(items) && (items.Varia || items.HasEnergyReserves(1)))
                })
            };
        }

        bool CanUnlockShip(Progression items) {
            return !Config.Keysanity || items.PhantoonKey;
        }

        public override bool CanEnter(Progression items) {
            return Config.Logic switch {
                Casual =>
                    items.Super && (
                        items.CanUsePowerBombs() && (items.SpeedBooster || items.Grapple || items.SpaceJump ||
                            items.Gravity && (items.CanFly() || items.HiJump)) ||
                        items.CanAccessMaridiaPortal(World) && items.Gravity && items.CanPassBombPassages()),
                _ =>
                    items.Super && (
                        items.CanUsePowerBombs() ||
                        items.CanAccessMaridiaPortal(World) && (items.HiJump || items.Gravity) && items.CanPassBombPassages())
            };
        }

        public bool CanComplete(Progression items) {
            return CanEnter(items) && CanUnlockShip(items);
        }

    }

}
