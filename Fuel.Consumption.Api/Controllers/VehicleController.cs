using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Request;
using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Api.Controllers;

public class VehicleController:BaseController
{
    private readonly IVehicleFacade _facade;

    public VehicleController(IVehicleFacade facade)
    {
        _facade = facade;
    }

    [HttpPost]
    public async Task<JsonResult> Add(VehicleRequest request) => await GetJsonResult(_facade.Add(request, ToUser(User)));
}