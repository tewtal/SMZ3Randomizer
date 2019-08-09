using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Randomizer.CLI.FileData;

namespace Randomizer.CLI.Verbs {

    abstract class SpriteTaskOptions {

        [Value(0, Required = true,
            HelpText = "Specify the path to the RDC root directory")]
        public string Directory { get; set; }

        [Value(1, Required = true,
            HelpText = "Specify the path to the JSON file to read or write")]
        public string Inventory { get; set; }

    }

    [Verb("spriteinventory", HelpText = "Generate a JSON file that make inventory of RDC sprite recourses under a directory")]
    class SpriteInventoryOptions : SpriteTaskOptions { }

    [Verb("spritemontage", HelpText = "")]
    class SpriteMontageOptions : SpriteTaskOptions {

        [Option("z3", Required = true,
            HelpText = "Specify the path to the PNG file to generate for the Z3 sprite montage")]
        public string Z3File { get; set; }

        [Option("sm", Required = true,
            HelpText = "Specify the path to the PNG file to generate for the SM sprite montage")]
        public string SMFile { get; set; }
    }

    static class SpriteTasks {

        enum Game { None, Z3, SM }

        static readonly Regex spriteNamePattern = new Regex(@"(\.[^.]*)*$");
        static readonly Regex capitalizePattern = new Regex(@"\b([\w-[\d_]])(\k<1>*)");

        public static void Run(SpriteInventoryOptions opts) {
            if (!Directory.Exists(opts.Directory)) {
                Console.Error.WriteLine($"The directory {opts.Directory} does not exist");
                return;
            }

            var root = opts.Directory;
            var files = Directory.EnumerateFiles(root, "*.rdc", SearchOption.AllDirectories);
            var sprites = files.Select(path => {
                using var stream = File.OpenRead(path);
                var rdc = Rdc.Parse(stream);

                var game =
                    rdc.Contains<LinkSprite>() ? Game.Z3 :
                    rdc.Contains<SamusSprite>() ? Game.SM : Game.None;

                return game switch {
                    Game.None => (Game.None, null),
                    _ => (game, data: new {
                        title = ComposeSpriteName(rdc, stream, path),
                        path = Path.GetRelativePath(root, path).Replace('\\', '/')
                    }),
                };
            });

            var json = new JObject(
                new JProperty("z3",
                    from sprite in sprites
                    where sprite.game == Game.Z3
                    orderby sprite.data.title
                    select new JObject(
                        new JProperty("title", sprite.data.title),
                        new JProperty("path", sprite.data.path))),
                new JProperty("sm",
                    from sprite in sprites
                    where sprite.game == Game.SM
                    orderby sprite.data.title
                    select new JObject(
                        new JProperty("title", sprite.data.title),
                        new JProperty("path", sprite.data.path))));

            File.WriteAllText(opts.Inventory, json.ToString(Formatting.Indented));
        }

        static string ComposeSpriteName(Rdc rdc, Stream stream, string path) {
            if (rdc.TryParse<MetaDataBlock>(stream, out var block)) {
                return (block as MetaDataBlock).Content.Value<string>("title");
            } else {
                var title = spriteNamePattern.Replace(Path.GetFileName(path), "");
                title = capitalizePattern.Replace(title, m => $"{m.Groups[1].Value.ToUpper()}{m.Groups[2].Value.ToLower()}");
                return title.Replace('_', ' ');
            }
        }

        public static void Run(SpriteMontageOptions opts) {
            if (!Directory.Exists(opts.Directory)) {
                Console.Error.WriteLine($"The directory {opts.Directory} does not exist");
                return;
            }
            if (!File.Exists(opts.Inventory)) {
                Console.Error.WriteLine($"The file {opts.Inventory} does not exist");
                return;
            }

            var root = opts.Directory;
            using var reader = File.OpenText(opts.Inventory);
            var json = JToken.ReadFrom(new JsonTextReader(reader)) as JObject;

            IEnumerable<string> paths(JArray json) {
                return from entry in json.Children<JObject>()
                       select Path.Combine(root, entry.Value<string>("path").Replace('/', '\\'));
            }

            CompileZ3Montage(opts.Z3File, paths(json.Value<JArray>("z3")));
            CompileSMMontage(opts.SMFile, paths(json.Value<JArray>("sm")));
        }

        static void CompileZ3Montage(string filename, IEnumerable<string> paths) {
            using var montage = new Bitmap(16 * (paths.Count() + 1), 24);
            using var g = Graphics.FromImage(montage);
            g.Clear(Color.Transparent);

            var offset = -1;

            using var linkResource = ManifestResource.EmbeddedStreamFor("Resources.link.png");
            using var link = new Bitmap(linkResource);
            g.DrawImage(link, 0, 0);
            offset += 1;

            foreach (var path in paths) {
                offset += 1;
                using var stream = File.OpenRead(path);
                var rdc = Rdc.Parse(stream);
                if (!rdc.TryParse<LinkSprite>(stream, out var block))
                    throw new InvalidDataException($"RDC file at {path} is assumed to have a link sprite block");

                var sprite = (LinkSprite) block;
                var palette = ConvertPalette(sprite.FetchPalette(0));

                Bitmap tile(int index) {
                    var bytes = sprite.Fetch8x8(index);
                    var tile = ConvertTile(bytes);
                    return RenderTile(tile, palette);
                }

                void pasteSprite(int index, Point origin) {
                    foreach (var (x, y) in Enumerable.Range(0, 4).Select(i => (i % 2, i / 2))) {
                        using var image = tile(index + x + y * 0x10);
                        g.DrawImage(image, origin + new Size(x * 8, y * 8));
                    }
                }

                pasteSprite(0x26, new Point { X = offset * 16, Y = 8 }); // Body
                pasteSprite(0x02, new Point { X = offset * 16, Y = 0 }); // Head
            }

            montage.Save(filename, ImageFormat.Png);
        }

        static void CompileSMMontage(string filename, IEnumerable<string> paths) {
            using var montage = new Bitmap(32 * (paths.Count() + 2), 48);
            using var g = Graphics.FromImage(montage);
            g.Clear(Color.Transparent);

            var offset = -1;

            using var samusResource = ManifestResource.EmbeddedStreamFor("Resources.samus.png");
            using var samus = new Bitmap(samusResource);
            g.DrawImage(samus, 0, 0);
            g.DrawImage(samus, 32, 0);
            offset += 2;

            var poseOffset = 6 * 1024;
            foreach (var path in paths) {
                offset += 1;
                using var stream = File.OpenRead(path);
                var rdc = Rdc.Parse(stream);
                if (!rdc.TryParse<SamusSprite>(stream, out var block))
                    throw new InvalidDataException($"RDC file at {path} is assumed to have a samus sprite block");

                var sprite = (SamusSprite) block;
                var palette = ConvertPalette(sprite.FetchPowerStandardPalette());

                Bitmap tile(int index) {
                    var bytes = sprite.FetchDma8x8(index);
                    var tile = ConvertTile(bytes);
                    return RenderTile(tile, palette);
                }

                void pasteRow(int index, int count, Point origin) {
                    foreach (var x in Enumerable.Range(0, count))
                        g.DrawImage(tile(index + x), origin + new Size(x * 8, 0));
                }

                pasteRow(poseOffset + 0, 4, new Point { X = offset * 32, Y = 0 });
                pasteRow(poseOffset + 4, 4, new Point { X = offset * 32, Y = 16 });
                pasteRow(poseOffset + 8, 4, new Point { X = offset * 32, Y = 32 });
                pasteRow(poseOffset + 12, 4, new Point { X = offset * 32, Y = 8 });
                pasteRow(poseOffset + 16, 4, new Point { X = offset * 32, Y = 24 });
                pasteRow(poseOffset + 20, 4, new Point { X = offset * 32, Y = 40 });
            }

            montage.Save(filename, ImageFormat.Png);
        }

        static IList<Color> ConvertPalette(byte[] data) {
            return data.Chunk(2).Select(x => ConvertColor(x.ToArray())).Prepend(Color.Transparent).ToList();
        }

        static Color ConvertColor(byte[] color) {
            ushort value = BitConverter.ToUInt16(color);
            return Color.FromArgb(
                ((value >> 0) & 0x1F) << 3,
                ((value >> 5) & 0x1F) << 3,
                ((value >> 10) & 0x1F) << 3);
        }

        static IEnumerable<int> ConvertTile(byte[] tile) {
            const byte m0 = 0x80;
            return (from row in (from x in tile.Select((b, i) => (b, row: i % 16 / 2))
                                 orderby x.row
                                 select x.b).Chunk(4)
                    select row.ToList() into row
                    from m in Enumerable.Range(0, 8).Select(k => m0 >> k)
                    select ((row[3] & m) == 0 ? 0 : 8)
                        | ((row[2] & m) == 0 ? 0 : 4)
                        | ((row[1] & m) == 0 ? 0 : 2)
                        | ((row[0] & m) == 0 ? 0 : 1)
            ).ToList();
        }

        static Bitmap RenderTile(IEnumerable<int> tile, IList<Color> palette) {
            var image = new Bitmap(8, 8);
            foreach (var (c, i) in tile.Select((c, i) => (palette[c], i)))
                image.SetPixel(i % 8, i / 8, c);
            return image;
        }

    }

}
