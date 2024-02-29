using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fuel.Consumption.Ui.Models;

namespace Fuel.Consumption.Ui.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}