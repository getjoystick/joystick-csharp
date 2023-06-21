namespace Joystick.Client.Models
{
    public class JoystickContentOptions
    {
        /// <summary>
        /// The cache should be avoided, and the data must be requested from the API.
        /// </summary>
        public bool Refresh { get; set; }
    }
}
