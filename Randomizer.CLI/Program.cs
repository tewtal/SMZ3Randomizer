using System;
using System.Collections.Generic;

namespace Randomizer.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var rando = new Randomizer.SuperMetroid.Randomizer();
                rando.GenerateSeed(new Dictionary<string, string>(), "");
            }
        }
    }
}
