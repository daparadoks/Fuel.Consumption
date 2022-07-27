using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IFuelUpService
{
    Task<FuelUp> GetById(string id);
    Task Add(FuelUp fuelUp);
    Task<int> Count(string userId, string vehicleId, DateTime? startDate, DateTime? endDate);
    Task<IList<FuelUp>> Search(int skip, int take, string userId, string vehicleId, DateTime? startDate, DateTime? endDate);
    Task<FuelUp> FindLastCompleted(string vehicleId);
    Task<IList<FuelUp>> FindAfter(string vehicleId, DateTime endDate);
    Task Delete(string id);
}

public class FuelUp
{
    public FuelUp()
    {

    }

    public FuelUp(string vehicleId, int odometer, int distance, double amount, double consumption, double price, int currency, bool complete,
        int cityPercentage, int fuelType, int fuelRate, string brand, string userId, DateTime createdAt, DateTime fuelUpDate)
    {
        VehicleId = vehicleId;
        Odometer = odometer;
        Distance = distance;
        Amount = amount;
        Consumption = consumption;
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

    public string VehicleId { get; set; }
    public int Odometer { get; set; }
    public int Distance { get; set; }
    public double Amount { get; set; }
    public double Consumption { get; set; }
    public double Price { get; set; }
    public int Currency { get; set; }
    public bool Complete { get; set; }
    public int CityPercentage { get; set; }
    public int FuelType { get; set; }
    public int FuelRate { get; set; }
    public string Brand { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime FuelUpDate { get; set; }
}