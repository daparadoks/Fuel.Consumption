using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class Repository<T>
{
    protected readonly IMongoCollection<T> _collection;
    public Repository(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("local");

        _collection = database.GetCollection<T>(typeof(T).Name);
    }

    public ValueTask DisposeAsync() => default;
}