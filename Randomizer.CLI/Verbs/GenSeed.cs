using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;
using Newtonsoft.Json;
using Randomizer.CLI.FileData;
using Randomizer.Shared.Contracts;
using static Randomizer.CLI.FileHelper;

namespace Randomizer.CLI.Verbs {

    abstract class GenSeedOptions {

        [Option(
            HelpText = "Generate a singleworld mode seed (defaults to multiworld")]
        public bool Single { get; set; }

        [Option('p', "players", Default = 1,
            HelpText = "The number of players for seeds")]
        public int Players { get; set; }

        [Option('n', "normal",
            HelpText = "Generate seeds with Normal (Casual) SM logic (default)")]
        public bool Normal { get; set; }

        [Option('h', "hard",
            HelpText = "Generate seeds with Hard (Tournament) SM logic")]
        public bool Hard { get; set; }

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

        [Option(
            HelpText = "Write patch and playthrough to the console instead of directly to files")]
        public bool Console { get; set; }

        [Option(
            HelpText = "Show json formated playthrough for each seed")]
        public bool Playthrough { get; set; }

        [Option(
            HelpText = "Show json formated patch for each world in the seed")]
        public bool Patch { get; set; }

        public virtual string LogicName { get; } = "";
        public virtual string LogicValue { get; } = "";

        public bool Multi => !Single;

        protected const string smFile = @".\Super_Metroid_JU_.sfc";
        protected const string z3File = @".\Zelda_no_Densetsu_-_Kamigami_no_Triforce_Japan.sfc";

        public abstract IRandomizer NewRandomizer();
        public abstract byte[] BaseRom();

    }

    [Verb("sm", HelpText = "Generate Super Metroid seeds")]
    class SMSeedOptions : GenSeedOptions {

        [Option(
            HelpText = "Generate seeds with full randomization")]
        public bool Full { get; set; }

        [Option(
            HelpText = "Generate seeds with Major/Minor split (default)")]
        public bool Split { get; set; }

        readonly Lazy<byte[]> smRom;

        public override string LogicName => "logic";
        public override string LogicValue => this switch {
            var o when o.Hard => "tournament",
            var o when o.Normal => "casual",
            _ => "casual",
        };

        public string Placement => this switch {
            var o when o.Full => "full",
            var o when o.Split => "split",
            _ => "split",
        };

        public SMSeedOptions() {
            smRom = new Lazy<byte[]>(() => {
                using var ips = OpenReadInnerStream(Ips.First());
                using var sm = File.OpenRead(smFile);
                /* Account for custom sprites */
                var rom = FileData.Rom.ExpandRom(sm, 0x400000);
                FileData.Rom.ApplyIps(rom, ips);
                return rom;
            });
        }

        public override IRandomizer NewRandomizer() => new SuperMetroid.Randomizer();
        public override byte[] BaseRom() => (byte[]) smRom.Value.Clone();

    }

    [Verb("smz3", HelpText = "Generate SMZ3 combo seeds")]
    class SMZ3SeedOptions : GenSeedOptions {

        [Option(
            HelpText = "Generate seeds with keysanity")]
        public bool Keysanity { get; set; }

        [Option(
            HelpText = "Generate seeds with original morph placement (defaults to randomized)")]
        public bool OriginalMorph { get; set; }

        [Option(
            HelpText = "Generate seeds with early morph placement (defaults to randomized)")]
        public bool EarlyMorph { get; set; }

        [Option(
            HelpText = "Generate seeds with uncle sword placement (defaults to randomized)")]
        public bool UncleSword { get; set; }

        [Option(
            HelpText = "Generate seeds with early sword placement (defaults to randomized)")]
        public bool EarlySword { get; set; }

        readonly Lazy<byte[]> smz3Rom;

        public override string LogicName => "smlogic";
        public override string LogicValue => this switch {
            var o when o.Hard => "hard",
            var o when o.Normal => "normal",
            _ => "normal"
        };

        public string KeyShuffle =>
            Keysanity ? "keysanity" : "none";

        public string MorphLocation => this switch {
            var o when o.EarlyMorph => "early",
            var o when o.OriginalMorph => "original",
            _ => "randomized",
        };

        public string SwordLocation => this switch {
            var o when o.EarlySword => "early",
            var o when o.UncleSword => "uncle",
            _ => "randomized",
        };

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
            if (opts.Players < 1 || opts.Players > 64)
                throw new ArgumentOutOfRangeException("players", "The players parameter must fall within the range 1-64");

            var optionList = new List<(string, string)> {
                ("gamemode", opts.Single ? "normal" : "multiworld"),
                (opts.LogicName, opts.LogicValue),
                ("players", opts.Players.ToString()),
            };
            if (opts is SMZ3SeedOptions smz3) {
                optionList.AddRange(new[] {
                    ("keyshuffle", smz3.KeyShuffle),
                    ("swordlocation", smz3.SwordLocation),
                    ("morphlocation", smz3.MorphLocation),
                });
            }
            if (opts is SMSeedOptions sm) {
                optionList.AddRange(new[] {
                    ("placement", sm.Placement),
                });
            }
            var players = from n in Enumerable.Range(0, opts.Players)
                          select ($"player-{n}", $"Player {n + 1}");
            var options = optionList.Concat(players).ToDictionary(x => x.Item1, x => x.Item2);

            //try {
                while (true) {
                    MakeSeed(options, opts);
                    if (!opts.Loop) break;
                }
            //} catch (Exception e) {
            //    Console.Error.WriteLine(e.Message);
            //}
        }

        static void MakeSeed(Dictionary<string, string> options, GenSeedOptions opts) {
            var rando = opts.NewRandomizer();
            var start = DateTime.Now;
            var data = rando.GenerateSeed(options, opts.Seed, new System.Threading.CancellationToken());
            var end = DateTime.Now;
            Console.WriteLine(string.Join(" - ",
                $"Generated seed: {data.Seed}",
                $"Players: {options["players"]}",
                $"Spheres: {data.Playthrough.Count}",
                $"Generation time: {end - start}"
            ));
            var world = data.Worlds.First();
            var filename = ComposeFilename(rando, opts, data.Seed, world.Player);
            if (opts.Rom) {
                try {
                    var rom = opts.BaseRom();
                    Rom.ApplySeed(rom, world.Patches);
                    AdditionalPatches(rom, opts.Ips.Skip(1));
                    ApplyRdcResources(rom, opts.Rdc);
                    File.WriteAllBytes($"{filename}.sfc", rom);
                } catch (Exception e) {
                    Console.Error.WriteLine(e.Message);
                }
            }
            if (opts.Playthrough) {
                var text = JsonConvert.SerializeObject(data.Playthrough, Formatting.Indented);
                if (opts.Console) {
                    Console.WriteLine(text);
                    Console.ReadLine();
                } else {
                    File.WriteAllText($"{filename}-playthrough.json", text);
                }
            }
            if (opts.Patch) {
                var text = JsonConvert.SerializeObject(
                    data.Worlds.ToDictionary(x => x.Player, x => x.Patches), Formatting.Indented,
                    new PatchWriteConverter()
                );
                if (opts.Console) {
                    Console.WriteLine(text);
                    Console.ReadLine();
                } else {
                    File.WriteAllText($"{filename}-patch.json", text);
                }
            }
        }

        static void AdditionalPatches(byte[] rom, IEnumerable<string> ips) {
            foreach (var patch in ips) {
                using var stream = OpenReadInnerStream(patch);
                Rom.ApplyIps(rom, stream);
            }
        }

        static void ApplyRdcResources(byte[] rom, IEnumerable<string> rdc) {
            foreach (var resource in rdc) {
                using var stream = OpenReadInnerStream(resource);
                var content = Rdc.Parse(stream);
                if (content.TryParse<LinkSprite>(stream, out var block))
                    (block as DataBlock)?.Apply(rom);
                if (content.TryParse<SamusSprite>(stream, out block))
                    (block as DataBlock)?.Apply(rom);
            }
        }

        static string ComposeFilename(IRandomizer rando, GenSeedOptions opts, string seed, string player) {
            var parts = new[] {
                new[] {
                    rando.Id.ToUpper(),
                    $"V{rando.Version}",
                },
                opts is SMZ3SeedOptions smz3 ? new[] {
                    $"ZLn+SL{smz3.LogicValue[0]}",
                    smz3.SwordLocation != "randomized" ? $"S{smz3.SwordLocation[0]}" : null,
                    smz3.MorphLocation != "randomized" ? $"M{smz3.MorphLocation[0]}" : null,
                    smz3.Keysanity ? "Kk" : null,
                } : opts is SMSeedOptions sm ? new[] {
                    $"L{sm.LogicValue[0]}",
                    $"I{sm.Placement[0]}",
                } : new string[] { },
                new[] {
                    seed,
                    opts.Multi ? player : null,
                }
            };
            /* Flatten, then keep non-null parts */
            return string.Join("-", parts.SelectMany(x => x).Where(x => x != null));
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
