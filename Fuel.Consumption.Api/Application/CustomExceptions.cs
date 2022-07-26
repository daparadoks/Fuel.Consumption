namespace Fuel.Consumption.Api.Application;

public class CustomExceptions:Exception
{
    public CustomExceptions(int code, string message)
    {
        Message = message;
        Code = code;
    }

    public int Code { get; }
    public override string Message { get; }
}


public class ContentExistsException : CustomExceptions
{
    public ContentExistsException(string itemName) : base(200, $"{itemName} zaten mevcut")
    {

    }
}

public class VehicleNotFoundException : CustomExceptions
{
    public VehicleNotFoundException():base(404,"Tanımsız araç")
    {
        
    }
}

public class NotFoundException : CustomExceptions
{
    public NotFoundException(string item) : base(404, $"{item} bulunamadı")
    {

    }
}