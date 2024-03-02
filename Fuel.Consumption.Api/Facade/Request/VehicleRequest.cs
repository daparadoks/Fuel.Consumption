using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Request;

public class VehicleRequest
{
    public string Name { get; set; }
    public string Brand { get; set; }
    public string ModelGroup { get; set; }
    public string Model { get; set; }
    public Vehicle ToDomain(string userId) => new Vehicle(Name, userId);
}