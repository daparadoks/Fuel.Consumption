using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IModelService
{
    Task<IList<Model>> GetByModelGroupId(Guid modelGroupId);
    Task<Model> GetByModelId(Guid modelId);
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
    public string ModelGroupId { get; set; }
    public string BrandId { get; set; }
    public bool IsActive { get; set; }
    public ModelGroup ModelGroup { get; set; }
}