using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade;

public class ModelFacade : IModelFacade
{
    private readonly IModelService _modelService;

    public ModelFacade(IModelService modelService)
    {
        _modelService = modelService;
    }

    public async Task<IEnumerable<ModelResponse>> GetByModelGroupId(Guid modelGroupId)
    {
        var models = await _modelService.GetByModelGroupId(modelGroupId);
        return models.Select(x => new ModelResponse(x.Id.ToString(), x.Name));
    }
}