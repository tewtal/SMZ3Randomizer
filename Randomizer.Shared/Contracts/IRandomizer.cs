using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

namespace Randomizer.Shared.Contracts {

    public interface ILocationData
    {
        int LocationId { get; }
        int ItemId { get; }
        int ItemWorldId { get; }
    }

    public interface IWorldData {
        int Id { get; }
        string Guid { get; }
        string Player { get; }
        Dictionary<int, byte[]> Patches { get; }
        List<ILocationData> Locations { get; }
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

    public interface IItemTypeData
    {
        int Id { get; }
        string Name { get; }
    }

    public interface ILocationTypeData
    {
        int Id { get; }
        string Name { get; }
        string Type { get; }
        string Region { get; }
        string Area { get; }
    }

    public interface IRandomizer {
        string Id { get; }
        string Name { get; }
        string Version { get; }
        List<IRandomizerOption> Options { get; }
        ISeedData GenerateSeed(IDictionary<string, string> options, string seed, CancellationToken cancellationToken);
        Dictionary<int, IItemTypeData> GetItems();
        Dictionary<int, ILocationTypeData> GetLocations();
    }

    public enum RandomizerOptionType
    {
        [EnumMember(Value = "seed")]
        Seed,
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
