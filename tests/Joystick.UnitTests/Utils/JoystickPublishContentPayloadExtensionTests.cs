using System.Collections.Generic;
using System.Linq;
using Joystick.Client.Exceptions;
using Joystick.Client.Models;
using Joystick.Client.Services.Serialization;
using Joystick.Client.Utils;
using Joystick.UnitTests.Helpers;
using Xunit;

namespace Joystick.UnitTests.Utils
{
    public class JoystickPublishContentPayloadExtensionTests
    {
        [Fact]
        public void Validate_ShouldThrowException_WhenDescriptionIsTooLong()
        {
            var payload = new JoystickPublishContentPayload()
            {
                Description = new string('a', 51),
                Content = new object(),
            };

            var exception = Assert.Throws<JoystickArgumentException>(() => payload.Validate());
            Assert.Contains(nameof(payload.Description), exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_ShouldThrowException_WhenDescriptionIsEmpty(string description)
        {
            var payload = new JoystickPublishContentPayload()
            {
                Description = description,
                Content = new object(),
            };

            var exception = Assert.Throws<JoystickArgumentException>(() => payload.Validate());
            Assert.Contains(nameof(payload.Description), exception.Message);
        }

        [Fact]
        public void Validate_ShouldThrowException_WhenDescriptionContentIsNull()
        {
            var payload = new JoystickPublishContentPayload()
            {
                Description = "This is description",
                Content = null,
            };

            var exception = Assert.Throws<JoystickArgumentException>(() => payload.Validate());
            Assert.Contains(nameof(payload.Content), exception.Message);
        }

        [Fact]
        public void Validate_Should_NoThrowException_WhenPayloadIsValid()
        {
            var payload = new JoystickPublishContentPayload()
            {
                Description = new string('a', 50),
                Content = new object(),
            };

            var exception = Record.Exception(() => payload.Validate());
            Assert.Null(exception);
        }

        [Fact]
        public void MapToUpsertContentRequestBody_ShouldMap_Description()
        {
            const string description = "This is description";

            var payload = new JoystickPublishContentPayload()
            {
                Description = description,
            };

            var requestBody = payload.MapToUpsertContentRequestBody(new JoystickDefaultContentJsonSerializer());

            Assert.Equal(description, requestBody.Description);
        }

        [Fact]
        public void MapToUpsertContentRequestBody_ShouldMap_Content()
        {
            var content = DataHelper.GetContent();

            var payload = new JoystickPublishContentPayload()
            {
                Content = content,
            };

            var requestBody = payload.MapToUpsertContentRequestBody(new JoystickDefaultContentJsonSerializer());

            Assert.Equal(3, requestBody.Content.Count());
            Assert.NotNull(requestBody.Content[nameof(content.Greeting)]);
            Assert.NotNull(requestBody.Content[nameof(content.Scales)]);
            Assert.NotNull(requestBody.Content[nameof(content.Theme)]);
        }

        [Fact]
        public void MapToUpsertContentRequestBody_ShouldMap_DynamicContentMap()
        {
            var dynamicContentMap = new List<object>
            {
                new
                {
                    CountryCode = "IT",
                    Percent = 0.75f,
                },
                new
                {
                    CountryCode = "HR",
                    Percent = 0.10f,
                },
            };

            var payload = new JoystickPublishContentPayload()
            {
                 DynamicContentMap = dynamicContentMap,
            };

            var requestBody = payload.MapToUpsertContentRequestBody(new JoystickDefaultContentJsonSerializer());

            Assert.Equal(dynamicContentMap.Count, requestBody.DynamicContentMap.Count());
        }

        [Fact]
        public void MapToUpsertContentRequestBody_ShouldMap_DynamicContentMap_AsEmptyArray()
        {
            var payload = new JoystickPublishContentPayload();

            var requestBody = payload.MapToUpsertContentRequestBody(new JoystickDefaultContentJsonSerializer());

            Assert.Empty(requestBody.DynamicContentMap);
        }
    }
}
