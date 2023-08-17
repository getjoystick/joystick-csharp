using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joystick.Client.Core;

namespace Joystick.Client.Models
{
    public class JoystickCacheOptions
    {
        private uint? cacheExpirationSeconds;

        /// <summary>
        /// Gets or sets Number of seconds while the cache is valid.
        /// </summary>
        public uint CacheExpirationSeconds
        {
            get => this.cacheExpirationSeconds ?? Constants.DefaultCacheExpirationSeconds;
            set => this.cacheExpirationSeconds = value;
        }
    }
}
