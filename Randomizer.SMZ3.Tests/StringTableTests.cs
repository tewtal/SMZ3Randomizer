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

        [Test]
        public void GeneratesTheCorrectStringTableData() {
            var expected = ParseHexData("TestData.StringTableData.txt");
            var actual = new StringTable().GetBytes();
            Assert.That(actual, Is.EqualTo(expected));
        }

        static IEnumerable<string> StringTableEntries() {
            return from entry in StringTable.template
                   select entry.name;
        }

        [TestCaseSource(nameof(StringTableEntries))]
        public void GeneratesTheCorrectDialogEntry(string name) {
            var table = ParseTableData("TestData.StringTableEntries.json");

            var expected = ParseHex(table[name]);
            var actual = StringTable.template.First(x => x.name == name).bytes;
            Assert.That(actual, Is.EqualTo(expected));
        }

        static IEnumerable<string> ItemTypeNames() {
            return Enum.GetNames(typeof(ItemType));
        }

        [TestCaseSource(nameof(ItemTypeNames))]
        public void GeneratesTheCorrectItemText(string name) {
            var table = ParseTableData("TestData.ItemTexts.json");
            var item = new Item { Type = Enum.Parse<ItemType>(name) };

            var expected = table.TryGetValue(name, out var value) ? ParseHex(value) : null;
            var actual = Dialog.Simple(Texts.ItemTextbox(item));
            Assert.That(actual, Is.EqualTo(expected));
        }

        //[Test]
        public void GenerateGoldenStandardFiles() {
            var stringTableData = string.Join("", from b in new StringTable().GetBytes() select $"{b:X2}");
            File.WriteAllText(@".\StringTableData.txt", stringTableData);

            var stringTableEntries = StringTable.template.ToDictionary(
                x => x.name,
                x => string.Join("", from b in x.bytes select $"{b:X2}")
            );
            File.WriteAllText(@".\StringTableEntries.json", JsonConvert.SerializeObject(stringTableEntries, Formatting.Indented));

            var itemTexts = ItemTypeNames().ToDictionary(
                name => name,
                name => {
                    var item = new Item { Type = Enum.Parse<ItemType>(name) };
                    var bytes = Dialog.Simple(Texts.ItemTextbox(item));
                    return string.Join("", from b in bytes select $"{b:X2}");
                }
            );
            File.WriteAllText(@".\ItemTexts.json", JsonConvert.SerializeObject(itemTexts, Formatting.Indented));
        }

        static IDictionary<string, string> ParseTableData(string resource) {
            using var stream = EmbeddedStream.For(resource);
            using var reader = new StreamReader(stream);
            return JsonConvert.DeserializeObject<IDictionary<string, string>>(reader.ReadToEnd());
        }

        static byte[] ParseHexData(string resource) {
            using var stream = EmbeddedStream.For(resource);
            using var reader = new StreamReader(stream);
            return ParseHex(reader.ReadToEnd());
        }

        static byte[] ParseHex(string text) {
            return new Regex("..").Matches(text).Select(m => Convert.ToByte(m.Value, 16)).ToArray();
        }

    }

}
