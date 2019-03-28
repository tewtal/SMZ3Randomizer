using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Maridia {

    class Inner : Region {

        public override string Name => "Maridia Inner";
        public override string Area => "Maridia";

        public Inner(World world, Logic logic) : base(world, logic) {
            Locations = new List<Location> {
                new Location(this, 140, 0x7C4AF, LocationType.Visible, "Super Missile (yellow Maridia)", Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CanPassBombPassages() &&
                        (items.Has(Gravity) || items.Has(Ice) || items.Has(HiJump) && items.CanSpringBallJump()))
                }),
                new Location(this, 141, 0x7C4B5, LocationType.Visible, "Missile (yellow Maridia super missile)", Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CanPassBombPassages() &&
                        (items.Has(Gravity) || items.Has(Ice) || items.Has(HiJump) && items.CanSpringBallJump()))
                }),
                new Location(this, 142, 0x7C533, LocationType.Visible, "Missile (yellow Maridia false wall)", Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.CanPassBombPassages() &&
                        (items.Has(Gravity) || items.Has(Ice) || items.Has(HiJump) && items.CanSpringBallJump()))
                }),
                new Location(this, 143, 0x7C559, LocationType.Chozo, "Plasma Beam", Logic switch {
                    Casual => items => items.CanDefeatDraygon(Logic) && (items.Has(ScrewAttack) || items.Has(Plasma)) &&
                        (items.Has(HiJump) || items.CanFly()),
                    _ => new Requirement(items => items.CanDefeatDraygon(Logic) &&
                        (items.Has(Charge) && items.HasEnergyReserves(3) || items.Has(ScrewAttack) || items.Has(Plasma) || items.Has(SpeedBooster)) &&
                        (items.Has(HiJump) || items.CanSpringBallJump() || items.CanFly() || items.Has(SpeedBooster)))
                }),
                new Location(this, 144, 0x7C5DD, LocationType.Visible, "Missile (left Maridia sand pit room)", Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.Has(HiJump) && (items.Has(SpaceJump) || items.CanSpringBallJump()) || items.Has(Gravity))
                }),
                new Location(this, 145, 0x7C5E3, LocationType.Chozo, "Reserve Tank, Maridia", Logic switch {
                    Casual => items => items.CanPassBombPassages(),
                    _ => new Requirement(items => items.Has(HiJump) && (items.Has(SpaceJump) || items.CanSpringBallJump()) || items.Has(Gravity))
                }),
                new Location(this, 146, 0x7C5EB, LocationType.Visible, "Missile (right Maridia sand pit room)", Logic switch {
                    Casual => new Requirement(items => true),
                    _ => items => items.Has(HiJump) || items.Has(Gravity)
                }),
                new Location(this, 147, 0x7C5F1, LocationType.Visible, "Power Bomb (right Maridia sand pit room)", Logic switch {
                    Casual => new Requirement(items => true),
                    _ => items => items.Has(HiJump) && items.CanSpringBallJump() || items.Has(Gravity)
                }),
                new Location(this, 148, 0x7C603, LocationType.Visible, "Missile (pink Maridia)", Logic switch {
                    Casual => items => items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Gravity))
                }),
                new Location(this, 149, 0x7C609, LocationType.Visible, "Super Missile (pink Maridia)", Logic switch {
                    Casual => items => items.Has(SpeedBooster),
                    _ => new Requirement(items => items.Has(Gravity))
                }),
                new Location(this, 150, 0x7C6E5, LocationType.Chozo, "Spring Ball", Logic switch {
                    Casual => items => items.Has(Grapple) && items.CanUsePowerBombs() && (items.Has(SpaceJump) || items.Has(HiJump)),
                    _ => new Requirement(items => items.Has(Grapple) && items.CanUsePowerBombs() && (
                        items.Has(Gravity) && (items.CanFly() || items.Has(HiJump)) ||
                        items.Has(Ice) && items.Has(HiJump) && items.CanSpringBallJump() && items.Has(SpaceJump)))
                }),
                new Location(this, 151, 0x7C74D, LocationType.Hidden, "Missile (Draygon)", Logic switch {
                    Casual => items => items.CanDefeatBotwoon(Logic),
                    _ => new Requirement(items => items.CanDefeatBotwoon(Logic) && items.Has(Gravity))
                }),
                new Location(this, 152, 0x7C755, LocationType.Visible, "Energy Tank, Botwoon", Logic switch {
                    _ => new Requirement(items => items.CanDefeatBotwoon(Logic))
                }),
                new Location(this, 154, 0x7C7A7, LocationType.Chozo, "Space Jump", Logic switch {
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
