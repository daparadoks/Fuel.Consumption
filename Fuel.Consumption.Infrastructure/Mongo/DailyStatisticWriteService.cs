using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class DailyStatisticWriteService : Repository<DailyStatistic>, IDailyStatisticWriteService
{
    public DailyStatisticWriteService(IOptions<ApiConfig> options): base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task DeleteByUserId(string userId) =>
        await _collection.DeleteManyAsync(x => x.UserId == userId);

    public async Task BulkAdd(IEnumerable<DailyStatistic> entities) =>
        await _collection.InsertManyAsync(entities);
}