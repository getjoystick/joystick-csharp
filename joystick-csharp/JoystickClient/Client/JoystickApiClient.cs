using JoystickClient.Api;
using JoystickClient.Interfaces;

namespace JoystickClient.Client;

public sealed class JoystickApiClient
{
    public IConfigApi Config { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="JoystickApiClient"/> class.
    /// </summary>
    /// <param name="apiKey">Api key for authentication.</param>   
    public JoystickApiClient(string apiKey) : this(apiKey, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JoystickApiClient"/> class.
    /// </summary>
    /// <param name="apiKey">Api key for authentication.</param>
    /// <param name="httpClient">Default <see cref="HttpClient"/></param>
    public JoystickApiClient(string apiKey, HttpClient httpClient)
    {
        var joystickApiHttpClient = new JoystickApiHttpClient(apiKey, httpClient);
        Config = new ConfigApi(joystickApiHttpClient);
    }
}
