using System;
using Fuel.Consumption.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fuel.Consumption.Test;

public class FuelAppTestStartup
{
    public readonly ServiceProvider ServiceProvider;
    private IConfigurationRoot Configuration { get; set; }

    public FuelAppTestStartup()
    {
        var workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var builder = new ConfigurationBuilder().AddJsonFile($"{workingDirectory}\\appsettings.Development.json");
        Configuration = builder.Build();
        var services = new ServiceCollection();
        services.Register(Configuration);
        ServiceProvider = services.BuildServiceProvider();
    }
}