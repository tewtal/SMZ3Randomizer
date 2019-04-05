using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda.LightWorld {

    class South : Region {

        public override string Name => "Light World South";
        public override string Area => "Light World";

        public South(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+45, 0x180142, LocationType.Regular, "Maze Race"),
                new Location(this, 256+46, 0x180012, LocationType.Regular, "Library",
                    items => items.Has(Boots)),
                new Location(this, 256+47, 0x18014A, LocationType.Regular, "Flute Spot",
                    items => items.Has(Shovel)),
                new Location(this, 256+48, 0x180003, LocationType.Regular, "South of Grove",
                    items => items.Has(Mirror) && World.CanEnter("Dark World South", items)),
                new Location(this, 256+49, 0xE9BC, LocationType.Regular, "Link's House"),
                new Location(this, 256+50, 0xE9F2, LocationType.Regular, "Aginah's Cave"),
                new Location(this, 256+51, 0xEB42, LocationType.Regular, "Mini Moldorm Cave - Far Left"),
                new Location(this, 256+52, 0xEB45, LocationType.Regular, "Mini Moldorm Cave - Left"),
                new Location(this, 256+53, 0x180010, LocationType.Regular, "Mini Moldorm Cave - NPC"),
                new Location(this, 256+54, 0xEB48, LocationType.Regular, "Mini Moldorm Cave - Right"),
                new Location(this, 256+55, 0xEB4B, LocationType.Regular, "Mini Moldorm Cave - Far Right"),
                new Location(this, 256+56, 0x180143, LocationType.Regular, "Desert Ledge",
                    items => World.CanEnter("Desert Palace", items)),
                new Location(this, 256+57, 0x180005, LocationType.Regular, "Checkerboard Cave",
                    items => items.Has(Mirror) && (
                        items.Has(Flute) && items.CanLiftHeavy() ||
                        items.CanAccessMiseryMirePortal(Config)
                    ) && items.CanLiftLight()),
                new Location(this, 256+58, 0x180017, LocationType.Bombos, "Bombos Tablet",
                    items => items.Has(Book) && items.HasMasterSword() && items.Has(Mirror) && World.CanEnter("Dark World South", items)),
                new Location(this, 256+59, 0xE98C, LocationType.Regular, "Floodgate Chest"),
                new Location(this, 256+60, 0x180145, LocationType.Regular, "Sunken Treasure"),
                new Location(this, 256+61, 0x180144, LocationType.Regular, "Lake Hylia Island",
                    items => items.Has(Flippers) && items.Has(MoonPearl) && items.Has(Mirror) && (
                        World.CanEnter("Dark World South", items) ||
                        World.CanEnter("Dark World North East", items))),
                new Location(this, 256+62, 0x33E7D, LocationType.Regular, "Hobo",
                    items => items.Has(Flippers)),
                new Location(this, 256+63, 0xEB4E, LocationType.Regular, "Ice Rod Cave"),
            };

        }

    }

}
