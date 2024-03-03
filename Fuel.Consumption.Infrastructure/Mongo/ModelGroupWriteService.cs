using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class ModelGroupWriteService : Repository<ModelGroup>, IModelGroupWriteService
{
    public ModelGroupWriteService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task Add(ModelGroup entity) => await _collection.InsertOneAsync(entity);
    public async Task Update(ModelGroup entity) => await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
}