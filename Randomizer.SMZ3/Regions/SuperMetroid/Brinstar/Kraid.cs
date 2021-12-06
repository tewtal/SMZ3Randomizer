using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Kraid : SMRegion, IReward {

        public override string Name => "Brinstar Kraid";
        public override string Area => "Brinstar";

        public RewardType Reward { get; set; } = RewardType.None;

        public Kraid(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 43, 0x8F899C, LocationType.Hidden, "Energy Tank, Kraid",
                    items => items.CardBrinstarBoss),
                new Location(this, 48, 0x8F8ACA, LocationType.Chozo, "Varia Suit",
                    items => items.CardBrinstarBoss),
                new Location(this, 44, 0x8F89EC, LocationType.Hidden, "Missile (Kraid)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return (items.CanDestroyBombWalls() || items.SpeedBooster || items.CanAccessNorfairUpperPortal()) &&
                items.Super && items.CanPassBombPassages();
        }

        public bool CanComplete(Progression items) {
            return GetLocation("Varia Suit").Available(items);
        }

    }

}
