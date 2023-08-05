namespace Joystick.Client.Services.Cache
{
    public interface IJoystickCacheService
    {
        void Set(string key, string value);

        bool TryGet(string key, out string value);
    }
}
