using Fuel.Consumption.Api.Application;
using Fuel.Consumption.Api.Controllers.Request;
using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Domain;
using IronXL;

namespace Fuel.Consumption.Api.Facade;

public class ImportDataFacade : IImportDataFacade
{
    private const int ConsumptionRow = 2;
    private const int OdometerRow = 3;
    private const int AmountRow = 5;
    private const int PriceRow = 6;
    private const int CityPercentageRow = 7;
    private const int DateRow = 8;
    private const int MissedRow = 12;
    private const int PartialRow = 13;

    private readonly ILogger<ImportDataFacade> _logger;
    private readonly IFuelUpReadService _fuelUpReadService;
    private readonly IFuelUpWriteService _fuelUpWriteService;
    private readonly IVehicleService _vehicleService;

    public ImportDataFacade(ILogger<ImportDataFacade> logger,
        IFuelUpReadService fuelUpReadService, 
        IFuelUpWriteService fuelUpWriteService, 
        IVehicleService vehicleService)
    {
        _logger = logger;
        _fuelUpReadService = fuelUpReadService;
        _fuelUpWriteService = fuelUpWriteService;
        _vehicleService = vehicleService;
    }

    public async Task ImportData(ImportDataRequest request, User user)
    {
        var vehicle = await _vehicleService.GetById(request.VehicleId);
        if (vehicle == null || vehicle.UserId != user.Id)
            throw new VehicleNotFoundException();
        
        var workBook = WorkBook.Load(request.ToStream());
        var workSheet = workBook.DefaultWorkSheet;

        var fuelUps = new List<FuelUp>();
        var rowCount = workSheet.RowCount;
        for (int i = 0; i < rowCount; i++)
        {
            var row = workSheet.Rows[i];
            fuelUps.Add(new FuelUpImport(row.Columns[ConsumptionRow].DecimalValue,
                row.Columns[OdometerRow].DecimalValue,
                row.Columns[AmountRow].DecimalValue,
                row.Columns[PriceRow].DecimalValue,
                row.Columns[CityPercentageRow].IntValue,
                row.Columns[DateRow].DateTimeValue,
                row.Columns[MissedRow].IntValue,
                row.Columns[PartialRow].IntValue).ToFuelUp(user.Id, vehicle));
        }

        var existsFuelUps = await _fuelUpReadService.GetByVehicle(request.VehicleId);
        try
        {
            await _fuelUpWriteService.BulkAdd(fuelUps);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Import data failed for vehicle id: {request.VehicleId}");
            throw new CustomException(400, "İçe aktarma sırasında beklenmeyen bi hata oluştu.", false);
        }
        finally
        {
            foreach (var existsFuelUp in existsFuelUps)
                await _fuelUpWriteService.Delete(existsFuelUp.Id);
        }
    }
}