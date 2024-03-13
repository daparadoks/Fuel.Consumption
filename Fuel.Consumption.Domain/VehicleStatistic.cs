namespace Fuel.Consumption.Domain;

public class VehicleStatistic
{
    public VehicleStatistic(IList<FuelUp> fuelUps, IEnumerable<Vehicle> vehicles, string userId)
    {
        FuelUpsByVehicle = new List<FuelUpsByVehicle>();
        UserId = userId;
        Vehicles = vehicles.ToList();

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
            if (!fuelUps.Any())
                continue;
            var previousFuelUp = fuelUps.First();
            foreach (var fuelUp in fuelUps)
            {
                var startDate = new DateTime(previousFuelUp.FuelUpDate.Year,
                    previousFuelUp.FuelUpDate.Month,
                    previousFuelUp.FuelUpDate.Day);
                var endDate = new DateTime(fuelUp.FuelUpDate.Year, fuelUp.FuelUpDate.Month, fuelUp.FuelUpDate.Day);
                var totalDays = (int)(startDate - endDate).TotalDays;
                var totalDistance = fuelUp.Odometer - previousFuelUp.Odometer;
                var dailyDistance = Math.Round((decimal)totalDistance / totalDays, MidpointRounding.ToZero);
                var averageConsumption = 0;
                var averageDistance = 0;
                var averageFuel = 0;

                var day = 1;
                while (startDate.AddDays(day) <= endDate)
                {
                    var theDate = startDate.AddDays(day);
                    var estimatedDistance = dailyDistance * day;
                    var estimatedOdometer =
                        theDate == endDate ? fuelUp.Odometer : previousFuelUp.Odometer + estimatedDistance;
                    var items = new List<DailyStatisticItem>
                    {
                        new(vehicle,
                            estimatedOdometer,
                            averageDistance,
                            averageFuel,
                            averageConsumption,
                            fuelUp.Price)
                    };
                    var statistic = new DailyStatistic(UserId, startDate, items);
                }
            }
        }
    }

    private string UserId { get; }
    private IList<Vehicle> Vehicles { get; }
    private IEnumerable<DailyStatistic> DailyStatistics { get; }
    private IList<FuelUpsByVehicle> FuelUpsByVehicle { get; }
}

internal class FuelUpsByVehicle
{
    public FuelUpsByVehicle(string vehicleId, IList<FuelUp> fuelUps)
    {
        VehicleId = vehicleId;
        FuelUps = fuelUps;
    }

    public string VehicleId { get; }
    public IList<FuelUp> FuelUps { get; }
}