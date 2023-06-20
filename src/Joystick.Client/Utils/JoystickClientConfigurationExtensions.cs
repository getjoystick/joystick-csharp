﻿using System.Collections.Generic;
using Joystick.Client.Models;
using Joystick.Client.Models.Api;
using Newtonsoft.Json;


namespace Joystick.Client.Utils
{
    internal static class JoystickClientConfigurationExtensions
    {
        internal static JoystickClientConfig Clone(this JoystickClientConfig config)
        {
            var jsonString = JsonConvert.SerializeObject(config);
            return JsonConvert.DeserializeObject<JoystickClientConfig>(jsonString);
        }

        internal static GetContentRequestBody MapToGetContentRequestBody(this JoystickClientConfig config)
        {
            var requestBody = new GetContentRequestBody
            {
                UserId = config.UserId ?? string.Empty,
                Version = config.SemVer ?? null,
                Params = config.Params ?? new Dictionary<string, object>(),
            };

            return requestBody;
        }
    }
}
