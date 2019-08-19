using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.Zelda.LightWorld {

    class South : Z3Region {

        public override string Name => "Light World South";
        public override string Area => "Light World";

        public South(World world, Config config) : base(world, config) {
            var sphereOne = -10;
            Locations = new List<Location> {
                new Location(this, 256+45, 0x180142, LocationType.Regular, "Maze Race").Weighted(sphereOne),
                new Location(this, 256+240, 0x180012, LocationType.Regular, "Library",
                    items => items.Boots),
                new Location(this, 256+241, 0x18014A, LocationType.Regular, "Flute Spot",
                    items => items.Shovel),
                new Location(this, 256+242, 0x180003, LocationType.Regular, "South of Grove",
                    items => items.Mirror && World.CanEnter("Dark World South", items)),
                new Location(this, 256+243, 0xE9BC, LocationType.Regular, "Link's House").Weighted(sphereOne),
                new Location(this, 256+244, 0xE9F2, LocationType.Regular, "Aginah's Cave").Weighted(sphereOne),
                new Location(this, 256+51, 0xEB42, LocationType.Regular, "Mini Moldorm Cave - Far Left").Weighted(sphereOne),
                new Location(this, 256+52, 0xEB45, LocationType.Regular, "Mini Moldorm Cave - Left").Weighted(sphereOne),
                new Location(this, 256+53, 0x180010, LocationType.Regular, "Mini Moldorm Cave - NPC").Weighted(sphereOne),
                new Location(this, 256+54, 0xEB48, LocationType.Regular, "Mini Moldorm Cave - Right").Weighted(sphereOne),
                new Location(this, 256+251, 0xEB4B, LocationType.Regular, "Mini Moldorm Cave - Far Right").Weighted(sphereOne),
                new Location(this, 256+252, 0x180143, LocationType.Regular, "Desert Ledge",
                    items => World.CanEnter("Desert Palace", items)),
                new Location(this, 256+253, 0x180005, LocationType.Regular, "Checkerboard Cave",
                    items => items.Mirror && (
                        items.Flute && items.CanLiftHeavy() ||
                        items.CanAccessMiseryMirePortal(Config)
                    ) && items.CanLiftLight()),
                new Location(this, 256+58, 0x180017, LocationType.Bombos, "Bombos Tablet",
                    items => items.Book && items.MasterSword && items.Mirror && World.CanEnter("Dark World South", items)),
                new Location(this, 256+59, 0xE98C, LocationType.Regular, "Floodgate Chest").Weighted(sphereOne),
                new Location(this, 256+60, 0x180145, LocationType.Regular, "Sunken Treasure").Weighted(sphereOne),
                new Location(this, 256+61, 0x180144, LocationType.Regular, "Lake Hylia Island",
                    items => items.Flippers && items.MoonPearl && items.Mirror && (
                        World.CanEnter("Dark World South", items) ||
                        World.CanEnter("Dark World North East", items))),
                new Location(this, 256+62, 0x33E7D, LocationType.Regular, "Hobo",
                    items => items.Flippers),
                new Location(this, 256+63, 0xEB4E, LocationType.Regular, "Ice Rod Cave").Weighted(sphereOne),
            };

        }

    }

}
