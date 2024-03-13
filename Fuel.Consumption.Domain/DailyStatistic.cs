using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;


public interface IDailyStatisticReadService
{
    
}

public interface IDailyStatisticWriteService
{
    
}

public class DailyStatistic(string id, string userId, DateTime date, IEnumerable<DailyStatisticItem> items)
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = id;

    public string UserId { get; set; } = userId;
    public DateTime Date { get; set; } = date;
    public IEnumerable<DailyStatisticItem> Items { get; set; } = items;
}

public abstract class DailyStatisticItem(
    string id,
    Vehicle vehicle,
    decimal distance,
    decimal fuelAmount,
    decimal consumption,
    decimal fuelPrice)
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = id;

    public Vehicle Vehicle { get; set; } = vehicle;
    public decimal Distance { get; set; } = distance;
    public decimal FuelAmount { get; set; } = fuelAmount;
    public decimal Consumption { get; set; } = consumption;
    public decimal FuelPrice { get; set; } = fuelPrice;
}