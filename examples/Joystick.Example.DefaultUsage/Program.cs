using Joystick.Client;
using Joystick.Client.Models;

var config = new JoystickClientConfig()
{
    ApiKey = "sjDqooGdxexWE19LMMiWDfzKqC99tzAk",
    UserId = "ann1212"
};

var joystickClient = new JoystickClient(config);

var myAppConf = await joystickClient.GetFullContentAsync<string>( "My-app");
Console.WriteLine(myAppConf.Data);

var contents = await joystickClient.GetContentsAsync<string>(new[] { "my-app", "my-auth-config" });
foreach (var key in contents.Keys)
{
    Console.WriteLine($"{key}: {contents[key]}");
}

Console.ReadKey();
