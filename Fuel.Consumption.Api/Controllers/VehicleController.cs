using Fuel.Consumption.Api.Controllers.Request;
using Fuel.Consumption.Api.Facade.Interface;
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

    [HttpPut("{id}")]
    public async Task<JsonResult> Update(string id, VehicleRequest request) =>
        await GetJsonResult(_facade.Update(id, request, ToUser(User)));
}