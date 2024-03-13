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

public class DailyStatistic(string userId, DateTime date, IEnumerable<DailyStatisticItem> items)
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string UserId { get; set; } = userId;
    public DateTime Date { get; set; } = date;
    public IEnumerable<DailyStatisticItem> Items { get; set; } = items;
}

public class DailyStatisticItem(
    Vehicle vehicle,
    decimal odometer,
    decimal distance,
    decimal fuelAmount,
    decimal consumption,
    decimal fuelPrice)
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public Vehicle Vehicle { get; set; } = vehicle;
    public decimal Odometer { get; set; } = odometer;
    public decimal Distance { get; set; } = distance;
    public decimal FuelAmount { get; set; } = fuelAmount;
    public decimal Consumption { get; set; } = consumption;
    public decimal FuelPrice { get; set; } = fuelPrice;
}