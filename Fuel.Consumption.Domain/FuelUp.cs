using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
// ReSharper disable PossibleLossOfFraction

namespace Fuel.Consumption.Domain;

public interface IFuelUpReadService
{
    Task<FuelUp> GetById(string id);
    Task<int> Count(string userId, string vehicleId, DateTime? startDate, DateTime? endDate);
    Task<IList<FuelUp>> Search(int skip, int take, string userId, string vehicleId, DateTime? startDate, DateTime? endDate);
    Task<IEnumerable<FuelUp>> SearchByVehicleId(string vehicleId, int skip, int take);
    Task<FuelUp> GetLastByVehicle(string vehicleId);
    Task<FuelUp> GetLastCompletedByVehicle(string vehicleId, DateTime endDate);
    Task<IList<FuelUp>> GetByStarDateAndVehicle(string vehicleId, DateTime startDate);
    Task<IList<FuelUp>> GetByUserId(string userId);
    Task<IList<FuelUp>> GetByDateRangeAndVehicle(string vehicleId, DateTime startDate, DateTime endDate);
    Task<FuelUp> GetNextCompletedByVehicle(string vehicleId, DateTime startDate);
    Task<long> CountByVehicle(string vehicleId);
    Task<FuelUp> GetPrevious(string vehicleId, DateTime endDate);
    Task<FuelUp> GetPreviousCompletedByVehicle(string vehicleId, DateTime endDate);
    Task<IEnumerable<FuelUp>> SearchForStatistic(IEnumerable<string> vehicleIds, string userId, DateTime? startDate, DateTime? endDate);
    Task<IEnumerable<FuelUp>> GetByVehicle(string vehicleId);
}

public interface IFuelUpWriteService{
    Task<FuelUp> Add(FuelUp entity);
    Task Update(FuelUp entity);
    Task Delete(string id);
    Task BulkAdd(IEnumerable<FuelUp> entities);
}

public class FuelUp
{
    public FuelUp()
    {

    }

    public FuelUp(int odometer,
        decimal amount,
        decimal price,
        decimal totalCost,
        int currency,
        bool complete,
        int cityPercentage,
        string userId,
        Vehicle vehicle,
        DateTime createdAt,
        DateTime fuelUpDate,
        DateTime updatedAt)
    {
        Odometer = odometer;
        Amount = amount;
        Price = price;
        TotalCost = totalCost;
        Currency = currency;
        Complete = complete;
        CityPercentage = cityPercentage;
        UserId = userId;
        CreatedAt = createdAt;
        FuelUpDate = fuelUpDate;
        UpdatedAt = updatedAt;
        Consumption = null;
        Vehicle = vehicle;
        VehicleId = vehicle.Id;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string VehicleId { get; set; }
    public int Odometer { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public decimal TotalCost { get; set; }
    public int Currency { get; set; }
    public bool Complete { get; set; }
    public int CityPercentage { get; set; }
    public string UserId { get; set; }
    public decimal? Consumption { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime FuelUpDate { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Vehicle Vehicle { get; set; }

    public void CalculateConsumption(List<FuelUp> previousFuelUps)
    {
        if (!previousFuelUps.Any())
            return;

        var totalDistance = Odometer - previousFuelUps.First().Odometer;
        var previousFuelAmount = previousFuelUps.Count > 1 ? previousFuelUps.Skip(1).Sum(x => x.Amount) : 0;
        var totalFuel = previousFuelAmount + Amount;

        Consumption = totalFuel / (totalDistance / 100);
    }
}