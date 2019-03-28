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

namespace WebRandomizer.Controllers {
    [Route("api/[controller]")]
    public class MultiworldController : Controller {
        private readonly RandomizerContext context;
        private readonly IHubContext<MultiworldHub> hubContext;
        public MultiworldController(RandomizerContext context, IHubContext<MultiworldHub> hubContext) {
            this.context = context;
            this.hubContext = hubContext;
        }

    }
}
