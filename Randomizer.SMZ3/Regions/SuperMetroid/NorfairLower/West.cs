using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairLower {

    class West : SMRegion {

        public override string Name => "Norfair Lower West";
        public override string Area => "Norfair Lower";

        public West(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 70, 0x8F8E6E, LocationType.Visible, "Missile (Gold Torizo)", Logic switch {
                    Normal => items => items.CanUsePowerBombs() && items.SpaceJump && items.Super,
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.SpaceJump && items.Varia && (
                        items.HiJump || items.Gravity ||
                        items.CanAccessNorfairLowerPortal() && (items.CanFly() || items.CanSpringBallJump() || items.SpeedBooster) && items.Super
                    )),
                }),
                new Location(this, 71, 0x8F8E74, LocationType.Hidden, "Super Missile (Gold Torizo)", Logic switch {
                    Normal => items => items.CanDestroyBombWalls() && (items.Super || items.Charge) &&
                        (items.CanAccessNorfairLowerPortal() || items.CanUsePowerBombs() && items.SpaceJump),
                    _ => new Requirement(items => items.CanDestroyBombWalls() && items.Varia && (items.Super || items.Charge))
                }),
                new Location(this, 79, 0x8F9110, LocationType.Chozo, "Screw Attack", Logic switch {
                    Normal => items => items.CanDestroyBombWalls() && (items.CanAccessNorfairLowerPortal() || items.CanUsePowerBombs() && items.SpaceJump),
                    _ => new Requirement(items => items.CanDestroyBombWalls() && (items.CanAccessNorfairLowerPortal() || items.Varia))
                }),
                new Location(this, 73, 0x8F8F30, LocationType.Visible, "Missile (Mickey Mouse room)", Logic switch {
                    Normal => items => items.Morph && items.Super &&
                        /* Climb worst room (from LN East CanEnter) */
                        items.CanFly() && items.CanUsePowerBombs() && (
                            /* Exit to Upper Norfair */
                            (items.CardLowerNorfairL1 || items.Gravity) /* Intended, or Reverse Lava Dive */ && items.CardNorfairL2 /* Bubble Mountain */ ||
                            items.Gravity && items.Wave /* Volcano Room and Blue Gate */ &&
                                (items.Grapple || items.SpaceJump) /* Spikey Acid Snakes -> Croc Escape (this shortcuts Frog Speedway) */ ||
                            /* GT fight -> Portal */
                            items.CanUsePowerBombs() && items.SpaceJump && (items.Super || items.Charge)
                        ),
                    _ => new Requirement(items => items.Varia && items.Morph && items.Super &&
                        /* Climb worst room (from LN East CanEnter) */
                        (items.CanFly() || items.HiJump || items.CanSpringBallJump() || items.Ice && items.Charge) &&
                        (items.CanPassBombPassages() || items.ScrewAttack && items.SpaceJump) && (
                            /* Exit to Upper Norfair */
                            items.CardNorfairL2 || items.SpeedBooster || items.CanFly() || items.Grapple ||
                            items.HiJump && (items.CanSpringBallJump() || items.Ice) ||
                            /* Portal (with GGG) */
                            items.CanUsePowerBombs()
                        )),
                }),
            };
        }

        // Todo: account for Croc Speedway once Norfair Upper East also do so, otherwise it would be inconsistent to do so here
        public override bool CanEnter(Progression items) {
            return Logic switch {
                Normal =>
                    items.Varia && (
                        World.CanEnter("Norfair Upper East", items) && items.CanUsePowerBombs() && items.SpaceJump && items.Gravity && (
                            /* Trivial case, Bubble Mountain access */
                            items.CardNorfairL2 ||
                            /* Frog Speedway -> UN Farming Room gate */
                            items.SpeedBooster && items.Wave
                        ) ||
                        items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls()
                    ),
                _ =>
                    World.CanEnter("Norfair Upper East", items) && items.CanUsePowerBombs() && items.Varia && (items.HiJump || items.Gravity) && (
                        /* Trivial case, Bubble Mountain access */
                        items.CardNorfairL2 ||
                        /* Frog Speedway -> UN Farming Room gate */
                        items.SpeedBooster && (items.Missile || items.Super || items.Wave /* Blue Gate */)
                    ) ||
                    items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls()
            };
        }

    }

}
