namespace Randomizer.SMZ3 {

    enum Z3Logic {
        Nmg,
        Owg,
        Mg,
    }

    enum SMLogic {
        Casual,
        Basic,
        Advanced,
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

        public Z3Logic Z3Logic { get; set; } = Z3Logic.Nmg;
        public SMLogic SMLogic { get; set; } = SMLogic.Advanced;
        public Difficulty Difficulty { get; set; } = Difficulty.Normal;
        public bool Keysanity { get; set; } = false;
        public GanonInvincible GanonInvincible { get; set; } = GanonInvincible.BeforeCrystals;

    }

}
