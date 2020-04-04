using Randomizer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Randomizer.SuperMetroid {

    [DefaultValue(Tournament)]
    enum Logic {
        [Description("Casual")]
        Casual,
        [Description("Tournament")]
        Tournament
    }
    
    [DefaultValue(Split)]
    enum Placement {
        [Description("Full randomization")]
        Full,
        [Description("Major/Minor split")]
        Split
    }

    [DefaultValue(Normal)]
    enum GameMode {
        [Description("Single player")]
        Normal,
        [Description("Multiworld")]
        Multiworld
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

        public Config(IDictionary<string, string> options) {
            GameMode = ParseOption(options, GameMode.Normal);
            Logic = ParseOption(options, Logic.Tournament);
            Goal = ParseOption(options, Goal.DefeatMB);
            Placement = ParseOption(options, Placement.Split);
            Race = ParseOption(options, "Race", false);
            Keysanity = false;
        }

        private TEnum ParseOption<TEnum>(IDictionary<string, string> options, TEnum defaultValue) where TEnum : Enum {
            string enumKey = typeof(TEnum).Name.ToLower();
            if (options.ContainsKey(enumKey)) {
                if (Enum.TryParse(typeof(TEnum), options[enumKey], true, out object enumValue)) {
                    return (TEnum)enumValue;
                }
            }
            return defaultValue;
        }

        private bool ParseOption(IDictionary<string, string> options, string option, bool defaultValue) {
            if (options.ContainsKey(option.ToLower())) {
                return bool.Parse(options[option.ToLower()]);
            }
            else {
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
    public static class EnumExtensions {
        public static string GetDescription(this Enum GenericEnum) {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0)) {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0)) {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }

        public static string ToLString(this Enum enumValue) {
            return enumValue.ToString().ToLower();
        }
    }
}
