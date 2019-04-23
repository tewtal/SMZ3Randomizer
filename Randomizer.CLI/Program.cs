using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using Newtonsoft.Json;
using Randomizer.Contracts;

namespace Randomizer.CLI {

    class Program {

        abstract class CliOptions {

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

            [Option("no-interact",
                HelpText = "Do not wait for keyboard input")]
            public bool NoInteract { get; set; }

            [Option("playthrough",
                HelpText = "Show json formated playthrough for each seed")]
            public bool Playthrough { get; set; }

            [Option("patch",
                HelpText = "Show json formated patch for each world in the seed")]
            public bool Patch { get; set; }

            public string Logic => this switch {
                var o when o.Tournament => "tournament",
                var o when o.Casual => "casual",
                _ => "tournament"
            };

            public bool Interact => !NoInteract;

            public abstract IRandomizer NewRandomizer();

        }

        [Verb("sm", HelpText = "Generate Super Metroid seeds")]
        class SMOptions : CliOptions {
            public override IRandomizer NewRandomizer() => new SuperMetroid.Randomizer();
        }

        [Verb("smz3", HelpText = "Generate SMZ3 combo seeds")]
        class SMZ3Options : CliOptions {
            public override IRandomizer NewRandomizer() => new SMZ3.Randomizer();
        }

        static void Main(string[] args) {

            var opts = Parser.Default.ParseArguments<SMOptions, SMZ3Options>(args)
                .WithParsed((CliOptions opts) => {
                    if (opts.Players < 1 || opts.Players > 64)
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
                });
        }

        static void MakeSeed(Dictionary<string, string> options, CliOptions opts) {
            var rando = opts.NewRandomizer();
            var time = DateTime.Now;
            var data = rando.GenerateSeed(options, opts.Seed);
            Console.WriteLine(string.Join(" - ",
                $"Generated seed: {data.Seed}",
                $"Players: {options["worlds"]}",
                $"Spheres: {data.Playthrough.Count}",
                $"Generation time: {DateTime.Now - time}"
            ));
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

        static void Interact(CliOptions opts) {
            if (opts.Interact) Console.ReadLine();
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
