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
        public bool Progression { get; set; }
        public World World { get; set; }

        public static Item Nothing(World world) {
            return new Item { Name = "Nothing", Type = ItemType.Nothing, World = world };
        }

        public static List<Item> CreateProgressionPool(World world) {
            var itemPool = new List<Item> {
                new Item { Name = "Progressive Shield", Type = ProgressiveShield },
                new Item { Name = "Progressive Shield", Type = ProgressiveShield },
                new Item { Name = "Progressive Shield", Type = ProgressiveShield },
                new Item { Name = "Progressive Sword", Type = ProgressiveSword },
                new Item { Name = "Progressive Sword", Type = ProgressiveSword },
                new Item { Name = "Bow", Type = Bow },
                new Item { Name = "Hookshot", Type = Hookshot },
                new Item { Name = "Mushroom", Type = Mushroom },
                new Item { Name = "Powder", Type = Powder },
                new Item { Name = "Fire Rod", Type = Firerod },
                new Item { Name = "Ice Rod", Type = Icerod },
                new Item { Name = "Bombos", Type = Bombos },
                new Item { Name = "Ether", Type = Ether },
                new Item { Name = "Quake", Type = Quake },
                new Item { Name = "Lamp", Type = Lamp },
                new Item { Name = "Hammer", Type = Hammer },
                new Item { Name = "Shovel", Type = Shovel },
                new Item { Name = "Flute", Type = Flute },
                new Item { Name = "Bug Catching Net", Type = Bugnet },
                new Item { Name = "Book of Modura", Type = Book },
                new Item { Name = "Bottle", Type = Bottle },
                new Item { Name = "Cane of Somaria", Type = Somaria },
                new Item { Name = "Cane of Byrna", Type = Byrna },
                new Item { Name = "Cape", Type = Cape },
                new Item { Name = "Magic Mirror", Type = Mirror },
                new Item { Name = "Pegasus Boots", Type = Boots },
                new Item { Name = "Progressive Glove", Type = ProgressiveGlove },
                new Item { Name = "Progressive Glove", Type = ProgressiveGlove },
                new Item { Name = "Flippers", Type = Flippers },
                new Item { Name = "Moon Pearl", Type = MoonPearl },
                new Item { Name = "Half Magic", Type = HalfMagic },

                new Item { Name = "Grappling Beam", Type = Grapple },
                new Item { Name = "Charge Beam", Type = Charge },
                new Item { Name = "Ice Beam", Type = Ice },
                new Item { Name = "Wave Beam", Type = Wave },
                new Item { Name = "Plasma Beam", Type = Plasma },
                new Item { Name = "Varia Suit", Type = Varia },
                new Item { Name = "Gravity Suit", Type = Gravity },
                new Item { Name = "Morphing Ball", Type = Morph },
                new Item { Name = "Bombs", Type = Bombs },
                new Item { Name = "Spring Ball", Type = SpringBall },
                new Item { Name = "Screw Attack", Type = ScrewAttack },
                new Item { Name = "Hi-Jump Boots", Type = HiJump },
                new Item { Name = "Space Jump", Type = SpaceJump },
                new Item { Name = "Speed Booster", Type = SpeedBooster },

                new Item { Name = "Missile", Type = Missile },
                new Item { Name = "Super", Type = Super },
                new Item { Name = "Power Bomb", Type = PowerBomb },
                new Item { Name = "Power Bomb", Type = PowerBomb },
                new Item { Name = "Energy Tank", Type = ETank },
                new Item { Name = "Energy Tank", Type = ETank },
                new Item { Name = "Energy Tank", Type = ETank },
                new Item { Name = "Energy Tank", Type = ETank },
                new Item { Name = "Energy Tank", Type = ETank },
            };

            foreach (var item in itemPool) {
                item.Progression = true;
                item.World = world;
            }

            return itemPool;
        }

        public static List<Item> CreateNicePool(World world) {
            var itemPool = new List<Item> {
                new Item { Name = "Progressive Tunic", Type = ProgressiveTunic },
                new Item { Name = "Progressive Tunic", Type = ProgressiveTunic },
                new Item { Name = "Progressive Sword", Type = ProgressiveSword },
                new Item { Name = "Progressive Sword", Type = ProgressiveSword },
                new Item { Name = "Silver Arrows Upgrade", Type = SilverArrows },
                new Item { Name = "Blue Boomerang", Type = BlueBoomerang },
                new Item { Name = "Red Boomerang", Type = RedBoomerang },
                new Item { Name = "Bottle", Type = Bottle },
                new Item { Name = "Bottle", Type = Bottle },
                new Item { Name = "Bottle", Type = Bottle },
                new Item { Name = "Sanctuary Heart Container", Type = HeartContainerRefill },

                new Item { Name = "Spazer", Type = Spazer },
                new Item { Name = "X-Ray Scope", Type = XRay },
            };

            itemPool.AddRange(Copies(10, () => new Item { Name = "Boss Heart Container", Type = HeartContainer, World = world }));

            foreach (var item in itemPool) item.World = world;

            return itemPool;
        }

        public static List<Item> CreateJunkPool(World world) {
            var itemPool = new List<Item> {
                new Item { Name = "Single Arrow", Type = Arrow },
                new Item { Name = "One Hundred Rupees", Type = OneHundredRupees }
            };

            itemPool.AddRange(Copies(24, () => new Item { Name = "Piece of Heart", Type = HeartPiece }));
            itemPool.AddRange(Copies(12, () => new Item { Name = "Ten Arrows", Type = TenArrows }));
            itemPool.AddRange(Copies(17, () => new Item { Name = "Three Bombs", Type = ThreeBombs }));
            itemPool.AddRange(Copies(2,  () => new Item { Name = "One Rupee", Type = OneRupee }));
            itemPool.AddRange(Copies(4,  () => new Item { Name = "Five Rupees", Type = FiveRupees }));
            itemPool.AddRange(Copies(28, () => new Item { Name = "Twenty Rupees", Type = TwentyRupees }));
            itemPool.AddRange(Copies(7,  () => new Item { Name = "Fifty Rupees", Type = FiftyRupees }));
            itemPool.AddRange(Copies(5,  () => new Item { Name = "Three Hundred Rupees", Type = ThreeHundredRupees }));

            itemPool.AddRange(Copies(9,  () => new Item { Name = "Energy Tank", Type = ETank }));
            itemPool.AddRange(Copies(4,  () => new Item { Name = "Reserve Tank", Type = ReserveTank }));
            itemPool.AddRange(Copies(39, () => new Item { Name = "Missile", Type = Missile }));
            itemPool.AddRange(Copies(15, () => new Item { Name = "Super Missile", Type = Super }));
            itemPool.AddRange(Copies(8,  () => new Item { Name = "Power Bomb", Type = PowerBomb }));

            foreach (var item in itemPool) item.World = world;

            return itemPool;
        }

        /* The order of the dungeon pool is significant */
        public static List<Item> CreateDungeonPool(World world) {
            var itemPool = new List<Item>();

            itemPool.AddRange(new[] {
                new Item { Type = BigKeyEP, Name = "Eastern Palace Big Key" },
                new Item { Type = BigKeyDP, Name = "Desert Palace Big Key" },
                new Item { Type = BigKeyTH, Name = "Tower of Hera Big Key" },
                new Item { Type = BigKeyPD, Name = "Palace of Darkness Big Key" },
                new Item { Type = BigKeySP, Name = "Swamp Palace Big Key" },
                new Item { Type = BigKeySW, Name = "Skull Woods Big Key" },
                new Item { Type = BigKeyTT, Name = "Thieves Town Big Key" },
                new Item { Type = BigKeyIP, Name = "Ice Palace Big Key" },
                new Item { Type = BigKeyMM, Name = "Misery Mire Big Key" },
                new Item { Type = BigKeyTR, Name = "Turtle Rock Big Key" },
                new Item { Type = BigKeyGT, Name = "Ganons Tower Big Key" },
            });

            itemPool.AddRange(Copies(1, () => new Item { Type = KeyHC, Name = "Sewer Key" }));
            itemPool.AddRange(Copies(2, () => new Item { Type = KeyCT, Name = "Castle Tower Key" }));
            itemPool.AddRange(Copies(1, () => new Item { Type = KeyDP, Name = "Desert Palace Key" }));
            itemPool.AddRange(Copies(1, () => new Item { Type = KeyTH, Name = "Tower of Hera Key" }));
            itemPool.AddRange(Copies(6, () => new Item { Type = KeyPD, Name = "Palace of Darkness Key" }));
            itemPool.AddRange(Copies(1, () => new Item { Type = KeySP, Name = "Swamp Palace Key" }));
            itemPool.AddRange(Copies(3, () => new Item { Type = KeySW, Name = "Skull Woods Key" }));
            itemPool.AddRange(Copies(1, () => new Item { Type = KeyTT, Name = "Thieves Town Key" }));
            itemPool.AddRange(Copies(2, () => new Item { Type = KeyIP, Name = "Ice Palace Key" }));
            itemPool.AddRange(Copies(3, () => new Item { Type = KeyMM, Name = "Misery Mire Key" }));
            itemPool.AddRange(Copies(4, () => new Item { Type = KeyTR, Name = "Turtle Rock Key" }));
            itemPool.AddRange(Copies(4, () => new Item { Type = KeyGT, Name = "Ganons Tower Key" }));

            itemPool.AddRange(new[] {
                new Item { Type = MapHC, Name = "Sewer Map" },
                new Item { Type = MapEP, Name = "Eastern Palace Map" },
                new Item { Type = MapDP, Name = "Desert Palace Map" },
                new Item { Type = MapTH, Name = "Tower of Hera Map" },
                new Item { Type = MapPD, Name = "Palace of Darkness Map" },
                new Item { Type = MapSP, Name = "Swamp Palace Map" },
                new Item { Type = MapSW, Name = "Skull Woods Map" },
                new Item { Type = MapTT, Name = "Thieves Town Map" },
                new Item { Type = MapIP, Name = "Ice Palace Map" },
                new Item { Type = MapMM, Name = "Misery Mire Map" },
                new Item { Type = MapTR, Name = "Turtle Rock Map" },
                new Item { Type = MapGT, Name = "Ganons Tower Map" },

                new Item { Type = CompassEP, Name = "Eastern Palace Compass" },
                new Item { Type = CompassDP, Name = "Desert Palace Compass" },
                new Item { Type = CompassTH, Name = "Tower of Hera Compass" },
                new Item { Type = CompassPD, Name = "Palace of Darkness Compass" },
                new Item { Type = CompassSP, Name = "Swamp Palace Compass" },
                new Item { Type = CompassSW, Name = "Skull Woods Compass" },
                new Item { Type = CompassTT, Name = "Thieves Town Compass" },
                new Item { Type = CompassIP, Name = "Ice Palace Compass" },
                new Item { Type = CompassMM, Name = "Misery Mire Compass" },
                new Item { Type = CompassTR, Name = "Turtle Rock Compass" },
                new Item { Type = CompassGT, Name = "Ganons Tower Compass" },
            });

            foreach (var item in itemPool) item.World = world;

            return itemPool;
        }

        static List<Item> Copies(int nr, Func<Item> template) {
            return Enumerable.Range(1, nr).Select(i => template()).ToList();
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
