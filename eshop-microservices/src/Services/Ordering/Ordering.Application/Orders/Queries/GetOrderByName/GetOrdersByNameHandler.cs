
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extension;

namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrdersByNameHandler(IApplicationDBContext dBContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dBContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Contains(query.Name))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);
        return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }
}
