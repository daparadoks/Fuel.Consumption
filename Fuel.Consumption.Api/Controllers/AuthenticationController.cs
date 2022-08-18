using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Request;
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

    [HttpPost]
    public async Task<JsonResult> Register(RegisterRequest request) =>
        await GetJsonResult(_userFacade.Register(request));
}