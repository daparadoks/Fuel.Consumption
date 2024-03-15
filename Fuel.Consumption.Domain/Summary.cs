namespace Fuel.Consumption.Domain;

public class Summary
{
    public Summary(IList<FuelUp> fuelUps, IList<Vehicle> vehicles, User user)
    {
        Success = false;
        var firstFuelUp = fuelUps.MinBy(x => x.Odometer);
        var lastFuelUp = fuelUps.MaxBy(x => x.Odometer);

        if (firstFuelUp == null || lastFuelUp == null)
            return;

        TotalDistance = lastFuelUp.Odometer - firstFuelUp.Odometer;
        TotalFuel = fuelUps.Sum(x => x.Amount);
        AverageConsumption = fuelUps.Average(x => x.Consumption) ?? 0;
        BestConsumption = fuelUps.Where(x => x.Complete).MinBy(x => x.Consumption)?.Consumption;
        HighestConsumption = fuelUps.Where(x => x.Complete).MaxBy(x => x.Consumption)?.Consumption;
        TotalSpent = fuelUps.Sum(x => x.TotalCost);
        AverageFuelPrice = fuelUps.Average(x => x.Price);
        AveragePricePerFuelUp = fuelUps.Average(x => x.TotalCost);
        CityPercentage = (decimal)fuelUps.Average(x => x.CityPercentage);

        Success = true;
    }

    public bool Success { get; }
    public int TotalDistance { get; }
    public decimal TotalFuel { get; }
    public decimal AverageConsumption { get; }
    public decimal? BestConsumption { get; }
    public decimal? HighestConsumption { get; }
    public decimal TotalSpent { get; }
    public decimal AveragePricePerFuelUp { get; }
    public decimal CityPercentage { get; }
    public decimal AverageFuelPrice { get; }
    public decimal AveragePricePerKm => TotalDistance == 0 ? 0 : TotalSpent / TotalDistance;
}