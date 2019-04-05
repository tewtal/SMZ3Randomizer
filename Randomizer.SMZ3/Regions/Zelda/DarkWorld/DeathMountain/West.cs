using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda.DarkWorld.DeathMountain {

    class West : Region {

        public override string Name => "Dark World Death Mountain West";
        public override string Area => "Dark World";

        public West(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+64, 0xEA8B, LocationType.Regular, "Spike Cave",
                    items => items.Has(MoonPearl) && items.Has(Hammer) && items.CanLiftLight() &&
                        (items.CanExtendMagic() && items.Has(Cape) || items.Has(Byrna)) &&
                        World.CanEnter("Light World Death Mountain West", items)),
            };
        }

    }

}
