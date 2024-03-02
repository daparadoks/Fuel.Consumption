namespace Fuel.Consumption.Api.Facade.Request;

public class SearchRequest<T>
{
    public T Filter { get; set; }
    public int Page { get; set; }
    public int Take { get; set; }
    public int ToSkip() => (ToPage() - 1) * ToTake();
    public int ToPage() => Page <= 0 ? 1 : Page;
    public int ToTake() => Take <= 0 ? 1 : Take > 50 ? 50 : Take;
}