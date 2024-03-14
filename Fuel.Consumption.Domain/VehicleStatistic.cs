namespace Fuel.Consumption.Domain;

public class VehicleStatistic
{
    public VehicleStatistic(IList<FuelUp> fuelUps, IEnumerable<Vehicle> vehicles, string userId)
    {
        FuelUpsByVehicle = new List<FuelUpsByVehicle>();
        UserId = userId;
        Vehicles = vehicles.ToList();
        DailyStatistics = new List<DailyStatistic>();

        var fuelUpsByVehicleId = fuelUps.GroupBy(x => x.VehicleId);
        foreach (var fuelUpByVehicleId in fuelUpsByVehicleId)
            FuelUpsByVehicle.Add(new FuelUpsByVehicle(fuelUpByVehicleId.Key, fuelUpByVehicleId.ToList()));
    }

    public IEnumerable<DailyStatistic> GetDailyStatistics()
    {
        Calculate();
        return DailyStatistics;
    }


    private void Calculate()
    {
        foreach (var fuelUpByVehicle in FuelUpsByVehicle)
        {
            var vehicle = Vehicles.FirstOrDefault(x => x.Id == fuelUpByVehicle.VehicleId);
            if (vehicle == null)
                continue;
            
            var fuelUps = fuelUpByVehicle.FuelUps.OrderBy(x => x.FuelUpDate).ToList();
            if (!fuelUps.Any() || fuelUps.Count == 1)
                continue;
            
            var previousFuelUp = fuelUps.First();
            foreach (var fuelUp in fuelUps.Skip(1))
            {
                var startDate = new DateTime(previousFuelUp.FuelUpDate.Year,
                    previousFuelUp.FuelUpDate.Month,
                    previousFuelUp.FuelUpDate.Day);
                var endDate = new DateTime(fuelUp.FuelUpDate.Year, fuelUp.FuelUpDate.Month, fuelUp.FuelUpDate.Day);
                var totalDays = (int)(endDate - startDate).TotalDays;
                var totalDistance = fuelUp.Odometer - previousFuelUp.Odometer;
                var dailyDistance = Math.Round((decimal)totalDistance / totalDays, MidpointRounding.ToZero);
                var averageDistance = totalDistance / totalDays;
                var totalFuel = fuelUp.Amount;
                var averageFuel = totalFuel/totalDays;

                var day = 1;
                while (startDate.AddDays(day) <= endDate)
                {
                    var theDate = startDate.AddDays(day);
                    var estimatedDistance = dailyDistance * day;
                    var estimatedOdometer =
                        theDate == endDate ? fuelUp.Odometer : previousFuelUp.Odometer + estimatedDistance;
                    var statistic = new DailyStatistic(UserId,
                        startDate,
                        vehicle,
                        estimatedOdometer,
                        averageDistance,
                        averageFuel,
                        fuelUp.Consumption,
                        fuelUp.Price);

                    DailyStatistics.Add(statistic);
                    
                    day++;
                }
            }
        }
    }

    private string UserId { get; }
    private IList<Vehicle> Vehicles { get; }
    private IList<DailyStatistic> DailyStatistics { get; }
    private IList<FuelUpsByVehicle> FuelUpsByVehicle { get; }
}

internal class FuelUpsByVehicle
{
    public FuelUpsByVehicle(string vehicleId, IList<FuelUp> fuelUps)
    {
        VehicleId = vehicleId;
        var containers = new List<FuelUpContainer>();

        var subFuelUps = new List<FuelUp>();
        foreach (var fuelUp in fuelUps)
        {
            subFuelUps.Add(fuelUp);

            if (!fuelUp.Complete)
                continue;

            containers.Add(new FuelUpContainer(subFuelUps));
            subFuelUps = new List<FuelUp>();
        }
        FuelUps = containers;
    }

    public string VehicleId { get; }
    public IList<FuelUpContainer> FuelUps { get; }
}

internal class FuelUpContainer
{
    public FuelUpContainer(IList<FuelUp> fuelUps)
    {
        FuelUps = fuelUps;
    }

    public int Odometer => LastOne.Odometer;
    public decimal Amount => FuelUps.Sum(x => x.Amount);
    public decimal Price => FuelUps.Average(x => x.Price);
    public decimal TotalCost => FuelUps.Sum(x => x.Price);
    public DateTime FuelUpDate => LastOne.FuelUpDate;
    public decimal Consumption => FuelUps.Average(x => x.Consumption) ?? 0;
    private FuelUp LastOne => FuelUps.MaxBy(x => x.Odometer);
    
    public IList<FuelUp> FuelUps { get; }
    
}