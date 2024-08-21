namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler (IApplicationDBContext dBContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);
        dBContext.Orders.Add(order);
        await dBContext.SaveChangesAsync(cancellationToken);
        return new CreateOrderResult(order.Id.Value);

    }

    private Order CreateNewOrder(OrderDto order)
    {
        var shippingAddress = Address.Of(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.Email, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode); 
        var billingAddress = Address.Of(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.Email, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode);

        var newOrder = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(order.CustomerId), OrderName.Of(order.OrderName), shippingAddress, billingAddress, 
            Payment.Of(order.PaymentDto.CardNumber, order.PaymentDto.CardName, order.PaymentDto.Expiration, order.PaymentDto.Cvv, order.PaymentDto.PaymentMethod));

        foreach (var item in order.OrderItems)
        {
            newOrder.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
        }
        return newOrder;
    }
}
