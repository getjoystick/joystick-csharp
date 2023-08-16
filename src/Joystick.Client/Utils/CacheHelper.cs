using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Joystick.Client.Models;
using Newtonsoft.Json;

namespace Joystick.Client.Utils
{
    internal static class CacheHelper
    {
        internal static string GenerateCacheKey(JoystickClientConfig config, bool isContentSerialized, IEnumerable<string> contentIds)
        {
            var stringBuilder = new StringBuilder();
            var stringKey = BuildStringCacheKey(config, isContentSerialized, contentIds);
            using (SHA256 hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(stringKey));

                foreach (var b in result)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
            }

            return stringBuilder.ToString();
        }

        internal static string BuildStringCacheKey(JoystickClientConfig config, bool isContentSerialized, IEnumerable<string> contentIds)
        {
            IEnumerable<KeyValuePair<string, object>> sortedParams;
            if (config.Params != null && config.Params.Any())
            {
                sortedParams = config.Params.OrderBy(obj => obj.Key, StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                sortedParams = new Dictionary<string, object>();
            }

            var contentIdsList = contentIds.ToList();
            var keyParts = new object[]
            {
                config.ApiKey,
                sortedParams,
                config.SemVer,
                config.UserId,
                contentIdsList.Select(x => x.ToLower()).OrderBy(x => x),
                isContentSerialized,
            };

            return JsonConvert.SerializeObject(keyParts);
        }
    }
}
