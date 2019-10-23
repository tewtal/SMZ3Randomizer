using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using CommandLine;
using Newtonsoft.Json;
using Randomizer.CLI.FileData;
using Randomizer.Contracts;

namespace Randomizer.CLI.Verbs {

    abstract class GenSeedOptions {

        [Option('p', "players", Default = 1,
            HelpText = "The number of players for seeds")]
        public int Players { get; set; }

        [Option('c', "casual",
            HelpText = "Generate seeds with Casual SM logic")]
        public bool Casual { get; set; }

        [Option('t', "tournament",
            HelpText = "Generate seeds with Tournament SM logic (default)")]
        public bool Tournament { get; set; }

        [Option('l', "loop",
            HelpText = "Generate seeds repeatedly")]
        public bool Loop { get; set; }

        [Option('s', "seed",
            HelpText = "Generate a specific seed")]
        public string Seed { get; set; } = string.Empty;

        [Option(
            HelpText = "Compile rom file of the first world for each seed. Use the ips option to provide all required IPS patchs.")]
        public bool Rom { get; set; }

        [Option(
            HelpText = "Specify paths for IPS patches to be applied in the specified order.")]
        public IEnumerable<string> Ips { get; set; }

        [Option(
            HelpText = "Specify paths for RDC resources to be applied in the specified order.")]
        public IEnumerable<string> Rdc { get; set; }

        [Option("no-interact",
            HelpText = "Do not wait for keyboard input")]
        public bool NoInteract { get; set; }

        [Option(
            HelpText = "Show json formated playthrough for each seed")]
        public bool Playthrough { get; set; }

        [Option(
            HelpText = "Show json formated patch for each world in the seed")]
        public bool Patch { get; set; }

        public string Logic => this switch
        {
            var o when o.Tournament => "tournament",
            var o when o.Casual => "casual",
            _ => "tournament"
        };

        public bool Interact => !NoInteract;

        protected const string smFile = @".\Super_Metroid_JU_.smc";
        protected const string z3File = @".\Zelda_no_Densetsu_-_Kamigami_no_Triforce_Japan.sfc";

        public abstract IRandomizer NewRandomizer();
        public abstract byte[] BaseRom();

        protected Stream OpenReadInnerStream(string filename) {
            Stream file = File.OpenRead(filename);
            if (Path.GetExtension(filename).ToLower() != ".gz")
                return file;

            using var source = new GZipStream(file, CompressionMode.Decompress);
            var stream = new MemoryStream();
            source.CopyTo(stream);
            return stream;
        }

    }

    [Verb("sm", HelpText = "Generate Super Metroid seeds")]
    class SMSeedOptions : GenSeedOptions {

        readonly Lazy<byte[]> smRom;

        public SMSeedOptions() {
            smRom = new Lazy<byte[]>(() => {
                using var ips = OpenReadInnerStream(Ips.First());
                var rom = File.ReadAllBytes(smFile);
                FileData.Rom.ApplyIps(rom, ips);
                return rom;
            });
        }

        public override IRandomizer NewRandomizer() => new SuperMetroid.Randomizer();
        public override byte[] BaseRom() => (byte[]) smRom.Value.Clone();

    }

    [Verb("smz3", HelpText = "Generate SMZ3 combo seeds")]
    class SMZ3SeedOptions : GenSeedOptions {

        readonly Lazy<byte[]> smz3Rom;

        public SMZ3SeedOptions() {
            smz3Rom = new Lazy<byte[]>(() => {
                using var sm = File.OpenRead(smFile);
                using var z3 = File.OpenRead(z3File);
                using var ips = OpenReadInnerStream(Ips.First());
                var rom = FileData.Rom.CombineSMZ3Rom(sm, z3);
                FileData.Rom.ApplyIps(rom, ips);
                return rom;
            });
        }

        public override IRandomizer NewRandomizer() => new SMZ3.Randomizer();
        public override byte[] BaseRom() => (byte[]) smz3Rom.Value.Clone();

    }

    static class GenSeed {

        public static void Run(GenSeedOptions opts) {
            if (opts.Players< 1 || opts.Players> 64)
                throw new ArgumentOutOfRangeException("players", "The players parameter must fall within the range 1-64");

            var optionList = new[] {
                ("logic", opts.Logic),
                ("worlds", opts.Players.ToString()),
            };
            var players = from n in Enumerable.Range(0, opts.Players)
                          select ($"player-{n}", $"Player {n + 1}");
            var options = optionList.Concat(players).ToDictionary(x => x.Item1, x => x.Item2);

            try {
                while (true) {
                    MakeSeed(options, opts);
                    if (!opts.Loop) break;
                }
            } catch (Exception e) {
                Console.Error.WriteLine(e.Message);
            }
        }

        static void MakeSeed(Dictionary<string, string> options, GenSeedOptions opts) {
            var rando = opts.NewRandomizer();
            var start = DateTime.Now;
            var data = rando.GenerateSeed(options, opts.Seed);
            var end = DateTime.Now;
            Console.WriteLine(string.Join(" - ",
                $"Generated seed: {data.Seed}",
                $"Players: {options["worlds"]}",
                $"Spheres: {data.Playthrough.Count}",
                $"Generation time: {end - start}"
            ));
            if (opts.Rom) {
                try {
                    var world = data.Worlds.First();
                    var rom = opts.BaseRom();
                    Rom.ApplySeed(rom, world.Patches);
                    AdditionalPatches(rom, opts.Ips.Skip(1));
                    ApplyRdcResources(rom, opts.Rdc);
                    File.WriteAllBytes($"{data.Game} {data.Logic} - {data.Seed} - {world.Player}.sfc", rom);
                } catch (Exception e) {
                    Console.Error.WriteLine(e.Message);
                }
            }
            if (opts.Playthrough) {
                Console.WriteLine(JsonConvert.SerializeObject(data.Playthrough, Formatting.Indented));
                Interact(opts);
            }
            if (opts.Patch) {
                Console.WriteLine(JsonConvert.SerializeObject(
                    data.Worlds.ToDictionary(x => x.Player, x => x.Patches), Formatting.Indented,
                    new PatchWriteConverter()
                ));
                Interact(opts);
            }
        }

        static void AdditionalPatches(byte[] rom, IEnumerable<string> ips) {
            foreach (var patch in ips) {
                using var stream = File.OpenRead(patch);
                Rom.ApplyIps(rom, stream);
            }
        }

        static void ApplyRdcResources(byte[] rom, IEnumerable<string> rdc) {
            foreach (var resource in rdc) {
                using var stream = File.OpenRead(resource);
                var content = Rdc.Parse(stream);
                if (content.TryParse<LinkSprite>(stream, out var block))
                    (block as DataBlock)?.Apply(rom);
                if (content.TryParse<SamusSprite>(stream, out block))
                    (block as DataBlock)?.Apply(rom);
            }
        }

        static void Interact(GenSeedOptions opts) {
            if (opts.Interact)
                Console.ReadLine();
        }

        public class PatchWriteConverter : JsonConverter<IDictionary<int, byte[]>> {

            public override void WriteJson(JsonWriter writer, IDictionary<int, byte[]> value, JsonSerializer serializer) {
                writer.WriteStartObject();
                foreach (var (address, bytes) in value) {
                    var width = address > 0x00FFFFFF ? 8 : address > 0x0000FFFF ? 6 : 4;
                    writer.WritePropertyName(address.ToString($"X{width}"));
                    writer.WriteValue(string.Join("", bytes.Select(b => b.ToString("X2"))));
                }
                writer.WriteEndObject();
            }

            #region Can not read

            public override bool CanRead => false;

            public override IDictionary<int, byte[]> ReadJson(JsonReader reader, Type objectType, IDictionary<int, byte[]> existingValue, bool hasExistingValue, JsonSerializer serializer) {
                throw new NotImplementedException("This converter can not read data");
            }

            #endregion

        }

    }

}
