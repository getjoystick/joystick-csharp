using Microsoft.Extensions.DependencyInjection;

namespace Joystick.Client
{
    public static class JoystickBootstrap
    {
        public static IServiceCollection AddJoystickClient(this IServiceCollection services)
        {
            services.AddScoped<IJoystickClient, JoystickClient>();
            return services;
        }
    }
}