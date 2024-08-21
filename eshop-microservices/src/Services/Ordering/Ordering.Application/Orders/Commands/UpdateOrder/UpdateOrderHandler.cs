

using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDBContext dBContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.OrderDto.Id);
        var order = await dBContext.Orders.FindAsync(orderId, cancellationToken);

        if (order == null)
        {
            throw new OrderNotFoundException(command.OrderDto.Id.ToString());
        }

        UpdateOrderWithNewValues(order, command.OrderDto);
        dBContext.Orders.Update(order);
        await dBContext.SaveChangesAsync(cancellationToken);
        return new UpdateOrderResult(true);
    }

    private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        var updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.Email, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
        var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.Email, orderDto.BillingAddress.AddressLine, order.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
        var updatedPayment = Payment.Of(orderDto.PaymentDto.CardNumber, orderDto.PaymentDto.CardName, orderDto.PaymentDto.Expiration, orderDto.PaymentDto.Cvv, orderDto.PaymentDto.PaymentMethod);

        order.Update(OrderName.Of(orderDto.OrderName),
                     updatedShippingAddress,
                     updatedBillingAddress,
                     updatedPayment,
                     orderDto.Status);
    }
}
