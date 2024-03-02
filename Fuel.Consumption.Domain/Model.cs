using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IModelService
{
    
}

public class Model
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public int FuelType { get; set; }
    public int ProductionStart { get; set; }
    public int? ProductionEnd { get; set; }
}