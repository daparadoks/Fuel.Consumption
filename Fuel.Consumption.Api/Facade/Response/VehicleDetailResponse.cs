using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Response;

public class VehicleDetailResponse
{
    public VehicleDetailResponse(Vehicle vehicle)
    {
        Id = vehicle.Id;
        Name = vehicle.Name;
        Brand = vehicle.ToBrandName();
        ModelGroup = vehicle.ToModelGroupName();
        Model = vehicle.ToModelName();
    }

    public string Id { get; }

    public string Name { get; }

    public string Brand { get; }

    public string ModelGroup { get; }

    public string Model { get; }
}