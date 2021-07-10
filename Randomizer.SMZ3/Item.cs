using System;
using System.Linq;
using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.SMLogic;
using static Randomizer.SMZ3.RewardType;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Randomizer.SMZ3 {

    public enum ItemType : byte {
        [Description("Nothing")] 
        Nothing,

        [Description("Hyrule Castle Map")]
        MapHC = 0x7F,
        [Description("Eastern Palace Map")]
        MapEP = 0x7D,
        [Description("Desert Palace Map")]
        MapDP = 0x7C,
        [Description("Tower of Hera Map")]
        MapTH = 0x75,
        [Description("Palace of Darkness Map")]
        MapPD = 0x79,
        [Description("Swamp Palace Map")]
        MapSP = 0x7A,
        [Description("Skull Woods Map")]
        MapSW = 0x77,
        [Description("Thieves Town Map")]
        MapTT = 0x74,
        [Description("Ice Palace Map")]
        MapIP = 0x76,
        [Description("Misery Mire Map")]
        MapMM = 0x78,
        [Description("Turtle Rock Map")]
        MapTR = 0x73,
        [Description("Ganons Tower Map")]
        MapGT = 0x72,

        [Description("Eastern Palace Compass")]
        CompassEP = 0x8D,
        [Description("Desert Palace Compass")]
        CompassDP = 0x8C,
        [Description("Tower of Hera Compass")]
        CompassTH = 0x85,
        [Description("Palace of Darkness Compass")]
        CompassPD = 0x89,
        [Description("Swamp Palace Compass")]
        CompassSP = 0x8A,
        [Description("Skull Woods Compass")]
        CompassSW = 0x87,
        [Description("Thieves Town Compass")]
        CompassTT = 0x84,
        [Description("Ice Palace Compass")]
        CompassIP = 0x86,
        [Description("Misery Mire Compass")]
        CompassMM = 0x88,
        [Description("Turtle Rock Compass")]
        CompassTR = 0x83,
        [Description("Ganons Tower Compass")]
        CompassGT = 0x82,

        [Description("Eastern Palace Big Key")]
        BigKeyEP = 0x9D,
        [Description("Desert Palace Big Key")]
        BigKeyDP = 0x9C,
        [Description("Tower of Hera Big Key")]
        BigKeyTH = 0x95,
        [Description("Palace of Darkness Big Key")]
        BigKeyPD = 0x99,
        [Description("Swamp Palace Big Key")]
        BigKeySP = 0x9A,
        [Description("Skull Woods Big Key")]
        BigKeySW = 0x97,
        [Description("Thieves Town Big Key")]
        BigKeyTT = 0x94,
        [Description("Ice Palace Big Key")]
        BigKeyIP = 0x96,
        [Description("Misery Mire Big Key")]
        BigKeyMM = 0x98,
        [Description("Turtle Rock Big Key")]
        BigKeyTR = 0x93,
        [Description("Ganons Tower Big Key")]
        BigKeyGT = 0x92,       
        
        [Description("Sewer Key")]
        KeyHC = 0xA0,
        [Description("Castle Tower Key")]
        KeyCT = 0xA4,
        [Description("Desert Palace Key")]
        KeyDP = 0xA3,
        [Description("Tower of Hera Key")]
        KeyTH = 0xAA,
        [Description("Palace of Darkness Key")]
        KeyPD = 0xA6,
        [Description("Swamp Palace Key")]
        KeySP = 0xA5,
        [Description("Skull Woods Key")]
        KeySW = 0xA8,
        [Description("Thieves Town Key")]
        KeyTT = 0xAB,
        [Description("Ice Palace Key")]
        KeyIP = 0xA9,
        [Description("Misery Mire Key")]
        KeyMM = 0xA7,
        [Description("Turtle Rock Key")]
        KeyTR = 0xAC,
        [Description("Ganons Tower Key")]
        KeyGT = 0xAD,

        [Description("Small Key")]
        Key = 0x24,
        [Description("Compass")]
        Compass = 0x25,
        [Description("Big Key")]
        BigKey = 0x32,
        [Description("Map")]
        Map = 0x33,


        [Description("Progressive Mail")]
        ProgressiveTunic = 0x60,
        [Description("Progressive Shield")]
        ProgressiveShield = 0x5F,
        [Description("Progressive Sword")]
        ProgressiveSword = 0x5E,
        [Description("Bow")]
        Bow = 0x0B,
        [Description("Silver Arrows")]
        SilverArrows = 0x58,
        [Description("Blue Boomerang")]
        BlueBoomerang = 0x0C,
        [Description("Red Boomerang")]
        RedBoomerang = 0x2A,
        [Description("Hookshot")]
        Hookshot = 0x0A,
        [Description("Mushroom")]
        Mushroom = 0x29,
        [Description("Magic Powder")]
        Powder = 0x0D,
        [Description("Fire Rod")]
        Firerod = 0x07,
        [Description("Ice Rod")]
        Icerod = 0x08,
        [Description("Bombos")]
        Bombos = 0x0f,
        [Description("Ether")]
        Ether = 0x10,
        [Description("Quake")]
        Quake = 0x11,
        [Description("Lamp")]
        Lamp = 0x12,
        [Description("Hammer")]
        Hammer = 0x09,
        [Description("Shovel")]
        Shovel = 0x13,
        [Description("Flute")]
        Flute = 0x14,
        [Description("Bug Catching Net")]
        Bugnet = 0x21,
        [Description("Book of Mudora")]
        Book = 0x1D,
        [Description("Bottle")]
        Bottle = 0x16,
        [Description("Cane of Somaria")]
        Somaria = 0x15,
        [Description("Cane of Byrna")]
        Byrna = 0x18,
        [Description("Magic Cape")]
        Cape = 0x19,
        [Description("Magic Mirror")]
        Mirror = 0x1A,
        [Description("Pegasus Boots")]
        Boots = 0x4B,
        [Description("Progressive Glove")]
        ProgressiveGlove = 0x61,
        [Description("Zora's Flippers")]
        Flippers = 0x1E,
        [Description("Moon Pearl")]
        MoonPearl = 0x1F,
        [Description("Half Magic")]
        HalfMagic = 0x4E,
        [Description("Piece of Heart")]
        HeartPiece = 0x17,
        [Description("Heart Container")]
        HeartContainer = 0x3E,
        [Description("Sanctuary Heart Container")]
        HeartContainerRefill = 0x3F,
        [Description("Three Bombs")]
        ThreeBombs = 0x28,
        [Description("Single Arrow")]
        Arrow = 0x43,
        [Description("Ten Arrows")]
        TenArrows = 0x44,
        [Description("One Rupee")]
        OneRupee = 0x34,
        [Description("Five Rupees")]
        FiveRupees = 0x35,
        [Description("Twenty Rupees")]
        TwentyRupees = 0x36,
        [Description("Twenty Rupees")]
        TwentyRupees2 = 0x47,
        [Description("Fifty Rupees")]
        FiftyRupees = 0x41,
        [Description("One Hundred Rupees")]
        OneHundredRupees = 0x40,
        [Description("Three Hundred Rupees")]
        ThreeHundredRupees = 0x46,
        [Description("+5 Bomb Capacity")]
        BombUpgrade5 = 0x51,
        [Description("+10 Bomb Capacity")]
        BombUpgrade10 = 0x52,
        [Description("+5 Arrow Capacity")]
        ArrowUpgrade5 = 0x53,
        [Description("+10 Arrow Capacity")]
        ArrowUpgrade10 = 0x54,

        [Description("Crateria Level 1 Keycard")]
        CardCrateriaL1 = 0xD0,
        [Description("Crateria Level 2 Keycard")]
        CardCrateriaL2 = 0xD1,
        [Description("Crateria Boss Keycard")]
        CardCrateriaBoss = 0xD2,
        [Description("Brinstar Level 1 Keycard")]
        CardBrinstarL1 = 0xD3,
        [Description("Brinstar Level 2 Keycard")]
        CardBrinstarL2 = 0xD4,
        [Description("Brinstar Boss Keycard")]
        CardBrinstarBoss = 0xD5,
        [Description("Norfair Level 1 Keycard")]
        CardNorfairL1 = 0xD6,
        [Description("Norfair Level 2 Keycard")]
        CardNorfairL2 = 0xD7,
        [Description("Norfair Boss Keycard")]
        CardNorfairBoss = 0xD8,
        [Description("Maridia Level 1 Keycard")]
        CardMaridiaL1 = 0xD9,
        [Description("Maridia Level 2 Keycard")]
        CardMaridiaL2 = 0xDA,
        [Description("Maridia Boss Keycard")]
        CardMaridiaBoss = 0xDB,
        [Description("Wrecked Ship Level 1 Keycard")]
        CardWreckedShipL1 = 0xDC,
        [Description("Wrecked Ship Boss Keycard")]
        CardWreckedShipBoss = 0xDD,
        [Description("Lower Norfair Level 1 Keycard")]
        CardLowerNorfairL1 = 0xDE,
        [Description("Lower Norfair Boss Keycard")]
        CardLowerNorfairBoss = 0xDF,

        [Description("Missile")]
        Missile = 0xC2,
        [Description("Super Missile")]
        Super = 0xC3,
        [Description("Power Bomb")]
        PowerBomb = 0xC4,
        [Description("Grappling Beam")]
        Grapple = 0xB0,
        [Description("X-Ray Scope")]
        XRay = 0xB1,
        [Description("Energy Tank")]
        ETank = 0xC0,
        [Description("Reserve Tank")]
        ReserveTank = 0xC1,
        [Description("Charge Beam")]
        Charge = 0xBB,
        [Description("Ice Beam")]
        Ice = 0xBC,
        [Description("Wave Beam")]
        Wave = 0xBD,
        [Description("Spazer")]
        Spazer = 0xBE,
        [Description("Plasma Beam")]
        Plasma = 0xBF,
        [Description("Varia Suit")]
        Varia = 0xB2,
        [Description("Gravity Suit")]
        Gravity = 0xB6,
        [Description("Morphing Ball")]
        Morph = 0xB4,
        [Description("Morph Bombs")]
        Bombs = 0xB9,
        [Description("Spring Ball")]
        SpringBall = 0xB3,
        [Description("Screw Attack")]
        ScrewAttack = 0xB5,
        [Description("Hi-Jump Boots")]
        HiJump = 0xB7,
        [Description("Space Jump")]
        SpaceJump = 0xB8,
        [Description("Speed Booster")]
        SpeedBooster = 0xBA,

        [Description("Bottle with Red Potion")]
        BottleWithRedPotion = 0x2B,
        [Description("Bottle with Green Potion")]
        BottleWithGreenPotion = 0x2C,
        [Description("Bottle with Blue Potion")]
        BottleWithBluePotion = 0x2D,
        [Description("Bottle with Fairy")]
        BottleWithFairy = 0x3D,
        [Description("Bottle with Bee")]
        BottleWithBee = 0x3C,
        [Description("Bottle with Gold Bee")]
        BottleWithGoldBee = 0x48,
        [Description("Red Potion Refill")]
        RedContent = 0x2E,
        [Description("Green Potion Refill")]
        GreenContent = 0x2F,
        [Description("Blue Potion Refill")]
        BlueContent = 0x30,
        [Description("Bee Refill")]
        BeeContent = 0x0E,
    }

    class Item {

        public string Name { get; set; }
        public ItemType Type { get; set; }
        public bool Progression { get; set; }
        public World World { get; set; }

        static readonly Regex dungeon = new("^(BigKey|Key|Map|Compass)");
        static readonly Regex bigKey = new("^BigKey");
        static readonly Regex key = new("^Key");
        static readonly Regex map = new("^Map");
        static readonly Regex compass = new("^Compass");
        static readonly Regex keycard = new("^Card");

        public bool IsDungeonItem => dungeon.IsMatch(Type.ToString());
        public bool IsBigKey => bigKey.IsMatch(Type.ToString());
        public bool IsKey => key.IsMatch(Type.ToString());
        public bool IsMap => map.IsMatch(Type.ToString());
        public bool IsCompass => compass.IsMatch(Type.ToString());
        public bool IsKeycard => keycard.IsMatch(Type.ToString());

        public bool Is(ItemType type, World world) => Type == type && World == world;
        public bool IsNot(ItemType type, World world) => !Is(type, world);

        public Item(ItemType itemType) {
            Name = itemType.GetDescription();
            Type = itemType;
        }

        public Item(ItemType itemType, World world) : this(itemType) {
            World = world;
        }

        public static Item Nothing(World world) {
            return new Item(ItemType.Nothing, world);
        }

        public static List<Item> CreateProgressionPool(World world) {
            var itemPool = new List<Item> {
                new Item(ProgressiveShield),
                new Item(ProgressiveShield),
                new Item(ProgressiveShield),
                new Item(ProgressiveSword),
                new Item(ProgressiveSword),
                new Item(Bow),
                new Item(Hookshot),
                new Item(Mushroom),
                new Item(Powder),
                new Item(Firerod),
                new Item(Icerod),
                new Item(Bombos),
                new Item(Ether),
                new Item(Quake),
                new Item(Lamp),
                new Item(Hammer),
                new Item(Shovel),
                new Item(Flute),
                new Item(Bugnet),
                new Item(Book),
                new Item(Bottle),
                new Item(Somaria),
                new Item(Byrna),
                new Item(Cape),
                new Item(Mirror),
                new Item(Boots),
                new Item(ProgressiveGlove),
                new Item(ProgressiveGlove),
                new Item(Flippers),
                new Item(MoonPearl),
                new Item(HalfMagic),

                new Item(Grapple),
                new Item(Charge),
                new Item(Ice),
                new Item(Wave),
                new Item(Plasma),
                new Item(Varia),
                new Item(Gravity),
                new Item(Morph),
                new Item(Bombs),
                new Item(SpringBall),
                new Item(ScrewAttack),
                new Item(HiJump),
                new Item(SpaceJump),
                new Item(SpeedBooster),

                new Item(Missile),
                new Item(Super),
                new Item(PowerBomb),
                new Item(PowerBomb),
                new Item(ETank),
                new Item(ETank),
                new Item(ETank),
                new Item(ETank),
                new Item(ETank),

                new Item(ReserveTank),
                new Item(ReserveTank),
                new Item(ReserveTank),
                new Item(ReserveTank),
            };

            foreach (var item in itemPool) {
                item.Progression = true;
                item.World = world;
            }

            return itemPool;
        }

        public static List<Item> CreateNicePool(World world) {
            var itemPool = new List<Item> {
                new Item(ProgressiveTunic),
                new Item(ProgressiveTunic),
                new Item(ProgressiveSword),
                new Item(ProgressiveSword),
                new Item(SilverArrows),
                new Item(BlueBoomerang),
                new Item(RedBoomerang),
                new Item(Bottle),
                new Item(Bottle),
                new Item(Bottle),
                new Item(HeartContainerRefill),

                new Item(Spazer),
                new Item(XRay),
            };

            itemPool.AddRange(Copies(10, () => new Item(HeartContainer, world)));

            foreach (var item in itemPool) item.World = world;

            return itemPool;
        }

        public static List<Item> CreateJunkPool(World world) {
            var itemPool = new List<Item> {
                new Item(Arrow),
                new Item(OneHundredRupees)
            };

            itemPool.AddRange(Copies(24, () => new Item(HeartPiece)));
            itemPool.AddRange(Copies(8,  () => new Item(TenArrows)));
            itemPool.AddRange(Copies(13, () => new Item(ThreeBombs)));
            itemPool.AddRange(Copies(4,  () => new Item(ArrowUpgrade5)));
            itemPool.AddRange(Copies(4,  () => new Item(BombUpgrade5)));
            itemPool.AddRange(Copies(2,  () => new Item(OneRupee)));
            itemPool.AddRange(Copies(4,  () => new Item(FiveRupees)));
            itemPool.AddRange(Copies(world.Config.Keysanity ? 25 : 28, () => new Item(TwentyRupees)));
            itemPool.AddRange(Copies(7,  () => new Item(FiftyRupees)));
            itemPool.AddRange(Copies(5,  () => new Item(ThreeHundredRupees)));

            itemPool.AddRange(Copies(9,  () => new Item(ETank)));
            itemPool.AddRange(Copies(39, () => new Item(Missile)));
            itemPool.AddRange(Copies(15, () => new Item(Super)));
            itemPool.AddRange(Copies(8,  () => new Item(PowerBomb)));

            foreach (var item in itemPool) item.World = world;

            return itemPool;
        }

        /* The order of the dungeon pool is significant */
        public static List<Item> CreateDungeonPool(World world) {
            var itemPool = new List<Item>();

            itemPool.AddRange(new[] {
                new Item(BigKeyEP),
                new Item(BigKeyDP),
                new Item(BigKeyTH),
                new Item(BigKeyPD),
                new Item(BigKeySP),
                new Item(BigKeySW),
                new Item(BigKeyTT),
                new Item(BigKeyIP),
                new Item(BigKeyMM),
                new Item(BigKeyTR),
                new Item(BigKeyGT),
            });

            itemPool.AddRange(Copies(1, () => new Item(KeyHC)));
            itemPool.AddRange(Copies(2, () => new Item(KeyCT)));
            itemPool.AddRange(Copies(1, () => new Item(KeyDP)));
            itemPool.AddRange(Copies(1, () => new Item(KeyTH)));
            itemPool.AddRange(Copies(6, () => new Item(KeyPD)));
            itemPool.AddRange(Copies(1, () => new Item(KeySP)));
            itemPool.AddRange(Copies(3, () => new Item(KeySW)));
            itemPool.AddRange(Copies(1, () => new Item(KeyTT)));
            itemPool.AddRange(Copies(2, () => new Item(KeyIP)));
            itemPool.AddRange(Copies(3, () => new Item(KeyMM)));
            itemPool.AddRange(Copies(4, () => new Item(KeyTR)));
            itemPool.AddRange(Copies(4, () => new Item(KeyGT)));

            itemPool.AddRange(new[] {
                new Item(MapEP),
                new Item(MapDP),
                new Item(MapTH),
                new Item(MapPD),
                new Item(MapSP),
                new Item(MapSW),
                new Item(MapTT),
                new Item(MapIP),
                new Item(MapMM),
                new Item(MapTR),
            });
            if (!world.Config.Keysanity) {
                itemPool.AddRange(new[] {
                    new Item(MapHC),
                    new Item(MapGT),
                    new Item(CompassEP),
                    new Item(CompassDP),
                    new Item(CompassTH),
                    new Item(CompassPD),
                    new Item(CompassSP),
                    new Item(CompassSW),
                    new Item(CompassTT),
                    new Item(CompassIP),
                    new Item(CompassMM),
                    new Item(CompassTR),
                    new Item(CompassGT),
                });
            }

            foreach (var item in itemPool) item.World = world;

            return itemPool;
        }

        public static List<Item> CreateKeycards(World world) {
            return new List<Item> {
                new Item(CardCrateriaL1, world),
                new Item(CardCrateriaL2, world),
                new Item(CardCrateriaBoss, world),
                new Item(CardBrinstarL1, world),
                new Item(CardBrinstarL2, world),
                new Item(CardBrinstarBoss, world),
                new Item(CardNorfairL1, world),
                new Item(CardNorfairL2, world),
                new Item(CardNorfairBoss, world),
                new Item(CardMaridiaL1, world),
                new Item(CardMaridiaL2, world),
                new Item(CardMaridiaBoss, world),
                new Item(CardWreckedShipL1, world),
                new Item(CardWreckedShipBoss, world),
                new Item(CardLowerNorfairL1, world),
                new Item(CardLowerNorfairBoss, world),
            };
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
            var item = items.FirstOrDefault(i => i.Is(itemType, world));
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
        public bool CardCrateriaL1 { get; private set; }
        public bool CardCrateriaL2 { get; private set; }
        public bool CardCrateriaBoss { get; private set; }
        public bool CardBrinstarL1 { get; private set; }
        public bool CardBrinstarL2 { get; private set; }
        public bool CardBrinstarBoss { get; private set; }
        public bool CardNorfairL1 { get; private set; }
        public bool CardNorfairL2 { get; private set; }
        public bool CardNorfairBoss { get; private set; }
        public bool CardMaridiaL1 { get; private set; }
        public bool CardMaridiaL2 { get; private set; }
        public bool CardMaridiaBoss { get; private set; }
        public bool CardWreckedShipL1 { get; private set; }
        public bool CardWreckedShipBoss { get; private set; }
        public bool CardLowerNorfairL1 { get; private set; }
        public bool CardLowerNorfairBoss { get; private set; }
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
                    ItemType.CardCrateriaL1 => CardCrateriaL1 = true,
                    ItemType.CardCrateriaL2 => CardCrateriaL2 = true,
                    ItemType.CardCrateriaBoss => CardCrateriaBoss = true,
                    ItemType.CardBrinstarL1 => CardBrinstarL1 = true,
                    ItemType.CardBrinstarL2 => CardBrinstarL2 = true,
                    ItemType.CardBrinstarBoss => CardBrinstarBoss = true,
                    ItemType.CardNorfairL1 => CardNorfairL1 = true,
                    ItemType.CardNorfairL2 => CardNorfairL2 = true,
                    ItemType.CardNorfairBoss => CardNorfairBoss = true,
                    ItemType.CardMaridiaL1 => CardMaridiaL1 = true,
                    ItemType.CardMaridiaL2 => CardMaridiaL2 = true,
                    ItemType.CardMaridiaBoss => CardMaridiaBoss = true,
                    ItemType.CardWreckedShipL1 => CardWreckedShipL1 = true,
                    ItemType.CardWreckedShipBoss => CardWreckedShipBoss = true,
                    ItemType.CardLowerNorfairL1 => CardLowerNorfairL1 = true,
                    ItemType.CardLowerNorfairBoss => CardLowerNorfairBoss = true,
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
                Normal =>
                    items.CardMaridiaL1 && items.CardMaridiaL2 && items.CanUsePowerBombs() && items.Super && items.Gravity && items.SpeedBooster,
                _ =>
                    items.CardMaridiaL1 && items.CardMaridiaL2 && items.CanUsePowerBombs() && items.Super &&
                    (items.Charge || items.Super && items.Missile) &&
                    (items.Gravity || items.HiJump && items.Ice && items.Grapple) &&
                    (items.Ice || items.Gravity && items.SpeedBooster)
            };
        }

        public static bool CanAccessMiseryMirePortal(this Progression items, Config config) {
            return config.SMLogic switch {
                Normal =>
                    (items.CardNorfairL2 || items.SpeedBooster && items.Wave) && items.Varia && items.Super &&
                        items.Gravity && items.SpaceJump && items.CanUsePowerBombs(),
                _ =>
                    (items.CardNorfairL2 || items.SpeedBooster) && items.Varia && items.Super && (
                        items.CanFly() || items.HiJump || items.SpeedBooster || items.CanSpringBallJump() || items.Ice
                    ) && (items.Gravity || items.HiJump) && items.CanUsePowerBombs()
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
                Normal =>
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
