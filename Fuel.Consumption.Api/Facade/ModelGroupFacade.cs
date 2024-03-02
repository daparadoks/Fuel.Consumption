using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade;

public class ModelGroupFacade : IModelGroupFacade
{
    private readonly IModelGroupService _modelGroupService;

    public ModelGroupFacade(IModelGroupService modelGroupService)
    {
        _modelGroupService = modelGroupService;
    }

    public async Task<IEnumerable<ModelGroupResponse>> GetByBrandId(Guid brandId)
    {
        var modelGroups = await _modelGroupService.GetByBrandId(brandId);
        return modelGroups.Select(x => new ModelGroupResponse(x.Id, x.Name));
    }
}