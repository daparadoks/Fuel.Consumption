namespace Fuel.Consumption.Domain;

public class FuelUpImport
{
    public FuelUpImport(decimal consumption, decimal odometer, decimal amount, decimal price, int cityPercentage, string date, int missed, int partial)
    {
        Consumption = consumption;
        Odometer = (int)odometer;
        Amount = amount;
        Price = price;
        CityPercentage = cityPercentage;
        var dateParsed = DateTime.TryParse(date, out var parsedDate);
        Date = dateParsed ? parsedDate : DateTime.Now;
        Complete = missed == 0 && partial == 0;
    }

    public decimal Consumption { get; }
    public int Odometer { get; }
    public decimal Amount { get; }
    public decimal Price { get; }
    public int CityPercentage { get; }
    public DateTime Date { get; }
    public bool Complete { get; }

    public FuelUp ToFuelUp(string userId, Vehicle vehicle) => 
        new(Odometer,
        Amount,
        Price,
        Amount * Price,
        (int)CurrencyEnum.Try,
        Complete,
        CityPercentage,
        userId,
        vehicle,
        Date,
        Date,
        DateTime.Now);
}