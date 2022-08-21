using Fuel.Consumption.Api.Application;
using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Request;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fuel.Consumption.Api.Facade;

public class FuelUpFacade:IFuelUpFacade
{
    private readonly IFuelUpService _service;
    private readonly IVehicleService _vehicleService;

    public FuelUpFacade(IFuelUpService service, IVehicleService vehicleService)
    {
        _service = service;
        _vehicleService = vehicleService;
    }

    public async Task<FuelUpDetailResponse> Get(string id, User user)
    {
        var fuelUp = await _service.GetById(id);
        if (fuelUp == null)
            throw new NotFoundException("Yakıt bilgisi");
        if(fuelUp.UserId != user.Id)
            throw new NotFoundException("Yakıt bilgisi");

        var vehicle = await _vehicleService.GetById(fuelUp.VehicleId.ToString());
        return new FuelUpDetailResponse(fuelUp, vehicle);
    }

    public async Task Add(FuelUpRequest request, User user)
    {
        var vehicle = await _vehicleService.GetById(request.VehicleId.ToString());
        if (vehicle == null)
            throw new VehicleNotFoundException();
        if (vehicle.UserId != user.Id)
            throw new VehicleNotFoundException();
        
        var lastCompleted = await _service.FindLastCompleted(request.VehicleId);
        var startOdometer = lastCompleted.Odometer;
        var fuelUpsToCalculate = new List<FuelUp> { lastCompleted };
        var missedTask = _service.FindAfter(request.VehicleId, lastCompleted.FuelUpDate);
        var indexTask = _service.GetLastIndex(request.VehicleId);
        await Task.WhenAll(missedTask, indexTask);
        
        var missed = missedTask.Result;
        var index = indexTask.Result + 1;
        fuelUpsToCalculate.AddRange(missed);
        var lastOdometer = fuelUpsToCalculate.Max(x => x.Odometer);
        if (lastOdometer >= request.Odometer)
            throw new OdometerInvalidException(startOdometer, lastOdometer);
        
        var totalAmount = fuelUpsToCalculate.Sum(x => x.Amount);
        
        //todo: calculate
        await _service.Add(request.ToDomain(user.Id, CalculateConsumption(startOdometer, request.Odometer, totalAmount),
            lastOdometer, index));
    }

    private double CalculateConsumption(int startOdometer, int endOdometer, double totalAmount)
    {
        if (totalAmount <= 0 || startOdometer <= 0)
            return 0;

        var totalDistance = endOdometer - startOdometer;
        if (totalDistance <= 0)
            return 0;
        
        return totalAmount / (totalDistance / 100);
    }

    public async Task Update(string id, FuelUpRequest request, User user)
    {
        throw new NotImplementedException();
    }

    public async Task<SearchResponse<FuelUpSearchResponse>> Search(SearchRequest<FuelUpSearchRequest> request, User user)
    {
        var countTask = _service.Count(user.Id, request.Filter.VehicleId, request.Filter.StartDate,
            request.Filter.EndDate);
        var itemsTask = _service.Search(request.Skip, request.Take, user.Id, request.Filter.VehicleId,
            request.Filter.StartDate, request.Filter.EndDate);

        await Task.WhenAll(countTask, itemsTask);
        var total = countTask.Result;
        var items = itemsTask.Result;
        var vehicles = new List<Vehicle>();
        foreach (var vehicleId in items.GroupBy(x => x.VehicleId).Select(x => x.Key))
            vehicles.Add(await _vehicleService.GetById(vehicleId));

        return new SearchResponse<FuelUpSearchResponse>(
            items.Select(x =>
                new FuelUpSearchResponse(x,
                    vehicles.FirstOrDefault(v => v.Id == x.VehicleId.ToString())?.Name ?? string.Empty)), total,
            request.Take);
    }

    public async Task BulkAdd(BulkAddRequest request, User user)
    {
        foreach (var bulkItem in request.Items)
        {
            var vehicle = await GetVehicle(user, bulkItem);
            var index = await _service.GetLastIndex(vehicle.Id);
            var fuelUp = bulkItem.ToDomain(vehicle.Id, user.Id, request.Currency, request.FuelType, request.FuelRate,
                request.TimeZone ?? EssentialConstants.DefaultTimeZone, index + 1);
            await _service.Add(fuelUp);
        }
    }

    public async Task Delete(string id, User user)
    {
        var fuelUp = await _service.GetById(id);
        if (fuelUp == null || fuelUp.UserId != user.Id)
            throw new NotFoundException("Yakıt bilgisi");

        await _service.Delete(id);
        await ReOrderFuelUps(id);
    }

    private async Task ReOrderFuelUps(string vehicleId)
    {
        var fuelUps = await _service.GetByVehicleId(vehicleId);
        var index = 1;
        foreach (var fuelUp in fuelUps.OrderBy(x=>x.FuelUpDate))
        {
            await _service.Update(new FuelUp(fuelUp.VehicleId, fuelUp.Odometer, fuelUp.Distance,
                fuelUp.Amount, fuelUp.Consumption, fuelUp.Price, fuelUp.Currency, fuelUp.Complete,
                fuelUp.CityPercentage, fuelUp.FuelType, fuelUp.FuelRate, fuelUp.Brand, fuelUp.UserId, index,
                fuelUp.CreatedAt, fuelUp.FuelUpDate, fuelUp.Id));
            index++;
        }
    }

    private async Task<Vehicle> GetVehicle(User user, BulkAddItem bulkItem)
    {
        var vehicle = await _vehicleService.GetByName(bulkItem.VehicleName, user.Id);
        if (vehicle == null)
            vehicle = await NewVehicle(bulkItem.VehicleName, user.Id);
        
        return vehicle;
    }

    private async Task<Vehicle> NewVehicle(string vehicleName, string userId)
    {
        await _vehicleService.Add(new Vehicle(vehicleName, userId));
        return await _vehicleService.GetByName(vehicleName, userId);
    }
}