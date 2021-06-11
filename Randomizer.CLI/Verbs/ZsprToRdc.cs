using System;
using System.IO;
using CommandLine;
using Newtonsoft.Json.Linq;
using Randomizer.CLI.FileData;

namespace Randomizer.CLI.Verbs {

    [Verb("zsprtordc", HelpText = "Convert a ZSPR link sprite file to an RDC file")]
    class ZsprToRdcOptions {

        [Value(0, Required = true)]
        public string File { get; set; }

        [Value(1)]
        public string Title { get; set; } = null;

        [Value(2)]
        public string Author { get; set; } = null;

    }

    static class ZsprToRdc {

        public static void Run(ZsprToRdcOptions opts) {
            if (!File.Exists(opts.File)) {
                Console.Error.WriteLine($"The file {opts.File} does not exist");
                return;
            }

            using var input = File.OpenRead(opts.File);
            using var output = File.Open(Path.ChangeExtension(opts.File, "z3.rdc"), FileMode.Create);

            var zspr = Zspr.Parse(input);

            var meta = new MetaDataBlock(new JObject(
                new JProperty("title", opts.Title ?? zspr.Title),
                new JProperty("author", opts.Author ?? zspr.Author)
            ));

            var sprite = new LinkSprite();
            sprite.Parse(new MemoryStream(zspr.Content));

            Rdc.Write(output, opts.Author ?? zspr.AuthorAscii, meta, sprite);
        }

    }

}
