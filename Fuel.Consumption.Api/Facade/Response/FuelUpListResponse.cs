namespace Fuel.Consumption.Api.Facade.Response;

public record FuelUpListResponse(string Id, DateTime Date, int Distance, decimal? Consumption, decimal Price, int Percentage);