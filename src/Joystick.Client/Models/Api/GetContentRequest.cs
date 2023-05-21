using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Joystick.Client.Models.Api
{
    internal class GetContentRequest
    {
        public GetContentRequest()
        {
        }

        public GetContentRequest(JoystickClientConfig clientConfig)
        {
            this.UserId = clientConfig.UserId ?? string.Empty;
            this.Version = clientConfig.SemVer ?? null;
            this.Params = clientConfig.Params ?? new Dictionary<string, object>();
        }

        [JsonPropertyName("u")]
        public string UserId { get; set; }

        [JsonPropertyName("v")]
        public string Version { get; set; }

        [JsonPropertyName("p")]
        public IDictionary<string, object> Params { get; set; }
    }
}
