using System.Net;

namespace Fuel.Consumption.Api.Controllers;

public record Response<T>(T Data, bool Success = true, string Message = "", int StatusCode = (int)HttpStatusCode.OK);
public record Response(bool Success, string Message = "", int StatusCode = (int)HttpStatusCode.OK);