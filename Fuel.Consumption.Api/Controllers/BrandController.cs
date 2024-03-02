using Fuel.Consumption.Api.Facade.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BrandController : BaseController
{
    private readonly IBrandFacade _facade;

    public BrandController(IBrandFacade facade)
    {
        _facade = facade;
    }

    [HttpGet]
    public async Task<JsonResult> GetAll() => await GetJsonResult(_facade.GetSelectable());
}