using System.Collections.Generic;
using System.Linq;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;

namespace Randomizer.SuperMetroid {

    public enum ItemType {
        Missile,
        Super,
        PowerBomb,
        Grapple,
        XRay,
        ETank,
        ReserveTank,
        Charge,
        Ice,
        Wave,
        Spazer,
        Plasma,
        Varia,
        Gravity,
        Morph,
        Bombs,
        SpringBall,
        ScrewAttack,
        HiJump,
        SpaceJump,
        SpeedBooster
    }

    class Item {

        public string Name { get; set; }
        public ItemType Type { get; set; }

    }

    static class ItemListExtensions {

        public static bool Has(this List<Item> items, ItemType itemType) {
            return items.Any(i => i.Type == itemType);
        }

        public static bool Has(this List<Item> items, ItemType itemType, int amount) {
            return items.Count(i => i.Type == itemType) >= amount;
        }

        public static bool CanIbj(this List<Item> items) {
            return items.Has(Morph) && items.Has(Bombs);
        }

        public static bool CanFly(this List<Item> items) {
            return items.Has(SpaceJump) || items.CanIbj();
        }

        public static bool CanUsePowerBombs(this List<Item> items) {
            return items.Has(Morph) && items.Has(PowerBomb);
        }

        public static bool CanPassBombPassages(this List<Item> items) {
            return items.Has(Morph) && (items.Has(Bombs) || items.Has(PowerBomb));
        }

        public static bool CanDestroyBombWalls(this List<Item> items) {
            return items.CanPassBombPassages() || items.Has(ScrewAttack);
        }

        public static bool CanSpringBallJump(this List<Item> items) {
            return items.Has(Morph) && items.Has(SpringBall);
        }

        public static bool CanHellRun(this List<Item> items) {
            return items.Has(Varia) || items.HasEnergyReserves(5);
        }

        public static bool HasEnergyReserves(this List<Item> items, int amount) {
            return (items.Count(i => i.Type == ETank) + items.Count(i => i.Type == ReserveTank)) >= amount;
        }

        public static bool CanOpenRedDoors(this List<Item> items) {
            return items.Has(Missile) || items.Has(Super);
        }

        public static bool CanDefeatBotwoon(this List<Item> items, Logic logic) {
            return logic switch {
                Casual => items.Has(SpeedBooster),
                _ => items.Has(Ice) || items.Has(SpeedBooster)
            };
        }

        public static bool CanDefeatDraygon(this List<Item> items, Logic logic) {
            return logic switch {
                Casual  => items.CanDefeatBotwoon(logic) && items.Has(Gravity) &&
                            (items.Has(SpeedBooster) && items.Has(HiJump) || items.CanFly()),
                _       => items.CanDefeatBotwoon(logic) && items.Has(Gravity)
            };
        }

    }

}
