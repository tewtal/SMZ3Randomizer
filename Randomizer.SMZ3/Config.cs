using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Randomizer.Shared.Contracts;

namespace Randomizer.SMZ3 {

    [DefaultValue(Normal)]
    enum GameMode {
        [Description("Single player")]
        Normal,
        [Description("Multiworld")]
        Multiworld
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
        Uncle
    }    

    [DefaultValue(Randomized)]
    enum MorphLocation {
        [Description("Randomized")]
        Randomized,
        [Description("Early")]
        Early,
        [Description("Original location")]
        Original
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
        public bool Keysanity => KeyShuffle != KeyShuffle.None;
        public bool Race { get; set; } = false;
        public GanonInvincible GanonInvincible { get; set; } = GanonInvincible.BeforeCrystals;

        public Config(IDictionary<string, string> options) {
            GameMode = ParseOption(options, GameMode.Normal);
            Z3Logic = ParseOption(options, Z3Logic.Normal);
            SMLogic = ParseOption(options, SMLogic.Normal);
            SwordLocation = ParseOption(options, SwordLocation.Randomized);
            MorphLocation = ParseOption(options, MorphLocation.Randomized);
            Goal = ParseOption(options, Goal.DefeatBoth);
            GanonInvincible = ParseOption(options, GanonInvincible.BeforeCrystals);
            KeyShuffle = ParseOption(options, KeyShuffle.None);
            Race = ParseOption(options, "Race", false);
        }

        private TEnum ParseOption<TEnum>(IDictionary<string, string> options, TEnum defaultValue) where TEnum: Enum {
            string enumKey = typeof(TEnum).Name.ToLower();
            if (options.ContainsKey(enumKey)) {
                if (Enum.TryParse(typeof(TEnum), options[enumKey], true, out object enumValue)) {
                    return (TEnum)enumValue;
                }
            }
            return defaultValue;
        }

        private bool ParseOption(IDictionary<string, string> options, string option, bool defaultValue) {
            if(options.ContainsKey(option.ToLower())) {
                return bool.Parse(options[option.ToLower()]);
            } else {
                return defaultValue;
            }
        }

        public static RandomizerOption GetRandomizerOption<T>(string description, string defaultOption = "") where T : Enum {
            var enumType = typeof(T);
            var values = Enum.GetValues(enumType).Cast<Enum>();

            return new RandomizerOption {
                Key = enumType.Name.ToLower(),
                Description = description,
                Type = RandomizerOptionType.Dropdown,
                Default = string.IsNullOrEmpty(defaultOption) ? GetDefaultValue<T>().ToLString() : defaultOption,
                Values = values.ToDictionary(k => k.ToLString(), v => v.GetDescription())
            };
        }

        public static RandomizerOption GetRandomizerOption(string name, string description, bool defaultOption = false) {
            return new RandomizerOption {
                Key = name.ToLower(),
                Description = description,
                Type = RandomizerOptionType.Checkbox,
                Default = defaultOption.ToString().ToLower(),
                Values = new Dictionary<string, string>()
            };
        }

        public static TEnum GetDefaultValue<TEnum>() where TEnum : Enum {
            Type t = typeof(TEnum);
            var attributes = (DefaultValueAttribute[])t.GetCustomAttributes(typeof(DefaultValueAttribute), false);
            if ((attributes?.Length ?? 0) > 0) {
                return (TEnum)attributes.First().Value;
            }
            else {
                return default;
            }
        }
    }
}
