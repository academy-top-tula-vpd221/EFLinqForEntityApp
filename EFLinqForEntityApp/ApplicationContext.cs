using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFLinqForEntityApp
{
    public class ApplicationContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;

        public ApplicationContext()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=data_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasQueryFilter(e => e.Company!.Country!.Title == "Russia");
        }
    }

}
