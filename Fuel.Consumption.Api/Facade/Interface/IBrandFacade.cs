using Fuel.Consumption.Api.Facade.Response;

namespace Fuel.Consumption.Api.Facade.Interface;

public interface IBrandFacade
{
    Task<IEnumerable<BrandResponse>> GetSelectable();
}