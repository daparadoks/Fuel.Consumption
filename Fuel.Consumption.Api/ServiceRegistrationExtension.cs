using Fuel.Consumption.Api.Facade;
using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Fuel.Consumption.Infrastructure.Mongo;

namespace Fuel.Consumption.Api;

public static class ServiceRegistrationExtension
{
    public static void Register(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiConfig>(configuration);

        services.AddScoped<IFuelUpFacade, FuelUpFacade>();
        
        services.AddScoped<IFuelUpService, FuelUpService>();
        services.AddScoped<IVehicleService, VehicleService>();
    }
}