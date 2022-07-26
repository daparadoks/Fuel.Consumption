using Fuel.Consumption.Api.Application;
using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Request;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade;

public class VehicleFacade:IVehicleFacade
{
    private readonly IVehicleService _service;

    public VehicleFacade(IVehicleService service)
    {
        _service = service;
    }

    public async Task Add(VehicleRequest request, User user)
    {
        var exists = await _service.GetByName(request.Name, user.Id);
        if (exists != null)
            throw new ContentExistsException("Araç");
        
        await _service.Add(request.ToDomain());
    }
}