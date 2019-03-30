using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class PalaceOfDarkness : Region {

        public override string Name => "Palace of Darkness";
        public override string Area => "Palace of Darkness";

        public PalaceOfDarkness(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 256+121, 0xEA5B, LocationType.Regular, "Palace of Darkness - Shooter Room"),
                new Location(this, 256+122, 0xEA37, LocationType.Regular, "Palace of Darkness - Big Key Chest",
                    items => items.Has(KeyPD, Locations.Get("Palace of Darkness - Big Key Chest").ItemType == KeyPD ? 1 :
                        items.Has(Hammer) && items.Has(Bow) && items.Has(Lamp) ? 6 : 5))
                    .AlwaysAllow((item, items) => item.Type == KeyPD && items.Has(KeyPD, 5)),
                new Location(this, 256+123, 0xEA49, LocationType.Regular, "Palace of Darkness - Stalfos Basement",
                    items => items.Has(KeyPD) || items.Has(Bow) && items.Has(Hammer)),
                new Location(this, 256+124, 0xEA3D, LocationType.Regular, "Palace of Darkness - The Arena - Bridge",
                    items => items.Has(KeyPD) || items.Has(Bow) && items.Has(Hammer)),
                new Location(this, 256+125, 0xEA3A, LocationType.Regular, "Palace of Darkness - The Arena - Ledge",
                    items => items.Has(Bow)),
                new Location(this, 256+126, 0xEA52, LocationType.Regular, "Palace of Darkness - Map Chest",
                    items => items.Has(Bow)),
                new Location(this, 256+127, 0xEA43, LocationType.Regular, "Palace of Darkness - Compass Chest",
                    items => items.Has(KeyPD, items.Has(Hammer) && items.Has(Bow) && items.Has(Lamp) ? 4 : 3)),
                new Location(this, 256+128, 0xEA46, LocationType.Regular, "Palace of Darkness - Harmless Hellway",
                    items => items.Has(KeyPD, Locations.Get("Palace of Darkness - Harmless Hellway").ItemType == KeyPD ?
                        items.Has(Hammer) && items.Has(Bow) && items.Has(Lamp) ? 4 : 3 :
                        items.Has(Hammer) && items.Has(Bow) && items.Has(Lamp) ? 6 : 5))
                    .AlwaysAllow((item, items) => item.Type == KeyPD && items.Has(KeyPD, 5)),
                new Location(this, 256+129, 0xEA4C, LocationType.Regular, "Palace of Darkness - Dark Basement - Left",
                    items => items.Has(Lamp) && items.Has(KeyPD, items.Has(Hammer) && items.Has(Bow) ? 4 : 3)),
                new Location(this, 256+130, 0xEA4F, LocationType.Regular, "Palace of Darkness - Dark Basement - Right",
                    items => items.Has(Lamp) && items.Has(KeyPD, items.Has(Hammer) && items.Has(Bow) ? 4 : 3)),
                new Location(this, 256+131, 0xEA55, LocationType.Regular, "Palace of Darkness - Dark Maze - Top",
                    items => items.Has(Lamp) && items.Has(KeyPD, items.Has(Hammer) && items.Has(Bow) ? 6 : 5)),
                new Location(this, 256+132, 0xEA58, LocationType.Regular, "Palace of Darkness - Dark Maze - Bottom",
                    items => items.Has(Lamp) && items.Has(KeyPD, items.Has(Hammer) && items.Has(Bow) ? 6 : 5)),
                new Location(this, 256+133, 0xEA40, LocationType.Regular, "Palace of Darkness - Big Chest",
                    items => items.Has(BigKeyPD) && items.Has(Lamp) && items.Has(KeyPD, items.Has(Hammer) && items.Has(Bow) ? 6 : 5)),
                new Location(this, 256+134, 0x180153, LocationType.Regular, "Palace of Darkness - Helmasaur King",
                    items => items.Has(Lamp) && items.Has(Hammer) && items.Has(Bow) && items.Has(BigKeyPD) && items.Has(KeyPD, 6)),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return items.Has(MoonPearl) && World.CanEnter("Dark World North East", items);
        }

    }

}
