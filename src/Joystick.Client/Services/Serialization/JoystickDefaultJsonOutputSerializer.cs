using Newtonsoft.Json;

namespace Joystick.Client.Services.Serialization
{
    internal class JoystickDefaultJsonOutputSerializer : IJsonOutputSerializer
    {
        public TOutput Deserialize<TOutput>(string json)
        {
            return JsonConvert.DeserializeObject<TOutput>(json);
        }
    }
}
