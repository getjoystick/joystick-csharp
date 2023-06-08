using System.Collections.Generic;
using Joystick.Client.Models;
using Joystick.Client.Utils;
using Xunit;

namespace Joystick.UnitTests.Utils
{
    public class JoystickClientConfigurationExtensionsTest
    {
        [Fact]
        public void Clone_Returns_IndependentInstance()
        {
            const string apiKey = "OriginalApiKey";
            var originalConfig = new JoystickClientConfig()
            {
                ApiKey = apiKey,
            };

            var clonedConfig = originalConfig.Clone();
            originalConfig.ApiKey = "newApiKey";

            Assert.Equal(apiKey, clonedConfig.ApiKey);
        }

        [Fact]
        public void MapToGetContentRequestBody_ShouldMap_UserId()
        {
            const string userId = "TestUserId";
            var originalConfig = new JoystickClientConfig()
            {
                UserId = userId,
            };

            var requestBody = originalConfig.MapToGetContentRequestBody();

            Assert.Equal(userId, requestBody.UserId);
        }

        [Fact]
        public void MapToGetContentRequestBody_ShouldMap_UserId_ToEmptyString()
        {
            var originalConfig = new JoystickClientConfig();

            var requestBody = originalConfig.MapToGetContentRequestBody();

            Assert.Equal(string.Empty, requestBody.UserId);
        }

        [Fact]
        public void MapToGetContentRequestBody_ShouldMap_Version()
        {
            const string semVer = "TestUserId";
            var originalConfig = new JoystickClientConfig()
            {
                SemVer = semVer,
            };

            var requestBody = originalConfig.MapToGetContentRequestBody();

            Assert.Equal(semVer, requestBody.Version);
        }

        [Fact]
        public void MapToGetContentRequestBody_ShouldMap_Version_ToNull()
        {
            var originalConfig = new JoystickClientConfig();

            var requestBody = originalConfig.MapToGetContentRequestBody();

            Assert.Null(requestBody.Version);
        }

        [Fact]
        public void MapToGetContentRequestBody_ShouldMap_Params()
        {
            var configParams = new Dictionary<string, object>
            {
                ["allowRequest"] = (object)true,
            };

            var originalConfig = new JoystickClientConfig()
            {
                Params = configParams,
            };

            var requestBody = originalConfig.MapToGetContentRequestBody();

            Assert.Equal(configParams, requestBody.Params);
        }

        [Fact]
        public void MapToGetContentRequestBody_ShouldMap_Params_ToEmpty()
        {
            var originalConfig = new JoystickClientConfig();

            var requestBody = originalConfig.MapToGetContentRequestBody();

            Assert.Equal(0, requestBody.Params.Keys.Count);
        }
    }
}
