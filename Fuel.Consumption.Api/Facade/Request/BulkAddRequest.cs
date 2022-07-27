namespace Fuel.Consumption.Api.Facade.Request;

public class BulkAddRequest
{
    public BulkAddRequest(IEnumerable<BulkAddItem> items, int currency, int fuelType, int fuelRate, int? timeZone)
    {
        Items = items;
        Currency = currency;
        FuelType = fuelType;
        FuelRate = fuelRate;
        TimeZone = timeZone;
    }
    public IEnumerable<BulkAddItem> Items { get; }
    public int Currency { get; }
    public int FuelType { get; }
    public int FuelRate { get; }
    public int? TimeZone { get; }
}