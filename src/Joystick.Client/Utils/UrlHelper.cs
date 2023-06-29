using System;
using System.Collections.Generic;
using Joystick.Client.Core;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Joystick.Client.Utils
{
    internal static class UrlHelper
    {
        internal static Uri ConstructGetContentUrl(IEnumerable<string> contentIds, bool isContentSerialized)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "dynamic", "true" },
                { "c", JsonConvert.SerializeObject(contentIds) },
            };

            if (isContentSerialized)
            {
                queryParameters.Add("responseType", "serialized");
            }

            return new Uri(QueryHelpers.AddQueryString($"{Constants.BaseReadUrl}/v1/combine/", queryParameters));
        }
    }
}
