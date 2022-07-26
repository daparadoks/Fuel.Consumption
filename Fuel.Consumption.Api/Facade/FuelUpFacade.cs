using Fuel.Consumption.Api.Application;
using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Request;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade;

public class FuelUpFacade:IFuelUpFacade
{
    private readonly IFuelUpService _service;
    private readonly IVehicleService _vehicleService;

    public FuelUpFacade(IFuelUpService service, IVehicleService vehicleService)
    {
        _service = service;
        _vehicleService = vehicleService;
    }

    public async Task<FuelUpDetailResponse> Get(string id, User user)
    {
        var fuelUp = await _service.GetById(id);
        if (fuelUp == null)
            throw new NotFoundException("Yakıt bilgisi");
        if(fuelUp.UserId != user.Id)
            throw new NotFoundException("Yakıt bilgisi");

        var vehicle = await _vehicleService.GetById(fuelUp.VehicleId.ToString());
        return new FuelUpDetailResponse(fuelUp, vehicle);
    }

    public async Task Add(FuelUpRequest request, User user)
    {
        var vehicle = await _vehicleService.GetById(request.VehicleId.ToString());
        if (vehicle == null)
            throw new VehicleNotFoundException();
        
        await _service.Add(request.ToDomain(user.Id));
    }

    public async Task Update(string id, FuelUpRequest request, User user)
    {
        throw new NotImplementedException();
    }

    public async Task<SearchResponse<FuelUpSearchResponse>> Search(SearchRequest<FuelUpSearchRequest> request, User user)
    {
        throw new NotImplementedException();
    }
}