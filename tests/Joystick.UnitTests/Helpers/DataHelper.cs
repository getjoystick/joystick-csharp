using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joystick.Client.Models;
using Joystick.Client.Models.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Joystick.UnitTests.Helpers
{
    internal static class DataHelper
    {
        internal static string GetSerializedContent()
        {
            var data = GetContent();

            return JsonConvert.SerializeObject(data);
        }

        internal static DesignConfigs GetContent()
        {
            return new DesignConfigs()
            {
                Greeting = "Hello!",
                Scales = new[] { 0.75f, 1f, 1.5f, 2.25f },
                Theme = DesignTheme.Dark,
            };
        }

        internal static JoystickClientConfig GetJoystickClientConfig()
        {
            return new JoystickClientConfig()
            {
                ApiKey = "TestApiKey",
            };
        }

        internal static JoystickPublishContentPayload GetJoystickPublishContentPayload()
        {
            return new JoystickPublishContentPayload()
            {
                Description = "This design config",
                Content = GetContent(),
            };
        }

        internal static UpsertContentRequestBody GetUpsertContentRequestBody()
        {
            return new UpsertContentRequestBody()
            {
                Description = "This design config",
                Content = JToken.FromObject(GetContent()),
            };
        }
    }
}
