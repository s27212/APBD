using TripApp.Application.Repositories;
using TripApp.Context;
using TripApp.Infrastructure.Repositories;

namespace TripApp.Infrastructure;

public static class InfrastructureServicesExtension
{
    public static void RegisterInfraServices(this IServiceCollection app)
    {
        app.AddScoped<ITripRepository, TripRepository>();
        app.AddDbContext<TripDbContext>();
    }
}