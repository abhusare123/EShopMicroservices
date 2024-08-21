using Ordering.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeletOrderHandler(IApplicationDBContext dBContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(request.OrderId);
        var order = await dBContext.Orders.FindAsync(orderId, cancellationToken);
        if (order == null)
        {
            throw new OrderNotFoundException(nameof(Order), request.OrderId);
        }

        dBContext.Orders.Remove(order);
        await dBContext.SaveChangesAsync(cancellationToken);
        return new DeleteOrderResult(true);
    }
}
