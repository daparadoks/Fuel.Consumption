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
    
    public async Task Add(FuelUp fuelUp) => await _collection.InsertOneAsync(fuelUp);
    public async Task Update(FuelUp fuelUp) => await _collection.ReplaceOneAsync(x => x.Id == fuelUp.Id, fuelUp);
    public async Task Delete(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
}