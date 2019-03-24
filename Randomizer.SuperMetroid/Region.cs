using System.Collections.Generic;

namespace Randomizer.SuperMetroid {

    abstract class Region {

        public virtual string Name { get; }
        public virtual string Area { get; }
        public List<Location> Locations { get; set; }
        internal World World { get; set; }
        internal Logic Logic { get; set; }

        public Region(World world, Logic logic) {
            Logic = logic;
            World = world;
        }

        public virtual bool CanEnter(List<Item> items) {
            return true;
        }

    }

}
