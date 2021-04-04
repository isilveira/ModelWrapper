using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Domain.Entities.Default;
using System;

namespace Store.Infrastructures.Data.EntityMappings.Default
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
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
                .Property<int?>("RootCategoryId")
                .IsRequired(false)
                .HasColumnType("int");

            builder
                .HasKey("Id");

            builder
                .HasOne(x => x.RootCategory)
                .WithMany(x => x.SubCategories)
                .HasForeignKey(x => x.RootCategoryId);

            builder
                .ToTable("Categories");
        }
    }
}
