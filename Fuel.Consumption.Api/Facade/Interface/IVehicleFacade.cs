using Fuel.Consumption.Api.Controllers.Request;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Interface;

public interface IVehicleFacade
{
    Task Add(VehicleRequest request, User toUser);
    Task<IEnumerable<VehicleListItem>> GetAll(User user);
    Task<VehicleDetailResponse> Get(string id, User user);
    Task Update(string vehicleId, VehicleRequest request, User user);
}