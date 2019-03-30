using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Randomizer.Contracts;
using WebRandomizer.Models;
using WebRandomizer.Hubs;
using Microsoft.EntityFrameworkCore;

namespace WebRandomizer.Controllers {

    [Route("api/[controller]")]
    public class MultiworldController : Controller {

        private readonly RandomizerContext context;
        private readonly IHubContext<MultiworldHub> hubContext;

        public MultiworldController(RandomizerContext context, IHubContext<MultiworldHub> hubContext) {
            this.context = context;
            this.hubContext = hubContext;
        }

        [HttpGet("[action]/{guid}")]
        [ProducesResponseType(typeof(Session), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Session(string guid) {
            var session = context.Sessions.Include(x => x.Clients).Include(x => x.Seed).ThenInclude(x => x.Worlds).SingleOrDefault(x => x.Guid == guid);
            if (session != null) {
                return new OkObjectResult(session);
            }
            else {
                return new NotFoundResult();
            }
        }

    }

}
