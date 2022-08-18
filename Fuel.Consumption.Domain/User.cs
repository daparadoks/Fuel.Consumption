namespace Fuel.Consumption.Domain;

public interface IUserService
{
    Task<User> GetByUsername(string username);
}

public class User
{
    public User(string id, string username)
    {
        Id = id;
        Username = username;
    }

    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}