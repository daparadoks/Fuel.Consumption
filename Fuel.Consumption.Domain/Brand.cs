using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IBrandService
{
    Task<IList<Brand>> GetSelectable();
}

public class Brand
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public bool IsActive { get; set; }
}