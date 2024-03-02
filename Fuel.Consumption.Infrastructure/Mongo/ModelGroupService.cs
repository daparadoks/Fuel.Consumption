using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class ModelGroupService : Repository<ModelGroup>, IModelGroupService
{
    public ModelGroupService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task<IList<ModelGroup>> GetByBrandId(Guid brandId) =>
        await _collection.Find(x => x.BrandId == brandId.ToString() && x.IsActive).ToListAsync();
}