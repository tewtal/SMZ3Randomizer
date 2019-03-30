using System;
using System.Collections.Generic;

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
        List<IWorldData> Worlds { get; }
        List<Dictionary<string, string>> Playthrough { get; }
    }

    public interface IRandomizer {
        ISeedData GenerateSeed(IDictionary<string, string> options, string seed);
    }

}
