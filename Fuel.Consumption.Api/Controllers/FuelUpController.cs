using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Fuel.Consumption.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FuelUpController:BaseController
{
    private readonly IFuelUpFacade _facade;

    public FuelUpController(IFuelUpFacade facade)
    {
        _facade = facade;
    }

    [HttpGet]
    public async Task<JsonResult> Get(string id) => await GetJsonResult(_facade.Get(id, ToUser(User)));

    [HttpGet("vehicle/{vehicleId}")]
    public async Task<JsonResult> GetByVehicle(string vehicleId, [FromQuery]SearchRequest request) =>
        await GetJsonResult(_facade.GetByVehicle(vehicleId, request, ToUser(User)));

    [HttpPost]
    public async Task<JsonResult> Add(FuelUpRequest request) => await GetJsonResult(_facade.Add(request, ToUser(User)));

    [HttpPut("{id}")]
    public async Task<JsonResult> Update(string id, FuelUpRequest request) =>
        await GetJsonResult(_facade.Update(id, request, ToUser(User)));

    [HttpDelete]
    public async Task<JsonResult> Delete(string id) => await GetJsonResult(_facade.Delete(id, ToUser(User)));

    [HttpPost("search")]
    public async Task<JsonResult> Search(SearchRequest<FuelUpSearchRequest> request) =>
        await GetJsonResult(_facade.Search(request, ToUser(User)));
}