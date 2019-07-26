using System.IO;
using System.Reflection;

namespace Randomizer.CLI {

    static class ManifestResource {

        public static Stream EmbeddedStreamFor(string name) {
            var forType = typeof(ManifestResource);
            var assembly = Assembly.GetAssembly(forType);
            return assembly.GetManifestResourceStream($"{forType.Namespace}.{name}");
        }

    }

}
