namespace Joystick.Client.Services.Serialization
{
    public interface IJsonOutputSerializer
    {
        TOutput Deserialize<TOutput>(string json);
    }
}
