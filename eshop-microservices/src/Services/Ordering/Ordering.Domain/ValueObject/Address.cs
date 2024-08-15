namespace Ordering.Domain.ValueObject;

public record Address
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string AddressLine { get; init; }
    public string Country { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }

    protected Address()
    {
    }

    private Address(string firstName, string lastName, string email, string addressLine, string country, string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public static Address Of(string firstName, string lastName, string email, string addressLine, string country, string state, string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine, nameof(addressLine));

        return new Address(firstName, lastName, email, addressLine, country, state, zipCode);
    }

}
