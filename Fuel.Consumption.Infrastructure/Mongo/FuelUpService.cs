using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class FuelUpReadService:Repository<FuelUp>, IFuelUpReadService
{
    public FuelUpReadService(IOptions<ApiConfig> options): base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task<FuelUp> GetById(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    

    public async Task<int> Count(string userId, string vehicleId, DateTime? startDate, DateTime? endDate) =>
        (int)await _collection.CountDocumentsAsync(x =>
            x.UserId == userId &&
            (string.IsNullOrEmpty(vehicleId) || x.VehicleId == vehicleId) &&
            (!startDate.HasValue || x.FuelUpDate >= startDate.Value) &&
            (!endDate.HasValue || x.FuelUpDate <= endDate));

    public async Task<IList<FuelUp>> Search(int skip, int take, string userId, string vehicleId, DateTime? startDate,
        DateTime? endDate) => await _collection.Find(x =>
            x.UserId == userId &&
            (string.IsNullOrEmpty(vehicleId) || x.VehicleId == vehicleId) &&
            (!startDate.HasValue || x.FuelUpDate >= startDate.Value) &&
            (!endDate.HasValue || x.FuelUpDate <= endDate))
        .SortByDescending(x => x.FuelUpDate)
        .Skip(skip)
        .Limit(take)
        .ToListAsync();

    public async Task<FuelUp> FindLastCompleted(string vehicleId) => await _collection
        .Find(x => x.Complete && x.VehicleId == vehicleId).SortByDescending(x => x.FuelUpDate)
        .FirstOrDefaultAsync();

    public async Task<IList<FuelUp>> FindAfter(string vehicleId, DateTime endDate) =>
        await _collection.Find(x => x.VehicleId == vehicleId && x.FuelUpDate > endDate)
            .ToListAsync();

    

    public async Task<IEnumerable<FuelUp>> GetByVehicleId(string vehicleId) =>
        await _collection.Find(x => x.VehicleId == vehicleId).ToListAsync();

    public async Task<FuelUp> GetLastByVehicle(string vehicleId) =>
        await _collection.Find(x => x.VehicleId == vehicleId)
            .SortByDescending(x => x.FuelUpDate)
            .FirstOrDefaultAsync();

}