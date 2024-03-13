using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Response;

public class FuelUpDetailResponse
{
    public FuelUpDetailResponse(FuelUp fuelUp, Vehicle vehicle)
    {
        Vehicle = vehicle.Name;
        Odometer = fuelUp.Odometer;
        Amount = fuelUp.Amount;
        Price = fuelUp.Price;
        Currency = fuelUp.Currency;
        Complete = fuelUp.Complete;
        CityPercentage = fuelUp.CityPercentage;
        FuelType = fuelUp.FuelType;
        CreatedAt = fuelUp.CreatedAt;
        FuelUpDate = fuelUp.FuelUpDate;
    }
    
    public string Vehicle { get; }
    public int Odometer { get; }
    public decimal Amount { get; }
    public decimal Price { get; }
    public int Currency { get; }
    public bool Complete { get; }
    public int CityPercentage { get; }
    public int FuelType { get; }
    public DateTime CreatedAt { get; }
    public DateTime FuelUpDate { get; }
}