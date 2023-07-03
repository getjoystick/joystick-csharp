using Joystick.Client.Utils;
using Xunit;

namespace Joystick.UnitTests.Utils
{
    public class UrlHelperTests
    {
        [Fact]
        public void ConstructGetContentUrl_Should_HandleCorrectlyAmpersand()
        {
            var expectedResult = "?dynamic=true&c=%5B%22my-app%26%22%5D";
            var contentIds = new[] { "my-app&" };

            var actualResult = UrlHelper.ConstructGetContentUrl(contentIds, false);

            Assert.Equal(expectedResult, actualResult.Query);
        }

        [Theory]
        [InlineData(true, "?dynamic=true&c=%5B%22my-app%22%5D&responseType=serialized")]
        [InlineData(false, "?dynamic=true&c=%5B%22my-app%22%5D")]
        public void ConstructGetContentUrl_Should_AddSetResponseTypeCorrectly(bool isContentSerialized, string expectedResult)
        {
            var contentIds = new[] { "my-app" };

            var actualResult = UrlHelper.ConstructGetContentUrl(contentIds, isContentSerialized);

            Assert.Equal(expectedResult, actualResult.Query);
        }
    }
}
