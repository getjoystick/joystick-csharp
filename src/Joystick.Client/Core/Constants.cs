namespace Joystick.Client.Core
{
    internal static class Constants
    {
        internal const string ApiKeyHeaderName = "X-Api-Key";
        internal const string BaseReadUrl = "https://api.getjoystick.com/api";
        internal const string BaseWriteUrl = "https://capi.getjoystick.com/api";
        internal const string SemVerPattern = "^(0|[1-9]\\d*)\\.(0|[1-9]\\d*)\\.(0|[1-9]\\d*)$";
    }
}
