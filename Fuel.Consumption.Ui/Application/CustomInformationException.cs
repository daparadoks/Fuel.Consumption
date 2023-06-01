namespace Fuel.Consumption.Ui.Application;

public class CustomInformationException: Exception
{
    public CustomInformationException(KeyValuePair<int, string> messageCodePair, string redirectUrl = ""):base(messageCodePair.Value)
    {
        Code = messageCodePair.Key;
        RedirectUrl = redirectUrl;
    }

    public CustomInformationException(string message, int code = 400):base(message)
    {
        Code = code;
    }

    public int Code { get; set; }
    public string RedirectUrl { get; set; }
}