using System;
using System.Net.Http;
using JoystickClient.Api;
using JoystickClient.Clients;
using JoystickClient.Interfaces;

namespace JoystickClient.Client
{
    public sealed class JoystickApiClient : IJoystickApiClient
    {
        public IConfigApi Config { get; }
        public IEnvironmentApi Environment { get; }
        public IInfoApi Info { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JoystickApiClient"/> class.
        /// </summary>
        /// <param name="apiKey">Api key for authentication.</param>   
        public JoystickApiClient(string apiKey) : this(apiKey, null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
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
            Environment = new EnvironmentApi(joystickApiHttpClient);
            Info = new InfoApi(joystickApiHttpClient);
        }
    }
}
