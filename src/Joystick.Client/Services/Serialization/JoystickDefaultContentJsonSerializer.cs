using Newtonsoft.Json;

namespace Joystick.Client.Services.Serialization
{
    internal class JoystickDefaultContentJsonSerializer : IJoystickContentJsonSerializer
    {
        public TOutput Deserialize<TOutput>(string json)
        {
            return JsonConvert.DeserializeObject<TOutput>(json);
        }

        public string Serialize<TEntity>(TEntity entity)
        {
            return JsonConvert.SerializeObject(entity);
        }
    }
}
