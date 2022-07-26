namespace Fuel.Consumption.Api.Facade.Request;

public class SearchRequest<T>
{
    public T Filter { get; set; }
    public int Page { get; set; }
    public int Take { get; set; }
    public int Skip => (Page - 1) * Take;
}