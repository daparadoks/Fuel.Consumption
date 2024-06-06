using ClosedXML.Excel;
using Fuel.Consumption.Api.Application;
using Fuel.Consumption.Api.Controllers.Request;
using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade;

public class ImportDataFacade : IImportDataFacade
{
    private const int ConsumptionRow = 3;
    private const int OdometerRow = 4;
    private const int AmountRow = 6;
    private const int PriceRow = 7;
    private const int CityPercentageRow = 8;
    private const int DateRow = 9;
    private const int MissedRow = 13;
    private const int PartialRow = 14;

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
        
        //var workBook = WorkBook.Load(request.ToStream());
        var workBook = new XLWorkbook(request.ToStreamV2());
        var workSheet = workBook.Worksheet(1);

        var fuelUps = new List<FuelUp>();
        var rowCount = workSheet.RowCount();
        for (int i = 1; i <= rowCount; i++)
        {
            var row = workSheet.Row(i+1);
            fuelUps.Add(new FuelUpImport(row.Cell(ConsumptionRow).GetValue<decimal>(),
                row.Cell(OdometerRow).GetValue<decimal>(),
                row.Cell(AmountRow).GetValue<decimal>(),
                row.Cell(PriceRow).GetValue<decimal>(),
                row.Cell(CityPercentageRow).GetValue<int>(),
                row.Cell(DateRow).GetValue<string>(),
                row.Cell(MissedRow).GetValue<int>(),
                row.Cell(PartialRow).GetValue<int>()).ToFuelUp(user.Id, vehicle));
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