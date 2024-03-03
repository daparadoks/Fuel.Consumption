using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade;

public class ModelGroupFacade : IModelGroupFacade
{
    private readonly IModelGroupReadService _modelGroupReadService;

    public ModelGroupFacade(IModelGroupReadService modelGroupReadService)
    {
        _modelGroupReadService = modelGroupReadService;
    }

    public async Task<IEnumerable<ModelGroupResponse>> GetByBrandId(string brandId)
    {
        var modelGroups = await _modelGroupReadService.GetByBrandId(brandId);
        return modelGroups.Select(x => new ModelGroupResponse(x.Id, x.Name));
    }
}