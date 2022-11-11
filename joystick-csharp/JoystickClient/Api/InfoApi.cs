using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using JoystickClient.Client;
using JoystickClient.Clients;
using JoystickClient.Interfaces;
using JoystickClient.Models.ApiResponse;
using JoystickClient.Models;

namespace JoystickClient.Api
{
    internal sealed class InfoApi : ApiBase, IInfoApi
    {
        private const string getOpsDeckApiVersionPath = "version";

        public InfoApi(IJoystickApiHttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<string> GetApiVersionAsync(CancellationToken token = default)
        {
            var request = CreateRequest(getOpsDeckApiVersionPath, HttpMethod.Get, useApiVersion: false);
            var response = await CallApiAsync(request, token);

            return HandleResponse<string, ErrorResponseBody>(response);
        }
    }
}
