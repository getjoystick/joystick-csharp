using System;
using System.Collections;
using System.Collections.Generic;
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
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('[');

            stringBuilder.Append(config.ApiKey);
            stringBuilder.Append(',');

            if (config.Params != null && config.Params.Any())
            {
                var sortedParams = config.Params.OrderBy(obj => obj.Key, StringComparer.OrdinalIgnoreCase);

                stringBuilder.Append(JsonConvert.SerializeObject(sortedParams));
            }

            stringBuilder.Append(',');
            stringBuilder.Append(config.SemVer);
            stringBuilder.Append(',');
            stringBuilder.Append(config.UserId);
            stringBuilder.Append(',');
            stringBuilder.Append(isContentSerialized);
            stringBuilder.Append(",[");
            stringBuilder.AppendJoin(',', contentIds.OrderBy(x => x, StringComparer.OrdinalIgnoreCase));
            stringBuilder.Append("]]");

            return stringBuilder.ToString().ToLower();
        }
    }
}
