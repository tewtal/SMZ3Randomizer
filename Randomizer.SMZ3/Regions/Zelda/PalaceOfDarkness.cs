using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class PalaceOfDarkness : Z3Region, IReward {

        public override string Name => "Palace of Darkness";
        public override string Area => "Dark Palace";

        public RewardType Reward { get; set; } = RewardType.None;

        public PalaceOfDarkness(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyPD, BigKeyPD, MapPD, CompassPD };

            Locations = new List<Location> {
                new Location(this, 256+121, 0x1EA5B, LocationType.Regular, "Palace of Darkness - Shooter Room"),
                new Location(this, 256+122, 0x1EA37, LocationType.Regular, "Palace of Darkness - Big Key Chest",
                    items => items.KeyPD >= (GetLocation("Palace of Darkness - Big Key Chest").ItemIs(KeyPD, World) ? 1 :
                        config.Keysanity || items.Hammer && items.Bow && items.Lamp ? 6 : 5))
                    .AlwaysAllow((item, items) => item.Is(KeyPD, World) && items.KeyPD >= 5),
                new Location(this, 256+123, 0x1EA49, LocationType.Regular, "Palace of Darkness - Stalfos Basement",
                    items => items.KeyPD >= 1 || items.Bow && items.Hammer),
                new Location(this, 256+124, 0x1EA3D, LocationType.Regular, "Palace of Darkness - The Arena - Bridge",
                    items => items.KeyPD >= 1 || items.Bow && items.Hammer),
                new Location(this, 256+125, 0x1EA3A, LocationType.Regular, "Palace of Darkness - The Arena - Ledge",
                    items => items.Bow),
                new Location(this, 256+126, 0x1EA52, LocationType.Regular, "Palace of Darkness - Map Chest",
                    items => items.Bow),
                new Location(this, 256+127, 0x1EA43, LocationType.Regular, "Palace of Darkness - Compass Chest",
                    items => items.KeyPD >= (config.Keysanity || items.Hammer && items.Bow && items.Lamp ? 4 : 3)),
                new Location(this, 256+128, 0x1EA46, LocationType.Regular, "Palace of Darkness - Harmless Hellway",
                    items => items.KeyPD >= (GetLocation("Palace of Darkness - Harmless Hellway").ItemIs(KeyPD, World) ?
                        config.Keysanity || items.Hammer && items.Bow && items.Lamp ? 4 : 3 :
                        config.Keysanity || items.Hammer && items.Bow && items.Lamp ? 6 : 5))
                    .AlwaysAllow((item, items) => item.Is(KeyPD, World) && items.KeyPD >= 5),
                new Location(this, 256+129, 0x1EA4C, LocationType.Regular, "Palace of Darkness - Dark Basement - Left",
                    items => items.Lamp && items.KeyPD >= (config.Keysanity || items.Hammer && items.Bow ? 4 : 3)),
                new Location(this, 256+130, 0x1EA4F, LocationType.Regular, "Palace of Darkness - Dark Basement - Right",
                    items => items.Lamp && items.KeyPD >= (config.Keysanity || items.Hammer && items.Bow ? 4 : 3)),
                new Location(this, 256+131, 0x1EA55, LocationType.Regular, "Palace of Darkness - Dark Maze - Top",
                    items => items.Lamp && items.KeyPD >= (config.Keysanity || items.Hammer && items.Bow ? 6 : 5)),
                new Location(this, 256+132, 0x1EA58, LocationType.Regular, "Palace of Darkness - Dark Maze - Bottom",
                    items => items.Lamp && items.KeyPD >= (config.Keysanity || items.Hammer && items.Bow ? 6 : 5)),
                new Location(this, 256+133, 0x1EA40, LocationType.Regular, "Palace of Darkness - Big Chest",
                    items => items.BigKeyPD && items.Lamp && items.KeyPD >= (config.Keysanity || items.Hammer && items.Bow ? 6 : 5)),
                new Location(this, 256+134, 0x308153, LocationType.Regular, "Palace of Darkness - Helmasaur King",
                    items => items.Lamp && items.Hammer && items.Bow && items.BigKeyPD && items.KeyPD >= 6),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && World.CanEnter("Dark World North East", items);
        }

        public bool CanComplete(Progression items) {
            return GetLocation("Palace of Darkness - Helmasaur King").Available(items);
        }

    }

}
