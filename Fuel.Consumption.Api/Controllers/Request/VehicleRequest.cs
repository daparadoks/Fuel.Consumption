using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Controllers.Request;

public class VehicleRequest
{
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public string ModelId { get; set; }
    public FuelType FuelType { get; set; }

    public Vehicle ToDomain(string userId, Model model, string vehicleId = "") => new(Name, userId, model, ImagePath, FuelType, vehicleId);
}