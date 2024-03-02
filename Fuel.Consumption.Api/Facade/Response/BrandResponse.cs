namespace Fuel.Consumption.Api.Facade.Response;

public record BrandResponse(string Id, string Name, string LogoUrl);
public record ModelGroupResponse(string Id, string Name);
public record ModelResponse(string Id, string Name);