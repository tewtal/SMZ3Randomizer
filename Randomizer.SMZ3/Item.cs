using System;
using System.Linq;
using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3 {

    public enum ItemType {
        Nothing,

        BigKeyEP,
        BigKeyDP,
        BigKeyTH,
        BigKeyPD,
        BigKeySP,
        BigKeySW,
        BigKeyTT,
        BigKeyIP,
        BigKeyMM,
        BigKeyTR,
        BigKeyGT,
        KeyHC,
        KeyCT,
        KeyDP,
        KeyTH,
        KeyPD,
        KeySP,
        KeySW,
        KeyTT,
        KeyIP,
        KeyMM,
        KeyTR,
        KeyGT,
        MapHC,
        MapEP,
        MapDP,
        MapTH,
        MapPD,
        MapSP,
        MapSW,
        MapTT,
        MapIP,
        MapMM,
        MapTR,
        MapGT,
        CompassEP,
        CompassDP,
        CompassTH,
        CompassPD,
        CompassSP,
        CompassSW,
        CompassTT,
        CompassIP,
        CompassMM,
        CompassTR,
        CompassGT,

        ProgressiveTunic,
        ProgressiveShield,
        ProgressiveSword,
        Bow,
        SilverArrows,
        BlueBoomerang,
        RedBoomerang,
        Hookshot,
        Mushroom,
        Powder,
        Firerod,
        Icerod,
        Bombos,
        Ether,
        Quake,
        Lamp,
        Hammer,
        Shovel,
        Flute,
        Bugnet,
        Book,
        Bottle,
        Somaria,
        Byrna,
        Cape,
        Mirror,
        Boots,
        ProgressiveGlove,
        Flippers,
        MoonPearl,
        HalfMagic,
        HeartPiece,
        HeartContainer,
        HeartContainerRefill,
        ThreeBombs,
        Arrow,
        TenArrows,
        OneRupee,
        FiveRupees,
        TwentyRupees,
        TwentyRupees2,
        FiftyRupees,
        OneHundredRupees,
        ThreeHundredRupees,
        BombUpgrade5,
        BombUpgrade10,
        ArrowUpgrade5,
        ArrowUpgrade10,

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

    class Item {

        public string Name { get; set; }
        public ItemType Type { get; set; }
        public World World { get; set; }

        public static Item Nothing(World world) {
            return new Item { Name = "Nothing", Type = ItemType.Nothing, World = world };
        }

        public static List<Item> CreateProgressionPool(World world) {
            return new List<Item> {
                new Item() { Name = "Morphing Ball", Type = Morph, World = world},
                new Item() { Name = "Bombs", Type = Bombs, World = world},
                new Item() { Name = "Ice Beam", Type = Ice, World = world},
                new Item() { Name = "Hi-Jump Boots", Type = HiJump, World = world},
                new Item() { Name = "Speed Booster", Type = SpeedBooster, World = world},
                new Item() { Name = "Screw Attack", Type = ScrewAttack, World = world},
                new Item() { Name = "Varia Suit", Type = Varia, World = world},
                new Item() { Name = "Gravity Suit", Type = Gravity, World = world},
                new Item() { Name = "Grappling Beam", Type = Grapple, World = world},
                new Item() { Name = "Space Jump", Type = SpaceJump, World = world},
                new Item() { Name = "Spring Ball", Type = SpringBall, World = world},
                new Item() { Name = "Wave Beam", Type = Wave, World = world},
                new Item() { Name = "Plasma Beam", Type = Plasma, World = world},
                new Item() { Name = "Charge Beam", Type = Charge, World = world},

                new Item() { Name = "Progression Missile", Type = Missile, World = world},
                new Item() { Name = "Progression Super", Type = Super, World = world},
                new Item() { Name = "Progression Power Bomb", Type = PowerBomb, World = world},
                new Item() { Name = "Progression Power Bomb", Type = PowerBomb, World = world},
                new Item() { Name = "Progression Energy Tank", Type = ETank, World = world},
                new Item() { Name = "Progression Energy Tank", Type = ETank, World = world},
                new Item() { Name = "Progression Energy Tank", Type = ETank, World = world},
                new Item() { Name = "Progression Energy Tank", Type = ETank, World = world},
            };
        }

        public static List<Item> CreateNicePool(World world) {
            return new List<Item> {
                new Item() { Name = "Spazer", Type = Spazer, World = world},
                new Item() { Name = "X-Ray Scope", Type = XRay, World = world},
            };
        }

        public static List<Item> CreateJunkPool(World world, Random random) {
            var itemPool = new List<Item>();

            for (int i = 0; i < 10; i++) {
                itemPool.Add(new Item() { Type = ETank, Name = "Energy Tank", World = world });
            }

            for (int i = 0; i < 4; i++) {
                itemPool.Add(new Item() { Type = ReserveTank, Name = "Reserve Tank", World = world });
            }

            for (int i = 0; i < 62; i++) {
                itemPool.Add(random.Next(7) switch {
                    int r when r < 3 => new Item() { Type = Missile, Name = "Missile", World = world },
                    int r when r >= 3 && r < 6 => new Item() { Type = Super, Name = "Super Missile", World = world },
                    _ => new Item() { Type = PowerBomb, Name = "Power Bomb", World = world }
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
            return logic switch {
                Casual => items.Has(SpeedBooster),
                _ => items.Has(Ice) || items.Has(SpeedBooster)
            };
        }

        public static bool CanDefeatDraygon(this List<Item> items, Logic logic) {
            return logic switch {
                Casual => items.CanDefeatBotwoon(logic) && items.Has(Gravity) &&
                    (items.Has(SpeedBooster) && items.Has(HiJump) || items.CanFly()),
                _ => items.CanDefeatBotwoon(logic) && items.Has(Gravity)
            };
        }

    }

}
