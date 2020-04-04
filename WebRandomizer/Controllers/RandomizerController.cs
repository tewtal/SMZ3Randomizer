using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Randomizer.Contracts;
using WebRandomizer.Models;
using Newtonsoft.Json;
using static WebRandomizer.Controllers.Helpers;

namespace WebRandomizer.Controllers {

    [Route("api/randomizers/")]

    public class RandomizerController : Controller {

        private readonly RandomizerContext context;
        private readonly List<IRandomizer> randomizers;

        public RandomizerController(RandomizerContext context) {
            this.context = context;
            randomizers = new List<IRandomizer> {
                new Randomizer.SMZ3.Randomizer(),
                new Randomizer.SuperMetroid.Randomizer()
            };
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Index() {
            return new OkObjectResult(SerializeEnumAsString(randomizers));
        }

        [HttpGet("{randomizerId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult GetRandomizer(string randomizerId) {
            return new OkObjectResult(SerializeEnumAsString(randomizers.FirstOrDefault(x => x.Id == randomizerId)));
        }

        [HttpPost("{randomizerId}/[action]")]
        [ProducesResponseType(typeof(ISeedData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Generate(string randomizerId, [FromBody] Dictionary<string, string> options) {
            if (options.Count < 1) {
                return new StatusCodeResult(400);
            }

            try {
                /* Initialize the randomizer and generate a seed with the given options */
                IRandomizer randomizer = randomizers.FirstOrDefault(x => x.Id == randomizerId);
                
                if(randomizer == null) {
                    return new StatusCodeResult(400);
                }

                var seedData = randomizer.GenerateSeed(options, options["seed"]);

                /* Store this seed to the database */
                var seed = new Seed {
                    GameName = seedData.Game,
                    GameVersion = randomizer.Version.ToString(),
                    GameId = randomizer.Id,
                    Guid = seedData.Guid,
                    Players = seedData.Worlds.Count,
                    SeedNumber = seedData.Seed,
                    Spoiler = JsonConvert.SerializeObject(seedData.Playthrough),
                    Mode = seedData.Mode,
                    Worlds = new List<World>()
                };

                /* If race mode is enabled, blank out the seed number to not reveal it to the client */
                if (options.ContainsKey("race") && options["race"] == "true") {
                    seed.SeedNumber = "";
                    options["seed"] = "";
                }

                foreach (var seedWorld in seedData.Worlds) {
                    var world = new World {
                        WorldId = seedWorld.Id,
                        Guid = seedWorld.Guid,
                        Settings = JsonConvert.SerializeObject(options),
                        Player = seedWorld.Player,
                        Patch = ConvertPatch(seedWorld.Patches),
                        Locations = seedWorld.Locations.Select(l => new Location() { LocationId = l.LocationId, ItemId = l.ItemId, ItemWorldId = l.ItemWorldId}).ToList()
                    };
                    seed.Worlds.Add(world);
                }

                context.Add(seed);
                await context.SaveChangesAsync();

                /* If this is a multiworld seed, we also create a new multiworld session with the same session guid as the seed guid */
                if (seed.Players > 1) {
                    var session = new Session {
                        Clients = new List<Client>(),
                        Guid = seed.Guid,
                        Seed = seed,
                        State = SessionState.Created
                    };

                    context.Add(session);
                    await context.SaveChangesAsync();
                }

                return new OkObjectResult(seed);
            }
            catch {
                return new StatusCodeResult(500);
            }
        }
        private byte[] ConvertPatch(Dictionary<int, byte[]> patches) {
            var bytes = new List<byte>();
            foreach (var patch in patches) {
                bytes.AddRange(BitConverter.GetBytes(patch.Key));
                bytes.AddRange(BitConverter.GetBytes((ushort)patch.Value.Length));
                bytes.AddRange(patch.Value);
            }

            return bytes.ToArray();
        }
    }
}
