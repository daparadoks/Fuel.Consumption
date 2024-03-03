using Fuel.Consumption.Api.Facade.Response;

namespace Fuel.Consumption.Api.Facade.Interface;

public interface IModelGroupFacade
{
    Task<IEnumerable<ModelGroupResponse>> GetByBrandId(string brandId);
}