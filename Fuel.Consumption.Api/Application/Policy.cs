namespace Fuel.Consumption.Api.Application;

public static class CustomRoles
{
    public const string Public = "1,2,99";
    public const string Editor = "2,99";
    public const string Admin = "99";
}

public static class CustomClaimTypes
{
    public const string UserRole = "UserRole";
}