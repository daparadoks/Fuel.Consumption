using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Constants;

namespace Fuel.Consumption.Api.Facade.Request;

public class FuelUpRequest
{
    public string VehicleId { get; set; }
    public int Odometer { get; set; }
    public double Amount { get; set; }
    public double Price { get; set; }
    public CurrencyEnum Currency { get; set; }
    public bool Complete { get; set; }
    public int CityPercentage { get; set; }
    public int FuelType { get; set; }
    public int FuelRate { get; set; }
    public string Brand { get; set; }
    public DateTime FuelUpDate { get; set; }
    public int? TimeZone { get; set; }

    public FuelUp ToDomain(string userId, double consumption, int lastOdometer) => new(VehicleId,
        Odometer,
        ToDistance(lastOdometer),
        Amount,
        consumption,
        Price,
        (int)Currency,
        Complete,
        CityPercentage,
        FuelType,
        FuelType,
        Brand,
        userId,
        ToNow(),
        FuelUpDate);

    private DateTime ToNow() => DateTime.UtcNow.AddHours(TimeZone ?? EssentialConstants.DefaultTimeZone);

    private int ToDistance(int lastOdometer) => Odometer - lastOdometer;
}