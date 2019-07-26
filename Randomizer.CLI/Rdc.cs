using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Randomizer.CLI {

    class Rdc {

        readonly static char[] header = "RETRODATACONTAINER".ToCharArray();
        const int Version = 1;

        IDictionary<uint, uint> offsets;
        IDictionary<uint, BlockType> blocks = new Dictionary<uint, BlockType>();

        public string Author { get; private set; }

        public static Rdc Parse(Stream stream) {
            using var data = new BinaryReader(stream, Encoding.UTF8, true);
            if (!data.ReadChars(18).SequenceEqual(header))
                throw new InvalidDataException("Could not find the RDC format header");
            var version = data.ReadByte();
            if (version != Version)
                throw new InvalidDataException($"RDC version {version} is not supported, expected version ${Version}");

            var blocks = data.ReadUInt32();
            var offsets = ParseBlockOffsets(data, blocks).ToList();

            var author = ReadNullTerm(data);

            return new Rdc(author, offsets);
        }

        static IEnumerable<(uint type, uint offset)> ParseBlockOffsets(BinaryReader data, uint blocks) {
            while (blocks > 0) {
                var type = data.ReadUInt32();
                var offset = data.ReadUInt32();
                yield return (type, offset);
                blocks -= 1;
            }
        }

        static string ReadNullTerm(BinaryReader reader) {
            char c;
            var text = new StringBuilder();
            while ((c = reader.ReadChar()) > 0)
                text.Append(c);
            return text.ToString();
        }

        Rdc(string author, IEnumerable<(uint type, uint offset)> offsets) {
            Author = author;
            this.offsets = offsets.ToDictionary(x => x.type, x => x.offset);
        }

        public bool TryParse<TBlock>(Stream stream, out BlockType block) where TBlock : BlockType, new() {
            block = default;

            var template = new TBlock();
            if (!offsets.ContainsKey(template.Type))
                return false;

            block = template;
            if (blocks.TryGetValue(block.Type, out var cache)) {
                block = (TBlock) cache;
                return true;
            }

            stream.Position = offsets[block.Type];
            block.Parse(stream);
            return true;
        }

        public bool Contains<TBlock>() where TBlock : BlockType, new() {
            var template = new TBlock();
            return offsets.ContainsKey(template.Type);
        }

        public static void Write(Stream stream, string author, params BlockType[] blocks) {
            using var data = new BinaryWriter(stream, Encoding.UTF8, true);
            data.Write(header);
            data.Write((byte) Version);

            var authorData = NullTermBytes(author);

            data.Write((uint) blocks.Length);
            var offset = stream.Position + blocks.Length * 2 * sizeof(uint) + authorData.Length;
            foreach (var block in blocks) {
                data.Write(block.Type);
                data.Write((uint) offset);
                offset += block.Length;
            }

            stream.Write(authorData);

            foreach (var block in blocks) {
                block.Write(stream);
            }
        }

        static byte[] NullTermBytes(string author) {
            using var writer = new MemoryStream();
            writer.Write(Encoding.UTF8.GetBytes(author));
            writer.WriteByte(0);
            return writer.ToArray();
        }

    }

    interface BlockType {
        uint Type { get; }
        int Length { get; }
        void Parse(Stream rdc);
        void Write(Stream rdc);
    }

    class MetaDataBlock : BlockType {

        public uint Type { get; } = 0;

        public JToken Content { get; private set; }

        public int Length {
            get { return sizeof(uint) + Encoding.UTF8.GetByteCount(Content.ToString(Formatting.None)); }
        }

        public MetaDataBlock() { }

        public MetaDataBlock(JToken content) {
            Content = content;
        }

        public void Parse(Stream stream) {
            using var data = new BinaryReader(stream, Encoding.UTF8, true);
            var length = data.ReadUInt32();
            var bytes = data.ReadBytes((int) length);
            var meta = Encoding.UTF8.GetString(bytes);
            Content = JToken.Parse(meta);
        }

        public void Write(Stream stream) {
            using var data = new BinaryWriter(stream, Encoding.UTF8, true);
            var meta = Content.ToString(Formatting.None);
            var bytes = Encoding.UTF8.GetBytes(meta);
            data.Write((uint) bytes.Length);
            data.Write(bytes);
        }

    }

    abstract class DataBlock : BlockType {

        public abstract uint Type { get; }

        protected abstract IList<(IEnumerable<int>, int, IEnumerable<int>)> Manifest { get; }

        protected readonly IList<byte[]> content = new List<byte[]>();

        public int Length {
            get {
                return Manifest.Sum(field => {
                    var (_, length, offsets) = field;
                    return length * offsets.Count();
                });
            }
        }

        public void Parse(Stream stream) {
            byte[] slice;
            foreach (var (_, length, offsets) in Manifest) {
                var count = offsets.Count();
                stream.Read(slice = new byte[count * length]);
                content.Add(slice);
            }
        }

        public void Write(Stream stream) {
            foreach (var data in content) {
                stream.Write(data);
            }
        }

        public void Apply(byte[] rom) {
            foreach (var (field, data) in Manifest.Zip(content, (m, d) => (m, d))) {
                var (addrs, length, offsets) = field;
                foreach (var addr in addrs) {
                    var i = -1;
                    foreach (var offset in offsets) {
                        Array.Copy(data, (i += 1) * length, rom, addr + offset, length);
                    }
                }
            }
        }

        protected static IEnumerable<int> Single { get; } = new[] { 0 };

        protected static IEnumerable<int> Addr(params int[] addrs) => addrs;

        protected static IEnumerable<int> Offsets(int n, int offset) {
            return Enumerable.Range(0, n).Select(x => x * offset).ToList();
        }

    }

    class LinkSprite : DataBlock {

        public override uint Type { get; } = 1;

        protected override IList<(IEnumerable<int>, int, IEnumerable<int>)> Manifest { get; } = manifest;

        static readonly IList<(IEnumerable<int>, int, IEnumerable<int>)> manifest;

        static LinkSprite() {
            manifest = new[] {
                (Addr(0x508000), 0x7000, Single), // sprite
                (Addr(0x5BD308), 4 * 30, Single), // palette
                (Addr(0x5BEDF5), 4, Single),      // gloves
            };
        }

        public byte[] Fetch8x8(int tileIndex) {
            byte[] bytes;
            content[0].AsSpan(tileIndex * 0x20, 0x20).CopyTo(bytes = new byte[0x20]);
            return bytes;
        }

        public byte[] FetchPalette(int index) {
            byte[] bytes;
            content[1].AsSpan(index * 30, 30).CopyTo(bytes = new byte[30]);
            return bytes;
        }

    }

    class SamusSprite : DataBlock {

        public override uint Type { get; } = 4;

        protected override IList<(IEnumerable<int>, int, IEnumerable<int>)> Manifest { get; } = manifest;

        static readonly IList<(IEnumerable<int>, int, IEnumerable<int>)> manifest;

        static SamusSprite() {
            var loaderOffsets = new[] { 0x0, 0x24, 0x4F, 0x73, 0x9E, 0xC2, 0xED, 0x111, 0x139 };
            manifest = new[] {
                // DMA banks
                (Addr(0x440000), 0x8000, Single),     // DMA bank 1
                (Addr(0x450000), 0x8000, Single),     // DMA bank 2
                (Addr(0x460000), 0x8000, Single),     // DMA bank 3
                (Addr(0x470000), 0x8000, Single),     // DMA bank 4
                (Addr(0x480000), 0x8000, Single),     // DMA bank 5
                (Addr(0x490000), 0x8000, Single),     // DMA bank 6
                (Addr(0x4A0000), 0x8000, Single),     // DMA bank 7
                (Addr(0x4B0000), 0x8000, Single),     // DMA bank 8
                (Addr(0x540000), 0x8000, Single),     // DMA bank 9
                (Addr(0x550000), 0x8000, Single),     // DMA bank 10
                (Addr(0x560000), 0x8000, Single),     // DMA bank 11
                (Addr(0x570000), 0x8000, Single),     // DMA bank 12
                (Addr(0x580000), 0x8000, Single),     // DMA bank 13
                (Addr(0x590000), 0x7880, Single),     // DMA bank 14
                (Addr(0x5A0000), 0x3F60, Single),     // Death left
                (Addr(0x5A4000), 0x3F60, Single),     // Death right
                (Addr(0x1A9A00), 0x3C0, Single),      // Gun port data
                (Addr(0x36DA00), 0x600, Single),      // File select sprites
                (Addr(0x36D900), 0x20, Single),       // File select missile
                (Addr(0x36D980), 0x20, Single),       // File select missile head
                (Addr(0x1B9402), 30, Single),         // Power Standard
                (Addr(0x1B9522), 30, Single),         // Varia Standard
                (Addr(0x1B9802), 30, Single),         // Gravity Standard
                (Addr(0xDDB6D), 30, loaderOffsets),     // Power Loader
                (Addr(0xDDCD3), 30, loaderOffsets),     // Varia Loader
                (Addr(0xDDE39), 30, loaderOffsets),     // Gravity Loader
                (Addr(0xDE468), 30, Offsets(16, 0x22)), // Power Heat
                (Addr(0xDE694), 30, Offsets(16, 0x22)), // Varia Heat
                (Addr(0xDE8C0), 30, Offsets(16, 0x22)), // Gravity Heat
                (Addr(0x1B9822), 30, Offsets(8, 0x20)), // Power Charge
                (Addr(0x1B9922), 30, Offsets(8, 0x20)), // Varia Charge
                (Addr(0x1B9A22), 30, Offsets(8, 0x20)), // Gravity Charge
                (Addr(0x1B9B22), 30, Offsets(4, 0x20)), // Power Speed boost
                (Addr(0x1B9D22), 30, Offsets(4, 0x20)), // Varia Speed boost
                (Addr(0x1B9F22), 30, Offsets(4, 0x20)), // Gravity Speed boost
                (Addr(0x1B9BA2), 30, Offsets(4, 0x20)), // Power Speed squat
                (Addr(0x1B9DA2), 30, Offsets(4, 0x20)), // Varia Speed squat
                (Addr(0x1B9FA2), 30, Offsets(4, 0x20)), // Gravity Speed squat
                (Addr(0x1B9C22), 30, Offsets(4, 0x20)), // Power Shinespark
                (Addr(0x1B9E22), 30, Offsets(4, 0x20)), // Varia Shinespark
                (Addr(0x1BA022), 30, Offsets(4, 0x20)), // Gravity Shinespark
                (Addr(0x1B9CA2), 30, Offsets(4, 0x20)), // Power Screw attack
                (Addr(0x1B9EA2), 30, Offsets(4, 0x20)), // Varia Screw attack
                (Addr(0x1BA0A2), 30, Offsets(4, 0x20)), // Gravity Screw attack
                (Addr(0x1B96C2), 30, Offsets(6, 0x20)), // Crystal flash
                (Addr(0x1BA122), 30, Offsets(9, 0x20)), // Death
                (Addr(0x1BA242), 30, Offsets(10, 0x20)),// Hyper beam
                (Addr(0x1BA3A2,
                      0xCE56B), 30, Single),          // Sepia
                (Addr(0x1BA382), 30, Single),         // Sepia hurt
                (Addr(0x1BA3C0,
                      0x1BA3C6), 6, Single),          // Xray
                (Addr(0x2E52C), 2, Single),           // Door Visor
                (Addr(0xEE5E2), 30, Single),          // File select
                (Addr(0xCE68B), 30, Single),          // Ship Intro
                (Addr(0xDD6C2), 30, Offsets(16, 0x24)), // Ship Outro
                (Addr(0x22A5A0), 28, Single),         // Ship Standard
                (Addr(0xDCA54), 2, Offsets(14, 0x6)),   // Ship Glow
            };
        }

        public byte[] FetchDma8x8(int tileIndex) {
            byte[] bytes;
            var addr = tileIndex * 0x20;
            var bank = content[addr / 0x8000];
            bank.AsSpan(addr % 0x8000, 0x20).CopyTo(bytes = new byte[0x20]);
            return bytes;
        }

        public byte[] FetchPowerStandardPalette() {
            return content[20];
        }

    }

}
