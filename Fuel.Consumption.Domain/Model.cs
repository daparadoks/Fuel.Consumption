using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IModelReadService
{
    Task<IList<Model>> GetByModelGroupId(string modelGroupId);
    Task<Model> GetByModelId(string modelId);
    Task<Model> GetAnyByName(string name);
}

public interface IModelWriteService
{
    Task Add(Model entity);
    Task Update(Model entity);
}

public class Model
{
    public Model(string name, int fuelType, int productionStart, int? productionEnd, bool isActive, ModelGroup modelGroup)
    {
        Name = name;
        FuelType = fuelType;
        ProductionStart = productionStart;
        ProductionEnd = productionEnd;
        IsActive = isActive;
        ModelGroup = modelGroup;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public int FuelType { get; set; }
    public int ProductionStart { get; set; }
    public int? ProductionEnd { get; set; }
    public bool IsActive { get; set; }
    public ModelGroup ModelGroup { get; set; }

    public void SetId(string id) => Id = id;
}