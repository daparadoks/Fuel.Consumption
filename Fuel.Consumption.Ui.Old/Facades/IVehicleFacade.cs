using Fuel.Consumption.Ui.Responses;

namespace Fuel.Consumption.Ui.Facades;

public interface IVehicleFacade
{
    Task<FacadeResponse<IEnumerable<VehicleDto>>> GetList();
}