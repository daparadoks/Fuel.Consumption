using Fuel.Consumption.Api.Controllers.Request;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Interface;

public interface IStatisticFacade
{
    Task<IEnumerable<DailyStatisticResponse>> GetDaily(StatisticRequest request, User user);
    Task<SummaryResponse> GetSummary(StatisticRequest request, User user);
}