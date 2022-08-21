namespace Fuel.Consumption.Domain;

public interface IUserService
{
    Task<User> GetByUsername(string username);
    Task Add(User user);
}

public class User
{
    public User(string id, string username)
    {
        Id = id;
        Username = username;
    }
    
    public User(string id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }

    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}