namespace Fuel.Consumption.Api.Application;

public class CustomException:Exception
{
    public CustomException(int code, string message)
    {
        Message = message;
        Code = code;
    }

    public int Code { get; }
    public override string Message { get; }
}


public class ContentExistsException : CustomException
{
    public ContentExistsException(string itemName) : base(200, $"{itemName} zaten mevcut")
    {

    }
}

public class VehicleNotFoundException : CustomException
{
    public VehicleNotFoundException():base(404,"Tanımsız araç")
    {
        
    }
}

public class NotFoundException : CustomException
{
    public NotFoundException(string item) : base(404, $"{item} bulunamadı")
    {

    }
}

public class OdometerInvalidException : CustomException
{
    public OdometerInvalidException(int startOdometer, int lastOdometer) : base(400,
        $"Girmiş olduğunuz odometre değeri {startOdometer} son girilen değer olan {lastOdometer}'dan büyük olmalıdır.")
    {

    }
}

public class UserNotFoundException : CustomException
{
    public UserNotFoundException():base(404,"Kullanıcı bulunamadı")
    {
        
    }
}

public class HashNotSupportedException : CustomException
{
    public HashNotSupportedException():base(500,"")
    {
        
    }
}

public class UserIsExistsException : CustomException
{
    public UserIsExistsException(string username):base(400,$"{username} adlı kullanıcı mevcut")
    {
        
    }
}

public class RegisterDetailsIsRequiredException : CustomException
{
    public RegisterDetailsIsRequiredException() : base(400, "Bilgileri eksiz girmeniz gerekmektedir")
    {

    }
}


public class FuelUpDateIsInvalidException : CustomException
{
    public FuelUpDateIsInvalidException(DateTime fuelUpDate, DateTime previousFuelUpDate) : base(400,
        $"Girmiş olduğunuz yakıt alım tarihi {fuelUpDate.ToString("dd.MM.yyyy HH:mm")} son girilen tarih olan {previousFuelUpDate.ToString("dd.MM.yyyy HH:mm")}'dan büyük olmalıdır.")
    {

    }
}