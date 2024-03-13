namespace Fuel.Consumption.Domain;

public class VehicleStatistic
{
    public VehicleStatistic(IList<FuelUp> fuelUps)
    {
        
    }

    public IEnumerable<DailyStatistic> GetDailyStatistics()
    {
        Calculate();
        return DailyStatistics;
    }


    private void Calculate()
    {
        throw new NotImplementedException();
    }

    private IEnumerable<DailyStatistic> DailyStatistics { get; }
}