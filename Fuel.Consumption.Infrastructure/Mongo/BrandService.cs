using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class BrandService : Repository<Brand>, IBrandService
{
    public BrandService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task<IList<Brand>> GetSelectable()
    {
        var brands = await _collection.FindAsync(x)
    }
}