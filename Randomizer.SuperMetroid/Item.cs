using System;
using System.Linq;
using System.Collections.Generic;
using static Randomizer.SuperMetroid.ItemType;
using static Randomizer.SuperMetroid.Logic;
using static Randomizer.SuperMetroid.ItemClass;

namespace Randomizer.SuperMetroid {

    public enum ItemType : byte {
        Missile = 1,
        Super = 2,
        PowerBomb = 3,
        Grapple = 16,
        XRay = 14,
        ETank = 0,
        ReserveTank = 20,
        Charge = 5,
        Ice = 6,
        Wave = 9,
        Spazer = 10,
        Plasma = 15,
        Varia = 12,
        Gravity = 13,
        Morph = 19,
        Bombs = 4,
        SpringBall = 11,
        ScrewAttack = 18,
        HiJump = 7,
        SpaceJump = 17,
        SpeedBooster = 8
    }

    enum ItemClass {
        Minor,
        Major
    }

    class Item {

        public string Name { get; set; }
        public ItemType Type { get; set; }
        public ItemClass Class { get; set; } = Major;
        public World World { get; set; }

        public Item(string name, ItemType type, World world)
            : this(type, world) {
            Name = name;
        }

        public Item(ItemType type, World world) {
            Type = type;
            World = world;
        }

        public static List<Item> CreateProgressionPool(World world, Random rnd) {
            return new List<Item> {
                new Item("Morphing Ball", Morph, world),
                new Item("Bombs", Bombs, world),
                new Item("Ice Beam", Ice,world),
                new Item("Hi-Jump Boots", HiJump, world),
                new Item("Speed Booster", SpeedBooster, world),
                new Item("Screw Attack", ScrewAttack, world),
                new Item("Varia Suit", Varia, world),
                new Item("Gravity Suit", Gravity, world),
                new Item("Grappling Beam", Grapple, world),
                new Item("Space Jump", SpaceJump, world),
                new Item("Spring Ball", SpringBall, world),
                new Item("Wave Beam", Wave, world),
                new Item("Plasma Beam", Plasma, world),
                new Item("Charge Beam", Charge, world),

                new Item("Progression Missile", Missile, world) { Class = Minor },
                new Item("Progression Super", Super, world) { Class = Minor },
                new Item("Progression Power Bomb", PowerBomb, world) { Class = Minor },
                new Item("Progression Power Bomb", PowerBomb, world) { Class = Minor },
                new Item("Progression Energy Tank", ETank, world),
                new Item("Progression Energy Tank", ETank, world),
                new Item("Progression Energy Tank", ETank, world),
                new Item("Progression Energy Tank", ETank, world),
            };
        }

        public static List<Item> CreateNicePool(World world, Random rnd) {
            return new List<Item> {
                new Item("Spazer", Spazer, world),
                new Item("X-Ray Scope", XRay, world),
            };
        }

        public static List<Item> CreateJunkPool(World world, Random rnd) {
            var itemPool = new List<Item>();

            for (int i = 0; i < 10; i++) {
                itemPool.Add(new Item("Energy Tank", ETank, world));
            }

            for (int i = 0; i < 4; i++) {
                itemPool.Add(new Item("Reserve Tank", ReserveTank, world));
            }

            for (int i = 0; i < 62; i++) {
                itemPool.Add(rnd.Next(7) switch {
                    int r when r < 3 => new Item("Missile", Missile, world) { Class = Minor },
                    int r when r >= 3 && r < 6 => new Item("Super Missile", Super, world) { Class = Minor },
                    _ => new Item("Power Bomb", PowerBomb, world) { Class = Minor },
                });
            }

            return itemPool;
        }
    }

    static class ItemListExtensions {

        public static Item Get(this List<Item> items, ItemType itemType) {
            return items.Find(i => i.Type == itemType);
        }

        public static Item Get(this List<Item> items, ItemType itemType, World world) {
            return items.Find(i => i.Type == itemType && i.World == world);
        }

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
            return logic switch
            {
                Casual => items.Has(SpeedBooster),
                _ => items.Has(Ice) || (items.Has(SpeedBooster) && items.Has(Gravity))
            };
        }

        public static bool CanDefeatDraygon(this List<Item> items, Logic logic) {
            return logic switch
            {
                Casual => items.CanDefeatBotwoon(logic) && items.Has(Gravity) &&
                            (items.Has(SpeedBooster) && items.Has(HiJump) || items.CanFly()),
                _ => items.CanDefeatBotwoon(logic) && items.Has(Gravity)
            };
        }

    }

}
