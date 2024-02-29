using System.Net;
using Fuel.Consumption.Ui.Application;

namespace Fuel.Consumption.Ui.Facades;

public class FacadeResponse<T>
    {
        public FacadeResponse(T data, string message = "")
        {
            Success = true;
            Message = message;
            Data = data;
        }

        public FacadeResponse(string message, int code = 400, bool success = false)
        {
            Message = message;
            Code = code;
            Success = success;
        }

        public FacadeResponse(HttpStatusCode status, string redirectUrl)
        {
            Code = (int) status;
            RedirectUrl = redirectUrl;
        }

        public FacadeResponse(KeyValuePair<int, string> pair)
        {
            Code = pair.Key;
            Message = pair.Value;
        }
        
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public string RedirectUrl { get; set; }
        public HttpStatusCode Status => (HttpStatusCode) Code;
        public T Data { get; set; }
    }

public class FacadeResponse
{
    public FacadeResponse()
    {
        Success = false;
    }

    public FacadeResponse(bool success = true, string message = "", int code = 200)
    {
        Message = message;
        Code = code;
        Success = success;
    }

    public FacadeResponse(string message, int code = 400)
    {
        Message = message;
        Code = code;
    }

    public FacadeResponse(KeyValuePair<int, string> message)
    {
        Success = message.Key == 200;
        Code = message.Key;
        Message = message.Value;
    }

    public FacadeResponse(CustomInformationException exception)
    {
        Success = exception.Code == 200;
        Message = exception.Message;
        Code = exception.Code;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public int Code { get; set; }
    public string RedirectUrl { get; set; }
    public HttpStatusCode Status => (HttpStatusCode)Code;
}