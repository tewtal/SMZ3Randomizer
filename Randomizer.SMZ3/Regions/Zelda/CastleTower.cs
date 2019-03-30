using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class CastleTower : Region {

        public override string Name => "Castle Tower";
        public override string Area => "Castle Tower";

        public CastleTower(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 256+101, 0xEAB5, LocationType.Regular, "Castle Tower - Foyer"),
                new Location(this, 256+102, 0xEAB2, LocationType.Regular, "Castle Tower - Dark Maze",
                    items => items.Has(Lamp) && items.Has(KeyCT)),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return items.Has(Cape) || items.HasMasterSword();
        }

        //Complete: items.Has(Lamp) && items.Has(KeyCT, 2) && items.HasSword()
    }

}
