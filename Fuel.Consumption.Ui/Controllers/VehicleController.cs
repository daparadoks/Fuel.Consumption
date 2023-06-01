using Fuel.Consumption.Ui.Facades;
using Microsoft.AspNetCore.Mvc;

namespace Fuel.Consumption.Ui.Controllers;

public class VehicleController:BaseController
{
    private readonly IVehicleFacade _facade;

    public VehicleController(IVehicleFacade facade)
    {
        _facade = facade;
    }

    public IActionResult List()
    {
        return View();
    }

    [Route("{id}")]
    public async Task<IActionResult> Detail(string id) =>
        await GetViewResult(async () => await _facade.GetList(), "detail");
}