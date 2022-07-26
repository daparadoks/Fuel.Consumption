namespace Fuel.Consumption.Infrastructure.Configs;

public class ApiConfig
{
    public ConnectionStringConfig ConnectionStrings { get; set; }
}

public class ConnectionStringConfig
{
    public string Mongo { get; set; }
}