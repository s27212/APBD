using MedApp.Context;
using MedApp.Repositories;
using MedApp.Services;

namespace MedApp.Configuration;

public static class ServicesExtension
{
    public static void RegisterServices(this IServiceCollection app)
    {
        app.AddDbContext<MedDbContext>();
        app.AddScoped<IMedRepository, MedRepository>();
        app.AddScoped<IMedService, MedService>();
    }
}