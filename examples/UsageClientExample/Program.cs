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
var resultWithSystemJson = await joystickClient.GetContentAsyncV1<JsonObject>("my-app");
Console.WriteLine($"Result with Data as JsonObject: {resultWithSystemJson.Data.ToJsonString()}");

Console.WriteLine("\n\r--Example with Newtonsoft.Json---");
try
{
    var resultWithNewtonsoftJson = await joystickClient.GetContentAsyncV1<JObject>("my-app");
}
catch(Exception ex)
{
    Console.WriteLine($"Error was occurred for deserializing to Newtonsoft.Json: {ex.Message}");
}

Console.WriteLine("\n\r--Example with string---");
try
{
    var resultWithStringGeneric = await joystickClient.GetContentAsyncV1<string>("my-app");
    Console.WriteLine($"Result with Data as string: {resultWithStringGeneric}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error was occurred for deserializing to string: {ex.Message}");
}


var resultWithString = await joystickClient.GetContentAsyncV1("my-app");
Console.WriteLine($"Result with Data as string: {resultWithString.Data}");


Console.WriteLine("\n\r--Example with DTO---");
var myAppConf = await joystickClient.GetContentAsyncV1<MyAppConfig>("my-app");
Console.WriteLine($"Result with Data as DTO: AppName = {myAppConf.Data.AppNamy}");


Console.WriteLine("\n\r--Example with Interface---");
Console.WriteLine("\n\r**FullResponse = true**");
var abstractContent = await joystickClient.GetContentV2<MyAppConfig>("my-app", new JoystickContentOptions() { FullResponse = true});
var concreteContent = (JoystickFullResponse<MyAppConfig>) abstractContent;
Console.WriteLine($"Result with Data as DTO: AppName = {concreteContent.Data.AppNamy}");

try
{
    var concreteContentWrong = (JoystickFullResponse<string>)abstractContent;
}
catch (Exception ex)
{
    Console.WriteLine($"Error was occurred for deserializing to string: {ex.Message}");
}

abstractContent = await joystickClient.GetContentV2<string>("my-app", new JoystickContentOptions() { FullResponse = true, Serialized = true });
var concreteContentSerialized = (JoystickFullResponse<string>)abstractContent;
Console.WriteLine($"Result with Data as string: {concreteContentSerialized.Data}");

Console.WriteLine("\n\r**FullResponse = false**");
abstractContent = await joystickClient.GetContentV2<JoystickSerializedContent>("my-app", new JoystickContentOptions() { Serialized = true });
var concreteSerializedData = (JoystickSerializedContent)abstractContent;
Console.WriteLine($"Result with Compact Data as string: {concreteSerializedData.Data}");

abstractContent = await joystickClient.GetContentV2<MyAppAuthConfig>("my-auth-config", new JoystickContentOptions());
var concreteData = (MyAppAuthConfig)abstractContent;
Console.WriteLine($"Result with Compact Data as DTO: AuthUrl = {concreteData.AuthUrl}");

Console.ReadKey();