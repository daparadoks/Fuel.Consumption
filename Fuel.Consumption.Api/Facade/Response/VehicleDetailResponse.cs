using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Response;

public class VehicleDetailResponse
{
    public VehicleDetailResponse(Vehicle vehicle)
    {
        Id = vehicle.Id;
        Name = vehicle.Name;
        Brand = vehicle.Brand;
        ModelGroup = vehicle.ModelGroup;
        Model = vehicle.Model;
    }

    public string Id { get; }

    public string Name { get; }

    public string Brand { get; }

    public string ModelGroup { get; }

    public string Model { get; }
}