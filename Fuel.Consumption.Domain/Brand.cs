using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IBrandReadService
{
    Task<IList<Brand>> GetSelectable();
    Task<Brand> GetAnyByName(string name);
    Task<Brand> GetByName(string name);
}

public interface IBrandWriteService
{
    Task Add(Brand brand);
    Task Update(Brand entity);
}

public class Brand
{
    public Brand(string name, string logoUrl, bool isActive)
    {
        Name = name;
        LogoUrl = logoUrl;
        IsActive = isActive;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public bool IsActive { get; set; }

    public void SetId(string id) => Id = id;
}