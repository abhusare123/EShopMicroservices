using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObject;

public record OrderId
{    
    public Guid Value { get; }

    private OrderId(Guid value) => Value = value;

    public static OrderId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Order id cannot be empty");
        }

        return new OrderId(value);
    }
}