﻿using Ordering.Domain.Abstraction;
using Ordering.Domain.ValueObject;

namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; } = default!;

    public static Product Create(ProductId id, string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentOutOfRangeException.ThrowIfNegative(price, nameof(price));
        return new Product
        {
            Id = id,
            Name = name,
            Price = price
        };
    }
}
