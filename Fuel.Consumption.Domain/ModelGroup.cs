using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IModelGroupReadService
{
    Task<IList<ModelGroup>> GetByBrandId(string brandId);
    Task<ModelGroup> GetAnyByName(string name);
    Task<ModelGroup> GetByName(string name);
}

public interface IModelGroupWriteService
{
    Task Add(ModelGroup entity);
    Task Update(ModelGroup entity);
}

public class ModelGroup
{
    public ModelGroup(string name, bool isActive, Brand brand)
    {
        Name = name;
        IsActive = isActive;
        Brand = brand;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public Brand Brand { get; set; }

    public void SetId(string id) => Id = id;
}