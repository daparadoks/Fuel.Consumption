using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fuel.Consumption.Infrastructure.Mongo;

public class UserService:Repository<User>,IUserService
{
    public UserService(IOptions<ApiConfig> options) : base(options.Value.ConnectionStrings.Mongo)
    {
    }

    public async Task<User> GetByUsername(string username) =>
        await _collection.Find(x => x.Username == username).FirstOrDefaultAsync();

    public async Task Add(User user) => await _collection.InsertOneAsync(user);
}