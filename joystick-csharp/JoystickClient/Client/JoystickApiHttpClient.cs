using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JoystickClient;
using JoystickClient.Client;
using JoystickClient.Core;
using JoystickClient.Interfaces;

namespace JoystickClient.Client
{
    internal class JoystickApiHttpClient : IJoystickApiHttpClient
    {
        private readonly HttpClient _httpClient;

        public JoystickApiHttpClient(string apiKey, HttpClient client = null)
        {
            _httpClient = client ?? new HttpClient();
            AddDefaultRequestHeader(Constants.ApiKeyHeaderName, apiKey);
        }

        private void AddDefaultRequestHeader(string header, string value)
        {
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header, value);
        }

        public async Task<JoystickApiResponse> SendRequestAsync(JoystickApiRequest request, CancellationToken cancellationToken)
        {
            try
            {
                using (var httpRequest = BuildHttpRequestMessage(request))
                {
                    using (var response = await _httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false))
                    {
                        string errorMessage = default;

                        try
                        {
                            response.EnsureSuccessStatusCode();
                        }
                        catch (HttpRequestException e)
                        {
                            errorMessage = e.Message;
                        }

                        return new JoystickApiResponse(
                            response.StatusCode,
                            response.Headers.ToDictionary(x => x.Key, x => x.Value.FirstOrDefault()),
                            await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false),
                            response.Content.Headers?.ContentType?.MediaType, errorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                return new JoystickApiResponse(HttpStatusCode.InternalServerError, errorMessage: e.Message);
            }
        }

        private static HttpRequestMessage BuildHttpRequestMessage(JoystickApiRequest request)
        {
            var contentType = string.IsNullOrEmpty(request.ContentType) ? "application/json" : request.ContentType;
            var requestMessage = new HttpRequestMessage(request.Method, request.Url);

            if (request.BodyContent != null)
            {
                requestMessage.Content = JsonContent.Create(request.BodyContent, request.BodyContent.GetType());
                if (!requestMessage.Content.Headers.Contains("Content-Type"))
                {
                    requestMessage.Content.Headers.Add("Content-Type", contentType);
                }
            }
            else
            {
                if (request.PostParams?.Count > 0)
                {
                    requestMessage.Content = new StringContent(string.Join("&", request.PostParams.Select(kvp => $"{kvp.Key}={System.Web.HttpUtility.UrlEncode(kvp.Value)}")),
                        Encoding.UTF8, "application/x-www-form-urlencoded");
                }
            }

            var uri = request.Url.ToString();
            if (request.PathParams?.Count > 0)
            {
                foreach (var kvp in request.PathParams)
                {
                    uri = uri.Replace($"{{{kvp.Key}}}", kvp.Value);
                }
            }

            if (request.QueryParams?.Count > 0)
            {
                var queryString = "?" + string.Join("&", request.QueryParams
                                .Select(kvp => $"{kvp.Key}={System.Web.HttpUtility.UrlEncode(kvp.Value)}"));
                uri += queryString;
            }

            requestMessage.RequestUri = new Uri(uri);

            if (request.HeaderParams?.Count > 0)
            {
                foreach (var headerParams in request.HeaderParams)
                {
                    if (!requestMessage.Headers?.Contains(headerParams.Key) ?? false)
                    {
                        requestMessage.Headers.Add(headerParams.Key, headerParams.Value);
                    }
                }
            }

            return requestMessage;
        }
    }
}