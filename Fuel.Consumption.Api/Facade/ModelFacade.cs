using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade;

public class ModelFacade : IModelFacade
{
    private readonly IModelReadService _modelReadService;

    public ModelFacade(IModelReadService modelReadService)
    {
        _modelReadService = modelReadService;
    }

    public async Task<IEnumerable<ModelResponse>> GetByModelGroupId(string modelGroupId)
    {
        var models = await _modelReadService.GetByModelGroupId(modelGroupId);
        return models.Select(x => new ModelResponse(x.Id, x.Name));
    }
}