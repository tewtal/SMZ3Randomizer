using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebRandomizer.Controllers {
    public class Helpers {
        public static string SerializeEnumAsString(object ob) {
            return JsonConvert.SerializeObject(
                ob,
                new JsonSerializerSettings {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented,
                    Converters = new List<JsonConverter> { new StringEnumConverter() }
                }
            );
        }
    }
}
