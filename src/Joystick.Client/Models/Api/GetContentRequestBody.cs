using System.Collections.Generic;
using Newtonsoft.Json;

namespace Joystick.Client.Models.Api
{
    public class GetContentRequestBody
    {
        [JsonProperty("u")]
        public string UserId { get; set; }

        [JsonProperty("v")]
        public string Version { get; set; }

        [JsonProperty("p")]
        public IDictionary<string, object> Params { get; set; }
    }
}
