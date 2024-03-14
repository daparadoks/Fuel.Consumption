using Fuel.Consumption.Api.Facade.Request;
using Fuel.Consumption.Api.Facade.Response;
using Fuel.Consumption.Domain;

namespace Fuel.Consumption.Api.Facade.Interface;

public interface IFuelUpFacade
{
    Task<FuelUpDetailResponse> Get(string id, User user);
    Task Add(FuelUpRequest request, User user);
    Task Update(string id, FuelUpRequest request, User user);
    Task<SearchResponse<FuelUpSearchResponse>> Search(SearchRequest<FuelUpSearchRequest> request, User user);
    Task Delete(string id, User user);
    Task<SearchResponse<FuelUpListResponse>> GetByVehicle(string vehicleId, SearchRequest request, User user);
}