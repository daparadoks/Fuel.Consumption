namespace Fuel.Consumption.Api.Facade.Request;

public class FuelUpSearchRequest
{
   public IEnumerable<string> Vehicles { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}