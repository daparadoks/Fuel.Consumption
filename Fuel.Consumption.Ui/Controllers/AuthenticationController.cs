using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Ui.Controllers;

public class AuthenticationController:BaseController
{
    [Route("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [Route("login")]
    public JsonResult SetToken(string token)
    {
        SetCookie("token", token);
        return Json(new { Success = true });
    }

    [HttpPost]
    [Route("logout")]
    public JsonResult Logout()
    {
        DeleteCookie("token");
        return Json(new { Success = true });
    }
}