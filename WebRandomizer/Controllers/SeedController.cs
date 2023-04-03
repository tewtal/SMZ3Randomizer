using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Randomizer.Shared.Contracts;
using Randomizer.Shared.Models;

namespace WebRandomizer.Controllers {
    [Route("api/[controller]")]
    public class SeedController : Controller {

        private readonly RandomizerContext context;
        public SeedController(RandomizerContext context) {
            this.context = context;
        }

        [HttpGet("{seedGuid}")]
        [ProducesResponseType(typeof(ISeedData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSeed(string seedGuid) {
            try {
                var seedData = await context.Seeds.Include(x => x.Worlds).SingleOrDefaultAsync(x => x.Guid == seedGuid);

                /* Remove WorldState from the response for any world that's configured as a race world since it contains spoiler information */
                foreach (var world in seedData.Worlds) {
                    var settings = JsonSerializer.Deserialize<Dictionary<string, string>>(world.Settings);
                    if (settings.ContainsKey("race") && settings["race"] == "true") {
                        world.WorldState = null;
                        world.Locations = null;
                    }
                    
                    /* If a spoiler key has been set, don't send it to the Permalink endpoint, except for info that one has been used */
                    if(settings.ContainsKey("spoilerKey")) {
                        settings["spoilerKey"] = "true";
                        world.Settings = JsonSerializer.Serialize(settings);
                    }
                }

                if (seedData != null) {
                    return new OkObjectResult(seedData);
                } else {
                    return new StatusCodeResult(404);
                }
            
            } catch {
                return new StatusCodeResult(500);
            }
        }
    }
}
