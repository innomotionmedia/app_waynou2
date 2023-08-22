// Copyright (c) Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT License.

using Microsoft.EntityFrameworkCore;

namespace TodoAppService.NET6.Db
{


    public partial class MigrationDbContext : DbContext
    {
        public MigrationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public MigrationDbContext(DbContextOptions<MigrationDbContext> options) : base(options)
        {
        }

        public DbSet<tblCategories> Categories => Set<tblCategories>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<tblCategories>(entity =>
            {
                entity.Property(e => e.Version)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });
         
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
