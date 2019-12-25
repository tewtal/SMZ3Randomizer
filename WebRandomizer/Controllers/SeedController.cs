using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Randomizer.Contracts;
using WebRandomizer.Models;

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
                if(seedData != null) {
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
