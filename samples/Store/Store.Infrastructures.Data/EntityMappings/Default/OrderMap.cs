using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Domain.Entities.Default;
using System;

namespace Store.Infrastructures.Data.EntityMappings.Default
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .UseIdentityColumn();

            builder
                .Property<DateTime>("RegisteredAt")
                .HasColumnType("datetime");

            builder
                .Property<DateTime?>("ConfirmedAt")
                .IsRequired(false)
                .HasColumnType("datetime");

            builder
                .Property<DateTime?>("CancelledAt")
                .IsRequired(false)
                .HasColumnType("datetime");

            builder
                .Property<int>("CustomerId")
                .HasColumnType("int");

            builder
                .HasKey("Id");

            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);

            builder
                .HasMany(x => x.OrderedProducts)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            builder
                .ToTable("Orders");
        }
    }
}
