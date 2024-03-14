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
        ModelId = vehicle.Model.Id;
        FuelTypeId = vehicle.FuelType;
        FuelType = vehicle.FuelType.ToString();
    }

    public string Id { get; }
    public string Name { get; }
    public string Brand { get; }
    public string ModelGroup { get; }
    public string Model { get; }
    public string ModelId { get; set; }
    public FuelType FuelTypeId { get; set; }
    public string FuelType { get; set; }
    
}