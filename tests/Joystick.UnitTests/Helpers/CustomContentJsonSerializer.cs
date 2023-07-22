using Joystick.Client.Services.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Joystick.UnitTests.Helpers
{
    internal class CustomContentJsonSerializer : IContentJsonSerializer
    {
        private StringEnumConverter converter;

        public CustomContentJsonSerializer()
        {
            this.converter = new StringEnumConverter();
        }

        public TOutput Deserialize<TOutput>(string json)
        {
            return JsonConvert.DeserializeObject<TOutput>(json, this.converter);
        }

        public string Serialize<TEntity>(TEntity entity)
        {
            return JsonConvert.SerializeObject(entity, this.converter);
        }
    }
}
