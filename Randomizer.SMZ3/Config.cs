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
        public SMLogic SMLogic { get; set; } = SMLogic.Normal;
        public SwordLocation SwordLocation { get; set; } = SwordLocation.Randomized;
        public MorphLocation MorphLocation { get; set; } = MorphLocation.Randomized;
        public Goal Goal { get; set; } = Goal.DefeatBoth;
        public KeyShuffle KeyShuffle { get; set; } = KeyShuffle.None;
        public bool Race { get; set; } = false;
        public OpenTower OpenTower { get; set; } = OpenTower.SevenCrystals;
        public GanonVulnerable GanonVulnerable { get; set; } = GanonVulnerable.SevenCrystals;
        public OpenTourian OpenTourian { get; set; } = OpenTourian.FourBosses;

        public bool SingleWorld => GameMode == GameMode.Normal;
        public bool MultiWorld => GameMode == GameMode.Multiworld;
        public bool Keysanity => KeyShuffle != KeyShuffle.None;

    }

}
