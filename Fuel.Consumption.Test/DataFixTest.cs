using System.Linq;
using Fuel.Consumption.Domain;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Fuel.Consumption.Test;

public class DataFixTest:IClassFixture<TestBase>
{
    private readonly IVehicleService _vehicleService;
    private readonly IFuelUpReadService _fuelUpReadService;
    private readonly IFuelUpWriteService _fuelUpWriteService;
    public DataFixTest(TestBase testBase)
    {
        var serviceProvider = testBase.ServiceProvider;
        _vehicleService = serviceProvider.GetRequiredService<IVehicleService>();
        _fuelUpReadService = serviceProvider.GetRequiredService<IFuelUpReadService>();
        _fuelUpWriteService = serviceProvider.GetRequiredService<IFuelUpWriteService>();
    }
    
}