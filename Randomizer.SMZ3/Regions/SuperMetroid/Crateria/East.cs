using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Crateria {

    class East : SMRegion {

        public override string Name => "Crateria East";
        public override string Area => "Crateria";

        public East(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 1, 0x8F81E8, LocationType.Visible, "Missile (outside Wrecked Ship bottom)", Logic switch {
                    Normal => items => items.Morph && (
                        items.SpeedBooster || items.Grapple || items.SpaceJump ||
                        items.Gravity && (items.CanIbj() || items.HiJump) ||
                        World.CanEnter("Wrecked Ship", items)
                    ),
                    _ => new Requirement(items => items.Morph)
                }),
                new Location(this, 2, 0x8F81EE, LocationType.Hidden, "Missile (outside Wrecked Ship top)", Logic switch {
                    _ => new Requirement(items => World.CanEnter("Wrecked Ship", items) && items.CardWreckedShipBoss && items.CanPassBombPassages())
                }),
                new Location(this, 3, 0x8F81F4, LocationType.Visible, "Missile (outside Wrecked Ship middle)", Logic switch {
                    _ => new Requirement(items => World.CanEnter("Wrecked Ship", items) && items.CardWreckedShipBoss && items.CanPassBombPassages())
                }),
                new Location(this, 4, 0x8F8248, LocationType.Visible, "Missile (Crateria moat)", Logic switch {
                    _ => new Requirement(items => true)
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Normal =>
                    /* Ship -> Moat */
                    (Config.Keysanity ? items.CardCrateriaL2 : items.CanUsePowerBombs()) && items.Super ||
                    /* UN Portal -> Red Tower -> Moat */
                    (Config.Keysanity ? items.CardCrateriaL2 : items.CanUsePowerBombs()) && items.CanAccessNorfairUpperPortal() &&
                        (items.Ice || items.HiJump || items.SpaceJump) ||
                    /* Through Maridia From Portal */
                    items.CanAccessMaridiaPortal(World) && items.Gravity && items.Super && (
                        /* Oasis -> Forgotten Highway */
                        items.CardMaridiaL2 && items.CanDestroyBombWalls() ||
                        /* Draygon -> Cactus Alley -> Forgotten Highway */
                        World.GetLocation("Space Jump").Available(items)
                    ) ||
                    /*Through Maridia from Pipe*/
                    items.CanUsePowerBombs() && items.Super && items.Gravity,
                _ =>
                    /* Ship -> Moat */
                    (Config.Keysanity ? items.CardCrateriaL2 : items.CanUsePowerBombs()) && items.Super ||
                    /* UN Portal -> Red Tower -> Moat */
                    (Config.Keysanity ? items.CardCrateriaL2 : items.CanUsePowerBombs()) && items.CanAccessNorfairUpperPortal() &&
                        (items.Ice || items.HiJump || items.CanFly() || items.CanSpringBallJump()) ||
                    /* Through Maridia From Portal */
                    items.CanAccessMaridiaPortal(World) && (
                        /* Oasis -> Forgotten Highway */
                        items.CardMaridiaL2 && items.Super && (
                            items.HiJump && items.CanPassBombPassages() ||
                            items.Gravity && items.CanDestroyBombWalls()
                        ) ||
                        /* Draygon -> Cactus Alley -> Forgotten Highway */
                        items.Gravity && World.GetLocation("Space Jump").Available(items)
                    ) ||
                    /* Through Maridia from Pipe */
                    items.CanUsePowerBombs() && items.Super && (
                        items.Gravity ||
                        items.HiJump && (items.Ice || items.CanSpringBallJump()) && items.Grapple && items.CardMaridiaL1
                    ),
            };
        }

    }

}
