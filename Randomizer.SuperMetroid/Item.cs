using System.Collections.Generic;
using System.Linq;
using static Randomizer.SuperMetroid.ItemType;

namespace Randomizer.SuperMetroid {

    public enum ItemType {
        ETank,
        Missile,
        Super,
        PowerBomb,
        Bombs,
        ChargeBeam,
        IceBeam,
        HiJump,
        SpeedBooster,
        WaveBeam,
        Spazer,
        SpringBall,
        Varia,
        Gravity,
        XRay,
        PlasmaBeam,
        Grapple,
        SpaceJump,
        ScrewAttack,
        Morph,
        ReserveTank
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

        public static bool HasEnergyReserves(this List<Item> items, int amount) {
            return (items.Count(i => i.Type == ETank) + items.Count(i => i.Type == ReserveTank)) >= amount;
        }

        public static bool CanOpenRedDoors(this List<Item> items) {
            return items.Has(Missile) || items.Has(Super);
        }

    }

}
