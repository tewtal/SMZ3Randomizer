namespace Randomizer.SMZ3 {

    enum Logic {
        Casual,
        Tournament,
        Normal,
        Hard
    }

    class Config {

        public Logic Logic { get; set; } = Logic.Tournament;
        public bool Keysanity { get; set; }

    }

}
