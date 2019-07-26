using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Randomizer.CLI.FileData {

    class Zspr {

        readonly static char[] Header = "ZSPR".ToCharArray();
        const int Version = 1;

        const int SpriteType = 1;

        public string Title { get; private set; }
        public string Author { get; private set; }
        public string AuthorAscii { get; private set; }

        public byte[] Content { get; private set; }

        static readonly IList<int> fields = new[] {
            0x7000, // sprite
            4 * 30, // palette
            4,      // gloves
        };

        public static Zspr Parse(Stream stream) {
            using var data = new BinaryReader(stream, Encoding.ASCII, true);
            if (!data.ReadChars(4).SequenceEqual(Header))
                throw new InvalidDataException("Could not find the ZSPR format header");
            var version = stream.ReadByte();
            if (version != Version)
                throw new Exception($"ZSPR version {version} is not supported, expected version ${Version}");

            stream.Position += 4; // skip checksum

            var spriteOffset = data.ReadUInt32();
            stream.Position += 2; // skip sprite length
            var paletteOffset = data.ReadUInt32();
            stream.Position += 2; // skip palette length

            var type = data.ReadUInt16();
            if (type != SpriteType)
                throw new Exception($"Expected type {SpriteType} (Link Sprite) but found type {type}");

            stream.Position += 6; // six reserved bytes

            using var utf16_data = new BinaryReader(stream, Encoding.Unicode, true);
            var title = NullTerm(utf16_data);
            var author = NullTerm(utf16_data);

            var authorAscii = NullTerm(data);

            var content = ParseContent(stream, new[] {
                spriteOffset,
                paletteOffset,
                paletteOffset + fields[1],
            });

            return new Zspr {
                Title = title,
                Author = author,
                AuthorAscii = authorAscii,
                Content = content,
            };
        }

        static byte[] ParseContent(Stream stream, IEnumerable<long> offsets) {
            using var content = new MemoryStream();
            foreach (var (length, offset) in fields.Zip(offsets, (l, o) => (l, o))) {
                stream.Position = offset;
                stream.CopyTo(content, length);
            }
            return content.ToArray();
        }

        static string NullTerm(BinaryReader reader) {
            char c;
            var text = new StringBuilder();
            while ((c = reader.ReadChar()) > 0)
                text.Append(c);
            return text.ToString();
        }

    }

}
