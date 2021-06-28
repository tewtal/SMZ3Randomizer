using System.ComponentModel;

namespace Randomizer.SuperMetroid {

    [DefaultValue(Normal)]
    enum GameMode {
        [Description("Single player")]
        Normal,
        [Description("Multiworld")]
        Multiworld,
    }

    [DefaultValue(Tournament)]
    enum Logic {
        [Description("Casual")]
        Casual,
        [Description("Tournament")]
        Tournament,
    }

    [DefaultValue(Split)]
    enum Placement {
        [Description("Full randomization")]
        Full,
        [Description("Major/Minor split")]
        Split,
    }

    [DefaultValue(DefeatMB)]
    enum Goal {
        [Description("Defeat Mother Brain")]
        DefeatMB,
    }

    class Config {

        public GameMode GameMode { get; set; } = GameMode.Normal;
        public Logic Logic { get; set; } = Logic.Tournament;
        public Goal Goal { get; set; } = Goal.DefeatMB;
        public Placement Placement { get; set; } = Placement.Split;
        public bool Race { get; set; } = false;
        public bool Keysanity { get; set; } = false;

        public bool SingleWorld => GameMode == GameMode.Normal;
        public bool MultiWorld => GameMode == GameMode.Multiworld;

    }

}
