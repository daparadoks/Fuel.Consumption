using Fuel.Consumption.Api.Facade.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ModelGroupController : BaseController
{
    private readonly IModelGroupFacade _facade;

    public ModelGroupController(IModelGroupFacade facade)
    {
        _facade = facade;
    }

    [HttpGet("{brandId}")]
    public async Task<JsonResult> GetByBrandId(Guid brandId) => await GetJsonResult(_facade.GetByBrandId(brandId));
}