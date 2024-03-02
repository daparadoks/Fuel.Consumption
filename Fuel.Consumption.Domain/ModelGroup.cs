using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IModelGroupService
{
    Task<IList<ModelGroup>> GetByBrandId(Guid brandId);
}

public class ModelGroup
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string BrandId { get; set; }
    public bool IsActive { get; set; }
    public Brand Brand { get; set; }
}