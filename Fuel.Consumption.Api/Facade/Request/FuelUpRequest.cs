using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Constants;

namespace Fuel.Consumption.Api.Facade.Request;

public class FuelUpRequest
{
    public string VehicleId { get; set; }
    public int Odometer { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public decimal TotalCost { get; set; }
    public CurrencyEnum Currency { get; set; }
    public bool Complete { get; set; }
    public int CityPercentage { get; set; }
    public DateTime FuelUpDate { get; set; }
    public int? TimeZone { get; set; }

    public FuelUp ToDomain(string userId, Vehicle vehicle, DateTime? createdAt) => new(Odometer,
        Amount,
        Price,
        TotalCost,
        (int)Currency,
        Complete,
        CityPercentage,
        userId,
        vehicle,
        createdAt ?? ToNow(),
        FuelUpDate,
        ToNow());

    private DateTime ToNow() => DateTime.UtcNow.AddHours(TimeZone ?? EssentialConstants.DefaultTimeZone);

    private int ToDistance(int lastOdometer) => Odometer - lastOdometer;
}