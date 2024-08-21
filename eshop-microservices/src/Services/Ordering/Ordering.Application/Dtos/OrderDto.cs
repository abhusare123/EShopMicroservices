using Ordering.Domain.Enums;

namespace Ordering.Application.Dtos;

public record class OrderDto (
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto PaymentDto,
    OrderStatus Status,
    List<OrderItemDto> OrderItems
    );