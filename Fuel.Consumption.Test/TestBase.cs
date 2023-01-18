using System;
using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Fuel.Consumption.Infrastructure.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fuel.Consumption.Test;

public class TestBase
{
    private IConfigurationRoot Configuration { get; set; }
    public readonly ServiceProvider ServiceProvider;
    public TestBase()
    {
        var workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
        const string environmentSpecificFileName = "appsettings.Development.json";
        var configPath = $"{workingDirectory}{environmentSpecificFileName}";
        var builder = new ConfigurationBuilder().AddJsonFile(configPath);
        Configuration = builder.Build();
        var services = new ServiceCollection();

        services.Configure<ApiConfig>(Configuration);
        
        services.AddScoped<IFuelUpReadService, FuelUpReadService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IUserService, UserService>();
        ServiceProvider = services.BuildServiceProvider();
    }
}