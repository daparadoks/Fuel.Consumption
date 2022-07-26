using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IVehicleService
{
    Task Add(Vehicle vehicle);
    Task<Vehicle> GetByName(string name, Guid userId);
    Task<Vehicle> GetById(string vehicleId);
}

public class Vehicle
{
    public Vehicle(string name)
    {
        Name = name;
    }
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }
    public Guid UserId { get; set; }
}