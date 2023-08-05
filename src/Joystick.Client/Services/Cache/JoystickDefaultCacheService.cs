using System;
using System.Runtime.Caching;
using Joystick.Client.Models;

namespace Joystick.Client.Services.Cache
{
    internal class JoystickDefaultCacheService : IJoystickCacheService
    {
        private JoystickCacheOptions options;
        private MemoryCache cache;

        public JoystickDefaultCacheService(JoystickCacheOptions options)
        {
            this.options = options ?? new JoystickCacheOptions();
            this.cache = MemoryCache.Default;
        }

        public void Set(string key, string value)
        {
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds((double)this.options.CacheExpirationSeconds),
            };

            this.cache.Add(key, value, policy);
        }

        public bool TryGet(string key, out string value)
        {
            var cachedValue = this.cache.Get(key);
            if (cachedValue != null)
            {
                value = (string)cachedValue;
                return true;
            }

            value = default(string);
            return false;
        }
    }
}
