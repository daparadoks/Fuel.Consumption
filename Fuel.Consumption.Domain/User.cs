namespace Fuel.Consumption.Domain;

public class User
{
    public User(Guid id, string username)
    {
        Id = id;
        Username = username;
    }

    public Guid Id { get; }
    public string Username { get; }
}