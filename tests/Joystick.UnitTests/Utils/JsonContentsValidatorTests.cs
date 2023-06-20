using System.Collections.Generic;
using Joystick.Client.Exceptions;
using Joystick.Client.Utils.Validators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Joystick.UnitTests.Utils
{
    public class JsonContentsValidatorTests
    {

        [Fact]
        public void Validate_ShouldThrow_JoystickApiBadRequestException()
        {
            var jsonContents = "{\"wrong_config\":\"Error 404,  https://api.getjoystick.com/api/v1 {\\\"data\\\":null,\\\"status\\\":2,\\\"message\\\":null,\\\"details\\\":null}.\", \"correct_config\":{\"data\":\"{\\\"darkModeOn\\\":true}\",\"hash\":\"4b398873\",\"meta\":{\"uid\":412667960,\"mod\":960,\"variants\":[],\"seg\":[]}}}";
            var testData = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(jsonContents);

            Assert.Throws<MultipleContentsApiException>(() => JsonContentsValidator.Validate(testData));
        }

        [Fact]
        public void Validate_ShouldNotThrow_Exception()
        {
            var jsonContents = "{\"correct_config\":{\"data\":\"{\\\"darkModeOn\\\":true}\",\"hash\":\"4b398873\",\"meta\":{\"uid\":412667960,\"mod\":960,\"variants\":[],\"seg\":[]}}}";
            var testData = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(jsonContents);

            var exception = Record.Exception(() => JsonContentsValidator.Validate(testData));

            Assert.Null(exception);
        }
    }
}
