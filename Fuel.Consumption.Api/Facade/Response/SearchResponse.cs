namespace Fuel.Consumption.Api.Facade.Response;

public class SearchResponse<T>
{
    public IEnumerable<T> Results { get; set; }
    public int Total { get; set; }
    public int PageSize { get; set; }
    public int TotalPage => (Total / PageSize) + 1;
}