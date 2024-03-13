using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class DailyStatisticReadService : Repository<DailyStatistic>, IDailyStatisticReadService
{
    public DailyStatisticReadService(IOptions<ApiConfig> options): base(options.Value.ConnectionStrings.Mongo)
    {
    }
}