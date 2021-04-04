using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Domain.Entities.Default;
using System;

namespace Store.Infrastructures.Data.EntityMappings.Default
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .UseIdentityColumn();

            builder
                .Property<string>("Name")
                .HasColumnType("nvarchar(128)");

            builder
                .Property<string>("Description")
                .HasColumnType("nvarchar(512)");

            builder
                .Property<string>("Specifications")
                .HasColumnType("nvarchar(max)");

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
                .Property<bool>("IsVisible")
                .HasColumnType("bit");

            builder
                .Property<int>("CategoryId")
                .HasColumnType("int");

            builder
                .HasKey("Id");

            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);

            builder
                .HasMany(x => x.Images)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);

            builder
                .HasMany(x => x.OrderedProducts)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);

            builder
                .ToTable("Products");
        }
    }
}
