using JoystickClient.Client;

namespace JoystickClient.Tests
{
    public class Tests
    {
        private JoystickApiClient _client;
        [SetUp]
        public void Setup()
        {
            _client = new JoystickApiClient("kl-555-0000");
        }

        [Test]
        public async Task Test1()
        {
            var configValue = await _client.Config.GetConfigAsync("api_push_in_target_01", CancellationToken.None);

            var dynConfigValue = await _client.Config.GetDynamicConfig("api_push_in_target_01", "userID",
                new Dictionary<string, object>() { { "param1", "value" }, { "param2", "value" } }, CancellationToken.None);

            Assert.Pass();
        }
    }
}