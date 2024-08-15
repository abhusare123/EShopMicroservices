using Ordering.Domain.Abstraction;
using Ordering.Domain.Models;

namespace Ordering.Domain.Events;

public record OrderCreatedEvent(Order order) : IDomainEvent;

public record OrderUpdatedEvent(Order order) : IDomainEvent;
