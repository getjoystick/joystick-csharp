namespace Joystick.Client.Services.Serialization
{
    public interface IContentJsonSerializer
    {
        TOutput Deserialize<TOutput>(string json);

        string Serialize<TEntity>(TEntity entity);
    }
}
