using System;
using System.IO;
using CommandLine;
using Newtonsoft.Json.Linq;

namespace Randomizer.CLI {

    [Verb("zsprtordc", HelpText = "Convert a ZSPR link sprite file to an RDC file")]
    class ZsprToRdcOptions {
        [Value(0, Required = true)] public string File { get; set; }
    }

    static class ZsprToRdc {

        public static void Run(ZsprToRdcOptions opts) {
            var inputFilename = opts.File;
            var outputFilename = Path.ChangeExtension(opts.File, "z3.rdc");

            if (!File.Exists(opts.File))
                Console.Error.WriteLine($"The file {opts.File} does not exist");

            using var input = File.OpenRead(opts.File);
            using var output = File.Open(outputFilename, FileMode.Create);

            var zspr = Zspr.Parse(input);

            var meta = new MetaDataBlock(new JObject(
                    new JProperty("title", zspr.Title),
                    new JProperty("author", zspr.Author)));

            var sprite = new LinkSprite();
            sprite.Parse(new MemoryStream(zspr.Content));

            Rdc.Write(output, zspr.AuthorAscii, meta, sprite);
        }

    }

}
