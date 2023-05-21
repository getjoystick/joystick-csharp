using System.Text.Json.Nodes;
using Joystick.Client;
using Joystick.Client.Models;
using Newtonsoft.Json.Linq;
using UsageClientExample;

var config = new JoystickClientConfig()
{
    ApiKey = "sjDqooGdxexWE19LMMiWDfzKqC99tzAk",
    UserId = "ann1212"
};

var joystickClient = new JoystickClient(config);

Console.WriteLine("--Example with System.Text.Json---");
var resultWithSystemJson = await joystickClient.GetContentAsync<JsonObject>("my-app");
Console.WriteLine($"Result with Data as JsonObject: {resultWithSystemJson.Data.ToJsonString()}");

Console.WriteLine("\n\r--Example with Newtonsoft.Json---");
try
{
    var resultWithNewtonsoftJson = await joystickClient.GetContentAsync<JObject>("my-app");
}
catch(Exception ex)
{
    Console.WriteLine($"Error was occurred for deserializing to Newtonsoft.Json: {ex.Message}");
}

Console.WriteLine("\n\r--Example with string---");
try
{
    var resultWithStringGeneric = await joystickClient.GetContentAsync<string>("my-app");
    Console.WriteLine($"Result with Data as string: {resultWithStringGeneric}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error was occurred for deserializing to string: {ex.Message}");
}


var resultWithString = await joystickClient.GetContentAsync("my-app");
Console.WriteLine($"Result with Data as string: {resultWithString.Data}");


Console.WriteLine("\n\r--Example with DTO---");
var myAppConf = await joystickClient.GetContentAsync<MyAppConfig>("my-app");
Console.WriteLine($"Result with Data as DTO: AppName = {myAppConf.Data.AppNamy}");


Console.ReadKey();