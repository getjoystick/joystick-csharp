using System.Collections.Generic;

namespace Joystick.Client.Models
{
    public class JoystickPublishContentPayload
    {
        /// <summary>
        /// Gets or sets the description of the content being updated.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the content object.
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// Gets or sets array of objects to use as dynamic content.
        /// </summary>
        public IEnumerable<object> DynamicContentMap { get; set; }
    }
}
