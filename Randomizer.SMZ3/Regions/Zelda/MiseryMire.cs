using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class MiseryMire : Z3Region, IReward, IMedallionAccess {

        public override string Name => "Misery Mire";
        public override string Area => "Misery Mire";

        public RewardType Reward { get; set; } = RewardType.None;
        public ItemType Medallion { get; set; }

        public MiseryMire(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyMM, BigKeyMM, MapMM, CompassMM };

            Locations = new List<Location> {
                new Location(this, 256+169, 0x1EA5E, LocationType.Regular, "Misery Mire - Main Lobby",
                    items => items.BigKeyMM || items.KeyMM >= 1),
                new Location(this, 256+170, 0x1EA6A, LocationType.Regular, "Misery Mire - Map Chest",
                    items => items.BigKeyMM || items.KeyMM >= 1),
                new Location(this, 256+171, 0x1EA61, LocationType.Regular, "Misery Mire - Bridge Chest"),
                new Location(this, 256+172, 0x1E9DA, LocationType.Regular, "Misery Mire - Spike Chest"),
                new Location(this, 256+173, 0x1EA64, LocationType.Regular, "Misery Mire - Compass Chest",
                    items => items.CanLightTorches() &&
                        items.KeyMM >= (Locations.Get("Misery Mire - Big Key Chest").ItemType == BigKeyMM ? 2 : 3)),
                new Location(this, 256+174, 0x1EA6D, LocationType.Regular, "Misery Mire - Big Key Chest",
                    items => items.CanLightTorches() &&
                        items.KeyMM >= (Locations.Get("Misery Mire - Compass Chest").ItemType == BigKeyMM ? 2 : 3)),
                new Location(this, 256+175, 0x1EA67, LocationType.Regular, "Misery Mire - Big Chest",
                    items => items.BigKeyMM),
                new Location(this, 256+176, 0x308158, LocationType.Regular, "Misery Mire - Vitreous",
                    items => items.BigKeyMM && items.Lamp && items.Somaria),
            };
        }

        // Need "CanKillManyEnemies" if implementing swordless
        public override bool CanEnter(Progression items) {
            return Medallion switch {
                    Bombos => items.Bombos,
                    Ether => items.Ether,
                    _ => items.Quake
                } && items.Sword &&
                items.MoonPearl && (items.Boots || items.Hookshot) &&
                World.CanEnter("Dark World Mire", items);
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Misery Mire - Vitreous").Available(items);
        }

    }

}
