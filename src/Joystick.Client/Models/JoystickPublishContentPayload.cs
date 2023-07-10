using System.Collections.Generic;

namespace Joystick.Client.Models
{
    public class JoystickPublishContentPayload
    {
        public string Description { get; set; }

        public object Content { get; set; }

        public IEnumerable<object> DynamicContentMap { get; set; }
    }
}
