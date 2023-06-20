using System;

namespace Joystick.Client.Models.Api
{
    internal class GetContentSettings
    {
        public GetContentSettings(JoystickClientConfig clientConfig, JoystickContentOptions contentOptions, bool isContentSerialized)
        {
            IsContentSerialized = isContentSerialized;
            ContentOptions = contentOptions;
            ClientConfig = clientConfig;
        }

        public GetContentSettings(JoystickClientConfig clientConfig, JoystickContentOptions contentOptions, Type dataType)
        {
            IsContentSerialized = dataType == typeof(string);
            ContentOptions = contentOptions;
            ClientConfig = clientConfig;
        }

        public bool IsContentSerialized { get; set; }

        public JoystickContentOptions ContentOptions { get; set; }

        public JoystickClientConfig ClientConfig { get; set; }
    }
}
