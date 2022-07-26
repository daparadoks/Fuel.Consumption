using Fuel.Consumption.Api.Facade.Request;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Interface;

public interface IVehicleFacade
{
    Task Add(VehicleRequest request, User toUser);
}