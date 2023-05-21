using System.Text.Json;
using Joystick.Client.Models;

namespace Joystick.Client.Utils
{
    public static class JoystickClientConfigurationExtensions
    {
        public static JoystickClientConfig Clone(this JoystickClientConfig config)
        {
            var jsonString = JsonSerializer.Serialize(config);
            return JsonSerializer.Deserialize<JoystickClientConfig>(jsonString);
        }
    }
}
