using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class ModelService : Repository<Model>, IModelService
{
    public ModelService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }
}