using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Joystick.Client.Models.Api
{
    internal class UpsertContentRequestBody
    {
        [JsonProperty("d")]
        public string Description { get; set; }

        [JsonProperty("c")]
        public JToken Content { get; set; }

        [JsonProperty("m")]
        public JToken DynamicContentMap { get; set; }
    }
}
