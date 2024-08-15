using Ordering.Domain.Abstraction;
using Ordering.Domain.ValueObject;

namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;

    public static Customer Create(CustomerId id, string name, string email)
    {

        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

        return new Customer
        {
            Id = id,
            Name = name,
            Email = email
        };
    }
}
