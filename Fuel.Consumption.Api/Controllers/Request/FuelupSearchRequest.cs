namespace Fuel.Consumption.Api.Controllers.Request;

public class FuelUpSearchRequest
{
    public string VehicleId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}