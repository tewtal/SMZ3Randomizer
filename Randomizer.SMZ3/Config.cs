using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Randomizer.Contracts;

namespace Randomizer.SMZ3 {

    enum Z3Logic {
        [Description("Normal")]
        Normal,
        [Description("No major glitches")]
        Nmg,
        [Description("Overworld glitches")]
        Owg,
    }

    enum SMLogic {
        [Description("Normal")]
        Normal,
        [Description("Hard")]
        Hard,
    }

    enum SwordLocation {
        [Description("Randomized")]
        Randomized,
        [Description("Uncle assured")]
        Uncle
    }

    enum MorphLocation {
        [Description("Randomized")]
        Randomized,
        [Description("Vanilla")]
        Vanilla
    }

    enum Goal {
        [Description("Defeat Ganon and Mother Brain")]
        DefeatBoth,
    }

    enum Difficulty {
        Easy = -1,
        Normal = 0,
        Hard = 1,
        Expert = 2,
        Insane = 3,
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

        public Z3Logic Z3Logic { get; set; } = Z3Logic.Normal;
        public SMLogic SMLogic { get; set; } = SMLogic.Normal;
        public Difficulty Difficulty { get; set; } = Difficulty.Normal;
        public SwordLocation SwordLocation { get; set; } = SwordLocation.Randomized;
        public MorphLocation MorphLocation { get; set; } = MorphLocation.Randomized;
        public Goal Goal { get; set; } = Goal.DefeatBoth;
        public bool Keysanity { get; set; } = false;
        public GanonInvincible GanonInvincible { get; set; } = GanonInvincible.BeforeCrystals;

        public Config(IDictionary<string, string> options) {
            Z3Logic = ParseOption(options, Z3Logic.Normal);
            SMLogic = ParseOption(options, SMLogic.Normal);
            Difficulty = ParseOption(options, Difficulty.Normal);
            SwordLocation = ParseOption(options, SwordLocation.Randomized);
            MorphLocation = ParseOption(options, MorphLocation.Randomized);
            Goal = ParseOption(options, Goal.DefeatBoth);
            GanonInvincible = ParseOption(options, GanonInvincible.BeforeCrystals);
            Keysanity = false;
        }

        private TEnum ParseOption<TEnum>(IDictionary<string, string> options, TEnum defaultValue) where TEnum: Enum {
            string enumKey = typeof(TEnum).Name.ToLower();
            if (options.ContainsKey(enumKey)) {
                object enumValue;
                if (Enum.TryParse(typeof(TEnum), options[enumKey], true, out enumValue)) {
                    return (TEnum)enumValue;
                }
            }
            return defaultValue;
        }

        public static RandomizerOption GetRandomizerOption<T>(string description, string defaultOption = "") where T : Enum {
            var enumType = typeof(T);
            var values = Enum.GetValues(enumType).Cast<Enum>();

            return new RandomizerOption {
                Key = enumType.Name.ToLower(),
                Description = description,
                Type = RandomizerOptionType.Dropdown,
                Default = string.IsNullOrEmpty(defaultOption) ? values.First().ToString().ToLower() : defaultOption,
                Values = values.ToDictionary(k => k.ToString().ToLower(), v => v.GetDescription())
            };
        }

    }
}
