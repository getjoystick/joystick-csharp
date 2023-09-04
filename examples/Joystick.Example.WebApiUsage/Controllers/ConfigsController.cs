using Joystick.Client;
using Joystick.Client.Models;
using Joystick.Example.WebApiUsage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Joystick.Example.WebApiUsage.Controllers
{
    [ApiController]
    [Route("configs")]
    public class ConfigsController : ControllerBase
    {
        private readonly ILogger<ConfigsController> _logger;
        private readonly IJoystickClient joystickClient;

        public ConfigsController(IJoystickClient joystickClient, ILogger<ConfigsController> logger)
        {
            this.joystickClient = joystickClient;
            _logger = logger;
        }

        [HttpGet]
        [Route("design/full-content")]
        public async Task<JoystickFullContent<DesignConfigs>> GetFullContentDesignConfigs()
        {
            /// Example of Json config
            /// {"Theme":2,"Greeting":"Hi!","Scale":75}
 
            var fullContent = await joystickClient.GetFullContentAsync<DesignConfigs>("design-config");

            return fullContent;
        }
    }
}