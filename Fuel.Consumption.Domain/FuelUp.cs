using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IFuelUpReadService
{
    Task<FuelUp> GetById(string id);
    Task<int> Count(string userId, string vehicleId, DateTime? startDate, DateTime? endDate);
    Task<IList<FuelUp>> Search(int skip, int take, string userId, string vehicleId, DateTime? startDate, DateTime? endDate);
    Task<IEnumerable<FuelUp>> GetByVehicleId(string vehicleId);
    Task<FuelUp> GetLastByVehicle(string vehicleId);
    Task<FuelUp> GetLastFullFuelUpByVehicle(string vehicleId);
    Task<IList<FuelUp>> GetByDateAndVehicleId(string vehicleId, DateTime startDate);
}

public interface IFuelUpWriteService{
    Task Add(FuelUp fuelUp);
    Task Update(FuelUp fuelUp);
    Task Delete(string id);
}

public class FuelUp
{
    public FuelUp()
    {

    }

    public FuelUp(string id,
        string vehicleId,
        int odometer,
        decimal amount,
        decimal price,
        int currency,
        bool complete,
        int cityPercentage,
        int fuelType,
        string userId,
        DateTime createdAt,
        DateTime fuelUpDate,
        DateTime updatedAt)
    {
        Id = id;
        VehicleId = vehicleId;
        Odometer = odometer;
        Amount = amount;
        Price = price;
        Currency = currency;
        Complete = complete;
        CityPercentage = cityPercentage;
        FuelType = fuelType;
        UserId = userId;
        CreatedAt = createdAt;
        FuelUpDate = fuelUpDate;
        UpdatedAt = updatedAt;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string VehicleId { get; set; }
    public int Odometer { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public int Currency { get; set; }
    public bool Complete { get; set; }
    public int CityPercentage { get; set; }
    public int FuelType { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime FuelUpDate { get; set; }
    public DateTime UpdatedAt { get; set; }
}