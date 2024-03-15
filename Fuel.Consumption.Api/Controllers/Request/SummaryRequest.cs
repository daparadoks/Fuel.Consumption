namespace Fuel.Consumption.Api.Controllers.Request;

public class SummaryRequest
{
    public IList<string> VehicleIds { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public bool GetAll() => !StartDate.HasValue && !EndDate.HasValue;
    public bool AllVehicles() => !VehicleIds.Any();
}