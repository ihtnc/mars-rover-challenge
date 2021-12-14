using MarsRoverChallenge.Send;
using MarsRoverChallenge.Send.Providers;
using MarsRoverChallenge.Apps.Web.Services;

namespace MarsRoverChallenge.Apps.Web;

public static class StartupExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IRoverProvider, RoverProvider>();
        services.AddSingleton<ILandscapeProvider, LandscapeProvider>();
        services.AddSingleton<IProcessor, Processor>();

        services.AddTransient<IRoverCommandService, RoverCommandService>();

        return services;
    }
}