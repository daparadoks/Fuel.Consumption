using Fuel.Consumption.Api.Application;
using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Request;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade;

public class FuelUpFacade:IFuelUpFacade
{
    private readonly IFuelUpReadService _fuelUpReadService;
    private readonly IFuelUpWriteService _fuelUpWriteService;
    private readonly IVehicleService _vehicleService;

    public FuelUpFacade(IFuelUpReadService fuelUpReadService, 
        IFuelUpWriteService fuelUpWriteService,
        IVehicleService vehicleService)
    {
        _fuelUpReadService = fuelUpReadService;
        _fuelUpWriteService = fuelUpWriteService;
        _vehicleService = vehicleService;
    }

    public async Task<FuelUpDetailResponse> Get(string id, User user)
    {
        var fuelUp = await _fuelUpReadService.GetById(id);
        if (fuelUp == null)
            throw new NotFoundException("Yakıt bilgisi");
        if(fuelUp.UserId != user.Id)
            throw new NotFoundException("Yakıt bilgisi");

        var vehicle = await _vehicleService.GetById(fuelUp.VehicleId);
        return new FuelUpDetailResponse(fuelUp, vehicle);
    }

    public async Task Add(FuelUpRequest request, User user)
    {
        await ValidateFuelUp(request, user);
        await _fuelUpWriteService.Add(request.ToDomain(Guid.NewGuid(), user.Id, null));
    }

    public async Task Update(string id, FuelUpRequest request, User user)
    {
        await ValidateFuelUp(request, user);
        var existsFuelUp = await _fuelUpReadService.GetById(id);
        if (existsFuelUp == null)
            throw new NotFoundException(id);

        await _fuelUpWriteService.Update(request.ToDomain(Guid.Parse(id), user.Id, existsFuelUp.CreatedAt));
    }

    public async Task<SearchResponse<FuelUpSearchResponse>> Search(SearchRequest<FuelUpSearchRequest> request, User user)
    {
        var countTask = _fuelUpReadService.Count(user.Id, request.Filter.VehicleId, request.Filter.StartDate,
            request.Filter.EndDate);
        var itemsTask = _fuelUpReadService.Search(request.ToSkip(), request.ToTake(), user.Id, request.Filter.VehicleId,
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
                    vehicles.FirstOrDefault(v => v.Id == x.VehicleId)?.Name ?? string.Empty)), total,
            request.ToTake());
    }

    public async Task Delete(string id, User user)
    {
        var fuelUp = await _fuelUpReadService.GetById(id);
        if (fuelUp == null || fuelUp.UserId != user.Id)
            throw new NotFoundException("Yakıt bilgisi");

        await _fuelUpWriteService.Delete(id);
    }

    private async Task ValidateFuelUp(FuelUpRequest request, User user)
    {
        var vehicle = await _vehicleService.GetById(request.VehicleId);
        if (vehicle == null)
            throw new VehicleNotFoundException();
        if (vehicle.UserId != user.Id)
            throw new VehicleNotFoundException();

        var lastFuelUp = await _fuelUpReadService.GetLastByVehicle(vehicle.Id);
        if (lastFuelUp != null && lastFuelUp.Odometer >= request.Odometer)
            throw new OdometerInvalidException(request.Odometer, lastFuelUp.Odometer);

        if (lastFuelUp != null && lastFuelUp.FuelUpDate >= request.FuelUpDate)
            throw new FuelUpDateIsInvalidException(request.FuelUpDate, lastFuelUp.FuelUpDate);
    }
}