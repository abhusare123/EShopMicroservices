using BuildingBlocks.CQRS;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);
