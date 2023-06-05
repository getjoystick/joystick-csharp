using Joystick.Client;
using Joystick.Client.Models;

var config = new JoystickClientConfig()
{
    ApiKey = "sjDqooGdxexWE19LMMiWDfzKqC99tzAk",
    UserId = "ann1212"
};
var joystickClient = new JoystickClient(config);

var myAppConf = await joystickClient.GetFullContentAsync<string>( "my-app");
Console.WriteLine(myAppConf.Data);
Console.ReadKey();