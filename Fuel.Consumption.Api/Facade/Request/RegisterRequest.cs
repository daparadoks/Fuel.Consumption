﻿using Fuel.Consumption.Api.Application;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Request;

public class RegisterRequest
{
    public string Username { get; set; }
    public string Password { get; set; }

    public User ToEntity() => new(Guid.NewGuid().ToString(), Username, Crypto.HashPassword(Password));
}