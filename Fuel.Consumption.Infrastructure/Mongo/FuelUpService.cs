using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class FuelUpService:Repository<FuelUp>, IFuelUpService
{
    private readonly ILogger<FuelUpService> _logger;

    public FuelUpService(IOptions<ApiConfig> options, ILogger<FuelUpService> logger): base(options.Value.ConnectionStrings.Mongo)
    {
        _logger = logger;
    }

    public async Task<FuelUp> GetById(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task Add(FuelUp fuelUp) => await _collection.InsertOneAsync(fuelUp);
}