using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace JoystickClient.Models.ApiResponse
{
    public class GetVeryDynamicConfigResponse
    {       
        public object Data { get; set; }
        public string Hash { get; set; }
        public ConfigDynamicMeta Meta { get; set; }
    }

    public class ConfigDynamicMeta
    {        
        [JsonProperty("uid")]
        public long ConsumerUintId { get; set; }
        [JsonProperty("mod")]
        public long ConsumerIdMod { get; set; }
        public List<List<string>> Variants { get; set; } = new List<List<string>>();
        public List<IDictionary<string, object>> Seg { get; set; } = new List<IDictionary<string, object>>();
    }
}
