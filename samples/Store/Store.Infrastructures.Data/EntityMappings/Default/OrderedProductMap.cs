using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Domain.Entities.Default;
using System;

namespace Store.Infrastructures.Data.EntityMappings.Default
{
    public class OrderedProductMap : IEntityTypeConfiguration<OrderedProduct>
    {
        public void Configure(EntityTypeBuilder<OrderedProduct> builder)
        {
            builder
                .Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .UseIdentityColumn();

            builder
                .Property<int>("Amount")
                .HasColumnType("int");

            builder
                .Property<decimal>("Value")
                .HasColumnType("decimal(18,2)");

            builder
                .Property<DateTime>("RegisteredAt")
                .HasColumnType("datetime");

            builder
                .Property<int>("ProductId")
                .HasColumnType("int");

            builder
                .Property<int>("OrderId")
                .HasColumnType("int");

            builder
                .HasKey("Id");

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.OrderedProducts)
                .HasForeignKey(x => x.ProductId);

            builder
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderedProducts)
                .HasForeignKey(x => x.OrderId);

            builder
                .ToTable("OrderedProducts");
        }
    }
}
