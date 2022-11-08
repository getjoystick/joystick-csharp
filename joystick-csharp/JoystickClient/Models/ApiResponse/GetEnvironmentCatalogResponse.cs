using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace JoystickClient.Models.ApiResponse
{
    public class GetEnvironmentCatalogResponse
    {
        public IDictionary<string, EnvironmentCatalogResponseItem> Catalog { get; set; }
    }

    public class EnvironmentCatalogResponseItem
    {
        [JsonProperty("h")]
        public string Hash { get; set; }
        public bool d { get; set; }
    }
}
