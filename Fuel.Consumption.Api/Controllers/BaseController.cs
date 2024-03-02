using System.Security.Claims;
using Fuel.Consumption.Api.Application;
using Fuel.Consumption.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseController:ControllerBase
{
    private const string ContentType = "application/json";

    protected static async Task<JsonResult> GetJsonResult<T>(Task<T> task) =>
        new(new Response<T>(await task)) { ContentType = ContentType };

    protected static async Task<JsonResult> GetJsonResult(Task task)
    {
        await task;
        return new JsonResult(new Response(true)) { ContentType = ContentType };
    }

    protected static User ToUser(ClaimsPrincipal user) =>
        new("4ddeea2a-80d7-441a-8103-99267a9da4a3", "paradox");
}