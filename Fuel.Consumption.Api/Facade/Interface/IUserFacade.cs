using Fuel.Consumption.Api.Controllers.Request;
using Fuel.Consumption.Api.Facade.Response;

namespace Fuel.Consumption.Api.Facade.Interface;

public interface IUserFacade
{
    Task<LoginResponse> Login(LoginRequest request);
    Task Register(RegisterRequest request);
}