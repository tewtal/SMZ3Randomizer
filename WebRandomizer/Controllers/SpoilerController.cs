using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Randomizer.Shared.Contracts;
using Randomizer.Shared.Models;
using System.Linq;
using YamlDotNet.Serialization;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

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
        public async Task<IActionResult> GetSpoiler(string seedGuid, string key = null, bool yaml = false) {
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
                            if (settings["race"] == "true" && (!settings.ContainsKey("spoilerKey") || settings["spoilerKey"] != key)) 
                            { 
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
                    if (yaml == false) {
                        return new OkObjectResult(new SpoilerData { Seed = seedData, Locations = spoilerLocationData });
                    } else {
                        return new OkObjectResult(ConvertSeedToYaml(seedData, spoilerLocationData));
                    }

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

        public string ConvertSeedToYaml(Seed seed, List<SpoilerLocationData> locations) {
            var world = seed.Worlds.First(); // Assuming you want the first world in the list
            var worldSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(world.Settings);
            var worldState = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(world.WorldState);

            var yamlOutput = new {
                Game = $"{seed.GameId.ToUpper()} {seed.GameVersion} {seed.Hash}",
                Meta = new {
                    guid = seed.Guid,
                    smlogic = worldSettings["smlogic"],
                    goal = worldSettings["goal"],
                    keyshuffle = worldSettings["keyshuffle"],
                    towerCrystals = worldSettings.TryGetValue("towerCrystals", out string towerCrystals) ? towerCrystals : "7",
                    ganonCrystals = worldSettings.TryGetValue("ganonCrystals", out string ganonCrystals) ? ganonCrystals : "7",
                    tourianBossTokens = worldSettings.TryGetValue("tourianBossTokens", out string bossTokens) ? bossTokens : "4",
                    dropPrizes = new {
                        treePulls = ((JObject)worldState["dropPrizes"])["treePulls"].ToArray().Select(t => t.ToString()),
                        crabContinous = ((JObject)worldState["dropPrizes"])["crabContinous"].ToString(),
                        crabFinal = ((JObject)worldState["dropPrizes"])["crabFinal"].ToString(),
                        stun = ((JObject)worldState["dropPrizes"])["stun"].ToString(),
                    }
                },
                PrizesAndRequirements = GeneratePrizesAndRequirements(worldState),
                Regions = GroupSpoilerLocationsByRegion(locations),
            };

            var serializer = new SerializerBuilder().Build();
            string yaml = serializer.Serialize(yamlOutput);

            return yaml;
        }

        private object GeneratePrizesAndRequirements(Dictionary<string, object> worldState) {
            var rewards = worldState["rewards"] as JArray;
            var medallions = worldState["medallions"] as JArray;
            var towerCrystals = worldState["towerCrystals"];
            var ganonCrystals = worldState["ganonCrystals"];
            var tourianBossTokens = worldState["tourianBossTokens"];

            var dungeonNames = new List<string>
            {
                "Eastern Palace",
                "Desert Palace",
                "Tower of Hera",
                "Dark Palace",
                "Swamp Palace",
                "Skull Woods",
                "Thieves' Town",
                "Ice Palace",
                "Misery Mire",
                "Turtle Rock",
                "Brinstar",
                "Wrecked Ship",
                "Maridia",
                "Norfair Lower"
            };

            var medallionDungeonNames = new List<string>
            {
                "Misery Mire",
                "Turtle Rock"
            };

            var prizesAndRequirements = new Dictionary<string, object>();

            for (int i = 0; i < rewards.Count; i++) {
                prizesAndRequirements.Add($"Prize - {dungeonNames[i]}", ConvertPrizeName(rewards[i].ToString()));
            }

            for (int i = 0; i < medallions.Count; i++) {
                prizesAndRequirements.Add($"Medallion Required - {medallionDungeonNames[i]}", medallions[i].ToString());
            }

            prizesAndRequirements.Add("Tower Crystals", towerCrystals);
            prizesAndRequirements.Add("Ganon Crystals", ganonCrystals);
            prizesAndRequirements.Add("Tourian Boss Tokens", tourianBossTokens);

            return prizesAndRequirements;
        }

        private Dictionary<string, Dictionary<string, string>> GroupSpoilerLocationsByRegion(List<SpoilerLocationData> spoilerLocations) {
            var grouped = new Dictionary<string, Dictionary<string, string>>();

            foreach (var region in ordering) {
                grouped[region] = new Dictionary<string, string>();
            }

            foreach (var location in spoilerLocations) {
                if (grouped.ContainsKey(location.LocationRegion)) {
                    grouped[location.LocationRegion][location.LocationName] = location.ItemName;
                } else {
                    grouped["Other Regions"][$"{location.LocationRegion} - {location.LocationName}"] = location.ItemName;
                }
            }

            if (grouped["Other Regions"].Count == 0) {
                grouped.Remove("Other Regions");
            }

            return grouped;
        }

        string ConvertPrizeName(string name) =>
            name switch {
                "BossTokenKraid" => "Kraid Boss Token",
                "BossTokenRidley" => "Ridley Boss Token",
                "BossTokenPhantoon" => "Phantoon Boss Token",
                "BossTokenDraygon" => "Draygon Boss Token",
                "PendantGreen" => "Green Pendant",
                "PendantNonGreen" => "Blue/Red Pendant",
                "CrystalRed" => "Red Crystal",
                "CrystalBlue" => "Blue Crystal",
                _ => name
            };


        private List<string> ordering = new List<string>
        {
            "Light World Death Mountain West",
            "Light World Death Mountain East",
            "Light World North West",
            "Light World North East",
            "Light World South",
            "Hyrule Castle",
            "Dark World Death Mountain West",
            "Dark World Death Mountain East",
            "Dark World North West",
            "Dark World North East",
            "Dark World South",
            "Dark World Mire",
            "Castle Tower",
            "Eastern Palace",
            "Desert Palace",
            "Tower of Hera",
            "Palace of Darkness",
            "Swamp Palace",
            "Skull Woods",
            "Thieves' Town",
            "Ice Palace",
            "Misery Mire",
            "Turtle Rock",
            "Ganon's Tower",
            "Crateria West",
            "Crateria Central",
            "Crateria East",
            "Brinstar Blue",
            "Brinstar Green",
            "Brinstar Pink",
            "Brinstar Red",
            "Brinstar Kraid",
            "Wrecked Ship",
            "Maridia Outer",
            "Maridia Inner",
            "Norfair Upper West",
            "Norfair Upper East",
            "Norfair Upper Crocomire",
            "Norfair Lower West",
            "Norfair Lower East",
            "Other Regions"
        };

    }

}
