using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class ModelGroupReadService : Repository<ModelGroup>, IModelGroupReadService
{
    public ModelGroupReadService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task<IList<ModelGroup>> GetByBrandId(string brandId) =>
        await _collection.Find(x => x.Brand.Id == brandId && x.IsActive).ToListAsync();

    public async Task<ModelGroup> GetAnyByName(string name) =>
        await _collection.Find(x => x.Name == name).FirstOrDefaultAsync();

    public async Task<ModelGroup> GetByName(string name) =>
        await _collection.Find(x => x.Name == name && x.IsActive).FirstOrDefaultAsync();
}