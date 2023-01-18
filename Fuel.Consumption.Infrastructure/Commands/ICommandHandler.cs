namespace Fuel.Consumption.Infrastructure.Commands;

public interface ICommandHandler<in T>
{
    Task Execute(T command);
}