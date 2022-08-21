using System.Net;
using System.Text.Json;

namespace Fuel.Consumption.Api.Application;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (CustomException e)
        {
            SetResponse(context, (HttpStatusCode)e.Code);
            var response = new
            {
                Success = false,
                Message = e.Message,
                Code = e.Code
            };
            var json = JsonSerializer.Serialize(response);
            _logger.LogError(e.Message, e);
            await context.Response.WriteAsync(json);
        }
        catch (Exception e)
        {
            SetResponse(context, HttpStatusCode.InternalServerError);
            var response = new
            {
                Success = false,
                Message = e.Message,
                Code = context.Response.StatusCode
            };
            var json = JsonSerializer.Serialize(response);
            _logger.LogError(e.Message, e);
            await context.Response.WriteAsync(json);
        }
    }

    private void SetResponse(HttpContext context, HttpStatusCode code)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int)code;
    }
}