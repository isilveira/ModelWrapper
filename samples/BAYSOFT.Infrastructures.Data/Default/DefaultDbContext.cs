﻿using BAYSOFT.Core.Domain.Default.Entities;
using BAYSOFT.Infrastructures.Data.Default.EntityMappings;
using Microsoft.EntityFrameworkCore;

namespace BAYSOFT.Infrastructures.Data.Contexts
{
    public class DefaultDbContext : DbContext
    {
        public DbSet<Sample> Samples { get; set; }

        protected DefaultDbContext()
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }

        public DefaultDbContext(DbContextOptions options) : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SampleMap());
        }
    }
}
