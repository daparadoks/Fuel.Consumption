using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class ModelService : Repository<Model>, IModelService
{
    public ModelService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task<IList<Model>> GetByModelGroupId(Guid modelGroupId) => await _collection
        .Find(x => x.IsActive && x.ModelGroupId == modelGroupId.ToString()).ToListAsync();

    public async Task<Model> GetByModelId(Guid modelId) =>
        await _collection.Find(x => x.Id == modelId.ToString() && x.IsActive).FirstOrDefaultAsync();
}