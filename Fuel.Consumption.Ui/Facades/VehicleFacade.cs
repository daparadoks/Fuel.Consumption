using Fuel.Consumption.Domain;
using Fuel.Consumption.Ui.Responses;

namespace Fuel.Consumption.Ui.Facades;

public class VehicleFacade:IVehicleFacade
{
    private readonly IVehicleService _service;

    public VehicleFacade(IVehicleService service)
    {
        _service = service;
    }

    public async Task<FacadeResponse<IEnumerable<VehicleDto>>> GetList()
    {
        var vehicles = await _service.GetByUserId("userId");
        return new FacadeResponse<IEnumerable<VehicleDto>>(vehicles.Select(x => new VehicleDto(x)));
    }
}