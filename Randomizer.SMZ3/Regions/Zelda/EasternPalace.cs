﻿using System.Collections.Generic;
using static Randomizer.SMZ3.Z3Logic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class EasternPalace : Z3Region, IReward {

        public override string Name => "Eastern Palace";

        public RewardType Reward { get; set; } = RewardType.None;

        public EasternPalace(World world, Config config) : base(world, config) {
            RegionItems = new[] { BigKeyEP, MapEP, CompassEP };

            Locations = new List<Location> {
                new Location(this, 256+103, 0x1E9B3, LocationType.Regular, "Eastern Palace - Cannonball Chest"),
                new Location(this, 256+104, 0x1E9F5, LocationType.Regular, "Eastern Palace - Map Chest"),
                new Location(this, 256+105, 0x1E977, LocationType.Regular, "Eastern Palace - Compass Chest"),
                new Location(this, 256+106, 0x1E97D, LocationType.Regular, "Eastern Palace - Big Chest",
                    items => items.BigKeyEP),
                new Location(this, 256+107, 0x1E9B9, LocationType.Regular, "Eastern Palace - Big Key Chest", Logic switch {
                    Normal => items => items.Lamp,
                    _ => new Requirement(items => items.Lamp || items.Sword)
                }),
                new Location(this, 256+108, 0x308150, LocationType.Regular, "Eastern Palace - Armos Knights", Logic switch {
                    Normal => items => items.BigKeyEP && items.Bow && items.Lamp,
                    _      => new Requirement(items => items.BigKeyEP && items.Bow && (items.Lamp || items.Firerod))
                }),
            };
        }

        public bool CanComplete(Progression items) {
            return GetLocation("Eastern Palace - Armos Knights").Available(items);
        }

    }

}
