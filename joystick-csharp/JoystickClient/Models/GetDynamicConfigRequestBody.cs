using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JoystickClient.Models;

internal sealed record GetDynamicConfigRequestBody
{
    [JsonPropertyName("u")]
    public string UserId { get; init; }
    [JsonPropertyName("p")]
    public IDictionary<string, object> Parameters { get; init; }
}
