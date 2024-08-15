using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObject;

namespace Ordering.Infrasturcture.Data.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(c => c.Id).HasConversion(
                         cutomerId => cutomerId.Value,
                         dbId => CustomerId.Of(dbId));

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).HasMaxLength(100);
        builder.HasIndex(c => c.Email).IsUnique();

    }
}
