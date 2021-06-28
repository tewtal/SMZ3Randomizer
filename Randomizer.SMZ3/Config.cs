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
    }

    [DefaultValue(None)]
    enum KeyShuffle {
        [Description("None")]
        None,
        [Description("Keysanity")]
        Keysanity
    }

    enum GanonInvincible {
        [Description("Never")]
        Never,
        [Description("Before Crystals")]
        BeforeCrystals,
        [Description("Before All Dungeons")]
        BeforeAllDungeons,
        [Description("Always")]
        Always,
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
        public GanonInvincible GanonInvincible { get; set; } = GanonInvincible.BeforeCrystals;

        public bool SingleWorld => GameMode == GameMode.Normal;
        public bool MultiWorld => GameMode == GameMode.Multiworld;
        public bool Keysanity => KeyShuffle != KeyShuffle.None;

    }

}
