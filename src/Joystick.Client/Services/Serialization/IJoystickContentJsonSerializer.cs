namespace Joystick.Client.Services.Serialization
{
    public interface IJoystickContentJsonSerializer
    {
        TOutput Deserialize<TOutput>(string json);

        string Serialize<TEntity>(TEntity entity);
    }
}
