using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Joystick.UnitTests.Helpers
{
    public static class DataHelper
    {
        public static string GetSerializedContent()
        {
            var data = new TestData()
            {
                Greeting = "Hello!",
                Multiplier = new[] { 1.5f, 2.25f, 5.75f },
            };

            return JsonConvert.SerializeObject(data);
        }
    }
}
