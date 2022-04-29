using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Randomizer.Shared.Contracts;
using Randomizer.Shared.Models;

namespace WebRandomizer.Controllers {

    [Route("api/[controller]")]
    public class SpoilerController : Controller {

        public class SpoilerLocationData {
            public int LocationId { get; set; }
            public string LocationName { get; set; }
            public string LocationType { get; set; }
            public string LocationArea { get; set; }
            public string LocationRegion { get; set; }
            public int ItemId { get; set; }
            public string ItemName { get; set; }
            public int WorldId { get; set; }
            public int ItemWorldId { get; set; }
        }

        public class SpoilerData {
            public Seed Seed { get; set; }
            public List<SpoilerLocationData> Locations { get; set; }
        }

        private readonly RandomizerContext context;
        public SpoilerController(RandomizerContext context) {
            this.context = context;
        }

        [HttpGet("{seedGuid}")]
        [ProducesResponseType(typeof(SpoilerData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSpoiler(string seedGuid) {
            try {
                var seedData = await context.Seeds.Include(x => x.Worlds).ThenInclude(x => x.Locations).SingleOrDefaultAsync(x => x.Guid == seedGuid);
                if (seedData != null) {
                    
                    IRandomizer randomizer = seedData.GameId switch
                    {
                        "smz3" => new Randomizer.SMZ3.Randomizer(),
                        _ => new Randomizer.SuperMetroid.Randomizer()
                    };

                    var itemData = randomizer.GetItems();
                    var locationData = randomizer.GetLocations();

                    /* Return 400 Bad Request if someone is trying to get the spoiler for a race rom */
                    foreach (var world in seedData.Worlds) {
                        try {
                            var settings = JsonSerializer.Deserialize<Dictionary<string, string>>(world.Settings);
                            if(settings["race"] == "true" && (!settings.ContainsKey("spoilerrace") || settings["spoilerrace"] != "true")) {
                                return new StatusCodeResult(400);
                            }
                        } catch {
                            world.Locations = null;
                        }
                    }

                    /* Generate spoiler location data */
                    var spoilerLocationData = new List<SpoilerLocationData>();
                    foreach(var world in seedData.Worlds) {
                        foreach(var location in world.Locations) {
                            var id = LegacyLocationId(location.LocationId);
                            spoilerLocationData.Add(new SpoilerLocationData {
                                LocationId = id,
                                LocationName = locationData[id].Name,
                                LocationType = locationData[id].Type,
                                LocationRegion = locationData[id].Region,
                                LocationArea = locationData[id].Area,

                                ItemId = location.ItemId,
                                ItemName = itemData[location.ItemId].Name,

                                WorldId = world.WorldId,
                                ItemWorldId = location.ItemWorldId,
                            });
                        }
                        world.Locations = null;
                        world.Patch = null;
                    }

                    return new OkObjectResult(new SpoilerData { Seed = seedData, Locations = spoilerLocationData });

                } else {
                    return new StatusCodeResult(404);
                }
            
            } catch {
                return new StatusCodeResult(500);
            }
        }

        static int LegacyLocationId(int id) => id switch {
            /* Z3 ids [196, 202] were moved to [230, 236] */
            _ when id is >= (256 + 196) and <= (256 + 202) => id + (230 - 196),
            _ => id,
        };

    }

}
