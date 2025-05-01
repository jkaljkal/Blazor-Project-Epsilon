using Microsoft.EntityFrameworkCore;
using BlazorApp.Shared.Models.Entites;

namespace BlazorApp.Data
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedData.Seed(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasKey(customer => customer.Id);

            modelBuilder.Entity<Customer>()
                .Property(customer => customer.Id)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(customer => customer.CompanyName)
                .HasMaxLength(50);

            modelBuilder.Entity<Customer>()
                .Property(customer => customer.ContactName)
                .HasMaxLength(50);

            modelBuilder.Entity<Customer>()
                .Property(customer => customer.Address)
                .HasMaxLength(30);

            modelBuilder.Entity<Customer>()
                .Property(customer => customer.City)
                .HasMaxLength(30);

            modelBuilder.Entity<Customer>()
                .Property(customer => customer.Region)
                .HasMaxLength(30);

            modelBuilder.Entity<Customer>()
                .Property(customer => customer.PostalCode)
                .HasMaxLength(10);

            modelBuilder.Entity<Customer>()
                .Property(customer => customer.Country)
                .HasMaxLength(30);

            modelBuilder.Entity<Customer>()
                .Property(customer => customer.Phone)
                .HasMaxLength(30);
        }
    }
}
