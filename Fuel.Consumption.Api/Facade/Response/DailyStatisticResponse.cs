namespace Fuel.Consumption.Api.Facade.Response;

public record DailyStatisticResponse(DateTime Date, decimal Odometer, decimal Distance, decimal FuelAmount, decimal Consumption, decimal FuelPrice);