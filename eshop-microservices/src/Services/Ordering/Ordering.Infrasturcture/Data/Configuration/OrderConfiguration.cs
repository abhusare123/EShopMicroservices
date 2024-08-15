using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObject;

namespace Ordering.Infrasturcture.Data.Configuration;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(o => o.Id)
            .HasConversion(v => v.Value,v => OrderId.Of(v));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .IsRequired();

        builder.HasMany(x => x.OrderItems)
            .WithOne()
            .HasForeignKey(x => x.OrderId);

        builder.ComplexProperty(o => o.OrderName, nameBuilder =>
        {
            nameBuilder.Property(x => x.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();


        });

        builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
        {
            addressBuilder.Property(x => x.FirstName)
                .HasColumnName(nameof(Order.ShippingAddress.FirstName))
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(x => x.LastName)
                .HasColumnName(nameof(Order.ShippingAddress.LastName))
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(x => x.State)
                .HasColumnName(nameof(Order.ShippingAddress.State))
                .HasMaxLength(100);

            addressBuilder.Property(x => x.Country)
                .HasColumnName(nameof(Order.ShippingAddress.Country))
                .HasMaxLength(100);

            addressBuilder.Property(x => x.ZipCode)
                .HasColumnName(nameof(Order.ShippingAddress.ZipCode))
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(x => x.Email)
            .HasColumnName(nameof(Order.ShippingAddress.Email))
                .HasMaxLength(50);

            addressBuilder.Property(x => x.AddressLine)
            .HasColumnName(nameof(Order.ShippingAddress.AddressLine))
                .HasMaxLength(180)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(x => x.FirstName)
                .HasColumnName(nameof(Order.BillingAddress.FirstName))
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(x => x.LastName)
                .HasColumnName(nameof(Order.BillingAddress.LastName))
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(x => x.State)
                .HasColumnName(nameof(Order.BillingAddress.State))
                .HasMaxLength(100);

            addressBuilder.Property(x => x.Country)
                .HasColumnName(nameof(Order.BillingAddress.Country))
                .HasMaxLength(100);

            addressBuilder.Property(x => x.ZipCode)
                .HasColumnName(nameof(Order.BillingAddress.ZipCode))
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(x => x.Email)
            .HasColumnName(nameof(Order.BillingAddress.Email))
                .HasMaxLength(50);

            addressBuilder.Property(x => x.AddressLine)
            .HasColumnName(nameof(Order.BillingAddress.AddressLine))
                .HasMaxLength(180)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.Payment, paymentBuilder =>
        {
            paymentBuilder.Property(p => p.CardHolderName).HasMaxLength(50);

            paymentBuilder.Property(p => p.CardNumber).HasMaxLength(16).IsRequired();

            paymentBuilder.Property(p => p.CVV).HasMaxLength(3);

            paymentBuilder.Property(p => p.PaymentMethod);
        });
        
        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
            s => s.ToString(),
            dbStatus => Enum.Parse<OrderStatus>(dbStatus));

        builder.Property(o => o.TotalPrice);
    }
}
