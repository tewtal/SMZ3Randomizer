using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Randomizer.SMZ3.Text;

namespace Randomizer.SMZ3.Tests.Text {

    public class DialogTests {

        [TestFixture]
        public class Simple : DialogTests {

            const byte ScrollRows = 0x73;
            const byte FeedAtFirstRow = 0x74;
            const byte FeedAtSecondRow = 0x75;
            const byte FeedAtThirdRow = 0x76;
            const byte PauseForInput = 0x7E;
            const byte DialogEnd = 0x7F;

            [Test]
            public void SplitsLinesOnNewline() {
                var text = Lines("a", "b", "c");

                var actual = Dialog.Simple(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(FeedAtFirstRow), Word(0x30),
                    Byte(FeedAtSecondRow), Word(0x31),
                    Byte(FeedAtThirdRow), Word(0x32),
                    Byte(DialogEnd)
                )));
            }

            [Test]
            public void MoreThanThreeLinesAddsInputAndScroll() {
                var text = Lines("a", "b", "c", "d");

                var actual = Dialog.Simple(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(FeedAtFirstRow), Word(0x30),
                    Byte(FeedAtSecondRow), Word(0x31),
                    Byte(FeedAtThirdRow), Word(0x32),
                    Byte(PauseForInput),
                    Byte(ScrollRows), Byte(FeedAtThirdRow), Word(0x33),
                    Byte(DialogEnd)
                )));
            }

            [Test]
            public void ManyLinesAddsMultipleInputAndScroll() {
                var text = Lines(
                    "a", "b", "c",
                    "d", "e", "f",
                    "g", "h", "i"
                );

                var actual = Dialog.Simple(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(FeedAtFirstRow), Word(0x30),
                    Byte(FeedAtSecondRow), Word(0x31),
                    Byte(FeedAtThirdRow), Word(0x32),
                    Byte(PauseForInput),
                    Byte(ScrollRows), Byte(FeedAtThirdRow), Word(0x33),
                    Byte(ScrollRows), Byte(FeedAtThirdRow), Word(0x34),
                    Byte(ScrollRows), Byte(FeedAtThirdRow), Word(0x35),
                    Byte(PauseForInput),
                    Byte(ScrollRows), Byte(FeedAtThirdRow), Word(0x36),
                    Byte(ScrollRows), Byte(FeedAtThirdRow), Word(0x37),
                    Byte(ScrollRows), Byte(FeedAtThirdRow), Word(0x38),
                    Byte(DialogEnd)
                )));
            }

            [Test]
            public void DoesNotWrapOneLongLine() {
                var lineOfA = new string('a', 20);

                var actual = Dialog.Simple(lineOfA);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(FeedAtFirstRow),
                    Repeat(Word(0x30), 19),
                    Byte(DialogEnd)
                )));
            }

            [Test]
            public void DoesNotWrapTwoLongLines() {
                var lineOfA = new string('a', 20);
                var lineOfB = new string('b', 20);
                var text = Lines(lineOfA, lineOfB);

                var actual = Dialog.Simple(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(FeedAtFirstRow),
                    Repeat(Word(0x30), 19),
                    Byte(FeedAtSecondRow),
                    Repeat(Word(0x31), 19),
                    Byte(DialogEnd)
                )));
            }

            [Test]
            public void HasASizeLimit() {
                var n = MinLinesGeneratingMoreThan256Bytes();
                var text = Lines(Enumerable.Repeat(new string('a', 19), n));
                
                var actual = Dialog.Simple(text);

                Assert.That(actual, Has.Length.EqualTo(256));
                Assert.That(actual, Has.ItemAt(255).EqualTo(DialogEnd));
            }

            int MinLinesGeneratingMoreThan256Bytes() {
                int n = 0, s = 0;
                foreach (var p in LinePrefixBytes()) {
                    n += 1;
                    // Each character takes two bytes
                    s += p + 19 * 2;
                    // Add one for DialogEnd
                    if (s + 1 > 256)
                        break;
                }
                return n;

                static IEnumerable<int> LinePrefixBytes() {
                    // One Feed for first three lines
                    yield return 1;
                    yield return 1;
                    yield return 1;
                    while (true) {
                        // Input for first line of screen,
                        // Scroll + Feed for each
                        yield return 3;
                        yield return 2;
                        yield return 2;
                    }
                }
            }

            [TestCaseSource(typeof(DialogTests), nameof(EncodingCases))]
            public void EncodesAllSupportedCharacters(string text, IEnumerable<byte> expected) {
                expected = Words(expected);
                
                var actual = Dialog.Simple(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(FeedAtFirstRow),
                    expected,
                    Byte(DialogEnd)
                )));
            }

        }

        [TestFixture]
        public class Compiled : DialogTests {

            const byte NarrowSpace = 0x4F;
            const byte ScrollRows = 0xF6;
            const byte StartSecondRow = 0xF8;
            const byte StartThirdRow = 0xF9;
            const byte PauseForInput = 0xFA;
            const byte DialogStart = 0xFB;

            [TestCase("a{INTRO}b", TestName = "DoesNotAllowLinesWithCommandAndText")]
            [TestCase("{INVALID}", TestName = "DoesNotAllowInvalidCommands")]
            public void DoesNotAllow(string text) {
                Assert.That(() => Dialog.Compiled(text), Throws.ArgumentException);
            }

            [TestCase("{NOTEXT}", TestName =    "NoTextWithCommandOnly")]
            [TestCase("a\n{NOTEXT}", TestName = "NoTextWithCommandAfterText")]
            [TestCase("{NOTEXT}\nb", TestName = "NoTextWithCommandBeforeText")]
            public void NoText(string text) {
                var actual = Dialog.Compiled(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Seq(0xFB, 0xFE, 0x6E, 0x00, 0xFE, 0x6B, 0x04)
                )));
            }

            [Test]
            public void SplitsLinesOnNewline() {
                var text = Lines("a", "b", "c");

                var actual = Dialog.Compiled(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(DialogStart), Byte(0x30),
                    Byte(StartSecondRow), Byte(0x31),
                    Byte(StartThirdRow), Byte(0x32)
                )));
            }

            [Test]
            public void WrapsOneLongLine() {
                var lineOfAWords = string.Join("", Enumerable.Repeat("a ", 11));
                var text = Lines("{NOPAUSE}", lineOfAWords);

                var actual = Dialog.Compiled(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(DialogStart), Repeat(Seq(0x30, NarrowSpace), 9), Byte(0x30),
                    Byte(StartSecondRow), Byte(0x30)
                )));
            }

            [Test]
            public void WrapsTwoLongLines() {
                var lineOfAWords = string.Join("", Enumerable.Repeat("a ", 11));
                var lineOfBWords = string.Join("", Enumerable.Repeat("b ", 11));
                var text = Lines("{NOPAUSE}", lineOfAWords, lineOfBWords);

                var actual = Dialog.Compiled(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(DialogStart), Repeat(Seq(0x30, NarrowSpace), 9), Byte(0x30),
                    Byte(StartSecondRow), Byte(0x30),
                    Byte(StartThirdRow), Repeat(Seq(0x31, NarrowSpace), 9), Byte(0x31),
                    Byte(ScrollRows), Byte(0x31)
                )));
            }

            [Test]
            public void WrapsOneLongWord() {
                var longWord = new string('b', 38);
                var text = Lines("{NOPAUSE}", $"a {longWord}");

                var actual = Dialog.Compiled(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(DialogStart), Byte(0x30),
                    Byte(StartSecondRow), Repeat(0x31, 19),
                    Byte(StartThirdRow), Repeat(0x31, 19)
                )));
            }

            [Test]
            public void MoreThanThreeLinesAddsInputAndScroll() {
                var text = Lines("a", "b", "c", "d");

                var actual = Dialog.Compiled(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(DialogStart), Byte(0x30),
                    Byte(StartSecondRow), Byte(0x31),
                    Byte(StartThirdRow), Byte(0x32),
                    Byte(ScrollRows), Byte(PauseForInput), Byte(0x33)
                )));
            }

            [Test]
            public void ManyLinesAddsMultipleInputAndScroll() {
                var text = Lines(
                    "a", "b", "c",
                    "d", "e", "f",
                    "g", "h", "i"
                );

                var actual = Dialog.Compiled(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(DialogStart), Byte(0x30),
                    Byte(StartSecondRow), Byte(0x31),
                    Byte(StartThirdRow), Byte(0x32),
                    Byte(ScrollRows), Byte(PauseForInput), Byte(0x33),
                    Byte(ScrollRows), Byte(0x34),
                    Byte(ScrollRows), Byte(0x35),
                    Byte(ScrollRows), Byte(PauseForInput), Byte(0x36),
                    Byte(ScrollRows), Byte(0x37),
                    Byte(ScrollRows), Byte(0x38)
                )));
            }

            [Test]
            public void ManyLinesWithNoPauseOnlyAddsScroll() {
                var text = Lines(
                    "{NOPAUSE}",
                    "a", "b", "c",
                    "d", "e", "f",
                    "g", "h", "i"
                );

                var actual = Dialog.Compiled(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(DialogStart), Byte(0x30),
                    Byte(StartSecondRow), Byte(0x31),
                    Byte(StartThirdRow), Byte(0x32),
                    Byte(ScrollRows), Byte(0x33),
                    Byte(ScrollRows), Byte(0x34),
                    Byte(ScrollRows), Byte(0x35),
                    Byte(ScrollRows), Byte(0x36),
                    Byte(ScrollRows), Byte(0x37),
                    Byte(ScrollRows), Byte(0x38)
                )));
            }

            [Test]
            public void IntroPadsFirstThreeLines() {
                var text = Lines("{NOPAUSE}", "{INTRO}", "a", "b", "c", "d", "e", "f");

                var actual = Dialog.Compiled(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(DialogStart), Intro, Byte(0x30), Repeat(NarrowSpace, 18),
                    Byte(StartSecondRow), Byte(0x31), Repeat(NarrowSpace, 18),
                    Byte(StartThirdRow), Byte(0x32), Repeat(NarrowSpace, 18),
                    Byte(ScrollRows), Byte(0x33),
                    Byte(ScrollRows), Byte(0x34),
                    Byte(ScrollRows), Byte(0x35)
                )));
            }

            [Test]
            public void HasASizeLimit() {
                var n = MinLinesGeneratingMoreThan2046Bytes();
                var lines = Lines(Enumerable.Repeat(new string('a', 19), n));
                var text = Lines("{NOPAUSE}", lines);

                var actual = Dialog.Compiled(text);

                Assert.That(actual, Has.Length.EqualTo(2046));
            }

            int MinLinesGeneratingMoreThan2046Bytes() {
                int n = 0, s = 0;
                foreach (var p in LinePrefixBytesWithoutPause()) {
                    n += 1;
                    s += p + 19;
                    if (s > 2046)
                        break;
                }
                return n;

                static IEnumerable<int> LinePrefixBytesWithoutPause() {
                    // Without pause, each row always have one prefix byte
                    while (true) {
                        yield return 1;
                    }
                }
            }

            [TestCaseSource(typeof(DialogTests), nameof(EncodingCases))]
            public void EncodesAllSupportedCharacters(string text, IEnumerable<byte> expected) {
                var actual = Dialog.Compiled(text);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(DialogStart),
                    expected
                )));
            }

            [TestCaseSource(nameof(CommandCases))]
            public void ConvertsAllCommandsIntoBytes(string command, IEnumerable<byte> expected) {
                var actual = Dialog.Compiled(command);

                Assert.That(actual, Is.EquivalentTo(Bytes(
                    Byte(DialogStart),
                    expected
                )));
            }

            #region Commands and cases

            static IEnumerable<TestCaseData> CommandCases => new TestCaseData[] {
                new("{SPEED0}", Seq(0xFC, 0x00)),
                new("{SPEED2}", Seq(0xFC, 0x02)),
                new("{SPEED6}", Seq(0xFC, 0x06)),
                new("{PAUSE1}", Seq(0xFE, 0x78, 0x01)),
                new("{PAUSE3}", Seq(0xFE, 0x78, 0x03)),
                new("{PAUSE5}", Seq(0xFE, 0x78, 0x05)),
                new("{PAUSE7}", Seq(0xFE, 0x78, 0x07)),
                new("{PAUSE9}", Seq(0xFE, 0x78, 0x09)),
                new("{INPUT}", Seq(0xFA)),
                new("{CHOICE}", Seq(0xFE, 0x68)),
                new("{ITEMSELECT}", Seq(0xFE, 0x69)),
                new("{CHOICE2}", Seq(0xFE, 0x71)),
                new("{CHOICE3}", Seq(0xFE, 0x72)),
                new("{C:GREEN}", Seq(0xFE, 0x77, 0x07)),
                new("{C:YELLOW}", Seq(0xFE, 0x77, 0x02)),
                new("{HARP}", Seq(0xFE, 0x79, 0x2D)),
                new("{MENU}", Seq(0xFE, 0x6D, 0x00)),
                new("{BOTTOM}", Seq(0xFE, 0x6D, 0x01)),
                new("{NOBORDER}", Seq(0xFE, 0x6B, 0x02)),
                new("{CHANGEPIC}", Seq(0xFE, 0x67, 0xFE, 0x67)),
                new("{CHANGEMUSIC}", Seq(0xFE, 0x67)),
                new("{INTRO}", Intro),
                new("{IBOX}", Seq(0xFE, 0x6B, 0x02, 0xFE, 0x77, 0x07, 0xFC, 0x03, 0xF7)),
            };

            static IEnumerable<byte> Intro => Seq(0xFE, 0x6E, 0x00, 0xFE, 0x77, 0x07, 0xFC, 0x03, 0xFE, 0x6B, 0x02, 0xFE, 0x67);

            #endregion

        }

        #region Character encoding cases

        static IEnumerable<TestCaseData> EncodingCases() => new TestCaseData[] {
            new("0123456789", Seq(
                0xA0, 0xA1, 0xA2, 0xA3, 0xA4,
                0xA5, 0xA6, 0xA7, 0xA8, 0xA9
            )),
            new("ABCDEFGHIJKLM", Seq(
                0xAA, 0xAB, 0xAC, 0xAD, 0xAE,
                0xAF, 0xB0, 0xB1, 0xB2, 0xB3,
                0xB4, 0xB5, 0xB6
            )),
            new("NOPQRSTUVWXYZ", Seq(
                0xB7, 0xB8, 0xB9, 0xBA, 0xBB,
                0xBC, 0xBD, 0xBE, 0xBF, 0xC0,
                0xC1, 0xC2, 0xC3
            )),
            new("abcdefghijklm", Seq(
                0x30, 0x31, 0x32, 0x33, 0x34,
                0x35, 0x36, 0x37, 0x38, 0x39,
                0x3A, 0x3B, 0x3C
            )),
            new("nopqrstuvwxyz", Seq(
                0x3D, 0x3E, 0x3F, 0x40, 0x41,
                0x42, 0x43, 0x44, 0x45, 0x46,
                0x47, 0x48, 0x49
            )),
            new TestCaseData("€δΔ０", Repeat(0xFF, 4))
                .SetDescription("Unsupported characters (symbol, lower, upper, numeric)"),
            new TestCaseData(" _", Seq(0x4F, 0xFF))
                .SetDescription("Narrow space, Full width space"),
            new(":@#?!,-….", Seq(
                0x4A, 0x4B, 0x4C, 0xC6, 0xC7, 0xC8, 0xC9, 0xCC, 0xCD
            )),
            new(@"~～'’""", Seq(
                0xCE, 0xCE, 0xD8, 0xD8, 0xD8
            )),
            new("%^=↑↓→←≥", Seq(
                0xDD, 0xDE, 0xDF, 0xE0, 0xE1, 0xE2, 0xE3, 0xE4
            )),
            new("¤£>¼½¾♥ᚋᚌᚍᚎ", Seq(
                0x4D, 0x4E,
                0xFE, 0x6A,
                0xD2, 0xD3,
                0xE5, 0xE7,
                0xE6, 0xE7,
                0xE8, 0xE9,
                0xEA, 0xEB,
                0xFE, 0x6C, 0x00,
                0xFE, 0x6C, 0x01,
                0xFE, 0x6C, 0x02,
                0xFE, 0x6C, 0x03
            )),
            new("あいうえおやゆよかきくけこわをん", Seq(
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
            )),
            new("さしすせそがぎぐたちつてとげござ", Seq(
                0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
                0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F
            )),
            new("なにぬねのじずぜはひふへほぞだぢ", Seq(
                0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27,
                0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F
            )),
            new("アイウエオヤユヨカキクケコワヲン", Seq(
                0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57,
                0x58, 0x59, 0x5A, 0x5B, 0x5C, 0x5D, 0x5E, 0x5F
            )),
            new("サシスセソガギグタチツテトゲゴザ", Seq(
                0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67,
                0x68, 0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F
            )),
            new("ナニヌネノジズゼハヒフヘホゾダ", Seq(
                0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77,
                0x78, 0x79, 0x7A, 0x7B, 0x7C, 0x7D, 0x7E
            )),
            new("マミムメモヅデドラリルレロバビブ", Seq(
                0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87,
                0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E, 0x8F
            )),
            new("ベボパピプペポャュョッァィゥェォ", Seq(
                0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97,
                0x98, 0x99, 0x9A, 0x9B, 0x9C, 0x9D, 0x9E, 0x9F
            )),
        };

        #endregion

        static string Lines(params string[] lines) => Lines(lines.AsEnumerable());
        static string Lines(IEnumerable<string> lines) => string.Join('\n', lines);

        static byte[] Bytes(params IEnumerable<byte>[] bytes)
            => bytes.SelectMany(x => x).ToArray();

        static IEnumerable<byte> Byte(byte value) => Enumerable.Repeat(value, 1);
        static IEnumerable<byte> Seq(params byte[] bytes) => bytes;
        static IEnumerable<byte> Repeat(byte value, int count)
            => Enumerable.Repeat(value, count);
        static IEnumerable<byte> Repeat(IEnumerable<byte> values, int count)
            => Enumerable.Repeat(values, count).SelectMany(x => x);
        static IEnumerable<byte> Word(byte value) => Words(Byte(value));
        static IEnumerable<byte> Words(IEnumerable<byte> values)
            => values.Select(v => new byte[] { 0x00, v }).SelectMany(x => x);

    }

}
