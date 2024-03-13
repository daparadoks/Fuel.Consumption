using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Constants;

namespace Fuel.Consumption.Api.Facade.Request;

public class FuelUpRequest
{
    public string VehicleId { get; set; }
    public int Odometer { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public CurrencyEnum Currency { get; set; }
    public bool Complete { get; set; }
    public int CityPercentage { get; set; }
    public int FuelType { get; set; }
    public DateTime FuelUpDate { get; set; }
    public int? TimeZone { get; set; }

    public FuelUp ToDomain(Guid id, string userId, DateTime? createdAt) => new(id.ToString(),
        VehicleId,
        Odometer,
        Amount,
        Price,
        (int)Currency,
        Complete,
        CityPercentage,
        FuelType,
        userId,
        createdAt ?? ToNow(),
        FuelUpDate,
        ToNow());

    private DateTime ToNow() => DateTime.UtcNow.AddHours(TimeZone ?? EssentialConstants.DefaultTimeZone);

    private int ToDistance(int lastOdometer) => Odometer - lastOdometer;
}