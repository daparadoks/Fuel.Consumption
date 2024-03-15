using Fuel.Consumption.Api.Controllers.Request;
using Fuel.Consumption.Api.Facade.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class StatisticsController : BaseController
{
    private readonly IStatisticFacade _facade;

    public StatisticsController(IStatisticFacade facade)
    {
        _facade = facade;
    }

    [HttpPost("daily")]
    public async Task<JsonResult> GetDaily(StatisticRequest request) =>
        await GetJsonResult(_facade.GetDaily(request, ToUser(User)));

    [HttpPost("summary")]
    public async Task<JsonResult> GetSummary(StatisticRequest request) =>
        await GetJsonResult(_facade.GetSummary(request, ToUser(User)));
}