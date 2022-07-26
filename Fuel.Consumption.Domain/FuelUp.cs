using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IFuelUpService
{
    Task<FuelUp> GetById(string id);
    Task Add(FuelUp fuelUp);
}

public class FuelUp
{
    public FuelUp()
    {

    }

    public FuelUp(Guid vehicleId, int odometer, double amount, double price, int currency, bool complete,
        int cityPercentage, int fuelType, int fuelRate, string brand, Guid userId, DateTime createdAt, DateTime fuelUpDate)
    {
        VehicleId = vehicleId;
        Odometer = odometer;
        Amount = amount;
        Price = price;
        Currency = currency;
        Complete = complete;
        CityPercentage = cityPercentage;
        FuelType = fuelType;
        FuelRate = fuelRate;
        Brand = brand;
        UserId = userId;
        CreatedAt = createdAt;
        FuelUpDate = fuelUpDate;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public Guid VehicleId { get; set; }
    public int Odometer { get; set; }
    public double Amount { get; set; }
    public double Price { get; set; }
    public int Currency { get; set; }
    public bool Complete { get; set; }
    public int CityPercentage { get; set; }
    public int FuelType { get; set; }
    public int FuelRate { get; set; }
    public string Brand { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime FuelUpDate { get; set; }
}