using Fuel.Consumption.Api.Facade.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ModelController : BaseController
{
    private readonly IModelFacade _facade;

    public ModelController(IModelFacade facade)
    {
        _facade = facade;
    }

    [HttpGet("{modelGroupId}")]
    public async Task<JsonResult> GetModelByModelGroupId(Guid modelGroupId) =>
        await GetJsonResult(_facade.GetByModelGroupId(modelGroupId));
}