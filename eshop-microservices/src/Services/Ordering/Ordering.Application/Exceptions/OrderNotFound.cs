using BuildingBlocks.Exeptions;

namespace Ordering.Application.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(string message) : base(message)
    {
    }

    public OrderNotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}
