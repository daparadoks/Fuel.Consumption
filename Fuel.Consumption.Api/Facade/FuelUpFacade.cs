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
    private readonly IDailyStatisticWriteService _dailyStatisticWriteService;
    private readonly ILogger<FuelUpFacade> _logger;

    public FuelUpFacade(IFuelUpReadService fuelUpReadService, 
        IFuelUpWriteService fuelUpWriteService,
        IVehicleService vehicleService, 
        IDailyStatisticWriteService dailyStatisticWriteService, 
        ILogger<FuelUpFacade> logger)
    {
        _fuelUpReadService = fuelUpReadService;
        _fuelUpWriteService = fuelUpWriteService;
        _vehicleService = vehicleService;
        _dailyStatisticWriteService = dailyStatisticWriteService;
        _logger = logger;
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
        await ValidateFuelUp(request, user, false);
        
        var vehicle = await _vehicleService.GetById(request.VehicleId);
        if (vehicle == null)
            throw new NotFoundException(request.VehicleId);

        var newFuelUp = await _fuelUpWriteService.Add(request.ToDomain(user.Id, vehicle, null));
        try
        {
            await ReCalculateConsumptionOfCompletedFuelUp(newFuelUp);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"ReCalculateConsumptionOfCompletedFuelUp Error for vehicleId: {request.VehicleId}");
            
            await _fuelUpWriteService.Delete(newFuelUp.Id);
            throw new CustomException(500, "Yakıt verisi girişinde beklenmedik bir hata oluştu.", false);
        }

        await CreateStatistics(user.Id);
    }

    public async Task Update(string id, FuelUpRequest request, User user)
    {
        await ValidateFuelUp(request, user, true);
        
        var existsFuelUp = await _fuelUpReadService.GetById(id);
        if (existsFuelUp == null)
            throw new NotFoundException(id);
        
        var vehicle = await _vehicleService.GetById(request.VehicleId);
        if (vehicle == null)
            throw new NotFoundException(request.VehicleId);

        var fuelUpToUpdate = request.ToDomain(user.Id, vehicle, existsFuelUp.CreatedAt);
        await _fuelUpWriteService.Update(fuelUpToUpdate);
        if (existsFuelUp.Complete != request.Complete)
        {
            try
            {
                if (request.Complete)
                    await ReCalculateConsumptionOfCompletedFuelUp(fuelUpToUpdate);

                var nextCompletedFuelUp =
                    await _fuelUpReadService.GetNextCompletedByVehicle(request.VehicleId, fuelUpToUpdate.FuelUpDate);
                if (nextCompletedFuelUp != null)
                    await ReCalculateConsumptionOfCompletedFuelUp(nextCompletedFuelUp, fuelUpToUpdate);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Fuel up update error for fuel up id: {id}");
                await _fuelUpWriteService.Update(existsFuelUp);
                throw new CustomException(400, $"Yakut bilgisi güncellenirken beklenmedik bir hata oluştu.", false);
            }
        }

        await CreateStatistics(user.Id);
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

        try
        {
            var nextCompletedFuelUp =
                await _fuelUpReadService.GetNextCompletedByVehicle(fuelUp.VehicleId, fuelUp.FuelUpDate);
            var previousCompletedFuelUp = fuelUp.Complete
                ? fuelUp
                : await _fuelUpReadService.GetPreviousCompletedByVehicle(fuelUp.VehicleId, fuelUp.FuelUpDate);
            if (nextCompletedFuelUp != null)
                await ReCalculateConsumptionOfCompletedFuelUp(nextCompletedFuelUp, previousCompletedFuelUp);
        }
        catch (Exception e)
        {
            var reAdded = await _fuelUpWriteService.Add(fuelUp);
            _logger.LogError(e, $"Fuel up delete error id: {reAdded.Id}");

            throw new CustomException(400, $"Yakıt bilgisi silinemedi.", false);
        }
    }

    public async Task<SearchResponse<FuelUpListResponse>> GetByVehicle(string vehicleId, SearchRequest request,
        User user)
    {
        var vehicle = await _vehicleService.GetById(vehicleId);
        if (vehicle == null || vehicle.UserId != user.Id)
            throw new NotFoundException(vehicleId);

        var searchTask = _fuelUpReadService.SearchByVehicleId(vehicleId, request.ToSkip(), request.ToTake());
        var countTask = _fuelUpReadService.CountByVehicle(vehicleId);
        await Task.WhenAll(searchTask, countTask);
        
        return new SearchResponse<FuelUpListResponse>(searchTask.Result.Select(x => new FuelUpListResponse(x.Id,
            x.FuelUpDate,
            0,
            x.Consumption,
            x.Price,
            x.CityPercentage)), countTask.Result, request.ToTake());
    }

    private async Task ValidateFuelUp(FuelUpRequest request, User user, bool update)
    {
        var lastFuelUp = update
            ? await _fuelUpReadService.GetPrevious(request.VehicleId, request.FuelUpDate)
            : await _fuelUpReadService.GetLastByVehicle(request.VehicleId);
        if (lastFuelUp != null && lastFuelUp.Odometer >= request.Odometer)
            throw new OdometerInvalidException(request.Odometer, lastFuelUp.Odometer);

        if (lastFuelUp != null && lastFuelUp.FuelUpDate >= request.FuelUpDate)
            throw new FuelUpDateIsInvalidException(request.FuelUpDate, lastFuelUp.FuelUpDate);
    }
    
    private async Task CreateStatistics(string userId)
    {
        try
        {
            var fuelUpTask = _fuelUpReadService.GetByUserId(userId);
            var vehicleTask = _vehicleService.GetByUserId(userId);
            await Task.WhenAll(fuelUpTask, vehicleTask);

            var allFuelUps = fuelUpTask.Result;
            var vehicles = vehicleTask.Result;
            var vehicleStatistic = new VehicleStatistic(allFuelUps.ToList(), vehicles, userId);
            await _dailyStatisticWriteService.DeleteByUserId(userId);
            await _dailyStatisticWriteService.BulkAdd(vehicleStatistic.GetDailyStatistics());
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"CreateStatistics error for user id: {userId}");
        }
    }
    
    private async Task ReCalculateConsumptionOfCompletedFuelUp(FuelUp fuelUp, FuelUp? previousCompletedFuelUp = null)
    {
        if (!fuelUp.Complete)
            return;

        previousCompletedFuelUp ??=
            await _fuelUpReadService.GetLastCompletedByVehicle(fuelUp.VehicleId, fuelUp.FuelUpDate);

        if (previousCompletedFuelUp == null)
            return;
        
        var nonCompletedFuelUps = await _fuelUpReadService.GetByDateRangeAndVehicle(fuelUp.VehicleId,
                previousCompletedFuelUp.FuelUpDate,
                fuelUp.FuelUpDate);
        var previousFuelUps = new List<FuelUp> { previousCompletedFuelUp };
        previousFuelUps.AddRange(nonCompletedFuelUps);
        fuelUp.CalculateConsumption(previousFuelUps);

        await _fuelUpWriteService.Update(fuelUp);
    }
}