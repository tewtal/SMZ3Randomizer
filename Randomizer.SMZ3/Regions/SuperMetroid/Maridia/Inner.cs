using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Maridia {

    class Inner : SMRegion, IReward {

        public override string Name => "Maridia Inner";
        public override string Area => "Maridia";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public Inner(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 140, 0x8FC4AF, LocationType.Visible, "Super Missile (yellow Maridia)", Logic switch {
                    Normal => items => items.CardMaridiaL1 && items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CardMaridiaL1 && items.CanPassBombPassages() &&
                        (items.Gravity || items.Ice || items.HiJump && items.CanSpringBallJump()))
                }),
                new Location(this, 141, 0x8FC4B5, LocationType.Visible, "Missile (yellow Maridia super missile)", Logic switch {
                    Normal => items => items.CardMaridiaL1 && items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CardMaridiaL1 && items.CanPassBombPassages() &&
                        (items.Gravity || items.Ice || items.HiJump && items.CanSpringBallJump()))
                }),
                new Location(this, 142, 0x8FC533, LocationType.Visible, "Missile (yellow Maridia false wall)", Logic switch {
                    Normal => items => items.CardMaridiaL1 && items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CardMaridiaL1 && items.CanPassBombPassages() &&
                        (items.Gravity || items.Ice || items.HiJump && items.CanSpringBallJump()))
                }),
                new Location(this, 143, 0x8FC559, LocationType.Chozo, "Plasma Beam", Logic switch {
                    Normal => items => CanDefeatDraygon(items) && (items.ScrewAttack || items.Plasma) && (items.HiJump || items.CanFly()),
                    _ => new Requirement(items => CanDefeatDraygon(items) &&
                        (items.Charge && items.HasEnergyReserves(3) || items.ScrewAttack || items.Plasma || items.SpeedBooster) &&
                        (items.HiJump || items.CanSpringBallJump() || items.CanFly() || items.SpeedBooster))
                }),
                new Location(this, 144, 0x8FC5DD, LocationType.Visible, "Missile (left Maridia sand pit room)", Logic switch {
                    Normal => items => CanReachAqueduct(items) && items.Super && items.CanPassBombPassages(),
                    _ => new Requirement(items => CanReachAqueduct(items) && items.Super &&
                        (items.Gravity || items.HiJump && (items.SpaceJump || items.CanSpringBallJump())))
                }),
                new Location(this, 145, 0x8FC5E3, LocationType.Chozo, "Reserve Tank, Maridia", Logic switch {
                    Normal => items => CanReachAqueduct(items) && items.Super && items.CanPassBombPassages(),
                    _ => new Requirement(items => CanReachAqueduct(items) && items.Super &&
                        (items.Gravity || items.HiJump && (items.SpaceJump || items.CanSpringBallJump())))
                }),
                new Location(this, 146, 0x8FC5EB, LocationType.Visible, "Missile (right Maridia sand pit room)", Logic switch {
                    Normal => new Requirement(items => CanReachAqueduct(items) && items.Super),
                    _ => items => CanReachAqueduct(items) && items.Super && (items.HiJump || items.Gravity)
                }),
                new Location(this, 147, 0x8FC5F1, LocationType.Visible, "Power Bomb (right Maridia sand pit room)", Logic switch {
                    Normal => new Requirement(items => CanReachAqueduct(items) && items.Super),
                    _ => items => CanReachAqueduct(items) && items.Super && (items.Gravity || items.HiJump && items.CanSpringBallJump())
                }),
                new Location(this, 148, 0x8FC603, LocationType.Visible, "Missile (pink Maridia)", Logic switch {
                    Normal => items => CanReachAqueduct(items) && items.SpeedBooster,
                    _ => new Requirement(items => CanReachAqueduct(items) && items.Gravity)
                }),
                new Location(this, 149, 0x8FC609, LocationType.Visible, "Super Missile (pink Maridia)", Logic switch {
                    Normal => items => CanReachAqueduct(items) && items.SpeedBooster,
                    _ => new Requirement(items => CanReachAqueduct(items) && items.Gravity)
                }),
                new Location(this, 150, 0x8FC6E5, LocationType.Chozo, "Spring Ball", Logic switch {
                    Normal => items => items.Super && items.Grapple && items.CanUsePowerBombs() && (items.SpaceJump || items.HiJump),
                    _ => new Requirement(items => items.Super && items.Grapple && items.CanUsePowerBombs() && (
                        items.Gravity && (items.CanFly() || items.HiJump) ||
                        items.Ice && items.HiJump && items.CanSpringBallJump() && items.SpaceJump)
                    )
                }),
                new Location(this, 151, 0x8FC74D, LocationType.Hidden, "Missile (Draygon)", Logic switch {
                    Normal => items =>
                        items.CardMaridiaL1 && items.CardMaridiaL2 && CanDefeatBotwoon(items) ||
                        items.CanAccessMaridiaPortal(World),
                    _ => new Requirement(items => (
                            items.CardMaridiaL1 && items.CardMaridiaL2 && CanDefeatBotwoon(items) ||
                            items.CanAccessMaridiaPortal(World)
                        ) && items.Gravity)
                }),
                new Location(this, 152, 0x8FC755, LocationType.Visible, "Energy Tank, Botwoon", Logic switch {
                    _ => new Requirement(items =>
                        items.CardMaridiaL1 && items.CardMaridiaL2 && CanDefeatBotwoon(items) ||
                        items.CanAccessMaridiaPortal(World) && items.CardMaridiaL2)
                }),
                new Location(this, 154, 0x8FC7A7, LocationType.Chozo, "Space Jump", Logic switch {
                    _ => new Requirement(items => CanDefeatDraygon(items))
                })
            };
        }

        bool CanReachAqueduct(Progression items) {
            return Logic switch { 
               Normal => items.CardMaridiaL1 && (items.CanFly() || items.SpeedBooster || items.Grapple) 
                        || items.CardMaridiaL2 && items.CanAccessMaridiaPortal(World),
               _ => items.CardMaridiaL1 && (items.Gravity || items.HiJump && (items.Ice || items.CanSpringBallJump()) && items.Grapple) 
                        || items.CardMaridiaL2 && items.CanAccessMaridiaPortal(World)
            };
        }

        bool CanDefeatDraygon(Progression items) {
            return Logic switch {
                Normal => (
                    items.CardMaridiaL1 && items.CardMaridiaL2 && CanDefeatBotwoon(items) ||
                    items.CanAccessMaridiaPortal(World)
                ) && items.CardMaridiaBoss && items.Gravity && (items.SpeedBooster && items.HiJump || items.CanFly()),
                _ => (
                    items.CardMaridiaL1 && items.CardMaridiaL2 && CanDefeatBotwoon(items) ||
                    items.CanAccessMaridiaPortal(World)
                ) && items.CardMaridiaBoss && items.Gravity
            };
        }

        bool CanDefeatBotwoon(Progression items) {
            return Logic switch {
                Normal => items.SpeedBooster || items.CanAccessMaridiaPortal(World),
                _ => items.Ice || items.SpeedBooster && items.Gravity || items.CanAccessMaridiaPortal(World)
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Normal => items.Gravity && (
                    World.CanEnter("Norfair Upper West", items) && items.Super && items.CanUsePowerBombs() &&
                        (items.CanFly() || items.SpeedBooster || items.Grapple) ||
                    items.CanAccessMaridiaPortal(World)
                ),
                _ =>
                    items.Super && World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() &&
                        (items.Gravity || items.HiJump && (items.Ice || items.CanSpringBallJump()) && items.Grapple) ||
                    items.CanAccessMaridiaPortal(World)
            };
        }

        public bool CanComplete(Progression items) {
            return GetLocation("Space Jump").Available(items);
        }

    }

}
