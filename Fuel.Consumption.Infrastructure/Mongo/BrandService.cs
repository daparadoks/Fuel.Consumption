using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class BrandService : Repository<Brand>, IBrandService
{
    public BrandService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task<IList<Brand>> GetSelectable() => await _collection.Find(x => x.IsActive).ToListAsync();
}