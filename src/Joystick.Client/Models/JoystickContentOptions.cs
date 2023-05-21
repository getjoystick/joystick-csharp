namespace Joystick.Client.Models
{
    public class JoystickContentOptions
    {
        /// <summary>
        /// When Refresh is set to true – existing cache should be ignored.
        /// </summary>
        public bool Refresh { get; set; }

        /// <summary>
        /// When Serialized set to true - output should be as serialized json.
        /// </summary>
        public bool Serialized { get; set; }

        /// <summary>
        /// When FullResponse set to true - output should contain metadata and hash in addition to data.
        /// </summary>
        public bool FullResponse { get; set; }
    }
}
