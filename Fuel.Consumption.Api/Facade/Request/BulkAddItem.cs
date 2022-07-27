using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Constants;
using Newtonsoft.Json;

namespace Fuel.Consumption.Api.Facade.Request;

public class BulkAddItem
{
    public string VehicleName { get; set; }
    public double Consumption { get; set; }
    public double Odometer { get; set; }
    public double Distance { get; set; }
    public double Amount { get; set; }
    public double Price { get; set; }
    public int Missed { get; set; }
    public int Partial { get; set; }
    public int CityPercentage { get; set; }
    public string Brand { get; set; }
    public DateTime FuelUpDate { get; set; }
    
    public FuelUp ToDomain(string vehicleId, string userId, int currency, int fuelType, int fuelRate, int? timeZone) => new(vehicleId,
        (int)Odometer,
        (int)Distance,
        Amount,
        Consumption,
        Price,
        currency,
        Missed == 0 && Partial == 0,
        CityPercentage,
        fuelType,
        fuelRate,
        Brand,
        userId,
        DateTime.UtcNow.AddHours(timeZone ?? EssentialConstants.DefaultTimeZone),
        FuelUpDate);
}