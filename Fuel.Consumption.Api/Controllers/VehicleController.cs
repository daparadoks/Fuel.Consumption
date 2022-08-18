using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class VehicleController:BaseController
{
    private readonly IVehicleFacade _facade;

    public VehicleController(IVehicleFacade facade)
    {
        _facade = facade;
    }

    [HttpGet]
    public async Task<JsonResult> GetAll() => await GetJsonResult(_facade.GetAll(ToUser(User)));

    [HttpGet("{id}")]
    public async Task<JsonResult> Get(string id) => await GetJsonResult(_facade.Get(id, ToUser(User)));
    [HttpPost]
    public async Task<JsonResult> Add(VehicleRequest request) => await GetJsonResult(_facade.Add(request, ToUser(User)));
}