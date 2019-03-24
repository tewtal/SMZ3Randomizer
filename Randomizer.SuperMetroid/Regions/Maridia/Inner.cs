using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;

namespace Randomizer.SuperMetroid.Regions.Maridia {

    class Inner : Region {

        public override string Name => "Maridia Inner";
        public override string Area => "Maridia";

        public Inner(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, "Super Missile (yellow Maridia)", LocationType.Visible, 0x7C4AF, Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CanPassBombPassages() &&
                        (items.Has(Gravity) || items.Has(Ice) || items.Has(HiJump) && items.CanSpringBallJump()))
                }),
                new Location(this, "Missile (yellow Maridia super missile)", LocationType.Visible, 0x7C4B5, Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CanPassBombPassages() &&
                        (items.Has(Gravity) || items.Has(Ice) || items.Has(HiJump) && items.CanSpringBallJump()))
                }),
                new Location(this, "Missile (yellow Maridia false wall)", LocationType.Visible, 0x7C533, Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CanPassBombPassages() &&
                        (items.Has(Gravity) || items.Has(Ice) || items.Has(HiJump) && items.CanSpringBallJump()))
                }),
                new Location(this, "Plasma Beam", LocationType.Chozo, 0x7C559, Logic switch {
                    Casual => items => items.CanDefeatDraygon(Logic) && (items.Has(ScrewAttack) || items.Has(Plasma)) &&
                        (items.Has(HiJump) || items.CanFly()),
                    _ => new Requirement(items => items.CanDefeatDraygon(Logic) &&
                        (items.Has(Charge) && items.HasEnergyReserves(3) || items.Has(ScrewAttack) || items.Has(Plasma) || items.Has(SpeedBooster)) &&
                        (items.Has(HiJump) || items.CanSpringBallJump() || items.CanFly() || items.Has(SpeedBooster)))
                }),
                new Location(this, "Missile (left Maridia sand pit room)", LocationType.Visible, 0x7C5DD, Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.Has(HiJump) && (items.Has(SpaceJump) || items.CanSpringBallJump()) || items.Has(Gravity))
                }),
                new Location(this, "Reserve Tank, Maridia", LocationType.Chozo, 0x7C5E3, Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.Has(HiJump) && (items.Has(SpaceJump) || items.CanSpringBallJump()) || items.Has(Gravity))
                }),
                new Location(this, "Missile (right Maridia sand pit room)", LocationType.Visible, 0x7C5EB, Logic switch {
                    Casual => new Requirement(items => true),
                    _ => items => items.Has(HiJump) || items.Has(Gravity)
                }),
                new Location(this, "Power Bomb (right Maridia sand pit room)", LocationType.Visible, 0x7C5F1, Logic switch {
                    Casual => new Requirement(items => true),
                    _ => items => items.Has(HiJump) && items.CanSpringBallJump() || items.Has(Gravity)
                }),
                new Location(this, "Missile (pink Maridia)", LocationType.Visible, 0x7C603, Logic switch {
                    Casual => items => items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Gravity))
                }),
                new Location(this, "Super Missile (pink Maridia)", LocationType.Visible, 0x7C609, Logic switch {
                    Casual => items => items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Gravity))
                }),
                new Location(this, "Spring Ball", LocationType.Chozo, 0x7C6E5, Logic switch {
                    Casual => items => items.Has(Grapple) && items.CanUsePowerBombs() && (items.Has(SpaceJump) || items.Has(HiJump)),
                    _ => new Requirement(items => items.Has(Grapple) && items.CanUsePowerBombs() && (
                        items.Has(Gravity) && (items.CanFly() || items.Has(HiJump)) ||
                        items.Has(Ice) && items.Has(HiJump) && items.CanSpringBallJump() && items.Has(SpaceJump)))
                }),
                new Location(this, "Missile (Draygon)", LocationType.Hidden, 0x7C74D, Logic switch {
                    Casual => items => items.CanDefeatBotwoon(Logic),
                    _ => new Requirement(items => items.CanDefeatBotwoon(Logic) && items.Has(Gravity))
                }),
                new Location(this, "Energy Tank, Botwoon", LocationType.Visible, 0x7C755, Logic switch {
                    _ => new Requirement(items => items.CanDefeatBotwoon(Logic))
                }),
                new Location(this, "Space Jump", LocationType.Chozo, 0x7C7A7, Logic switch {
                    Casual => items => items.CanDefeatDraygon(Logic),
                    _ => new Requirement(items => items.CanDefeatDraygon(Logic) &&
                        (items.CanFly() || items.Has(SpeedBooster) && items.Has(HiJump)))
                })
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Logic switch {
                Casual => 
                    World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() &&
                    (items.CanFly() || items.Has(SpeedBooster) || items.Has(Grapple)) && items.Has(Gravity),
                _ =>
                    World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() && (
                        items.Has(Gravity) ||
                        items.Has(HiJump) && (items.Has(Ice) || items.CanSpringBallJump()) && items.Has(Grapple))
            };
        }

    }

}
