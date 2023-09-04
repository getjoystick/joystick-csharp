using System.Collections.Generic;

namespace Joystick.Client.Models
{
    public class JoystickClientConfig
    {
        /// <summary>
        /// Gets or sets Api Key provided by the Joystick platform.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets UserId value for API request payload u field.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets version of the content to return.
        /// </summary>
        public string SemVer { get; set; }

        /// <summary>
        /// Gets or sets params object to send with the requests.
        /// </summary>
        public IDictionary<string, object> Params { get; set; }

        /// <summary>
        /// Gets or sets options for caching.
        /// </summary>
        public JoystickCacheOptions CacheOptions { get; set; }
    }
}
