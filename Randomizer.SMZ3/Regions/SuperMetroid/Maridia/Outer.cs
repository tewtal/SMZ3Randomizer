using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Maridia {

    class Outer : SMRegion {

        public override string Name => "Maridia Outer";
        public override string Area => "Maridia";

        public Outer(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 136, 0xC7C437, LocationType.Visible, "Missile (green Maridia shinespark)", Logic switch {
                    Casual => items => items.SpeedBooster,
                    _ => new Requirement(items => items.Gravity && items.SpeedBooster)
                }),
                new Location(this, 137, 0xC7C43D, LocationType.Visible, "Super Missile (green Maridia)"),
                new Location(this, 138, 0xC7C47D, LocationType.Visible, "Energy Tank, Mama turtle", Logic switch {
                    Casual => items => items.CanFly() || items.SpeedBooster || items.Grapple,
                    _ => new Requirement(items => items.CanFly() || items.SpeedBooster || items.Grapple ||
                        items.CanSpringBallJump() && (items.Gravity || items.HiJump))
                }),
                new Location(this, 139, 0xC7C483, LocationType.Hidden, "Missile (green Maridia tatori)"),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Casual => (
                        World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() ||
                        items.CanAccessMaridiaPortal(World)
                    ) && items.Gravity,
                _ =>
                    World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() &&
                    (items.Gravity || items.HiJump && (items.CanSpringBallJump() || items.Ice))
                    || items.CanAccessMaridiaPortal(World)
            };
        }

    }

}
