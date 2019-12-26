using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Maridia {

    class Inner : SMRegion, IReward {

        public override string Name => "Maridia Inner";
        public override string Area => "Maridia";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public Inner(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 140, 0xC7C4AF, LocationType.Visible, "Super Missile (yellow Maridia)", Logic switch {
                    Normal => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CanPassBombPassages() &&
                        (items.Gravity || items.Ice || items.HiJump && items.CanSpringBallJump()))
                }),
                new Location(this, 141, 0xC7C4B5, LocationType.Visible, "Missile (yellow Maridia super missile)", Logic switch {
                    Normal => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CanPassBombPassages() &&
                        (items.Gravity || items.Ice || items.HiJump && items.CanSpringBallJump()))
                }),
                new Location(this, 142, 0xC7C533, LocationType.Visible, "Missile (yellow Maridia false wall)", Logic switch {
                    Normal => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CanPassBombPassages() &&
                        (items.Gravity || items.Ice || items.HiJump && items.CanSpringBallJump()))
                }),
                new Location(this, 143, 0xC7C559, LocationType.Chozo, "Plasma Beam", Logic switch {
                    Normal => items => CanDefeatDraygon(items) && (items.ScrewAttack || items.Plasma) && (items.HiJump || items.CanFly()),
                    _ => new Requirement(items => CanDefeatDraygon(items) &&
                        (items.Charge && items.HasEnergyReserves(3) || items.ScrewAttack || items.Plasma || items.SpeedBooster) &&
                        (items.HiJump || items.CanSpringBallJump() || items.CanFly() || items.SpeedBooster))
                }),
                new Location(this, 144, 0xC7C5DD, LocationType.Visible, "Missile (left Maridia sand pit room)", Logic switch {
                    Normal => items => items.Super && items.CanPassBombPassages(),
                    _ => new Requirement(items => items.Super && (items.HiJump && (items.SpaceJump || items.CanSpringBallJump()) || items.Gravity))
                }),
                new Location(this, 145, 0xC7C5E3, LocationType.Chozo, "Reserve Tank, Maridia", Logic switch {
                    Normal => items => items.Super && items.CanPassBombPassages(),
                    _ => new Requirement(items => items.Super && (items.HiJump && (items.SpaceJump || items.CanSpringBallJump()) || items.Gravity))
                }),
                new Location(this, 146, 0xC7C5EB, LocationType.Visible, "Missile (right Maridia sand pit room)", Logic switch {
                    Normal => new Requirement(items => items.Super),
                    _ => items => items.Super && (items.HiJump || items.Gravity)
                }),
                new Location(this, 147, 0xC7C5F1, LocationType.Visible, "Power Bomb (right Maridia sand pit room)", Logic switch {
                    Normal => new Requirement(items => items.Super),
                    _ => items => items.Super && (items.HiJump && items.CanSpringBallJump() || items.Gravity)
                }),
                new Location(this, 148, 0xC7C603, LocationType.Visible, "Missile (pink Maridia)", Logic switch {
                    Normal => items => items.SpeedBooster,
                    _ => new Requirement(items => items.Gravity)
                }),
                new Location(this, 149, 0xC7C609, LocationType.Visible, "Super Missile (pink Maridia)", Logic switch {
                    Normal => items => items.SpeedBooster,
                    _ => new Requirement(items => items.Gravity)
                }),
                new Location(this, 150, 0xC7C6E5, LocationType.Chozo, "Spring Ball", Logic switch {
                    Normal => items => items.Super && items.Grapple && items.CanUsePowerBombs() && (items.SpaceJump || items.HiJump),
                    _ => new Requirement(items => items.Super && items.Grapple && items.CanUsePowerBombs() 
                                        && (items.Gravity && (items.CanFly() || items.HiJump) 
                                            || items.Ice && items.HiJump && items.CanSpringBallJump() && items.SpaceJump))
                }),
                new Location(this, 151, 0xC7C74D, LocationType.Hidden, "Missile (Draygon)", Logic switch {
                    Normal => items => CanDefeatBotwoon(items),
                    _ => new Requirement(items => CanDefeatBotwoon(items) && items.Gravity)
                }),
                new Location(this, 152, 0xC7C755, LocationType.Visible, "Energy Tank, Botwoon", Logic switch {
                    _ => new Requirement(items => CanDefeatBotwoon(items))
                }),
                new Location(this, 154, 0xC7C7A7, LocationType.Chozo, "Space Jump", Logic switch {
                    Normal => items => CanDefeatDraygon(items),
                    _ => new Requirement(items => CanDefeatDraygon(items) &&
                        (items.CanFly() || items.SpeedBooster && items.HiJump))
                })
            };
        }

        bool CanDefeatDraygon(Progression items) {
            return Logic switch {
                Normal => CanDefeatBotwoon(items) && (!World.Config.Keysanity || items.DraygonKey) &&
                    items.Gravity && (items.SpeedBooster && items.HiJump || items.CanFly()),
                _ => CanDefeatBotwoon(items) && (!World.Config.Keysanity || items.DraygonKey) &&
                    items.Gravity
            };
        }

        bool CanDefeatBotwoon(Progression items) {
            return Logic switch {
                Normal => items.SpeedBooster || items.CanAccessMaridiaPortal(World),
                _ => items.Ice || items.SpeedBooster || items.CanAccessMaridiaPortal(World)
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Normal => items.Gravity && (World.CanEnter("Norfair Upper West", items) && items.Super && items.CanUsePowerBombs() && (items.CanFly() || items.SpeedBooster || items.Grapple)
                                        || items.CanAccessMaridiaPortal(World)),
                     _ => items.Super && World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() && (items.Gravity || items.HiJump && (items.Ice || items.CanSpringBallJump()) && items.Grapple)
                       || items.CanAccessMaridiaPortal(World)
            };
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Space Jump").Available(items);
        }

    }

}
