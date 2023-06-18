using Joystick.UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Joystick.Client.Exceptions;
using Joystick.Client.Services.Http;
using Joystick.Client.Utils.Validators;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Joystick.UnitTests.Utils
{
    public class JsonContentsValidatorTests
    {

        [Fact]
        public void Validate_ShouldThrow_JoystickApiBadRequestException()
        {
            var test = new Dictionary<string, JToken>()
            {
                {
                    "wrong_config",
                    JToken.Parse(
                        "Error 404,  <https://api.getjoystick.com/api/> {\"data\":null,\"status\":2,\"message\":null,\"details\":null}")
                },
                {
                    "correct_config",
                    JToken.Parse("{\"data\":\"{\\\"darkModeOn\\\":true}\",\"hash\":\"4b398873\",\"meta\":{\"uid\":412667960,\"mod\":960,\"variants\":[],\"seg\":[]}}")
                },
            };

            Assert.Throws<MultipleContentsApiException>(() => JsonContentsValidator.Validate(test));
        }

        [Fact]
        public void Validate_ShouldNotThrow_Exception()
        {
            var test = new Dictionary<string, JToken>()
            {
                {
                    "correct_config",
                    JToken.Parse("{\"data\":\"{\\\"darkModeOn\\\":true}\",\"hash\":\"4b398873\",\"meta\":{\"uid\":412667960,\"mod\":960,\"variants\":[],\"seg\":[]}}")
                },
            };

            Assert.Throws<MultipleContentsApiException>(() => JsonContentsValidator.Validate(test));
        }
    }
}
