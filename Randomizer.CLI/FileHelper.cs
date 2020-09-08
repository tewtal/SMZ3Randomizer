using System.IO;
using System.IO.Compression;

namespace Randomizer.CLI {

    public static class FileHelper {

        public static Stream OpenReadInnerStream(string filename) {
            Stream file = File.OpenRead(filename);
            if (Path.GetExtension(filename).ToLower() != ".gz")
                return file;

            using var source = new GZipStream(file, CompressionMode.Decompress);
            var stream = new MemoryStream();
            source.CopyTo(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

    }

}
