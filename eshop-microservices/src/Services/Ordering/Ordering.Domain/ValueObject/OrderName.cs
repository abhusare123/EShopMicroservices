namespace Ordering.Domain.ValueObject;

public record OrderName
{
    private const int MaxLength = 5;
    public string Value { get; }

    private OrderName(string value) => Value = value;

    public static OrderName Of(string value)
    {
       ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
       ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, MaxLength);

       return new OrderName(value);
    }


}
