namespace Randomizer.SMZ3 {

    enum Logic {
        Casual,
        Tournament,
        Normal,
        Hard
    }

    enum GanonInvincible {
        Never,
        BeforeCrystals,
        BeforeAllDungeons,
        Always,
    }

    class Config {

        public Logic Logic { get; set; } = Logic.Tournament;
        public bool Keysanity { get; set; } = false;
        public GanonInvincible GanonInvincible { get; set; } = GanonInvincible.BeforeCrystals;

    }

}
