namespace Joystick.Client.Models
{
    public class JoystickClientConfiguration
    {
        public string ApiKey { get; set; }

        public string UserId { get; set; }

        public uint CacheExpirationSeconds { get; set; }

        public bool Serialized { get; set; }
    }
}
