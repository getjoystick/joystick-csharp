using System;

namespace Joystick.Client.Models.Api
{
    internal class GetContentSettings
    {
        public GetContentSettings(JoystickClientConfig clientConfig, JoystickContentOptions contentOptions, bool isContentSerialized)
        {
            this.IsContentSerialized = isContentSerialized;
            this.ContentOptions = contentOptions;
            this.ClientConfig = clientConfig;
        }

        public GetContentSettings(JoystickClientConfig clientConfig, JoystickContentOptions contentOptions, Type dataType)
        {
            this.IsContentSerialized = dataType == typeof(string);
            this.ContentOptions = contentOptions;
            this.ClientConfig = clientConfig;
        }

        public bool IsContentSerialized { get; set; }

        public JoystickContentOptions ContentOptions { get; set; }

        public JoystickClientConfig ClientConfig { get; set; }
    }
}
