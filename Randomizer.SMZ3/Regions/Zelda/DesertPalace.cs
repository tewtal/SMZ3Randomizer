using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class DesertPalace : Region {

        public override string Name => "Desert Palace";
        public override string Area => "Desert Palace";

        public DesertPalace(World world, Logic logic) : base(world, logic) {
            RegionItems = new[] { KeyDP, BigKeyDP, MapDP, CompassDP };

            Locations = new List<Location> {
                new Location(this, 256+109, 0xE98F, LocationType.Regular, "Desert Palace - Big Chest",
                    items => items.Has(BigKeyDP)),
                new Location(this, 256+110, 0x180160, LocationType.Regular, "Desert Palace - Torch",
                    items => items.Has(Boots)),
                new Location(this, 256+111, 0xE9B6, LocationType.Regular, "Desert Palace - Map Chest"),
                new Location(this, 256+112, 0xE9C2, LocationType.Regular, "Desert Palace - Big Key Chest",
                    items => items.Has(KeyDP)),
                new Location(this, 256+113, 0xE9CB, LocationType.Regular, "Desert Palace - Compass Chest",
                    items => items.Has(KeyDP)),
                new Location(this, 256+114, 0x180151, LocationType.Regular, "Desert Palace - Lanmolas",
                    items => (
                        items.CanLiftLight() ||
                        items.CanAccessMiseryMirePortal(Logic) && items.Has(Mirror)
                    ) && items.Has(BigKeyDP) && items.Has(KeyDP) && items.CanLightTorches() && CanBeatBoss(items)),
            };
        }

        static bool CanBeatBoss(List<Item> items) {
            return items.HasSword() || items.Has(Hammer) || items.Has(Bow) ||
                items.Has(Firerod) || items.Has(Icerod) ||
                items.Has(Byrna) || items.Has(Somaria);
        }

        public override bool CanEnter(List<Item> items) {
            return items.Has(Book) ||
                items.Has(Mirror) && items.CanLiftHeavy() && items.Has(Flute) ||
                items.CanAccessMiseryMirePortal(Logic) && items.Has(Mirror);
        }

    }

}
