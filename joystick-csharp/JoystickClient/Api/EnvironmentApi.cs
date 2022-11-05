using JoystickClient.Client;
using JoystickClient.Interfaces;

namespace JoystickClient.Api
{

    internal sealed class EnvironmentApi : ApiBase, IEnvironmentApi
    {
        public EnvironmentApi(IJoystickApiHttpClient httpClient) : base(httpClient)
        {
        }

        public JoystickApiClient ApiClient { get; set; }
    }
}