namespace Fuel.Consumption.Ui.Application;

public static class UserContext
{
    private static string _token;
    public static string Token => _token;

    public static bool IsAuthenticated => !string.IsNullOrEmpty(Token);

    public static void SetUser(string token)
    {
        _token = token;
    }

    public static void Logout()
    {
        _token = string.Empty;
    }
}