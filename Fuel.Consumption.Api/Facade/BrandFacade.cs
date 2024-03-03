using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade;

public class BrandFacade : IBrandFacade
{
    private readonly IBrandReadService _brandReadService;

    public BrandFacade(IBrandReadService brandReadService)
    {
        _brandReadService = brandReadService;
    }

    public async Task<IEnumerable<BrandResponse>> GetSelectable()
    {
        var brands = await _brandReadService.GetSelectable();
        return brands.Select(b => new BrandResponse(b.Id, b.Name, b.LogoUrl));
    }
}