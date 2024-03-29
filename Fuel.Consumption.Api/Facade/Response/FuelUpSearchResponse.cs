﻿using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Response;

public class FuelUpSearchResponse
{
    public FuelUpSearchResponse(FuelUp fuelUp, string vehicleName)
    {
        VehicleName = vehicleName;
        Odometer = fuelUp.Odometer;
        Amount = fuelUp.Amount;
        Price = fuelUp.Price;
        FormattedPrice = $"{fuelUp.Price.ToString()} {((CurrencyEnum)fuelUp.Currency).ToString()}";
        Date = fuelUp.FuelUpDate.ToString("dd.MM.yyyy HH:mm");
    }


    public string VehicleName { get; }
    public int Odometer { get; }
    public decimal Amount { get; }
    public string FormattedAmount => $"{Amount.ToString()} Litre";
    public decimal Consumption { get; }
    public string FormattedConsumption => $"{Consumption.ToString()} L/100 Km";
    public decimal Price { get; }
    public string FormattedPrice { get; }
    public string Date { get; }
}