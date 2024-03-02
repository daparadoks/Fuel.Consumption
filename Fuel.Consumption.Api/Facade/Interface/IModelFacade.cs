using Fuel.Consumption.Api.Facade.Response;

namespace Fuel.Consumption.Api.Facade.Interface;

public interface IModelFacade
{
    Task<IEnumerable<ModelResponse>> GetByModelGroupId(Guid modelGroupId);
}