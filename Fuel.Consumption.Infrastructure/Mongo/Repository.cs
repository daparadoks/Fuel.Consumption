using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class Repository<T>
{
    protected readonly IMongoCollection<T> _collection;
    public Repository(string connectionString)
    {
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        
        var client = new MongoClient(settings);
        var database = client.GetDatabase("fuelConsumption");

        _collection = database.GetCollection<T>(typeof(T).Name);
    }

    public ValueTask DisposeAsync() => default;
}