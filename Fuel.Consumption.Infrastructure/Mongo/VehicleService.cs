using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class VehicleService:Repository<Vehicle>, IVehicleService
{
    public VehicleService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
        
    }

    public async Task Add(Vehicle vehicle) => await _collection.InsertOneAsync(vehicle);

    public async Task<Vehicle> GetByName(string name, string userId) =>
        await _collection.Find(x => x.Name == name && x.UserId == userId).FirstOrDefaultAsync();

    public async Task<Vehicle> GetById(string vehicleId) =>
        await _collection.Find(x => x.Id == vehicleId).FirstOrDefaultAsync();

    public async Task<IEnumerable<Vehicle>> GetByUserId(string userId) =>
        await _collection.Find(x => x.UserId == userId).ToListAsync();

    public async Task<IEnumerable<Vehicle>> GetAll() =>
        await _collection.Find(x => true).ToListAsync();

    public async Task Update(Vehicle entity) => await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

    public async Task<IEnumerable<Vehicle>> GetByIds(IList<string> ids, string userId) =>
        await _collection.Find(x => ids.Contains(x.Id) && x.UserId == userId).ToListAsync();
}