using System.Collections.Generic;
using System.Linq;

namespace Randomizer.SMZ3 {

    enum LocationType {
        Regular,
        HeraStandingKey,
        Pedestal,
        Ether,
        Bombos,

        Visible,
        Chozo,
        Hidden
    }

    delegate bool Requirement(List<Item> items);

    class Location {

        public int Id { get; set; }
        public string Name { get; set; }
        public LocationType Type { get; set; }
        public int Address { get; set; }
        public Region Region { get; set; }
        public Item Item { get; set; }

        private readonly Requirement canAccess;

        public Location(Region region, int id, int address, LocationType type, string name)
            : this(region, id, address, type, name, items => true) {
        }

        public Location(Region region, int id, int address, LocationType type, string name, Requirement access) {
            Region = region;
            Id = id;
            Name = name;
            Type = type;
            Address = address;
            canAccess = access;
        }

        public bool Available(List<Item> items) {
            return Region.CanEnter(items) && canAccess(items);
        }

    }

    public static class LocationListExtensions {

        internal static List<Location> AvailableWithinWorld(this List<Location> locations, List<Item> items) {
            return locations.Where(l => l.Available(items.Where(i => i.World == l.Region.World).ToList())).ToList();
        }
        
        internal static List<Location> Available(this List<Location> locations, List<Item> items) {
            return locations.Where(l => l.Available(items)).ToList();
        }

        internal static List<Location> Empty(this List<Location> locations) {
            return locations.Where(l => l.Item == null).ToList();
        }

    }

}
