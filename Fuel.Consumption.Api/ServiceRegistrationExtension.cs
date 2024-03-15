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
        services.AddScoped<IVehicleFacade, VehicleFacade>();
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<IBrandFacade, BrandFacade>();
        services.AddScoped<IModelGroupFacade, ModelGroupFacade>();
        services.AddScoped<IModelFacade, ModelFacade>();
        services.AddScoped<IStatisticFacade, StatisticFacade>();
        
        services.AddScoped<IFuelUpReadService, FuelUpReadService>();
        services.AddScoped<IFuelUpWriteService, FuelUpWriteService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IUserService, UserService>();
        
        services.AddScoped<IBrandReadService, BrandReadService>();
        services.AddScoped<IModelGroupReadService, ModelGroupReadService>();
        services.AddScoped<IModelReadService, ModelReadService>();
        
        services.AddScoped<IBrandWriteService, BrandWriteService>();
        services.AddScoped<IModelGroupWriteService, ModelGroupWriteService>();
        services.AddScoped<IModelWriteService, ModelWriteService>();
        services.AddScoped<IDailyStatisticWriteService, DailyStatisticWriteService>();
        services.AddScoped<IDailyStatisticReadService, DailyStatisticReadService>();
    }
}