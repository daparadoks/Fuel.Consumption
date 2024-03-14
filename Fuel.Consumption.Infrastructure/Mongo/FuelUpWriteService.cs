using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class FuelUpWriteService : Repository<FuelUp>, IFuelUpWriteService
{
    public FuelUpWriteService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task<FuelUp> Add(FuelUp entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }
    public async Task Update(FuelUp entity) => await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    public async Task Delete(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
}