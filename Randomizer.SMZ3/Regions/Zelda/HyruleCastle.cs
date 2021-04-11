using System.Collections.Generic;
using static Randomizer.SMZ3.Z3Logic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class HyruleCastle : Z3Region {

        public override string Name => "Hyrule Castle";

        public HyruleCastle(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyHC, MapHC };

            var sphereOne = -10;
            Locations = new List<Location> {
                new Location(this, 256+91, 0x1EA79, LocationType.Regular, "Sanctuary").Weighted(sphereOne),
                new Location(this, 256+92, 0x1EB5D, LocationType.Regular, "Sewers - Secret Room - Left", Logic switch {
                    Normal => items => items.CanLiftLight() || items.Lamp && items.KeyHC,
                    _ => new Requirement(items => items.CanLiftLight() || ((items.Lamp || items.Firerod) && items.KeyHC)),
                }),
                new Location(this, 256+93, 0x1EB60, LocationType.Regular, "Sewers - Secret Room - Middle", Logic switch {
                    Normal => items => items.CanLiftLight() || items.Lamp && items.KeyHC,
                    _ => new Requirement(items => items.CanLiftLight() || ((items.Lamp || items.Firerod) && items.KeyHC)),
                }),
                new Location(this, 256+94, 0x1EB63, LocationType.Regular, "Sewers - Secret Room - Right", Logic switch {
                    Normal => items => items.CanLiftLight() || items.Lamp && items.KeyHC,
                    _ => new Requirement(items => items.CanLiftLight() || ((items.Lamp || items.Firerod) && items.KeyHC)),
                }),
                new Location(this, 256+95, 0x1E96E, LocationType.Regular, "Sewers - Dark Cross", Logic switch {
                    Normal => items => items.Lamp,
                    _ => new Requirement(items => items.Lamp || items.Firerod || items.Sword)
                }),
                new Location(this, 256+96, 0x1EB0C, LocationType.Regular, "Hyrule Castle - Map Chest").Weighted(sphereOne),
                new Location(this, 256+97, 0x1E974, LocationType.Regular, "Hyrule Castle - Boomerang Chest",
                    items => items.KeyHC).Weighted(sphereOne),
                new Location(this, 256+98, 0x1EB09, LocationType.Regular, "Hyrule Castle - Zelda's Cell",
                    items => items.KeyHC).Weighted(sphereOne),
                new Location(this, 256+99, 0x5DF45, LocationType.NotInDungeon, "Link's Uncle")
                    .Allow((item, items) => Config.Keysanity || !item.IsDungeonItem).Weighted(sphereOne),
                new Location(this, 256+100, 0x1E971, LocationType.NotInDungeon, "Secret Passage")
                    .Allow((item, items) => Config.Keysanity || !item.IsDungeonItem).Weighted(sphereOne),
            };
        }

    }

}
