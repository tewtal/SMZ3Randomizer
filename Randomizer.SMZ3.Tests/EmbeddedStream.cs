using System.IO;
using System.Reflection;

namespace Randomizer.SMZ3.Tests {

    public class EmbeddedStream {

        public static Stream For(string name) {
            var type = typeof(EmbeddedStream);
            var assembly = Assembly.GetAssembly(type);
            return assembly.GetManifestResourceStream($"{type.Namespace}.{name}");
        }

    }

}
