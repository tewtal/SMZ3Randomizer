using CommandLine;
using Randomizer.CLI.Verbs;

namespace Randomizer.CLI {

    class Program {

        static void Main(string[] args) {
            Parser.Default.ParseArguments(args,
                typeof(SMSeedOptions),
                typeof(SMZ3SeedOptions),
                typeof(ZsprToRdcOptions),
                typeof(SpriteInventoryOptions),
                typeof(SpriteMontageOptions),
                typeof(SpriteZ3AvatarOptions),
                typeof(CheckCaseLogicOptions))
                .WithParsed<GenSeedOptions>(GenSeed.Run)
                .WithParsed<ZsprToRdcOptions>(ZsprToRdc.Run)
                .WithParsed<SpriteInventoryOptions>(SpriteTasks.Run)
                .WithParsed<SpriteMontageOptions>(SpriteTasks.Run)
                .WithParsed<SpriteZ3AvatarOptions>(SpriteTasks.Run)
                .WithParsed<CheckCaseLogicOptions>(CheckCaseLogic.Run);
        }

    }

}
