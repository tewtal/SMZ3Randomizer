using System.Collections.Generic;
using System.ComponentModel;

namespace Randomizer.SMZ3 {

    [DefaultValue(Normal)]
    enum GameMode {
        [Description("Single player")]
        Normal,
        [Description("Multiworld")]
        Multiworld,
    }

    [DefaultValue(Normal)]
    enum Z3Logic {
        [Description("Normal")]
        Normal,
        [Description("No major glitches")]
        Nmg,
        [Description("Overworld glitches")]
        Owg,
    }

    [DefaultValue(Normal)]
    enum SMLogic {
        [Description("Normal")]
        Normal,
        [Description("Hard")]
        Hard,
    }

    [DefaultValue(Default)]
    enum SkillSetting {
        [Description("Default (per logic)")]
        Default,
        [Description("Enabled")]
        Enabled,
        [Description("Disabled")]
        Disabled,
    }

    /// <summary>
    /// The Super Metroid / dungeon tricks the player is expected to be able to perform.
    /// <see cref="SMLogic"/> only selects a preset of these, each can be overridden individually.
    /// </summary>
    class SMSkills {

        /// <summary>Generic "hard" Super Metroid routes (everything the Hard logic adds over Normal).</summary>
        public bool HardSkills { get; set; }
        /// <summary>Infinite bomb jumping, Morph + Bombs counts as flight.</summary>
        public bool InfiniteBombJump { get; set; }
        /// <summary>Mockball (entering morph-only passages without stopping).</summary>
        public bool MockBall { get; set; }
        /// <summary>Short charge / short spark with a minimal speed booster runway.</summary>
        public bool ShortSpark { get; set; }
        /// <summary>Energy tank / reserve requirements are ignored.</summary>
        public bool HealthIndifferent { get; set; }
        /// <summary>Dark rooms can be navigated without the Lamp.</summary>
        public bool DarkRooms { get; set; }

        public static SMSkills Preset(SMLogic logic) => logic switch {
            SMLogic.Hard => new SMSkills { HardSkills = true, InfiniteBombJump = true },
            _ => new SMSkills { InfiniteBombJump = true },
        };

    }

    [DefaultValue(Randomized)]
    enum SwordLocation {
        [Description("Randomized")]
        Randomized,
        [Description("Early")]
        Early,
        [Description("Uncle assured")]
        Uncle,
    }

    [DefaultValue(Randomized)]
    enum MorphLocation {
        [Description("Randomized")]
        Randomized,
        [Description("Early")]
        Early,
        [Description("Original location")]
        Original,
    }

    [DefaultValue(DefeatBoth)]
    enum Goal {
        [Description("Defeat Ganon and Mother Brain")]
        DefeatBoth,
        [Description("Fast Ganon and Defeat Mother Brain")]
        FastGanonDefeatMotherBrain,
        [Description("All Dungeons and Defeat Mother Brain")]
        AllDungeonsDefeatMotherBrain,
    }

    [DefaultValue(None)]
    enum KeyShuffle {
        [Description("None")]
        None,
        [Description("Keysanity")]
        Keysanity
    }

    [DefaultValue(SevenCrystals)]
    enum OpenTower {
        [Description("Random")]
        Random = -1,
        [Description("No Crystals")]
        NoCrystals = 0,
        [Description("One Crystal")]
        OneCrystal = 1,
        [Description("Two Crystals")]
        TwoCrystals = 2,
        [Description("Three Crystals")]
        ThreeCrystals = 3,
        [Description("Four Crystals")]
        FourCrystals = 4,
        [Description("Five Crystals")]
        FiveCrystals = 5,
        [Description("Six Crystals")]
        SixCrystals = 6,
        [Description("Seven Crystals")]
        SevenCrystals = 7,
    }

    [DefaultValue(SevenCrystals)]
    enum GanonVulnerable {
        [Description("Random")]
        Random = -1,
        [Description("No Crystals")]
        NoCrystals = 0,
        [Description("One Crystal")]
        OneCrystal = 1,
        [Description("Two Crystals")]
        TwoCrystals = 2,
        [Description("Three Crystals")]
        ThreeCrystals = 3,
        [Description("Four Crystals")]
        FourCrystals = 4,
        [Description("Five Crystals")]
        FiveCrystals = 5,
        [Description("Six Crystals")]
        SixCrystals = 6,
        [Description("Seven Crystals")]
        SevenCrystals = 7,
    }

    [DefaultValue(FourBosses)]
    enum OpenTourian {
        [Description("Random")]
        Random = -1,
        [Description("No Bosses")]
        NoBosses = 0,
        [Description("One Boss")]
        OneBoss = 1,
        [Description("Two Bosses")]
        TwoBosses = 2,
        [Description("Three Bosses")]
        ThreeBosses = 3,
        [Description("Four Bosses")]
        FourBosses = 4,
    }

    class Config {

        public GameMode GameMode { get; set; } = GameMode.Normal;
        public Z3Logic Z3Logic { get; set; } = Z3Logic.Normal;
        SMLogic smLogic = SMLogic.Normal;
        SMSkills skills = SMSkills.Preset(SMLogic.Normal);

        /// <summary>Selecting a logic resets <see cref="Skills"/> to that logic's preset.</summary>
        public SMLogic SMLogic {
            get => smLogic;
            set {
                smLogic = value;
                skills = SMSkills.Preset(value);
            }
        }
        public SMSkills Skills { get => skills; set => skills = value; }
        public SwordLocation SwordLocation { get; set; } = SwordLocation.Randomized;
        public MorphLocation MorphLocation { get; set; } = MorphLocation.Randomized;
        public Goal Goal { get; set; } = Goal.DefeatBoth;
        public KeyShuffle KeyShuffle { get; set; } = KeyShuffle.None;
        public bool Race { get; set; } = false;
        public OpenTower OpenTower { get; set; } = OpenTower.SevenCrystals;
        public GanonVulnerable GanonVulnerable { get; set; } = GanonVulnerable.SevenCrystals;
        public OpenTourian OpenTourian { get; set; } = OpenTourian.FourBosses;
        public Dictionary<ItemType, int> InitialItems = new Dictionary<ItemType, int>();
        /// <summary>Latest sphere each copy of an item may appear in, sorted ascending. Items not listed are random.</summary>
        public Dictionary<ItemType, List<int>> ItemSpheres { get; set; } = new Dictionary<ItemType, List<int>>();

        public bool SingleWorld => GameMode == GameMode.Normal;
        public bool MultiWorld => GameMode == GameMode.Multiworld;
        public bool Keysanity => KeyShuffle != KeyShuffle.None;

    }

}
