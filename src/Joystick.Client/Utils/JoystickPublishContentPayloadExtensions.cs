using Joystick.Client.Exceptions;
using Joystick.Client.Models;
using Joystick.Client.Models.Api;
using Joystick.Client.Services.Serialization;
using Newtonsoft.Json.Linq;

namespace Joystick.Client.Utils
{
    internal static class JoystickPublishContentPayloadExtensions
    {
        internal static void Validate(this JoystickPublishContentPayload payload)
        {
            const int DescriptionMaxLength = 50;
            if (string.IsNullOrWhiteSpace(payload.Description))
            {
                throw new JoystickArgumentException($"{nameof(JoystickPublishContentPayload)}.{nameof(payload.Description)} is required.");
            }

            if (payload.Description.Length > DescriptionMaxLength)
            {
                throw new JoystickArgumentException($"{nameof(JoystickPublishContentPayload)}.{nameof(payload.Description)} should be between 1 and {DescriptionMaxLength} characters long");
            }

            if (payload.Content == null)
            {
                throw new JoystickArgumentException($"{nameof(JoystickPublishContentPayload)}.{nameof(payload.Content)} is required.");
            }
        }

        internal static UpsertContentRequestBody MapToUpsertContentRequestBody(this JoystickPublishContentPayload payload, IContentJsonSerializer serializer)
        {
            var dynamicContentMap = payload.DynamicContentMap ?? new object[0];
            var body = new UpsertContentRequestBody()
            {
                Content = JToken.Parse(serializer.Serialize(payload.Content)),
                Description = payload.Description,
                DynamicContentMap = JToken.Parse(serializer.Serialize(dynamicContentMap)),
            };

            return body;
        }
    }
}