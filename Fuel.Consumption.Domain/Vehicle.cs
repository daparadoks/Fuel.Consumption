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
    Task Update(Vehicle entity);
    Task<IEnumerable<Vehicle>> GetByIds(IList<string> ids, string userId);
}

public class Vehicle
{
    public Vehicle(string name, string userId, Model model, string imagePath, FuelType fuelType, string vehicleId)
    {
        if (!string.IsNullOrEmpty(vehicleId))
            Id = vehicleId;
        
        Name = name;
        UserId = userId;
        ModelId = model.Id;
        Model = model;
        ImagePath = imagePath;
        FuelType = fuelType;
    }
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public string ModelId { get; set; }
    public string ImagePath { get; set; }
    public FuelType FuelType { get; set; }
    public Model Model { get; set; }

    public string ToBrandName() => Model.ModelGroup.Brand.Name;
    public string ToModelGroupName() => Model.ModelGroup.Name;
    public string ToModelName() => Model.Name;
}