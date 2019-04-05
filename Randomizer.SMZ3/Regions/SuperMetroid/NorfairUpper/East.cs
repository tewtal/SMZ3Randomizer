using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairUpper {

    class East : Region {

        public override string Name => "Norfair Upper East";
        public override string Area => "Norfair Upper";

        public East(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 49, 0x78AE4, LocationType.Hidden, "Missile (lava room)", Config.Logic switch {
                    _ => new Requirement(items => items.Has(Morph))
                }),
                new Location(this, 61, 0x78C3E, LocationType.Chozo, "Reserve Tank, Norfair", Config.Logic switch {
                    Casual => items => items.Has(Morph) && (
                        items.CanFly() || items.Has(Grapple) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice)),
                    _ => new Requirement(items => items.Has(Morph) && items.Has(Super))
                }),
                new Location(this, 62, 0x78C44, LocationType.Hidden, "Missile (Norfair Reserve Tank)", Config.Logic switch {
                    Casual => items => items.Has(Morph) && (
                        items.CanFly() || items.Has(Grapple) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice)),
                    _ => new Requirement(items => items.Has(Morph) && items.Has(Super))
                }),
                new Location(this, 63, 0x78C52, LocationType.Visible, "Missile (bubble Norfair green door)", Config.Logic switch {
                    Casual => items => items.CanFly() ||
                        items.Has(Grapple) && items.Has(Morph) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice),
                    _ => new Requirement(items => items.Has(Super))
                }),
                new Location(this, 64, 0x78C66, LocationType.Visible, "Missile (bubble Norfair)"),
                new Location(this, 65, 0x78C74, LocationType.Hidden, "Missile (Speed Booster)", Config.Logic switch {
                    Casual => items => items.CanFly() || items.Has(Morph) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice),
                    _ => new Requirement(items => items.Has(Super))
                }),
                new Location(this, 66, 0x78C82, LocationType.Chozo, "Speed Booster", Config.Logic switch {
                    Casual => items => items.CanFly() || items.Has(Morph) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice),
                    _ => new Requirement(items => items.Has(Super))
                }),
                new Location(this, 67, 0x78CBC, LocationType.Visible, "Missile (Wave Beam)", Config.Logic switch {
                    Casual => items => items.CanFly() || items.Has(Morph) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice),
                    _ => new Requirement(items => true)
                }),
                new Location(this, 68, 0x78CCA, LocationType.Chozo, "Wave Beam", Config.Logic switch {
                    Casual => items => items.Has(Morph) && (
                        items.CanFly() || items.Has(Morph) && (items.Has(SpeedBooster) || items.CanPassBombPassages()) ||
                        items.Has(HiJump) || items.Has(Ice)),
                    _ => new Requirement(items => items.CanOpenRedDoors() &&
                        (items.Has(Morph) || items.Has(Grapple) || items.Has(HiJump) && items.Has(Varia) || items.Has(SpaceJump)))
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Config.Logic switch {
                Casual => (
                        (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph) ||
                        items.CanAccessNorfairUpperPortal()
                    ) &&
                    items.Has(Varia) && items.Has(Super) &&
                    (items.CanFly() || items.Has(HiJump) || items.Has(SpeedBooster)),
                _ => (
                        // Norfair Main Street Access
                        (items.CanDestroyBombWalls() || items.Has(SpeedBooster)) && items.Has(Super) && items.Has(Morph) ||
                        items.CanAccessNorfairUpperPortal()
                    ) &&
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
