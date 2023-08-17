# .NET Client for [Joystick Remote Config](https://www.getjoystick.com/)

This is a library that simplifies communicating with the [Joystick API](https://docs.getjoystick.com/) for using remote configs with your C# project. Joystick is a modern remote config platform where you manage all of your configurable parameters. We are natively multi-environment, preserve your version history, have advanced work-flow & permissions management, and much more. Have one API to use for any JSON configs.

- [Full Developer Documentation](https://docs.getjoystick.com)
- [Joystick Remote Config](https://getjoystick.com)

Provided client is supporting .NET Standard 2.1+, .NET Core 3.1+, .NET 5.0+, .NET 6.0+, .NET 7.0+.

## Installation

Using the [.NET Core command-line interface (CLI) tools][dotnet-core-cli-tools]:

```sh
dotnet add package Joystick
```

Using the [NuGet Command Line Interface (CLI)][nuget-cli]:

```sh
nuget install Joystick
```

Using the [Package Manager Console][package-manager-console]:

```powershell
Install-Package Joystick
```

From within Visual Studio:

1. Open the Solution Explorer.
2. Right-click on a project within your solution.
3. Click on *Manage NuGet Packages...*
4. Click on the *Browse* tab and search for "Joystick".
5. Click on the Stripe.net package, select the appropriate version in the
   right-tab and click *Install*.

## Usage

Using Joystick to get remote configurations in your .NET project is a breeze.

```C#
// Import the package.
using Joystick.Client;

// Initialize a client with a Joystick API Key
var config = new JoystickClientConfig()
{
    ApiKey = "<put-your-api_key-here>",
};
var joystickClient = new JoystickClient(config);

// Request a single configuration as Newtonsoft JObject
const contentId1 = await joystickClient.GetContentAsync("content-id1");

// Request a typed single configuration
const contentId1Typed = await joystickClient.GetContentAsync<TypeForContentId1>(
"content-id1"
);

// Request multiple configurations at the same time
const configurations = await joystickClient.GetContentsAsync(new[]{
    "content-id1",
    "content-id2",
});

// {
//     "content-id1": {...},
//     "content-id2": {...}
// }
```

### Specifying Additional Parameters

When creating the `Joystick` object, you can specify additional parameters which will be used by all API calls to the Joystick API. These additional parameters are used for AB Testing (`userId`), segmentation (`params`), and backward-compatible version delivery (`semVer`).

For more details see [API documentation](https://docs.getjoystick.com/api-reference/).

```C#
// Initializing a client with options
var config = new JoystickClientConfig()
{
  ApiKey = "some-api-key",
  SemVer = "0.0.1",
  UserId = "user-id-1",
  Params = new Dictionary<string, object>()
  {
    { "Country", "PL" },
    { "UserPrc", 85.08 },
  },
  CacheOptions = new JoystickCacheOptions() {
    CacheExpirationSeconds = 600, // default 600 (10 mins)
  },
};
```

### Error handling

The client can raise different types of exceptions with the base class of `JoystickException`.

```C#
try {
    const configurations = await joystickClient.GetContentsAsync(new[]{
        "content-id1",
        "content-id2",
    });
} 
catch (JoystickApiHttpException e) {
    // Handle HTTP error (i.e. timeout, or invalid HTTP code)
}
catch (MultipleContentsApiException e) {
    // Handle API exception (i.e. content is not found, or some of the keys can't be retrieved)
}
```

### Caching

By default, the client uses [JoystickDefaultCacheService](./src/Joystick.Client/Services/Cache/JoystickDefaultCacheService.cs), based on [MemoryCache](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.caching.memorycache?redirectedfrom=MSDN&view=dotnet-plat-ext-7.0).

You can specify your own cache implementation by implementing the interface [SdkCache](./src/Joystick.Client/Services/Cache/IJoystickCacheService.cs).

#### `Refresh` option

To ignore the existing cache when requesting a config – pass this option as `true`.

```C#
var options = new JoystickContentOptions()
{
    Refresh = true;
}

await joystickClient
  .GetContentAsync("content-id1", options);

// OR

await joystickClient
  .GetContentsAsync(new[] {"content-id1", "content-id2"}, options);
```
#### Clear the cache

If you want to clear the cache:

```C#
joystickClient.ClearCache();
```

## License

The MIT. Please see [License File](LICENSE.md) for more information.