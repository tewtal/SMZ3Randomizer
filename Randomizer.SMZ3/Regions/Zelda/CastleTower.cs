using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class CastleTower : Region, Reward {

        public override string Name => "Castle Tower";
        public override string Area => "Castle Tower";

        public RewardType Reward { get; set; } = RewardType.Agahnim;

        public CastleTower(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyCT };

            Locations = new List<Location> {
                new Location(this, 256+101, 0xEAB5, LocationType.Regular, "Castle Tower - Foyer"),
                new Location(this, 256+102, 0xEAB2, LocationType.Regular, "Castle Tower - Dark Maze",
                    items => items.Has(Lamp) && items.Has(KeyCT)),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return items.Has(Cape) || items.HasMasterSword();
        }

        public bool CanComplete(List<Item> items) {
            return CanEnter(items) && items.Has(Lamp) && items.Has(KeyCT, 2) && items.HasSword();
        }

    }

}
