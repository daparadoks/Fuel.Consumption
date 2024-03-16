using Fuel.Consumption.Api.Controllers.Request;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Interface;

public interface IImportDataFacade
{
    Task ImportData(ImportDataRequest request, User user);
}