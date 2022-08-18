namespace Fuel.Consumption.Infrastructure.Configs;

public class ApiConfig
{
    public ConnectionStringConfig ConnectionStrings { get; set; }
    public Jwt Jwt { get; set; }
}

public class ConnectionStringConfig
{
    public string Mongo { get; set; }
}

public class Jwt
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
}