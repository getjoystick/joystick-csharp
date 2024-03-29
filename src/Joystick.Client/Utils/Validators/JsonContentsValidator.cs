﻿using System.Collections.Generic;
using System.Linq;
using Joystick.Client.Exceptions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Joystick.Client.Utils.Validators
{
    internal static class JsonContentsValidator
    {
        internal static void Validate(Dictionary<string, JToken> contents)
        {
            var schema = JSchema.Parse(@"{
              'type': 'object',
              'properties': {
                'hash': {'type':'string'},
                'meta': {'type':'object'},
                'data': {
                  'type': ['object', 'string'] ,
                }
              }
            }");
            var errors = contents.Where(x => !x.Value.IsValid(schema)).ToList();
            if (errors.Any())
            {
                throw new MultipleContentsApiException(string.Join(';', errors.Select(x => $"{x.Key}: {x.Value}")));
            }
        }
    }
}
