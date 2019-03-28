using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebRandomizer.Models;

namespace WebRandomizer.Hubs {
    public class MultiworldHub : Hub {
        private readonly RandomizerContext context;

        public MultiworldHub(RandomizerContext context) {
            this.context = context;
        }
    }
}
