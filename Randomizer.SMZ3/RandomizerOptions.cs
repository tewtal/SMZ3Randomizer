using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Randomizer.Shared.Contracts;
using static Randomizer.Shared.Contracts.RandomizerOptionType;

namespace Randomizer.SMZ3 {

    static class RandomizerOptions {

        public static List<IRandomizerOption> List { get; } = new List<IRandomizerOption> {
            CreateEnumOption<SMLogic>("Super Metroid Logic"),
            CreateEnumOption<Goal>("Goal"),
            CreateEnumOption<OpenTower>("Open Ganon's Tower"),
            CreateEnumOption<GanonVulnerable>("Ganon Vulnerable"),
            CreateEnumOption<OpenTourian>("Open Tourian"),
            //GetRandomizerOption<Z3Logic>("A Link to the Past Logic"),
            CreateEnumOption<SwordLocation>("First Sword"),
            CreateEnumOption<MorphLocation>("Morph Ball"),
            CreateEnumOption<KeyShuffle>("Key shuffle"),
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
                Z3Logic = ParseOption(options, Z3Logic.Normal),
                SMLogic = ParseOption(options, SMLogic.Normal),
                SwordLocation = ParseOption(options, SwordLocation.Randomized),
                MorphLocation = ParseOption(options, MorphLocation.Randomized),
                KeyShuffle = ParseOption(options, KeyShuffle.None),
                Goal = ParseOption(options, Goal.DefeatBoth),
                OpenTower = ParseOption(options, OpenTower.SevenCrystals),
                GanonVulnerable = ParseOption(options, GanonVulnerable.SevenCrystals),
                OpenTourian = ParseOption(options, OpenTourian.FourBosses),
                Race = ParseOption(options, "Race", false),
                InitialItems = ParseOption(options, "InitialItems")
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

        static Dictionary<ItemType, int> ParseOption(IDictionary<string, string> options, string option) {
            // Extract a set of ItemType, int from the option value InitialItems encoded as "ItemType:Quantity, ItemType:Quantity"
            var result = new Dictionary<ItemType, int>();
            if (options.ContainsKey(option.ToLower())) {
                var items = options[option.ToLower()].Split(',');
                foreach (var itemData in items) {
                    var parts = itemData.Trim().Split(':');
                    var quantity = parts.Length == 2 && int.TryParse(parts[1].Trim(), out int q) ? q : 1;
                    var item = Enum.TryParse(parts[0].Trim(), true, out ItemType itemType) ? itemType : ItemType.Nothing;
                    if (item != ItemType.Nothing) {
                        result.Add(item, quantity);
                    }
                }

                if (result.ContainsKey(ItemType.Random)) {
                    var randomItemCount = result[ItemType.Random];
                    var randomPool = Item.CreateProgressionPool(new World(new Config(), "", 1, ""))
                        .Where(i => !result.ContainsKey(i.Type) && i.Type != ItemType.ETank && i.Type != ItemType.Missile && i.Type != ItemType.Super && i.Type != ItemType.PowerBomb && i.Type != ItemType.ReserveTank && i.Type != ItemType.HalfMagic)
                        .Select(x => x.Type)
                        .Distinct()
                        .ToList();

                    var rnd = new Random();

                    for (int i = 0; i < randomItemCount; i++) {
                        var item = randomPool[rnd.Next(randomPool.Count)];
                        result.Add(item, 1);
                        randomPool.Remove(item);
                    }

                    result.Remove(ItemType.Random);
                }

            }
            return result;
        }

    }

}
