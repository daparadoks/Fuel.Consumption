using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade;

public class BrandFacade : IBrandFacade
{
    private readonly IBrandService _brandService;

    public BrandFacade(IBrandService brandService)
    {
        _brandService = brandService;
    }

    public async Task<IEnumerable<BrandResponse>> GetSelectable()
    {
        var brands = await _brandService.GetSelectable();
    }
}