using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Randomizer.SMZ3.Regions.Zelda;
using SharpYaml.Model;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Text {

    class Texts {

        static YamlMapping scripts;
        static IList<string> blind;
        static IList<string> ganon;
        static IList<string> tavernMan;
        static IList<string> triforceRoom;

        static Texts() {
            scripts = ParseYamlScripts("Text.Scripts.General.yaml") as YamlMapping;
            blind = ParseTextScript("Text.Scripts.Blind.txt");
            ganon = ParseTextScript("Text.Scripts.Ganon.txt");
            tavernMan = ParseTextScript("Text.Scripts.TavernMan.txt");
            triforceRoom = ParseTextScript("Text.Scripts.TriforceRoom.txt");
        }

        static YamlElement ParseYamlScripts(string resource) {
            using var stream = EmbeddedStream.For(resource);
            using var reader = new StreamReader(stream);
            return YamlStream.Load(reader).First().Contents;
        }

        static IList<string> ParseTextScript(string resource) {
            using var stream = EmbeddedStream.For(resource);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd().Replace("\r", "").Split("---\n")
                .Select(text => text.TrimEnd('\n'))
                .Where(text => text != string.Empty)
                .ToList();
        }

        public static string SahasrahlaReveal(Region dungeon) {
            string text = (scripts["SahasrahlaReveal"] as YamlValue).Value;
            return text.Replace("<dungeon>", dungeon.Area);
        }

        public static string BombShopReveal(IEnumerable<Region> dungeons) {
            var (first, second, _) = dungeons;
            string text = (scripts["BombShopReveal"] as YamlValue).Value;
            return text.Replace("<first>", first.Area).Replace("<second>", second.Area);
        }

        public static string GanonThirdPhaseSingle(Region silvers) {
            var node = (scripts["GanonSilversReveal"] as YamlMapping)["single"] as YamlMapping;
            string text = (node[silvers is GanonsTower ? "local" : "remote"] as YamlValue).Value;
            return text.Replace("<region>", silvers.Area);
        }

        public static string GanonThirdPhaseMulti(Region silvers, World myWorld) {
            var node = (scripts["GanonSilversReveal"] as YamlMapping)["multi"] as YamlMapping;
            if (silvers.World == myWorld)
                return (node["local"] as YamlValue).Value;
            var player = silvers.World.Player;
            player = player.PadLeft(7 + player.Length / 2);
            string text = (node["remote"] as YamlValue).Value;
            return text.Replace("<player>", player);
        }

        public static string ItemTextbox(Item item) {
            var name = item.Type switch {
                _ when item.IsMap => "Map",
                _ when item.IsCompass => "Compass",
                _ when item.IsSmMap => "SmMap",
                BottleWithGoldBee => BottleWithBee.ToString(),
                HeartContainerRefill => HeartContainer.ToString(),
                OneRupee => "PocketRupees",
                FiveRupees => "PocketRupees",
                TwentyRupees => "CouchRupees",
                TwentyRupees2 => "CouchRupees",
                FiftyRupees => "CouchRupees",
                BombUpgrade5 => "BombUpgrade",
                BombUpgrade10 => "BombUpgrade",
                ArrowUpgrade5 => "ArrowUpgrade",
                ArrowUpgrade10 => "ArrowUpgrade",
                var type => type.ToString(),
            };

            var items = scripts["Items"] as YamlMapping;
            return (items[name] as YamlValue)?.Value ?? (items["default"] as YamlValue).Value;
        }

        public static string Blind(Random rnd) => RandomLine(rnd, blind);

        public static string TavernMan(Random rnd) => RandomLine(rnd, tavernMan);

        public static string GanonFirstPhase(Random rnd) => RandomLine(rnd, ganon);

        public static string TriforceRoom(Random rnd) => RandomLine(rnd, triforceRoom);

        static string RandomLine(Random rnd, IList<string> lines) => lines[rnd.Next(lines.Count)];

    }

}
