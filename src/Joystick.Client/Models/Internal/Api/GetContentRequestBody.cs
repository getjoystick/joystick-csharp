using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("Joystick.UnitTests")]

namespace Joystick.Client.Models.Internal.Api
{
    internal class GetContentRequestBody
    {
        [JsonProperty("u")]
        public string UserId { get; set; }

        [JsonProperty("v")]
        public string Version { get; set; }

        [JsonProperty("p")]
        public IDictionary<string, object> Params { get; set; }
    }
}
