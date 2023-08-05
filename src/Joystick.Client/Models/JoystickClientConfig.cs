using System.Collections.Generic;

namespace Joystick.Client.Models
{
    public class JoystickClientConfig
    {
        /// <summary>
        /// API Key provided by the Joystick platform.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// UserId value for API request payload u field.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Version of the content to return.
        /// </summary>
        public string SemVer { get; set; }

        /// <summary>
        /// Params object to send with the requests.
        /// </summary>
        public IDictionary<string, object> Params { get; set; }

        /// <summary>
        /// Options for caching.
        /// </summary>
        public JoystickCacheOptions CacheOptions { get; set; }
    }
}
