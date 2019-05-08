namespace Randomizer.SMZ3 {

    enum SMLogic {
        Casual,
        Tournament,
        Normal,
        Hard,
    }

    enum Difficulty {
        Easy = -1,
        Normal = 0,
        Hard = 1,
        Expert = 2,
        Insane = 3,
    }

    enum GanonInvincible {
        Never,
        BeforeCrystals,
        BeforeAllDungeons,
        Always,
    }

    class Config {

        public SMLogic SMLogic { get; set; } = SMLogic.Tournament;
        public Difficulty Difficulty { get; set; } = Difficulty.Normal;
        public bool Keysanity { get; set; } = false;
        public GanonInvincible GanonInvincible { get; set; } = GanonInvincible.BeforeCrystals;

    }

}
