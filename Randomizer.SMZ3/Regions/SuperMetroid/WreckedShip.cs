using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class WreckedShip : Region, Reward {

        public override string Name => "Wrecked Ship";
        public override string Area => "Wrecked Ship";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public WreckedShip(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 128, 0x7C265, LocationType.Visible, "Missile (Wrecked Ship middle)"),
                new Location(this, 129, 0x7C2E9, LocationType.Chozo, "Reserve Tank, Wrecked Ship", Config.Logic switch {
                    Casual => items => items.Has(SpeedBooster) && items.CanUsePowerBombs() &&
                        (items.Has(Grapple) || items.Has(SpaceJump) || items.Has(Varia) && items.HasEnergyReserves(2) || items.HasEnergyReserves(3)),
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Has(SpeedBooster) && (items.Has(Varia) || items.HasEnergyReserves(2)))
                }),
                new Location(this, 130, 0x7C2EF, LocationType.Visible, "Missile (Gravity Suit)", Config.Logic switch {
                    Casual => items => items.Has(Grapple) || items.Has(SpaceJump) || items.Has(Varia) && items.HasEnergyReserves(2) || items.HasEnergyReserves(3),
                    _ => new Requirement(items => items.Has(Varia) || items.HasEnergyReserves(1))
                }),
                new Location(this, 131, 0x7C319, LocationType.Visible, "Missile (Wrecked Ship top)"),
                new Location(this, 132, 0x7C337, LocationType.Visible, "Energy Tank, Wrecked Ship", Config.Logic switch {
                    Casual => items => items.Has(HiJump) || items.Has(SpaceJump) || items.Has(SpeedBooster) || items.Has(Gravity),
                    _ => new Requirement(items => items.Has(Bombs) || items.Has(PowerBomb) || items.CanSpringBallJump() ||
                        items.Has(HiJump) || items.Has(SpaceJump) || items.Has(SpeedBooster) || items.Has(Gravity))
                }),
                new Location(this, 133, 0x7C357, LocationType.Visible, "Super Missile (Wrecked Ship left)"),
                new Location(this, 134, 0x7C365, LocationType.Visible, "Right Super, Wrecked Ship"),
                new Location(this, 135, 0x7C36D, LocationType.Chozo, "Gravity Suit", Config.Logic switch {
                    Casual => items => items.Has(Grapple) || items.Has(SpaceJump) || items.Has(Varia) && items.HasEnergyReserves(2) || items.HasEnergyReserves(3),
                    _ => new Requirement(items => items.Has(Varia) || items.HasEnergyReserves(1))
                })
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Config.Logic switch {
                Casual =>
                    items.Has(Super) && (
                        items.CanUsePowerBombs() && (items.Has(SpeedBooster) || items.Has(Grapple) || items.Has(SpaceJump) ||
                            items.Has(Gravity) && (items.CanFly() || items.Has(HiJump))) ||
                        items.CanAccessMaridiaPortal(World) && items.Has(Gravity) && items.CanPassBombPassages()
                    ),
                _ =>
                    items.Has(Super) && (
                        items.CanUsePowerBombs() ||
                        items.CanAccessMaridiaPortal(World) && (items.Has(HiJump) || items.Has(Gravity)) && items.CanPassBombPassages()
                    )
            };
        }

        public bool CanComplete(List<Item> items) {
            return CanEnter(items);
        }

    }

}
