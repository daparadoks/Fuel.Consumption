using Fuel.Consumption.Api.Application;
using Fuel.Consumption.Api.Facade.Interface;
using Fuel.Consumption.Api.Facade.Request;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade;

public class VehicleFacade:IVehicleFacade
{
    private readonly IVehicleService _service;
    private readonly IModelService _modelService;

    public VehicleFacade(IVehicleService service, 
        IModelService modelService)
    {
        _service = service;
        _modelService = modelService;
    }

    public async Task Add(VehicleRequest request, User user)
    {
        var exists = await _service.GetByName(request.Name, user.Id);
        if (exists != null)
            throw new ContentExistsException("Araç");

        var model = await _modelService.GetByModelId(request.ModelId);
        if (model == null)
            throw new NotFoundException("araç modeli");

        var vehicle = new Vehicle(request.Name, user.Id, model, request.ImagePath);
        await _service.Add(vehicle);
    }

    public async Task<IEnumerable<VehicleListItem>> GetAll(User user)
    {
        var vehicles = await _service.GetByUserId(user.Id);
        return vehicles.Select(x => new VehicleListItem(x));
    }

    public async Task<VehicleDetailResponse> Get(string id, User user)
    {
        var vehicle = await _service.GetById(id);
        if (vehicle == null || vehicle.UserId != user.Id)
            throw new VehicleNotFoundException();

        return new VehicleDetailResponse(vehicle);
    }
}