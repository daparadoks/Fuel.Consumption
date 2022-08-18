using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Response;

public class LoginResponse
{
    public LoginResponse(User user, string token)
    {
        Username = user.Username;
        Token = token;
    }

    public string Username { get; }

    public string Token { get; }
}