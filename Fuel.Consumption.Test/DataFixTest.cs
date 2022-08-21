using System.Linq;
using Fuel.Consumption.Domain;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Fuel.Consumption.Test;

public class DataFixTest:IClassFixture<TestBase>
{
    private readonly IVehicleService _vehicleService;
    private readonly IFuelUpService _fuelUpService;
    public DataFixTest(TestBase testBase)
    {
        var serviceProvider = testBase.ServiceProvider;
        _vehicleService = serviceProvider.GetRequiredService<IVehicleService>();
        _fuelUpService = serviceProvider.GetRequiredService<IFuelUpService>();
    }
    [Fact]
    public async void Update_FuelUp_Index()
    {
        var vehicles = await _vehicleService.GetAll();
        
        foreach (var vehicle in vehicles)
        {
            var index = 1;
            var fuelUpList = await _fuelUpService.GetByVehicleId(vehicle.Id);
            
            foreach (var fuelUp in fuelUpList.OrderBy(x=>x.FuelUpDate))
            {
                await _fuelUpService.Update(new FuelUp(fuelUp.VehicleId, fuelUp.Odometer, fuelUp.Distance,
                    fuelUp.Amount, fuelUp.Consumption, fuelUp.Price, fuelUp.Currency, fuelUp.Complete,
                    fuelUp.CityPercentage, fuelUp.FuelType, fuelUp.FuelRate, fuelUp.Brand, fuelUp.UserId, index,
                    fuelUp.CreatedAt, fuelUp.FuelUpDate, fuelUp.Id));
                index++;
            }

            
        }
    }
}