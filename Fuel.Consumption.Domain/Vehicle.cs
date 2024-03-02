using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IVehicleService
{
    Task Add(Vehicle vehicle);
    Task<Vehicle> GetByName(string name, string userId);
    Task<Vehicle> GetById(string vehicleId);
    Task<IEnumerable<Vehicle>> GetByUserId(string userId);
    Task<IEnumerable<Vehicle>> GetAll();
}

public class Vehicle
{
    public Vehicle(string name, string userId)
    {
        Name = name;
        UserId = userId;
    }
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public string Brand { get; set; }
    public string ModelGroup { get; set; }
    public string Model { get; set; }
    public int FuelTypeId { get; set; }
}