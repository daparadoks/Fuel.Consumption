using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class ModelReadService : Repository<Model>, IModelReadService
{
    public ModelReadService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task<IList<Model>> GetByModelGroupId(string modelGroupId) => await _collection
        .Find(x => x.IsActive && x.ModelGroup.Id == modelGroupId).ToListAsync();

    public async Task<Model> GetByModelId(string modelId) =>
        await _collection.Find(x => x.Id == modelId && x.IsActive).FirstOrDefaultAsync();

    public async Task<Model> GetAnyByName(string name) =>
        await _collection.Find(x => x.Name == name).FirstOrDefaultAsync();
}