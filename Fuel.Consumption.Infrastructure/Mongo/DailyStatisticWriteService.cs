using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class DailyStatisticWriteService : Repository<DailyStatistic>, IDailyStatisticWriteService
{
    public DailyStatisticWriteService(IOptions<ApiConfig> options): base(options.Value.ConnectionStrings.Mongo)
    {
    }
}