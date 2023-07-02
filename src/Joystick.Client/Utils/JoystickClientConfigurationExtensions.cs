using System.Collections.Generic;
using System.Text.RegularExpressions;
using Joystick.Client.Core;
using Joystick.Client.Exceptions;
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

        internal static void Validate(this JoystickClientConfig config)
        {
            if (string.IsNullOrWhiteSpace(config.ApiKey))
            {
                throw new JoystickException($"{nameof(config.ApiKey)} is required and can't be empty.");
            }

            if (!(config.SemVer == null || Regex.IsMatch(config.SemVer, Constants.SemVerPattern)))
            {
                throw new JoystickException($"{nameof(config.SemVer)} must correspond semantic version pattern.");
            }

            if (config.CacheExpirationSeconds == 0)
            {
                throw new JoystickException($"{nameof(config.CacheExpirationSeconds)} must be null or greater than 0.");
            }
        }
    }
}
