using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fuel.Consumption.Domain;

public interface IUserService
{
    Task<User> GetByUsername(string username);
    Task Add(User user);
}

public class User
{
    public User(string username)
    {
        Username = username;
        Role = (int)UserRole.User;
    }
    
    public User(string username, string password)
    {
        Username = username;
        Password = password;
        Role = (int)UserRole.User;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int Role { get; set; }
}

public enum UserRole
{
    User = 1,
    Editor = 2,
    Admin = 99,
}