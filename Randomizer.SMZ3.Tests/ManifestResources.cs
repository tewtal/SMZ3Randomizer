using System.IO;
using System.Reflection;

namespace Randomizer.SMZ3.Tests {

    public class ManifestResources {

        public static Stream GetEmbeddedStreamFor(string name) {
            var forType = typeof(ManifestResources);
            var assembly = Assembly.GetAssembly(forType);
            return assembly.GetManifestResourceStream($"{forType.Namespace}.{name}");
        }

    }

}
