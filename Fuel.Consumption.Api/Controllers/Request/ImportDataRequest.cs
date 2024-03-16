namespace Fuel.Consumption.Api.Controllers.Request;

public class ImportDataRequest
{
    public string VehicleId { get; set; }
    public IFormFile File { get; set; }

    public byte[] ToStream()
    {
        using var memoryStream = new MemoryStream();
        File.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}