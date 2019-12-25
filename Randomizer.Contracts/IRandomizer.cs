using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Randomizer.Contracts {

    public interface IWorldData {
        int Id { get; }
        string Guid { get; }
        string Player { get; }
        Dictionary<int, byte[]> Patches { get; }
    }

    public interface ISeedData {
        string Guid { get; }
        string Seed { get; }
        string Game { get; }
        string Logic { get; }
        string Mode { get; }
        List<IWorldData> Worlds { get; }
        List<Dictionary<string, string>> Playthrough { get; }
    }

    public interface IRandomizer {
        string Id { get; }
        string Name { get; }
        string Version { get; }
        List<IRandomizerOption> Options { get; }
        ISeedData GenerateSeed(IDictionary<string, string> options, string seed);
    }

    public enum RandomizerOptionType
    {
        [EnumMember(Value = "input")]
        Input,
        [EnumMember(Value = "dropdown")]
        Dropdown,
        [EnumMember(Value = "checkbox")]
        Checkbox,
        [EnumMember(Value = "players")]
        Players
    };

    public interface IRandomizerOption
    {
        string Key { get; }
        string Description { get; }
        RandomizerOptionType Type { get; }
        Dictionary<string, string> Values { get; }
        string Default { get; }
    }

}
