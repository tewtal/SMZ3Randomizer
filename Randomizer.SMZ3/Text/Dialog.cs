using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Randomizer.SMZ3.Text {

    static class Dialog {

        static readonly Regex command = new Regex(@"^\{[^}]*\}");
        static readonly Regex invalid = new Regex(@"(?<!^)\{[^}]*\}(?!$)", RegexOptions.Multiline);
        static readonly Regex digit = new Regex(@"\d");
        static readonly Regex uppercaseLetter = new Regex("[A-Z]");
        static readonly Regex lowercaseLetter = new Regex("[a-z]");

        public static byte[] Simple(string text) {
            const int maxBytes = 256;
            const int wrap = 19;

            var bytes = new List<byte>();
            var lines = text.Split('\n');
            var lineIndex = 0;
            foreach (var line in lines) {
                bytes.Add(lineIndex switch {
                    0 => (byte)0x74,
                    1 => (byte)0x75,
                    _ => (byte)0x76,
                });
                var letters = line.Length > wrap ? line[..wrap] : line;
                foreach (var letter in letters) {
                    var write = LetterToBytes(letter);
                    if (write[0] == 0xFD) {
                        bytes.AddRange(write);
                    } else {
                        foreach (var b in write)
                            bytes.AddRange(new byte[] { 0x00, b });
                    }
                }

                lineIndex += 1;

                if (lineIndex % 3 == 0 && lineIndex < lines.Length)
                    bytes.Add(0x7E);
                if (lineIndex >= 3 && lineIndex < lines.Length)
                    bytes.Add(0x73);
            }

            bytes.Add(0x7F);
            if (bytes.Count > maxBytes)
                return bytes.Take(maxBytes - 1).Append<byte>(0x7F).ToArray();

            return bytes.ToArray();
        }

        public static byte[] Compiled(string text, bool pause = true) {
            const int maxBytes = 2046;
            const int wrap = 19;

            if (invalid.IsMatch(text))
                throw new ArgumentException("Dialog commands must be placed on separate lines", nameof(text));

            bool padOut = false;

            var bytes = new List<byte> { 0xFB };
            var lines = Wordwrap(text, wrap);
            var lineCount = lines.Count(l => !command.IsMatch(l));
            var lineIndex = 0;
            foreach (var line in lines) {
                var match = command.Match(line);
                if (match.Success) {
                    if (match.Value == "{NOTEXT}")
                        return new byte[] { 0xFB, 0xFE, 0x6E, 0x00, 0xFE, 0x6B, 0x04 };
                    if (match.Value == "{INTRO}")
                        padOut = true;

                    bytes.AddRange(match.Value switch {
                        "{SPEED0}" => new byte[] { 0xFC, 0x00 },
                        "{SPEED2}" => new byte[] { 0xFC, 0x02 },
                        "{SPEED6}" => new byte[] { 0xFC, 0x06 },
                        "{PAUSE1}" => new byte[] { 0xFE, 0x78, 0x01 },
                        "{PAUSE3}" => new byte[] { 0xFE, 0x78, 0x03 },
                        "{PAUSE5}" => new byte[] { 0xFE, 0x78, 0x05 },
                        "{PAUSE7}" => new byte[] { 0xFE, 0x78, 0x07 },
                        "{PAUSE9}" => new byte[] { 0xFE, 0x78, 0x09 },
                        "{INPUT}" => new byte[] { 0xFA },
                        "{CHOICE}" => new byte[] { 0xFE, 0x68 },
                        "{ITEMSELECT}" => new byte[] { 0xFE, 0x69 },
                        "{CHOICE2}" => new byte[] { 0xFE, 0x71 },
                        "{CHOICE3}" => new byte[] { 0xFE, 0x72 },
                        "{C:GREEN}" => new byte[] { 0xFE, 0x77, 0x07 },
                        "{C:YELLOW}" => new byte[] { 0xFE, 0x77, 0x02 },
                        "{HARP}" => new byte[] { 0xFE, 0x79, 0x2D },
                        "{MENU}" => new byte[] { 0xFE, 0x6D, 0x00 },
                        "{BOTTOM}" => new byte[] { 0xFE, 0x6D, 0x01 },
                        "{NOBORDER}" => new byte[] { 0xFE, 0x6B, 0x02 },
                        "{CHANGEPIC}" => new byte[] { 0xFE, 0x67, 0xFE, 0x67 },
                        "{CHANGEMUSIC}" => new byte[] { 0xFE, 0x67 },
                        "{INTRO}" => new byte[] { 0xFE, 0x6E, 0x00, 0xFE, 0x77, 0x07, 0xFC, 0x03, 0xFE, 0x6B, 0x02, 0xFE, 0x67 },
                        "{IBOX}" => new byte[] { 0xFE, 0x6B, 0x02, 0xFE, 0x77, 0x07, 0xFC, 0x03, 0xF7 },
                        var command => throw new ArgumentException($"Dialog text contained unknown command {command}", nameof(text)),
                    });

                    if (bytes.Count > maxBytes)
                        throw new ArgumentException("Command overflowed maximum byte length", nameof(text));

                    continue;
                }

                if (lineIndex == 1)
                    bytes.Add(0xF8); // row 2
                else if (lineIndex >= 3 && lineIndex < lineCount)
                    bytes.Add(0xF6); // scroll
                else if (lineIndex >= 2)
                    bytes.Add(0xF9); // row 3

                // The first box needs to fill the full width with spaces as the palette is loaded weird.
                var letters = padOut && lineIndex < 3 ? line.PadRight(wrap) : line;
                bytes.AddRange(letters.SelectMany(LetterToBytes));

                lineIndex += 1;

                if (pause && lineIndex % 3 == 0 && lineIndex < lineCount)
                    bytes.Add(0xFA); // wait for input
            }

            return bytes.Take(maxBytes).ToArray();
        }

        static IEnumerable<string> Wordwrap(string text, int width) {
            return text.Split('\n').SelectMany(line => {
                line = line.TrimEnd();
                if (line.Length <= width)
                    return new[] { line };
                var words = line.Split(' ');
                IEnumerable<string> lines = new[] { "" };
                lines = words.Aggregate(new Stack<string>(lines), (lines, word) => {
                    var line = lines.Pop();
                    if (line.Length + word.Length <= width) {
                        line = $"{line}{word} ";
                    }
                    else {
                        if (line.Length > 0)
                            lines.Push(line);
                        line = word;
                        while (line.Length > width) {
                            lines.Push(line[..width]);
                            line = line[width..];
                        }
                        line = $"{line} ";
                    }
                    lines.Push(line);
                    return lines;
                });
                return lines.Reverse().Select(l => l.TrimEnd());
            });
        }

        static byte[] LetterToBytes(char c) {
            return c switch {
                _ when digit.IsMatch(c.ToString()) => new byte[] { (byte)(c - '0' + 0xA0) },
                _ when uppercaseLetter.IsMatch(c.ToString()) => new byte[] { (byte)(c - 'A' + 0xAA) },
                // TODO: The lowercase character ID range is not yet fully decided
                _ when lowercaseLetter.IsMatch(c.ToString()) => new byte[] { (byte)(c - 'a' + 0x30) },
                _ => letters.TryGetValue(c, out byte[] bytes) ? bytes : new byte[] { 0xFF },
            };
        }

        #region letter bytes lookup

        static readonly IDictionary<char, byte[]> letters = new Dictionary<char, byte[]> {
            { ' ', new byte[] { 0xFF } },
            { '?', new byte[] { 0xC6 } },
            { '!', new byte[] { 0xC7 } },
            { ',', new byte[] { 0xC8 } },
            { '-', new byte[] { 0xC9 } },
            { '…', new byte[] { 0xCC } },
            { '.', new byte[] { 0xCD } },
            { '~', new byte[] { 0xCE } },
            { '～', new byte[] { 0xCE } },
            { '\'', new byte[] { 0xD8 } },
            { '’', new byte[] { 0xD8 } },
            { '@', new byte[] { 0xFE, 0x6A } }, // link's name compressed
            { '>', new byte[] { 0xD2, 0xD3 } }, // link face
            { '%', new byte[] { 0xDD } }, // Hylian Bird
            { '^', new byte[] { 0xDE } }, // Hylian Ankh
            { '=', new byte[] { 0xDF } }, // Hylian Wavy lines
            { '↑', new byte[] { 0xE0 } },
            { '↓', new byte[] { 0xE1 } },
            { '→', new byte[] { 0xE2 } },
            { '←', new byte[] { 0xE3 } },
            { '≥', new byte[] { 0xE4 } }, // cursor
            { '¼', new byte[] { 0xE5, 0xE7 } }, // 1/4 heart
            { '½', new byte[] { 0xE6, 0xE7 } }, // 1/2 heart
            { '¾', new byte[] { 0xE8, 0xE9 } }, // 3/4 heart
            { '♥', new byte[] { 0xEA, 0xEB } }, // full heart
            { 'ᚋ', new byte[] { 0xFE, 0x6C, 0x00 } }, // var 0
            { 'ᚌ', new byte[] { 0xFE, 0x6C, 0x01 } }, // var 1
            { 'ᚍ', new byte[] { 0xFE, 0x6C, 0x02 } }, // var 2
            { 'ᚎ', new byte[] { 0xFE, 0x6C, 0x03 } }, // var 3
            { 'あ', new byte[] { 0x00 } },
            { 'い', new byte[] { 0x01 } },
            { 'う', new byte[] { 0x02 } },
            { 'え', new byte[] { 0x03 } },
            { 'お', new byte[] { 0x04 } },
            { 'や', new byte[] { 0x05 } },
            { 'ゆ', new byte[] { 0x06 } },
            { 'よ', new byte[] { 0x07 } },
            { 'か', new byte[] { 0x08 } },
            { 'き', new byte[] { 0x09 } },
            { 'く', new byte[] { 0x0A } },
            { 'け', new byte[] { 0x0B } },
            { 'こ', new byte[] { 0x0C } },
            { 'わ', new byte[] { 0x0D } },
            { 'を', new byte[] { 0x0E } },
            { 'ん', new byte[] { 0x0F } },
            { 'さ', new byte[] { 0x10 } },
            { 'し', new byte[] { 0x11 } },
            { 'す', new byte[] { 0x12 } },
            { 'せ', new byte[] { 0x13 } },
            { 'そ', new byte[] { 0x14 } },
            { 'が', new byte[] { 0x15 } },
            { 'ぎ', new byte[] { 0x16 } },
            { 'ぐ', new byte[] { 0x17 } },
            { 'た', new byte[] { 0x18 } },
            { 'ち', new byte[] { 0x19 } },
            { 'つ', new byte[] { 0x1A } },
            { 'て', new byte[] { 0x1B } },
            { 'と', new byte[] { 0x1C } },
            { 'げ', new byte[] { 0x1D } },
            { 'ご', new byte[] { 0x1E } },
            { 'ざ', new byte[] { 0x1F } },
            { 'な', new byte[] { 0x20 } },
            { 'に', new byte[] { 0x21 } },
            { 'ぬ', new byte[] { 0x22 } },
            { 'ね', new byte[] { 0x23 } },
            { 'の', new byte[] { 0x24 } },
            { 'じ', new byte[] { 0x25 } },
            { 'ず', new byte[] { 0x26 } },
            { 'ぜ', new byte[] { 0x27 } },
            { 'は', new byte[] { 0x28 } },
            { 'ひ', new byte[] { 0x29 } },
            { 'ふ', new byte[] { 0x2A } },
            { 'へ', new byte[] { 0x2B } },
            { 'ほ', new byte[] { 0x2C } },
            { 'ぞ', new byte[] { 0x2D } },
            { 'だ', new byte[] { 0x2E } },
            { 'ぢ', new byte[] { 0x2F } },

            // TODO: Remove these when the lowercase character ID range is completely decided
            //{ 'ま', new byte[] { 0x30 } },
            //{ 'み', new byte[] { 0x31 } },
            //{ 'む', new byte[] { 0x32 } },
            //{ 'め', new byte[] { 0x33 } },
            //{ 'も', new byte[] { 0x34 } },
            //{ 'づ', new byte[] { 0x35 } },
            //{ 'で', new byte[] { 0x36 } },
            //{ 'ど', new byte[] { 0x37 } },
            //{ 'ら', new byte[] { 0x38 } },
            //{ 'り', new byte[] { 0x39 } },
            //{ 'る', new byte[] { 0x3A } },
            //{ 'れ', new byte[] { 0x3B } },
            //{ 'ろ', new byte[] { 0x3C } },
            //{ 'ば', new byte[] { 0x3D } },
            //{ 'び', new byte[] { 0x3E } },
            //{ 'ぶ', new byte[] { 0x3F } },
            //{ 'べ', new byte[] { 0x40 } },
            //{ 'ぼ', new byte[] { 0x41 } },
            //{ 'ぱ', new byte[] { 0x42 } },
            //{ 'ぴ', new byte[] { 0x43 } },
            //{ 'ぷ', new byte[] { 0x44 } },
            //{ 'ぺ', new byte[] { 0x45 } },
            //{ 'ぽ', new byte[] { 0x46 } },
            //{ 'ゃ', new byte[] { 0x47 } },
            //{ 'ゅ', new byte[] { 0x48 } },
            //{ 'ょ', new byte[] { 0x49 } },
            //{ 'っ', new byte[] { 0x4A } },
            //{ 'ぁ', new byte[] { 0x4B } },
            //{ 'ぃ', new byte[] { 0x4C } },
            //{ 'ぅ', new byte[] { 0x4D } },
            //{ 'ぇ', new byte[] { 0x4E } },
            //{ 'ぉ', new byte[] { 0x4F } },
            
            { 'ア', new byte[] { 0x50 } },
            { 'イ', new byte[] { 0x51 } },
            { 'ウ', new byte[] { 0x52 } },
            { 'エ', new byte[] { 0x53 } },
            { 'オ', new byte[] { 0x54 } },
            { 'ヤ', new byte[] { 0x55 } },
            { 'ユ', new byte[] { 0x56 } },
            { 'ヨ', new byte[] { 0x57 } },
            { 'カ', new byte[] { 0x58 } },
            { 'キ', new byte[] { 0x59 } },
            { 'ク', new byte[] { 0x5A } },
            { 'ケ', new byte[] { 0x5B } },
            { 'コ', new byte[] { 0x5C } },
            { 'ワ', new byte[] { 0x5D } },
            { 'ヲ', new byte[] { 0x5E } },
            { 'ン', new byte[] { 0x5F } },
            { 'サ', new byte[] { 0x60 } },
            { 'シ', new byte[] { 0x61 } },
            { 'ス', new byte[] { 0x62 } },
            { 'セ', new byte[] { 0x63 } },
            { 'ソ', new byte[] { 0x64 } },
            { 'ガ', new byte[] { 0x65 } },
            { 'ギ', new byte[] { 0x66 } },
            { 'グ', new byte[] { 0x67 } },
            { 'タ', new byte[] { 0x68 } },
            { 'チ', new byte[] { 0x69 } },
            { 'ツ', new byte[] { 0x6A } },
            { 'テ', new byte[] { 0x6B } },
            { 'ト', new byte[] { 0x6C } },
            { 'ゲ', new byte[] { 0x6D } },
            { 'ゴ', new byte[] { 0x6E } },
            { 'ザ', new byte[] { 0x6F } },
            { 'ナ', new byte[] { 0x70 } },
            { 'ニ', new byte[] { 0x71 } },
            { 'ヌ', new byte[] { 0x72 } },
            { 'ネ', new byte[] { 0x73 } },
            { 'ノ', new byte[] { 0x74 } },
            { 'ジ', new byte[] { 0x75 } },
            { 'ズ', new byte[] { 0x76 } },
            { 'ゼ', new byte[] { 0x77 } },
            { 'ハ', new byte[] { 0x78 } },
            { 'ヒ', new byte[] { 0x79 } },
            { 'フ', new byte[] { 0x7A } },
            { 'ヘ', new byte[] { 0x7B } },
            { 'ホ', new byte[] { 0x7C } },
            { 'ゾ', new byte[] { 0x7D } },
            { 'ダ', new byte[] { 0x7E } },
            { 'マ', new byte[] { 0x80 } },
            { 'ミ', new byte[] { 0x81 } },
            { 'ム', new byte[] { 0x82 } },
            { 'メ', new byte[] { 0x83 } },
            { 'モ', new byte[] { 0x84 } },
            { 'ヅ', new byte[] { 0x85 } },
            { 'デ', new byte[] { 0x86 } },
            { 'ド', new byte[] { 0x87 } },
            { 'ラ', new byte[] { 0x88 } },
            { 'リ', new byte[] { 0x89 } },
            { 'ル', new byte[] { 0x8A } },
            { 'レ', new byte[] { 0x8B } },
            { 'ロ', new byte[] { 0x8C } },
            { 'バ', new byte[] { 0x8D } },
            { 'ビ', new byte[] { 0x8E } },
            { 'ブ', new byte[] { 0x8F } },
            { 'ベ', new byte[] { 0x90 } },
            { 'ボ', new byte[] { 0x91 } },
            { 'パ', new byte[] { 0x92 } },
            { 'ピ', new byte[] { 0x93 } },
            { 'プ', new byte[] { 0x94 } },
            { 'ペ', new byte[] { 0x95 } },
            { 'ポ', new byte[] { 0x96 } },
            { 'ャ', new byte[] { 0x97 } },
            { 'ュ', new byte[] { 0x98 } },
            { 'ョ', new byte[] { 0x99 } },
            { 'ッ', new byte[] { 0x9A } },
            { 'ァ', new byte[] { 0x9B } },
            { 'ィ', new byte[] { 0x9C } },
            { 'ゥ', new byte[] { 0x9D } },
            { 'ェ', new byte[] { 0x9E } },
            { 'ォ', new byte[] { 0x9F } },
        };

        #endregion

    }

}
