using System;
using System.Collections.Generic;
using System.IO;

namespace Joystick.Client.Models
{
    public class JoystickClientConfig
    {
        public string ApiKey { get; set; }

        public string UserId { get; set; }

        public string SemVer { get; set; }

        public IDictionary<string, object> Params { get; set; }

        public uint CacheExpirationSeconds { get; set; }

        public bool Serialized { get; set; }
    }
}
