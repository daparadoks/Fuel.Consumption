using System.Runtime.Serialization;

namespace Fuel.Consumption.Infrastructure.Exceptions;

public class CommandHandlerNotFoundException : Exception
{
    private Type type;

    public CommandHandlerNotFoundException()
    {
    }

    public CommandHandlerNotFoundException(string message) : base(message)
    {
    }

    public CommandHandlerNotFoundException(Type type)
    {
        this.type = type;
    }

    public CommandHandlerNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected CommandHandlerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}