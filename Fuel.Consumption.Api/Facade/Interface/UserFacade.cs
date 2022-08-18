using Fuel.Consumption.Api.Application;
using Fuel.Consumption.Api.Facade.Request;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;
using Fuel.Consumption.Infrastructure.Configs;
using Microsoft.Extensions.Options;

namespace Fuel.Consumption.Api.Facade.Interface;

public class UserFacade:IUserFacade
{
    private readonly IUserService _service;
    private readonly IOptions<ApiConfig> _options;

    public UserFacade(IUserService service, IOptions<ApiConfig> options)
    {
        _service = service;
        _options = options;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _service.GetByUsername(request.Username);
        if (user == null)
            throw new UserNotFoundException();
        
        if (!Crypto.VerifyPassword(request.Password, user.Password))
            throw new UserNotFoundException();

        var token = Crypto.GenerateJwtToken(user, _options.Value.Jwt.Issuer, _options.Value.Jwt.Audience,
            _options.Value.Jwt.Secret);

        return new LoginResponse(user, token);
    }
}