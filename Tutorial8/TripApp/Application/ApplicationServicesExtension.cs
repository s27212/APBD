using TripApp.Application.Services;

namespace TripApp.Application;

public static class ApplicationServicesExtension
{
    public static void RegisterApplicationServices(this IServiceCollection app)
    {
        app.AddScoped<ITripService, TripService>();
    }
}