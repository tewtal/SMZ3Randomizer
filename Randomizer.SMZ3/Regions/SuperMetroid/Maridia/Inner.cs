using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Maridia {

    class Inner : Region, Reward {

        public override string Name => "Maridia Inner";
        public override string Area => "Maridia";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public Inner(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 140, 0x7C4AF, LocationType.Visible, "Super Missile (yellow Maridia)", Config.Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CanPassBombPassages() &&
                        (items.Gravity || items.Ice || items.HiJump && items.CanSpringBallJump()))
                }),
                new Location(this, 141, 0x7C4B5, LocationType.Visible, "Missile (yellow Maridia super missile)", Config.Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CanPassBombPassages() &&
                        (items.Gravity || items.Ice || items.HiJump && items.CanSpringBallJump()))
                }),
                new Location(this, 142, 0x7C533, LocationType.Visible, "Missile (yellow Maridia false wall)", Config.Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CanPassBombPassages() &&
                        (items.Gravity || items.Ice || items.HiJump && items.CanSpringBallJump()))
                }),
                new Location(this, 143, 0x7C559, LocationType.Chozo, "Plasma Beam", Config.Logic switch {
                    Casual => items => CanDefeatDraygon(items) && (items.ScrewAttack || items.Plasma) && (items.HiJump || items.CanFly()),
                    _ => new Requirement(items => CanDefeatDraygon(items) &&
                        (items.Charge && items.HasEnergyReserves(3) || items.ScrewAttack || items.Plasma || items.SpeedBooster) &&
                        (items.HiJump || items.CanSpringBallJump() || items.CanFly() || items.SpeedBooster))
                }),
                new Location(this, 144, 0x7C5DD, LocationType.Visible, "Missile (left Maridia sand pit room)", Config.Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.HiJump && (items.SpaceJump || items.CanSpringBallJump()) || items.Gravity)
                }),
                new Location(this, 145, 0x7C5E3, LocationType.Chozo, "Reserve Tank, Maridia", Config.Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.HiJump && (items.SpaceJump || items.CanSpringBallJump()) || items.Gravity)
                }),
                new Location(this, 146, 0x7C5EB, LocationType.Visible, "Missile (right Maridia sand pit room)", Config.Logic switch {
                    Casual => new Requirement(items => true),
                    _ => items => items.HiJump || items.Gravity
                }),
                new Location(this, 147, 0x7C5F1, LocationType.Visible, "Power Bomb (right Maridia sand pit room)", Config.Logic switch {
                    Casual => new Requirement(items => true),
                    _ => items => items.HiJump && items.CanSpringBallJump() || items.Gravity
                }),
                new Location(this, 148, 0x7C603, LocationType.Visible, "Missile (pink Maridia)", Config.Logic switch {
                    Casual => items => items.SpeedBooster,
                    _ => new Requirement(items => items.Gravity)
                }),
                new Location(this, 149, 0x7C609, LocationType.Visible, "Super Missile (pink Maridia)", Config.Logic switch {
                    Casual => items => items.SpeedBooster,
                    _ => new Requirement(items => items.Gravity)
                }),
                new Location(this, 150, 0x7C6E5, LocationType.Chozo, "Spring Ball", Config.Logic switch {
                    Casual => items => items.Grapple && items.CanUsePowerBombs() && (items.SpaceJump || items.HiJump),
                    _ => new Requirement(items => items.Grapple && items.CanUsePowerBombs() && (
                        items.Gravity && (items.CanFly() || items.HiJump) ||
                        items.Ice && items.HiJump && items.CanSpringBallJump() && items.SpaceJump))
                }),
                new Location(this, 151, 0x7C74D, LocationType.Hidden, "Missile (Draygon)", Config.Logic switch {
                    Casual => items => CanDefeatBotwoon(items),
                    _ => new Requirement(items => CanDefeatBotwoon(items) && items.Gravity)
                }),
                new Location(this, 152, 0x7C755, LocationType.Visible, "Energy Tank, Botwoon", Config.Logic switch {
                    _ => new Requirement(items => CanDefeatBotwoon(items))
                }),
                new Location(this, 154, 0x7C7A7, LocationType.Chozo, "Space Jump", Config.Logic switch {
                    Casual => items => CanDefeatDraygon(items),
                    _ => new Requirement(items => CanDefeatDraygon(items) &&
                        (items.CanFly() || items.SpeedBooster && items.HiJump))
                })
            };
        }

        bool CanDefeatDraygon(Progression items) {
            return World.Config.Logic switch {
                Casual => CanDefeatBotwoon(items) && (!World.Config.Keysanity || items.Has(DraygonKey)) &&
                    items.Gravity && (items.SpeedBooster && items.HiJump || items.CanFly()),
                _ => CanDefeatBotwoon(items) && (!World.Config.Keysanity || items.Has(DraygonKey)) &&
                    items.Gravity
            };
        }

        bool CanDefeatBotwoon(Progression items) {
            return World.Config.Logic switch {
                Casual => items.SpeedBooster || items.CanAccessMaridiaPortal(World),
                _ => items.Ice || items.SpeedBooster || items.CanAccessMaridiaPortal(World)
            };
        }

        public override bool CanEnter(Progression items) {
            return Config.Logic switch {
                Casual => (
                        World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() &&
                        (items.CanFly() || items.SpeedBooster || items.Grapple)
                        || items.CanAccessMaridiaPortal(World)
                    ) && items.Gravity,
                _ =>
                    World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() &&
                    (items.Gravity || items.HiJump && (items.Ice || items.CanSpringBallJump()) && items.Grapple)
                    || items.CanAccessMaridiaPortal(World)
            };
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Space Jump").Available(items);
        }

    }

}
