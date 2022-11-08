using System.Threading.Tasks;
using System.Threading;
using JoystickClient.Client;
using JoystickClient.Interfaces;
using JoystickClient.Models.ApiResponse;
using JoystickClient.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace JoystickClient.Api
{

    internal sealed class EnvironmentApi : ApiBase, IEnvironmentApi
    {
        private const string getEnvironmentCatalogPath = "env/catalog";

        public EnvironmentApi(IJoystickApiHttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<GetEnvironmentCatalogResponse> GetEnvironmentCatalogAsync(CancellationToken token = default)
        {
            var request = CreateRequest(getEnvironmentCatalogPath, HttpMethod.Get);
            var response = await CallApiAsync(request, token);

            return HandleResponse<GetEnvironmentCatalogResponse, ErrorResponseBody>(response);
        }
    }
}