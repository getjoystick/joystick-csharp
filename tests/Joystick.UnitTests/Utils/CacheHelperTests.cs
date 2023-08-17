using System.Collections.Generic;
using Joystick.Client.Models;
using Joystick.Client.Utils;
using Xunit;

namespace Joystick.UnitTests.Utils
{
    public class CacheHelperTests
    {
        [Fact]
        public void GenerateCacheKey_Should_BeCaseInsensitiveForContentIds()
        {
            var config = new JoystickClientConfig()
            {
                ApiKey = "TestApiKey",
                SemVer = "1.0.34",
                Params = new Dictionary<string, object>()
                {
                    { "Country", "UA" },
                    { "UserPrc", 85.08 },
                },
            };

            var contentIdWitUpper = new[] { "Auth", "appDesign" };
            var contentIdWitLower = new[] { "auth", "appdesign" };

            var keyFromUpper = CacheHelper.GenerateCacheKey(config, true, contentIdWitUpper);
            var keyFromLower = CacheHelper.GenerateCacheKey(config, true, contentIdWitLower);

            Assert.Equal(keyFromLower, keyFromUpper);
        }

        [Fact]
        public void GenerateCacheKey_Should_BeIndependentOnParamsOrder()
        {
            var configV1 = new JoystickClientConfig()
            {
                ApiKey = "TestApiKey",
                SemVer = "1.0.34",
                Params = new Dictionary<string, object>()
                {
                    { "UserPrc", 85.08 },
                    { "AuthClientSecret", "189d58e8-c6bd-4240-adde-ab293d4bdf97" },
                    { "Country", "PL" },
                },
            };

            var configV2 = new JoystickClientConfig()
            {
                ApiKey = "TestApiKey",
                SemVer = "1.0.34",
                Params = new Dictionary<string, object>()
                {
                    { "Country", "PL" },
                    { "AuthClientSecret", "189d58e8-c6bd-4240-adde-ab293d4bdf97" },
                    { "UserPrc", 85.08 },
                },
            };

            var contentIds = new[] { "Auth", "Design" };

            var keyV1 = CacheHelper.GenerateCacheKey(configV1, true, contentIds);
            var keyV2 = CacheHelper.GenerateCacheKey(configV2, true, contentIds);

            Assert.Equal(keyV1, keyV2);
        }

        [Fact]
        public void GenerateCacheKey_Should_BeIndependentOnContentIdsOrder()
        {
            var config = new JoystickClientConfig()
            {
                ApiKey = "TestApiKey",
                SemVer = "1.0.34",
            };

            var contentIdsV1 = new[] { "Design", "Targeting", "Auth" };
            var contentIdsV2 = new[] { "Targeting", "Auth", "Design" };

            var keyV1 = CacheHelper.GenerateCacheKey(config, false, contentIdsV1);
            var keyV2 = CacheHelper.GenerateCacheKey(config, false, contentIdsV2);

            Assert.Equal(keyV1, keyV2);
        }

        [Fact]
        public void GenerateCacheKey_Should_DependOnContentIdsValue()
        {
            var config = new JoystickClientConfig()
            {
                ApiKey = "TestApiKey",
                SemVer = "1.0.34",
            };

            var contentIdsV1 = new[] { "Design", "Targeting" };
            var contentIdsV2 = new[] { "Targeting", "Auth" };

            var keyV1 = CacheHelper.GenerateCacheKey(config, false, contentIdsV1);
            var keyV2 = CacheHelper.GenerateCacheKey(config, false, contentIdsV2);

            Assert.NotEqual(keyV1, keyV2);
        }

        [Fact]
        public void GenerateCacheKey_Should_DependOnIsContentSerializedOption()
        {
            var config = new JoystickClientConfig()
            {
                ApiKey = "TestApiKey",
                SemVer = "1.0.34",
            };

            var contentIds = new[] { "Design", "Targeting", "Auth" };

            var keyV1 = CacheHelper.GenerateCacheKey(config, false, contentIds);
            var keyV2 = CacheHelper.GenerateCacheKey(config, true, contentIds);

            Assert.NotEqual(keyV1, keyV2);
        }

        [Fact]
        public void GenerateCacheKey_Should_DependOnParamsValue()
        {
            var configV1 = new JoystickClientConfig()
            {
                ApiKey = "TestApiKey",
                SemVer = "1.0.34",
                Params = new Dictionary<string, object>()
                {
                    { "UserPrc", new[] { 85.08, 67.00, 20.40 } },
                },
            };

            var configV2 = new JoystickClientConfig()
            {
                ApiKey = "TestApiKey",
                SemVer = "1.0.34",
                Params = new Dictionary<string, object>()
                {
                    { "UserPrc", new[] { 50.00, 17.00, 20.00 } },
                },
            };

            var contentIds = new[] { "Auth", "Design" };

            var keyV1 = CacheHelper.GenerateCacheKey(configV1, true, contentIds);
            var keyV2 = CacheHelper.GenerateCacheKey(configV2, true, contentIds);

            Assert.NotEqual(keyV1, keyV2);
        }

        [Fact]
        public void BuildStringCacheKey_Should_ReturnCorrectString()
        {
            var expectedCacheKey =
                "[\"TestApiKey\",[{\"Key\":\"UserPrc\",\"Value\":[85.08,67.0,20.4]}],\"1.0.34\",\"TestUserId\",[\"auth\",\"design\"],true]";
            var config = new JoystickClientConfig()
            {
                ApiKey = "TestApiKey",
                SemVer = "1.0.34",
                UserId = "TestUserId",
                Params = new Dictionary<string, object>()
                {
                    { "UserPrc", new[] { 85.08, 67.00, 20.40 } },
                },
            };

            var isContentSerialized = true;
            var contentIds = new[] { "Auth", "Design" };

            var actualCacheKay = CacheHelper.BuildStringCacheKey(config, isContentSerialized, contentIds);

            Assert.Equal(expectedCacheKey, actualCacheKay);
        }
    }
}
