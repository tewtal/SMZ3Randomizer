using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;

namespace Randomizer.SuperMetroid.Regions.NorfairUpper {

    class East : Region {

        public override string Name => "Norfair Upper East";
        public override string Area => "Norfair Upper";

        public East(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 49, "Missile (lava room)", LocationType.Hidden, 0x78AE4, Logic switch {
                    _ => new Requirement(items => items.Has(Morph))
                }),
                new Location(this, 61, "Reserve Tank, Norfair", LocationType.Chozo, 0x78C3E, Logic switch {
                    Casual => items => items.Has(Morph) && (
                        items.CanFly() || items.Has(Grapple) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice)),
                    _ => new Requirement(items => items.Has(Morph) && items.Has(Super))
                }),
                new Location(this, 62, "Missile (Norfair Reserve Tank)", LocationType.Hidden, 0x78C44, Logic switch {
                    Casual => items => items.Has(Morph) && (
                        items.CanFly() || items.Has(Grapple) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice)),
                    _ => new Requirement(items => items.Has(Morph) && items.Has(Super))
                }),
                new Location(this, 63, "Missile (bubble Norfair green door)", LocationType.Visible, 0x78C52, Logic switch {
                    Casual => items => items.CanFly() ||
                        items.Has(Grapple) && items.Has(Morph) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice),
                    _ => new Requirement(items => items.Has(Super))
                }),
                new Location(this, 64, "Missile (bubble Norfair)", LocationType.Visible, 0x78C66),
                new Location(this, 65, "Missile (Speed Booster)", LocationType.Hidden, 0x78C74, Logic switch {
                    Casual => items => items.CanFly() || items.Has(Morph) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice),
                    _ => new Requirement(items => items.Has(Super))
                }),
                new Location(this, 66, "Speed Booster", LocationType.Chozo, 0x78C82, Logic switch {
                    Casual => items => items.CanFly() || items.Has(Morph) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice),
                    _ => new Requirement(items => items.Has(Super))
                }),
                new Location(this, 67, "Missile (Wave Beam)", LocationType.Visible, 0x78CBC, Logic switch {
                    Casual => items => items.CanFly() || items.Has(Morph) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice),
                    _ => new Requirement(items => true)
                }),
                new Location(this, 68, "Wave Beam", LocationType.Chozo, 0x78CCA, Logic switch {
                    Casual => items => items.Has(Morph) && (
                        items.CanFly() || items.Has(Morph) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice)),
                    _ => new Requirement(items => items.CanOpenRedDoors() &&
                        (items.Has(Morph) || items.Has(Grapple) || items.Has(HiJump) && items.Has(Varia) || items.Has(SpaceJump)))
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Logic switch {
                Casual => 
                    (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph) &&
                    items.Has(Varia) && items.Has(Super) &&
                    (items.CanFly() || items.Has(HiJump) || items.Has(SpeedBooster)),
                _ =>
                    // Norfair Main Street Access
                    (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph) &&
                    // Hell Run and Green Door
                    items.CanHellRun() && (
                        items.Has(Super) && (
                            // Cathedral Route
                            items.CanFly() || items.Has(HiJump) || items.Has(SpeedBooster) || items.CanSpringBallJump() ||
                            items.Has(Varia) && (items.Has(Ice) || items.Has(SpeedBooster))
                        ) ||
                        // Speedway route
                        items.Has(SpeedBooster) && items.CanUsePowerBombs())
            };
        }

    }

}
