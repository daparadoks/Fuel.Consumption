using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class BrandWriteService : Repository<Brand>, IBrandWriteService
{
    public BrandWriteService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task Add(Brand brand) => await _collection.InsertOneAsync(brand);
    public async Task Update(Brand entity) => await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
}