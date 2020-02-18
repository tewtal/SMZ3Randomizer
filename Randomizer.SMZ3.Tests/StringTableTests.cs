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

        [Test]
        public void GeneratesTheCorrectByteArray() {
            var text = TextFrom(StringTableFile);

            var expected = ParseHex(text);
            var actual = new StringTable().GetBytes();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(StringTableEntries))]
        public void GeneratesTheCorrectDialogEntry(string name) {
            var text = TextFrom(StringEntriesFile);
            var table = JsonConvert.DeserializeObject<IDictionary<string, string>>(text);

            var expected = ParseEntry(table, name);
            var actual = new StringTable().entries.First(x => x.name == name).bytes;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(SimpleTextEntries))]
        public void GeneratesTheCorrectSimpleText(string name) {
            var text = TextFrom(SimpleTextFile);
            var table = JsonConvert.DeserializeObject<IDictionary<string, string>>(text);
            var item = new Item { Type = Enum.Parse<ItemType>(name) };

            var expected = ParseEntry(table, name);
            var actual = Dialog.Simple(Texts.ItemTextbox(item));
            Assert.That(actual, Is.EqualTo(expected));
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
                table.entries.ToDictionary(x => x.name, x => string.Join("", x.bytes.Select(b => b.ToString("X2")))), Formatting.Indented));

            File.WriteAllText(SimpleTextFile, JsonConvert.SerializeObject(
                SimpleTextEntries().ToDictionary(x => x, x => {
                    var type = Enum.Parse<ItemType>(x);
                    var bytes = Dialog.Simple(Texts.ItemTextbox(new Item { Type = type }));
                    return string.Join("", bytes.Select(b => b.ToString("X2")));
                }), Formatting.Indented));
        }

        static IEnumerable<string> StringTableEntries() {
            return new StringTable().entries.Select(x => x.name);
        }

        static IEnumerable<string> SimpleTextEntries() {
            var text = TextFrom(SimpleTextFile);
            return JsonConvert.DeserializeObject<IDictionary<string, string>>(text).Keys;
        }

        static string TextFrom(string file) {
            using var stream = EmbeddedStream.For(file);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        static byte[] ParseEntry(IDictionary<string, string> table, string name) => ParseHex(table[name]);

        static byte[] ParseHex(string text) {
            return new Regex("..").Matches(text).Select(m => Convert.ToByte(m.Value, 16)).ToArray();
        }

    }

}
