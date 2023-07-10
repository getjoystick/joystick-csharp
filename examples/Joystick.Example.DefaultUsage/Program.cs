using Joystick.Client;
using Joystick.Client.Exceptions;
using Joystick.Client.Models;
using Joystick.Example.DefaultUsage;

var config = new JoystickClientConfig()
{
    ApiKey = "sjDqooGdxexWE19LMMiWDfzKqC99tzAk",
    UserId = "ann1212"
};

var joystickClient = new JoystickClient(config);

var myAppConf = await joystickClient.GetFullContentAsync<string>("My-app");
Console.WriteLine(myAppConf.Data);

var contents = await joystickClient.GetContentsAsync<string>(new[] { "my-app", "my-auth-config" });
foreach (var key in contents.Keys)
{
    Console.WriteLine($"{key}: {contents[key]}");
}

try
{
    var contentsWithError = await joystickClient.GetContentsAsync<string>(new[] { "my-app_771" });
}
catch (MultipleContentsApiException exception)
{
    Console.WriteLine(exception.Message);
}

var designConfigContent = new DesignConfigs()
{
    Thema = DesignThema.Light,
    Greeting = "Hi!",
    Scale = 75,
};

var payload = new JoystickPublishContentPayload()
{
    Content = designConfigContent,
    Description = "Config to describe web app design",
};

await joystickClient.PublishContentUpdateAsync("new-config", payload);

Console.ReadKey();