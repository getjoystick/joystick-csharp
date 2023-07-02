using System.Collections.Generic;
using Joystick.Client.Exceptions;
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_ShouldThrowException_WhenApiKeyInvalid(string apiKey)
        {
            var config = new JoystickClientConfig()
            {
                ApiKey = apiKey,
            };

            var exception = Assert.Throws<JoystickException>(() => config.Validate());
            Assert.Contains(nameof(config.ApiKey), exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("v1.2.3")]
        [InlineData("0.0.1-prerelease")]

        public void Validate_ShouldThrowException_WhenSenVerInvalid(string semVer)
        {
            var config = new JoystickClientConfig()
            {
                ApiKey = "key",
                SemVer = semVer,
            };

            var exception = Assert.Throws<JoystickException>(() => config.Validate());
            Assert.Contains(nameof(config.SemVer), exception.Message);
        }

        [Fact]
        public void Validate_ShouldThrowException_WhenCacheExpirationSecondsInvalid()
        {
            var config = new JoystickClientConfig()
            {
                ApiKey = "key",
                CacheExpirationSeconds = 0,
            };

            var exception = Assert.Throws<JoystickException>(() => config.Validate());
            Assert.Contains(nameof(config.CacheExpirationSeconds), exception.Message);
        }

        [Fact]
        public void Validate_Should_NoThrowException_WhenCacheExpirationSecondsIsNull()
        {
            var config = new JoystickClientConfig()
            {
                ApiKey = "key",
                CacheExpirationSeconds = null,
            };

            var exception = Record.Exception(() => config.Validate());
            Assert.Null(exception);
        }

        [Fact]
        public void Validate_Should_NotThrowException_WhenConfigValid()
        {
            var config = new JoystickClientConfig()
            {
                ApiKey = "key",
                CacheExpirationSeconds = 500,
                SemVer = "1.0.5",
            };

            var exception = Record.Exception(() => config.Validate());
            Assert.Null(exception);
        }
    }
}
