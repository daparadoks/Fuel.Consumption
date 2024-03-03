using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class BrandReadService : Repository<Brand>, IBrandReadService
{
    public BrandReadService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task<IList<Brand>> GetSelectable() => await _collection.Find(x => x.IsActive).ToListAsync();

    public async Task<Brand> GetAnyByName(string name) =>
        await _collection.Find(x => x.Name == name).FirstOrDefaultAsync();
    
    public async Task<Brand> GetByName(string name) =>
        await _collection.Find(x => x.Name == name && x.IsActive).FirstOrDefaultAsync();
}