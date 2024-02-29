using Fuel.Consumption.Ui.Application;
using Fuel.Consumption.Ui.Facades;
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

    protected async Task<IActionResult> GetViewResult<T>(Func<Task<FacadeResponse<T>>> func, string viewName)
    {
        try
        {
            var result = await func();
            if (!string.IsNullOrEmpty(result.RedirectUrl))
                return Redirect(UrlHelper.ErrorPage(result.RedirectUrl));
            return View(viewName, result.Data);
        }
        catch (Exception e)
        {
            ViewBag.Error = new FacadeResponse(false, e.Message, 400);
            return Redirect(UrlHelper.ErrorPage());
        }
    }
}