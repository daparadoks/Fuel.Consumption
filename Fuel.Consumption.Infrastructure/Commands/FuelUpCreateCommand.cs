using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Infrastructure.Commands;

public class FuelUpCreateCommand
{
    public FuelUpCreateCommand(string vehicleId,
        int odometer,
        int distance,
        decimal amount,
        decimal consumption,
        decimal price,
        int currency,
        bool complete,
        int cityPercentage,
        int fuelType,
        int fuelRate,
        string brand,
        string userId,
        int index,
        DateTime createdAt,
        DateTime fuelUpDate,
        string id)
    {
        VehicleId = vehicleId;
        Odometer = odometer;
        Distance = distance;
        Amount = amount;
        Consumption = consumption;
        Price = price;
        Currency = currency;
        Complete = complete;
        CityPercentage = cityPercentage;
        FuelType = fuelType;
        FuelRate = fuelRate;
        Brand = brand;
        UserId = userId;
        Index = index;
        CreatedAt = createdAt;
        FuelUpDate = fuelUpDate;
        Id = id;
    }
    
    public string Id { get; set; }
    public string VehicleId { get; set; }
    public int Odometer { get; set; }
    public int Distance { get; set; }
    public decimal Amount { get; set; }
    public decimal Consumption { get; set; }
    public decimal Price { get; set; }
    public int Currency { get; set; }
    public bool Complete { get; set; }
    public int CityPercentage { get; set; }
    public int FuelType { get; set; }
    public int FuelRate { get; set; }
    public string Brand { get; set; }
    public string UserId { get; set; }
    public int Index { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime FuelUpDate { get; set; }

    public FuelUp ToEntity()
    {
        throw new NotImplementedException();
    }
}

public class FuelUpCreateHandler:ICommandHandler<FuelUpCreateCommand>
{
    private readonly IFuelUpWriteService _fuelUpWriteService;

    public FuelUpCreateHandler(IFuelUpWriteService fuelUpWriteService)
    {
        _fuelUpWriteService = fuelUpWriteService;
    }

    public async Task Execute(FuelUpCreateCommand command)
    {
        var entity = command.ToEntity();
    }
}