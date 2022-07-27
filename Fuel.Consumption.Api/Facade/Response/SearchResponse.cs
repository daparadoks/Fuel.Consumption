namespace Fuel.Consumption.Api.Facade.Response;

public class SearchResponse<T>
{
    public SearchResponse(IEnumerable<T> results, int total, int pageSize)
    {
        Results = results;
        Total = total;
        PageSize = pageSize;
    }
    public IEnumerable<T> Results { get; }
    public int Total { get; }
    public int PageSize { get; }
    public int TotalPage => (Total / PageSize) + 1;
}