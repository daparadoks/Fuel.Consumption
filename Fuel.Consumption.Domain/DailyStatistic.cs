using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;


public interface IDailyStatisticReadService
{
    
}

public interface IDailyStatisticWriteService
{
    Task DeleteByUserId(string userId);
    Task BulkAdd(IEnumerable<DailyStatistic> entities);
}

public class DailyStatistic
{
    public DailyStatistic(string userId,
        DateTime date,
        Vehicle vehicle,
        decimal odometer,
        decimal distance,
        decimal fuelAmount,
        decimal consumption,
        decimal fuelPrice)
    {
        UserId = userId;
        Date = date;
        Vehicle = vehicle;
        Odometer = odometer;
        Distance = distance;
        FuelAmount = fuelAmount;
        Consumption = consumption;
        FuelPrice = fuelPrice;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string UserId { get; set; }
    public DateTime Date { get; set; }
    public Vehicle Vehicle { get; set; }
    public decimal Odometer { get; set; }
    public decimal Distance { get; set; }
    public decimal FuelAmount { get; set; }
    public decimal Consumption { get; set; }
    public decimal FuelPrice { get; set; }
}