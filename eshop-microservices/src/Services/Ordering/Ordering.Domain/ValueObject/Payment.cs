namespace Ordering.Domain.ValueObject;

public record Payment
{
    public string CardNumber { get; init; } = default!;
    public string CardHolderName { get; init; } = default!;
    public string Expiration { get; init; } = default!;
    public string CVV { get; init; } = default!;
    public int PaymentMethod { get; init; } = default!;

    protected Payment()
    {
    }

    private Payment(string cardNumber, string cardHolderName, string expiration, string cvv, int paymentMethod)
    {
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardNumber, string cardHolderName, string expiration, string cvv, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardHolderName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3, "CVV must be 3 digits");
        return new Payment(cardNumber, cardHolderName, expiration, cvv, paymentMethod);
    }
}
