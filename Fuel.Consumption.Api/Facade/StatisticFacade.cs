using Fuel.Consumption.Api.Application;
using Fuel.Consumption.Api.Controllers.Request;
using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade;

public class StatisticFacade : IStatisticFacade
{
    private readonly IFuelUpReadService _fuelUpReadService;
    private readonly IVehicleService _vehicleService;

    public StatisticFacade(IFuelUpReadService fuelUpReadService, 
        IVehicleService vehicleService)
    {
        _fuelUpReadService = fuelUpReadService;
        _vehicleService = vehicleService;
    }

    public async Task<IEnumerable<DailyStatisticResponse>> GetDaily(StatisticRequest request, User user)
    {
        var (fuelUps, vehicles) = await GetFuelUps(request, user);
        
        var vehicleStatistic = new VehicleStatistic(fuelUps.ToList(), vehicles, user.Id);
        var dailyStatistics = vehicleStatistic.GetDailyStatistics();

        return dailyStatistics.Select(x =>
            new DailyStatisticResponse(x.Date, x.Odometer, x.Distance, x.FuelAmount, x.Consumption, x.FuelPrice));
    }

    public async Task<SummaryResponse> GetSummary(StatisticRequest request, User user)
    {
        var (fuelUps, vehicles) = await GetFuelUps(request, user);

        var summary = new Summary(fuelUps.ToList(), vehicles.ToList(), user);

        return new SummaryResponse(summary.TotalDistance,
            summary.TotalFuel,
            summary.AverageConsumption,
            summary.BestConsumption,
            summary.HighestConsumption,
            summary.TotalSpent,
            summary.AveragePricePerKm,
            summary.AveragePricePerFuelUp,
            summary.CityPercentage,
            summary.AverageFuelPrice);
    }

    private async Task<(IEnumerable<FuelUp>, IEnumerable<Vehicle>)> GetFuelUps(StatisticRequest request, User user)
    {
        var vehicles = request.AllVehicles()
            ? (await _vehicleService.GetByUserId(user.Id)).ToList()
            : (await _vehicleService.GetByIds(request.VehicleIds, user.Id)).ToList();

        if (!vehicles.Any())
            throw new VehicleNotFoundException(false);

        var fuelUps = await _fuelUpReadService.SearchForStatistic(vehicles.Select(x => x.Id),
            user.Id,
            request.StartDate,
            request.EndDate);

        return (fuelUps, vehicles);
    }
}