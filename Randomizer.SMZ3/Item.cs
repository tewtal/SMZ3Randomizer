using System;
using System.Linq;
using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.SMLogic;
using static Randomizer.SMZ3.RewardType;
using System.Text.RegularExpressions;

namespace Randomizer.SMZ3 {

    public enum ItemType : byte {
        Nothing,

        MapHC = 0x7F,
        MapEP = 0x7D,
        MapDP = 0x7C,
        MapTH = 0x75,
        MapPD = 0x79,
        MapSP = 0x7A,
        MapSW = 0x77,
        MapTT = 0x74,
        MapIP = 0x76,
        MapMM = 0x78,
        MapTR = 0x73,
        MapGT = 0x72,
        CompassEP = 0x8D,
        CompassDP = 0x8C,
        CompassTH = 0x85,
        CompassPD = 0x89,
        CompassSP = 0x8A,
        CompassSW = 0x87,
        CompassTT = 0x84,
        CompassIP = 0x86,
        CompassMM = 0x88,
        CompassTR = 0x83,
        CompassGT = 0x82,
        BigKeyEP = 0x9D,
        BigKeyDP = 0x9C,
        BigKeyTH = 0x95,
        BigKeyPD = 0x99,
        BigKeySP = 0x9A,
        BigKeySW = 0x97,
        BigKeyTT = 0x94,
        BigKeyIP = 0x96,
        BigKeyMM = 0x98,
        BigKeyTR = 0x93,
        BigKeyGT = 0x92,
        KeyHC = 0xA0,
        KeyCT = 0xA4,
        KeyDP = 0xA3,
        KeyTH = 0xAA,
        KeyPD = 0xA6,
        KeySP = 0xA5,
        KeySW = 0xA8,
        KeyTT = 0xAB,
        KeyIP = 0xA9,
        KeyMM = 0xA7,
        KeyTR = 0xAC,
        KeyGT = 0xAD,

        Key = 0x24,
        Compass = 0x25,
        BigKey = 0x32,
        Map = 0x33,

        ProgressiveTunic = 0x60,
        ProgressiveShield = 0x5F,
        ProgressiveSword = 0x5E,
        Bow = 0x0B,
        SilverArrows = 0x58,
        BlueBoomerang = 0x0C,
        RedBoomerang = 0x2A,
        Hookshot = 0x0A,
        Mushroom = 0x29,
        Powder = 0x0D,
        Firerod = 0x07,
        Icerod = 0x08,
        Bombos = 0x0f,
        Ether = 0x10,
        Quake = 0x11,
        Lamp = 0x12,
        Hammer = 0x09,
        Shovel = 0x13,
        Flute = 0x14,
        Bugnet = 0x21,
        Book = 0x1D,
        Bottle = 0x16,
        Somaria = 0x15,
        Byrna = 0x18,
        Cape = 0x19,
        Mirror = 0x1A,
        Boots = 0x4B,
        ProgressiveGlove = 0x61,
        Flippers = 0x1E,
        MoonPearl = 0x1F,
        HalfMagic = 0x4E,
        HeartPiece = 0x17,
        HeartContainer = 0x3E,
        HeartContainerRefill = 0x3F,
        ThreeBombs = 0x28,
        Arrow = 0x43,
        TenArrows = 0x44,
        OneRupee = 0x34,
        FiveRupees = 0x35,
        TwentyRupees = 0x36,
        TwentyRupees2 = 0x47,
        FiftyRupees = 0x41,
        OneHundredRupees = 0x40,
        ThreeHundredRupees = 0x46,
        BombUpgrade5 = 0x51,
        BombUpgrade10 = 0x52,
        ArrowUpgrade5 = 0x53,
        ArrowUpgrade10 = 0x54,

        KraidKey/* = ?*/,
        PhantoonKey/* = ?*/,
        DraygonKey/* = ?*/,
        RidleyKey/* = ?*/,

        Missile = 0xC2,
        Super = 0xC3,
        PowerBomb = 0xC4,
        Grapple = 0xB0,
        XRay = 0xB1,
        ETank = 0xC0,
        ReserveTank = 0xC1,
        Charge = 0xBB,
        Ice = 0xBC,
        Wave = 0xBD,
        Spazer = 0xBE,
        Plasma = 0xBF,
        Varia = 0xB2,
        Gravity = 0xB6,
        Morph = 0xB4,
        Bombs = 0xB9,
        SpringBall = 0xB3,
        ScrewAttack = 0xB5,
        HiJump = 0xB7,
        SpaceJump = 0xB8,
        SpeedBooster = 0xBA,

        BottleWithRedPotion = 0x2B,
        BottleWithGreenPotion = 0x2C,
        BottleWithBluePotion = 0x2D,
        BottleWithFairy = 0x3D,
        BottleWithBee = 0x3C,
        BottleWithGoldBee = 0x48,
        RedContent = 0x2E,
        GreenContent = 0x2F,
        BlueContent = 0x30,
        BeeContent = 0x0E,
    }

    class Item {

        public string Name { get; set; }
        public ItemType Type { get; set; }
        public bool Progression { get; set; }
        public World World { get; set; }

        static readonly Regex dungeon = new Regex("^(BigKey|Key|Map|Compass)");
        static readonly Regex bigKey = new Regex("^BigKey");
        static readonly Regex key = new Regex("^Key");
        static readonly Regex map = new Regex("^Map");
        static readonly Regex compass = new Regex("^Compass");

        public bool IsDungeonItem => dungeon.IsMatch(Type.ToString());
        public bool IsBigKey => bigKey.IsMatch(Type.ToString());
        public bool IsKey => key.IsMatch(Type.ToString());
        public bool IsMap => map.IsMatch(Type.ToString());
        public bool IsCompass => compass.IsMatch(Type.ToString());

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

                new Item { Name = "Reserve Tank", Type = ReserveTank },
                new Item { Name = "Reserve Tank", Type = ReserveTank },
                new Item { Name = "Reserve Tank", Type = ReserveTank },
                new Item { Name = "Reserve Tank", Type = ReserveTank },
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

    static class ItemsExtensions {

        public static Item Get(this IEnumerable<Item> items, ItemType itemType) {
            var item = items.FirstOrDefault(i => i.Type == itemType);
            if (item == null)
                throw new InvalidOperationException($"Could not find an item of type {itemType}");
            return item;
        }

        public static Item Get(this IEnumerable<Item> items, ItemType itemType, World world) {
            var item = items.FirstOrDefault(i => i.Type == itemType && i.World == world);
            if (item == null)
                throw new InvalidOperationException($"Could not find an item of type {itemType} in world {world.Id}");
            return item;
        }

    }

    class Progression {

        public bool BigKeyEP { get; private set; }
        public bool BigKeyDP { get; private set; }
        public bool BigKeyTH { get; private set; }
        public bool BigKeyPD { get; private set; }
        public bool BigKeySP { get; private set; }
        public bool BigKeySW { get; private set; }
        public bool BigKeyTT { get; private set; }
        public bool BigKeyIP { get; private set; }
        public bool BigKeyMM { get; private set; }
        public bool BigKeyTR { get; private set; }
        public bool BigKeyGT { get; private set; }
        public bool KeyHC { get; private set; }
        public bool KeyDP { get; private set; }
        public bool KeyTH { get; private set; }
        public bool KeySP { get; private set; }
        public bool KeyTT { get; private set; }
        public int KeyCT { get; private set; }
        public int KeyPD { get; private set; }
        public int KeySW { get; private set; }
        public int KeyIP { get; private set; }
        public int KeyMM { get; private set; }
        public int KeyTR { get; private set; }
        public int KeyGT { get; private set; }
        public bool KraidKey { get; private set; }
        public bool PhantoonKey { get; private set; }
        public bool DraygonKey { get; private set; }
        public bool RidleyKey { get; private set; }
        public bool CanBlockLasers { get { return shield >= 3; } }
        public bool Sword { get; private set; }
        public bool MasterSword { get; private set; }
        public bool Bow { get; private set; }
        public bool Hookshot { get; private set; }
        public bool Mushroom { get; private set; }
        public bool Powder { get; private set; }
        public bool Firerod { get; private set; }
        public bool Icerod { get; private set; }
        public bool Bombos { get; private set; }
        public bool Ether { get; private set; }
        public bool Quake { get; private set; }
        public bool Lamp { get; private set; }
        public bool Hammer { get; private set; }
        public bool Shovel { get; private set; }
        public bool Flute { get; private set; }
        public bool Book { get; private set; }
        public bool Bottle { get; private set; }
        public bool Somaria { get; private set; }
        public bool Byrna { get; private set; }
        public bool Cape { get; private set; }
        public bool Mirror { get; private set; }
        public bool Boots { get; private set; }
        public bool Glove { get; private set; }
        public bool Mitt { get; private set; }
        public bool Flippers { get; private set; }
        public bool MoonPearl { get; private set; }
        public bool HalfMagic { get; private set; }
        public bool Grapple { get; private set; }
        public bool Charge { get; private set; }
        public bool Ice { get; private set; }
        public bool Wave { get; private set; }
        public bool Plasma { get; private set; }
        public bool Varia { get; private set; }
        public bool Gravity { get; private set; }
        public bool Morph { get; private set; }
        public bool Bombs { get; private set; }
        public bool SpringBall { get; private set; }
        public bool ScrewAttack { get; private set; }
        public bool HiJump { get; private set; }
        public bool SpaceJump { get; private set; }
        public bool SpeedBooster { get; private set; }
        public bool Missile { get; private set; }
        public bool Super { get; private set; }
        public bool PowerBomb { get; private set; }
        public bool TwoPowerBombs { get; private set; }
        public int ETank { get; private set; }
        public int ReserveTank { get; private set; }

        int shield;

        public Progression(IEnumerable<Item> items) {
            Add(items);
        }

        public void Add(IEnumerable<Item> items) {
            foreach (var item in items) {
                bool done = item.Type switch {
                    ItemType.BigKeyEP => BigKeyEP = true,
                    ItemType.BigKeyDP => BigKeyDP = true,
                    ItemType.BigKeyTH => BigKeyTH = true,
                    ItemType.BigKeyPD => BigKeyPD = true,
                    ItemType.BigKeySP => BigKeySP = true,
                    ItemType.BigKeySW => BigKeySW = true,
                    ItemType.BigKeyTT => BigKeyTT = true,
                    ItemType.BigKeyIP => BigKeyIP = true,
                    ItemType.BigKeyMM => BigKeyMM = true,
                    ItemType.BigKeyTR => BigKeyTR = true,
                    ItemType.BigKeyGT => BigKeyGT = true,
                    ItemType.KeyHC => KeyHC = true,
                    ItemType.KeyDP => KeyDP = true,
                    ItemType.KeyTH => KeyTH = true,
                    ItemType.KeySP => KeySP = true,
                    ItemType.KeyTT => KeyTT = true,
                    ItemType.KraidKey => KraidKey = true,
                    ItemType.PhantoonKey => PhantoonKey = true,
                    ItemType.DraygonKey => DraygonKey = true,
                    ItemType.RidleyKey => RidleyKey = true,
                    ItemType.Bow => Bow = true,
                    ItemType.Hookshot => Hookshot = true,
                    ItemType.Mushroom => Mushroom = true,
                    ItemType.Powder => Powder = true,
                    ItemType.Firerod => Firerod = true,
                    ItemType.Icerod => Icerod = true,
                    ItemType.Bombos => Bombos = true,
                    ItemType.Ether => Ether = true,
                    ItemType.Quake => Quake = true,
                    ItemType.Lamp => Lamp = true,
                    ItemType.Hammer => Hammer = true,
                    ItemType.Shovel => Shovel = true,
                    ItemType.Flute => Flute = true,
                    ItemType.Book => Book = true,
                    ItemType.Bottle => Bottle = true,
                    ItemType.Somaria => Somaria = true,
                    ItemType.Byrna => Byrna = true,
                    ItemType.Cape => Cape = true,
                    ItemType.Mirror => Mirror = true,
                    ItemType.Boots => Boots = true,
                    ItemType.Flippers => Flippers = true,
                    ItemType.MoonPearl => MoonPearl = true,
                    ItemType.HalfMagic => HalfMagic = true,
                    ItemType.Grapple => Grapple = true,
                    ItemType.Charge => Charge = true,
                    ItemType.Ice => Ice = true,
                    ItemType.Wave => Wave = true,
                    ItemType.Plasma => Plasma = true,
                    ItemType.Varia => Varia = true,
                    ItemType.Gravity => Gravity = true,
                    ItemType.Morph => Morph = true,
                    ItemType.Bombs => Bombs = true,
                    ItemType.SpringBall => SpringBall = true,
                    ItemType.ScrewAttack => ScrewAttack = true,
                    ItemType.HiJump => HiJump = true,
                    ItemType.SpaceJump => SpaceJump = true,
                    ItemType.SpeedBooster => SpeedBooster = true,
                    ItemType.Missile => Missile = true,
                    ItemType.Super => Super = true,
                    _ => false
                };

                if (done)
                    continue;

                switch (item.Type) {
                    case ItemType.KeyCT: KeyCT += 1; break;
                    case ItemType.KeyPD: KeyPD += 1; break;
                    case ItemType.KeySW: KeySW += 1; break;
                    case ItemType.KeyIP: KeyIP += 1; break;
                    case ItemType.KeyMM: KeyMM += 1; break;
                    case ItemType.KeyTR: KeyTR += 1; break;
                    case ItemType.KeyGT: KeyGT += 1; break;
                    case ItemType.ETank: ETank += 1; break;
                    case ItemType.ReserveTank: ReserveTank += 1; break;
                    case ProgressiveShield: shield += 1; break;
                    case ProgressiveSword:
                        MasterSword = Sword;
                        Sword = true;
                        break;
                    case ProgressiveGlove:
                        Mitt = Glove;
                        Glove = true;
                        break;
                    case ItemType.PowerBomb:
                        TwoPowerBombs = PowerBomb;
                        PowerBomb = true;
                        break;
                }
            }
        }

    }

    static class ProgressionExtensions {

        public static bool CanLiftLight(this Progression items) => items.Glove;
        public static bool CanLiftHeavy(this Progression items) => items.Mitt;

        public static bool CanLightTorches(this Progression items) {
            return items.Firerod || items.Lamp;
        }

        public static bool CanMeltFreezors(this Progression items) {
            return items.Firerod || items.Bombos && items.Sword;
        }

        public static bool CanExtendMagic(this Progression items, int bars = 2) {
            return (items.HalfMagic ? 2 : 1) * (items.Bottle ? 2 : 1) >= bars;
        }

        public static bool CanKillManyEnemies(this Progression items) {
            return items.Sword || items.Hammer || items.Bow || items.Firerod ||
                items.Somaria || items.Byrna && items.CanExtendMagic();
        }

        public static bool CanAccessDeathMountainPortal(this Progression items) {
            return (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph;
        }

        public static bool CanAccessDarkWorldPortal(this Progression items, Config config) {
            return config.SMLogic switch {
                Casual =>
                    items.CanUsePowerBombs() && items.Super && items.Gravity && items.SpeedBooster,
                _ =>
                    items.CanUsePowerBombs() && items.Super &&
                    (items.Charge || items.Super && items.Missile) &&
                    (items.Gravity || items.HiJump && items.Ice && items.Grapple) &&
                    (items.Ice || items.Gravity && items.SpeedBooster)
            };
        }

        public static bool CanAccessMiseryMirePortal(this Progression items, Config config) {
            return config.SMLogic switch {
                Casual =>
                    items.Varia && items.Super && (items.Gravity && items.SpaceJump) && items.CanUsePowerBombs(),
                _ =>
                    items.Varia && items.Super && (items.Gravity || items.HiJump) && items.CanUsePowerBombs()
            };
        }

        public static bool CanIbj(this Progression items) {
            return items.Morph && items.Bombs;
        }

        public static bool CanFly(this Progression items) {
            return items.SpaceJump || items.CanIbj();
        }

        public static bool CanUsePowerBombs(this Progression items) {
            return items.Morph && items.PowerBomb;
        }

        public static bool CanPassBombPassages(this Progression items) {
            return items.Morph && (items.Bombs || items.PowerBomb);
        }

        public static bool CanDestroyBombWalls(this Progression items) {
            return items.CanPassBombPassages() || items.ScrewAttack;
        }

        public static bool CanSpringBallJump(this Progression items) {
            return items.Morph && items.SpringBall;
        }

        public static bool CanHellRun(this Progression items) {
            return items.Varia || items.HasEnergyReserves(5);
        }

        public static bool HasEnergyReserves(this Progression items, int amount) {
            return (items.ETank + items.ReserveTank) >= amount;
        }

        public static bool CanOpenRedDoors(this Progression items) {
            return items.Missile || items.Super;
        }

        public static bool CanAccessNorfairUpperPortal(this Progression items) {
            return items.Flute || items.CanLiftLight() && items.Lamp;
        }

        public static bool CanAccessNorfairLowerPortal(this Progression items) {
            return items.Flute && items.CanLiftHeavy();
        }

        public static bool CanAccessMaridiaPortal(this Progression items, World world) {
            return world.Config.SMLogic switch {
                Casual =>
                    items.MoonPearl && items.Flippers &&
                    items.Gravity && items.Morph &&
                    (world.CanAquire(items, Agahnim) || items.Hammer && items.CanLiftLight() || items.CanLiftHeavy()),
                _ =>
                    items.MoonPearl && items.Flippers &&
                    (items.CanSpringBallJump() || items.HiJump || items.Gravity) && items.Morph &&
                    (world.CanAquire(items, Agahnim) || items.Hammer && items.CanLiftLight() || items.CanLiftHeavy())
            };
        }

    }

}
