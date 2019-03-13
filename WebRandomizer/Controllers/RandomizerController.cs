using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Randomizer.Contracts;

namespace WebRandomizer.Controllers
{
    [Route("api/[controller]")]
    public class RandomizerController : Controller
    {
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(IPatchData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Generate([FromBody] Option option)
        {
            if(option == null || option.options.Count < 1)
            {
                return new StatusCodeResult(400);
            }

            try
            {
                IRandomizer randomizer = new Randomizer.SuperMetroid.Randomizer();
                var patchData = randomizer.GenerateSeed(option.options, "");
                return new OkObjectResult(patchData);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
    }

    public class Option
    {
        public Dictionary<string, string> options;
    }
}
