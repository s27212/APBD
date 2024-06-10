﻿using MedApp.Context;
using MedApp.Repositories;
using MedApp.Services;
using Tutorial10.Properties.Helpers;
using Tutorial10.Properties.Services;

namespace MedApp.Configuration;

public static class ServicesExtension
{
    public static void RegisterServices(this IServiceCollection app)
    {
        app.AddDbContext<MedDbContext>();
        app.AddScoped<IMedRepository, MedRepository>();
        app.AddScoped<IMedService, MedService>();
        app.AddScoped<IAuthService, AuthService>();
        app.AddSingleton<SecurityHelpers>();
    }
}