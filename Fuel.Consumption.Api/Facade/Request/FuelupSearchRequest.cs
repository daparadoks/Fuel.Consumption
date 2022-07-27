namespace Fuel.Consumption.Api.Facade.Request;

public class FuelUpSearchRequest
{
    public string VehicleId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}