using NUnit.Framework;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Randomizer.SMZ3.Text;

namespace Randomizer.SMZ3.Tests {

    [TestFixture]
    public class StringTableTests {

        const string StringTableFile = "TestData.StringTable.txt";
        const string StringEntriesFile = "TestData.StringEntries.json";
        const string SimpleTextFile = "TestData.SimpleText.json";

        static byte[] ParseEntry(IDictionary<string, string> table, string name) {
            return ParseText(table[name]);
        }

        static byte[] ParseText(string text) {
            return new Regex("..").Matches(text).Select(m => Convert.ToByte(m.Value, 16)).ToArray();
        }

        static void WithTextFrom(string file, Action<string> action) {
            using (var stream = ManifestResources.GetEmbeddedStreamFor(file))
            using (var reader = new StreamReader(stream)) {
                action(reader.ReadToEnd());
            }
        }

        [Test]
        public void GeneratesTheCorrectByteArray() {
            WithTextFrom(StringTableFile, text => {
                var expected = ParseText(text);
                var actual = new StringTable().GetBytes();
                Assert.That(actual, Is.EqualTo(expected));
            });
        }

        [TestCaseSource(nameof(StringTableEntries))]
        public void GeneratesTheCorrectDialogEntry(string name) {
            WithTextFrom(StringEntriesFile, text => {
                var table = JsonConvert.DeserializeObject<IDictionary<string, string>>(text);
                var expected = ParseEntry(table, name);
                var actual = new StringTable().entries.First(x => x.Item1 == name).Item2;
                Assert.That(actual, Is.EqualTo(expected));
            });
        }

        [TestCaseSource(nameof(SimpleTextEntries))]
        public void GeneratesTheCorrectSimpleText(string name) {
            WithTextFrom(SimpleTextFile, text => {
                var item = new Item { Type = Enum.Parse<ItemType>(name) };
                var table = JsonConvert.DeserializeObject<IDictionary<string, string>>(text);
                var expected = ParseEntry(table, name);
                var actual = Dialog.Simple(Texts.ItemTextbox(item));
                Assert.That(actual, Is.EqualTo(expected));
            });
        }

        //[Test]
        public void GenerateGoldenStandardFiles() {
            const string TableFile = @".\table.txt";
            const string EntriesFile = @".\entries.json";
            const string SimpleTextFile = @".\simple.json";
            var table = new StringTable();

            File.WriteAllText(TableFile,
                string.Join("", table.GetBytes().Select(x => x.ToString("X2"))));

            File.WriteAllText(EntriesFile, JsonConvert.SerializeObject(
                table.entries.ToDictionary(x => x.Item1, x => string.Join("", x.Item2.Select(b => b.ToString("X2")))), Formatting.Indented));

            File.WriteAllText(SimpleTextFile, JsonConvert.SerializeObject(
                SimpleTextEntries().ToDictionary(x => x, x => {
                    var type = Enum.Parse<ItemType>(x);
                    var bytes = Dialog.Simple(Texts.ItemTextbox(new Item { Type = type }));
                    return string.Join("", bytes.Select(b => b.ToString("X2")));
                }), Formatting.Indented));
        }

        static IEnumerable<string> StringTableEntries() {
            return new StringTable().entries.Select(x => x.Item1);
        }

        static IEnumerable<string> SimpleTextEntries() {
            var names = Enumerable.Empty<string>();
            WithTextFrom(SimpleTextFile, text => names = JsonConvert.DeserializeObject<IDictionary<string, string>>(text).Keys);
            return names;
        }

    }

}
