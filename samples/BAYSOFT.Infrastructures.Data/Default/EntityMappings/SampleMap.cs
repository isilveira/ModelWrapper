﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BAYSOFT.Core.Domain.Default.Entities;

namespace BAYSOFT.Infrastructures.Data.Default.EntityMappings
{
    public class SampleMap : IEntityTypeConfiguration<Sample>
    {
        public void Configure(EntityTypeBuilder<Sample> builder)
        {
            builder
                .Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .UseIdentityColumn();

            builder
                .Property<string>("Description")
                .HasColumnType("nvarchar(512)");

            builder
                .HasKey("Id");

            builder
                .ToTable("Samples");
        }
    }
}
