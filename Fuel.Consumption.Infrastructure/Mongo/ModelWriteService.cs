using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class ModelWriteService : Repository<Model>, IModelWriteService
{
    public ModelWriteService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task Add(Model entity) => await _collection.InsertOneAsync(entity);
    public async Task Update(Model entity) => await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
}