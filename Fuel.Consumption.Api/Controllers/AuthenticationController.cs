using Fuel.Consumption.Api.Controllers.Request;
using Fuel.Consumption.Api.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Api.Controllers;

public class AuthenticationController:BaseController
{
    private readonly IUserFacade _userFacade;

    public AuthenticationController(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [HttpGet]
    public async Task<JsonResult> Login(string username, string password) =>
        await GetJsonResult(_userFacade.Login(new LoginRequest(username, password)));

    [HttpPost("register")]
    public async Task<JsonResult> Register(RegisterRequest request) =>
        await GetJsonResult(_userFacade.Register(request));
}