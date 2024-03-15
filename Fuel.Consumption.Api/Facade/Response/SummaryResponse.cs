namespace Fuel.Consumption.Api.Facade.Response;

public record SummaryResponse(
    int TotalDistance,
    decimal TotalFuel,
    decimal AverageConsumption,
    decimal? BestConsumption,
    decimal? HighestConsumption,
    decimal TotalSpent,
    decimal AveragePricePerKm,
    decimal AveragePricePerFuelUp,
    decimal CityPercentage,
    decimal AverageFuelPrice);