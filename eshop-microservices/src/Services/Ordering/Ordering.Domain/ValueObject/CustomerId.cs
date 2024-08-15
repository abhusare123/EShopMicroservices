using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObject;

public record CustomerId
{
    public Guid Value { get; }

    private CustomerId(Guid value) => Value = value;

    public static CustomerId Of(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("Customer id cannot be empty");
        }

        return new CustomerId(id);
    }
   
}
