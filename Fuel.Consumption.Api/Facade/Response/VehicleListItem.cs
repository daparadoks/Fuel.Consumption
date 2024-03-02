using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Response;

public class VehicleListItem
{
    public VehicleListItem(Vehicle vehicle)
    {
        Id = vehicle.Id;
        Name = vehicle.Name;
        Model =
            $"{vehicle.ToBrandName()} {vehicle.ToModelGroupName()} {ToModel(vehicle.ToModelGroupName(), vehicle.ToModelName())}";
    }

    public string Id { get; }

    public string Name { get; }

    public string Model { get; }

    private string ToModel(string modelGroup, string model) => model.Replace(modelGroup, "").Trim();
}