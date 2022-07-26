using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Request;
using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Api.Controllers;

public class FuelUpController:BaseController
{
    private readonly IFuelUpFacade _facade;

    public FuelUpController(IFuelUpFacade facade)
    {
        _facade = facade;
    }

    [HttpGet("/{id}")]
    public async Task<JsonResult> Get(string id) => await GetJsonResult(_facade.Get(id, ToUser(User)));

    [HttpPost]
    public async Task<JsonResult> Add(FuelUpRequest request) => await GetJsonResult(_facade.Add(request, ToUser(User)));

    [HttpPut("/id")]
    public async Task<JsonResult> Update(string id, FuelUpRequest request) =>
        await GetJsonResult(_facade.Update(id, request, ToUser(User)));

    [HttpPost("search")]
    public async Task<JsonResult> Search(SearchRequest<FuelUpSearchRequest> request) =>
        await GetJsonResult(_facade.Search(request, ToUser(User)));
}