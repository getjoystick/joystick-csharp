using System;
using System.Runtime.Caching;
using Joystick.Client.Models;
using Joystick.Client.Services.Cache;
using Xunit;

namespace Joystick.UnitTests.Services
{
    public class JoystickDefaultCacheServiceTests
    {
        [Fact]
        public void Set_Should_StoreValueToMemoryCache()
        {
            var key = Guid.NewGuid().ToString();
            var value = "valueForCaching";
            var memoryCache = MemoryCache.Default;
            var cacheService = new JoystickDefaultCacheService(new JoystickCacheOptions());

            cacheService.Set(key, value);

            var cachedValue = memoryCache.Get(key);
            Assert.Equal(value, cachedValue);
        }

        [Fact]
        public void TryGet_Should_ReadValueFromMemoryCache()
        {
            var key = Guid.NewGuid().ToString();
            var value = "valueToReadFromCache";
            var memoryCache = MemoryCache.Default;
            memoryCache.Add(key, value, DateTimeOffset.UtcNow.AddSeconds(3));

            var cacheService = new JoystickDefaultCacheService(new JoystickCacheOptions());

            cacheService.TryGet(key, out var actualValue);

            Assert.Equal(value, actualValue);
        }

        [Fact]
        public void TryGet_Should_ReturnTrueWhenValueExists()
        {
            var key = Guid.NewGuid().ToString();
            var memoryCache = MemoryCache.Default;
            memoryCache.Add(key, "valueToReadFromCache", DateTimeOffset.UtcNow.AddSeconds(3));

            var cacheService = new JoystickDefaultCacheService(new JoystickCacheOptions());

            var result = cacheService.TryGet(key, out var actualValue);

            Assert.True(result);
        }

        [Fact]
        public void TryGet_Should_ReturnFalseWhenValueDoesNotExist()
        {
            var key = Guid.NewGuid().ToString();

            var cacheService = new JoystickDefaultCacheService(new JoystickCacheOptions());

            var result = cacheService.TryGet(key, out var actualValue);

            Assert.False(result);
        }

        [Fact]
        public void ClearAll_Should_RemoveEverythingFromCache()
        {
            var key = Guid.NewGuid().ToString();
            var memoryCache = MemoryCache.Default;
            memoryCache.Add(key, "valueToReadFromCache", DateTimeOffset.UtcNow.AddSeconds(3));

            var cacheService = new JoystickDefaultCacheService(new JoystickCacheOptions());

            cacheService.ClearAll();

            var resultTryGet = cacheService.TryGet(key, out var actualValue);
            Assert.False(resultTryGet);
        }
    }
}
