using CommandLine;

namespace Randomizer.CLI {

    class Program {

        static void Main(string[] args) {
            var opts = Parser.Default.ParseArguments<SMSeedOptions, SMZ3SeedOptions>(args)
                .WithParsed<GenSeedOptions>(GenSeed.Run);
        }

    }

}
