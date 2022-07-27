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
}