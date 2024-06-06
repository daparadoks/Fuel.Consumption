namespace Fuel.Consumption.Api.Controllers.Request;

public class ImportDataRequest
{
    public string VehicleId { get; set; }
    public IFormFile File { get; set; }

    public Stream ToStreamV2() => File.OpenReadStream();
}