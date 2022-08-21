using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Ui.Controllers;

public class BaseController:Controller
{
    public void SetCookie(string name, string value, int hours = 12)
    {
        var options = new CookieOptions
        {
            Expires = DateTime.Now.AddHours(hours)
        };
        HttpContext.Response.Cookies.Append(name, value, options);
    }
    
    public void DeleteCookie(string name) => SetCookie(name, "", -60);
    public string GetBasicCookieValue(string name) => HttpContext.Request.Cookies[name];
}