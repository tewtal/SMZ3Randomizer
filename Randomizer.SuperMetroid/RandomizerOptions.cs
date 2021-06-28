using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Randomizer.Shared.Contracts;
using static Randomizer.Shared.Contracts.RandomizerOptionType;

namespace Randomizer.SuperMetroid {

    static class RandomizerOptions {

        public static List<IRandomizerOption> List { get; } = new List<IRandomizerOption> {
            CreateEnumOption<Logic>("Logic"),
            CreateEnumOption<Goal>("Goal"),
            CreateEnumOption<Placement>("Item Placement"),
            CreateSeedOption(),
            CreateBoolOption("Race", "Race ROM (no spoilers)", false),
            CreateEnumOption<GameMode>("Game mode"),
            CreatePlayerOption(),
        };

        static RandomizerOption CreateEnumOption<TEnum>(string description, string defaultOption = null) where TEnum : Enum {
            var enumType = typeof(TEnum);
            return new RandomizerOption {
                Key = enumType.Name.ToLower(),
                Description = description,
                Type = Dropdown,
                Default = string.IsNullOrEmpty(defaultOption) ? GetDefaultValue<TEnum>().ToLowerString() : defaultOption,
                Values = Enum.GetValues(enumType).Cast<Enum>().ToDictionary(k => k.ToLowerString(), v => v.GetDescription()),
            };
        }

        static TEnum GetDefaultValue<TEnum>() where TEnum : Enum {
            var enumType = typeof(TEnum);
            var attrs = (DefaultValueAttribute[])enumType.GetCustomAttributes(typeof(DefaultValueAttribute), false);
            return (attrs?.Length ?? 0) > 0 ? (TEnum)attrs[0].Value : default;
        }

        static RandomizerOption CreateBoolOption(string name, string description, bool defaultOption = false) {
            return new RandomizerOption {
                Key = name.ToLower(),
                Description = description,
                Type = Checkbox,
                Default = defaultOption.ToString().ToLower(),
                Values = new Dictionary<string, string>(),
            };
        }

        static RandomizerOption CreateSeedOption() =>
            new RandomizerOption { Key = "seed", Description = "Seed", Type = Seed };

        static RandomizerOption CreatePlayerOption() =>
            new RandomizerOption { Key = "players", Description = "Players", Type = Players, Default = "2" };

        public static Config Parse(IDictionary<string, string> options) {
            return new Config {
                GameMode = ParseOption(options, GameMode.Normal),
                Logic = ParseOption(options, Logic.Tournament),
                Goal = ParseOption(options, Goal.DefeatMB),
                Placement = ParseOption(options, Placement.Split),
                Race = ParseOption(options, "Race", false),
            };
        }

        static TEnum ParseOption<TEnum>(IDictionary<string, string> options, TEnum defaultValue) where TEnum : Enum {
            var enumKey = typeof(TEnum).Name.ToLower();
            if (options.ContainsKey(enumKey)) {
                if (Enum.TryParse(typeof(TEnum), options[enumKey], true, out object enumValue)) {
                    return (TEnum)enumValue;
                }
            }
            return defaultValue;
        }

        static bool ParseOption(IDictionary<string, string> options, string option, bool defaultValue) {
            if (options.ContainsKey(option.ToLower())) {
                return bool.Parse(options[option.ToLower()]);
            } else {
                return defaultValue;
            }
        }

    }

}
