using JoystickClient.Client;
using JoystickClient.Clients;
using JoystickClient.Interfaces;

namespace JoystickClient.Api;

internal sealed class InfoApi : ApiBase, IInfoApi
{
    public InfoApi(IJoystickApiHttpClient httpClient) : base(httpClient)
    {
    }

    public JoystickApiClient ApiClient { get; set; }
}
