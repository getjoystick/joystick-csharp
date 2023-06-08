using Newtonsoft.Json;

namespace Joystick.Client.Services
{
    public interface IJsonOutputSerializer
    {
        TOutput Deserialize<TOutput>(string json);
    }
}
