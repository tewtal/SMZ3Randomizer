using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Randomizer.Contracts;
using WebRandomizer.Models;
using Newtonsoft.Json;

namespace WebRandomizer.Controllers {
    [Route("api/[controller]")]
    public class RandomizerController : Controller {
        private readonly RandomizerContext context;

        public RandomizerController(RandomizerContext context) {
            this.context = context;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ISeedData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Generate([FromBody] Option option) {
            if (option == null || option.options.Count < 1) {
                return new StatusCodeResult(400);
            }

            try {
                /* Initialize the randomizer and generate a seed with the given options */
                IRandomizer randomizer = new Randomizer.SuperMetroid.Randomizer();
                var seedData = randomizer.GenerateSeed(option.options, "");

                /* Store this seed to the database */
                var seed = new Seed {
                    GameName = seedData.Game,
                    Guid = seedData.Guid,
                    Players = seedData.Worlds.Count,
                    SeedNumber = seedData.Seed,
                    Spoiler = JsonConvert.SerializeObject(seedData.Playthrough),
                    Type = "multiworld",
                    Worlds = new List<World>()
                };

                foreach (var seedWorld in seedData.Worlds) {
                    var world = new World {
                        Guid = seedWorld.Guid,
                        Logic = option.options["logic"],
                        Player = seedWorld.Player,
                        Patch = new byte[] { 0x13, 0x37 }
                    };
                    seed.Worlds.Add(world);
                }

                context.Add(seed);
                await context.SaveChangesAsync();
                return new OkObjectResult(seedData);
            }
            catch {
                return new StatusCodeResult(500);
            }
        }
    }

    public class Option {
        public Dictionary<string, string> options;
    }
}
