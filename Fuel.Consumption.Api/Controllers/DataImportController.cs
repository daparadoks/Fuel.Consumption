using Fuel.Consumption.Api.Controllers.Request;
using Fuel.Consumption.Api.Facade.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DataImportController : BaseController
{
    private readonly IImportDataFacade _facade;

    public DataImportController(IImportDataFacade facade)
    {
        _facade = facade;
    }

    [HttpPost]
    public async Task<JsonResult> ImportData([FromForm]ImportDataRequest request) =>
        await GetJsonResult(_facade.ImportData(request, ToUser(User)));
}