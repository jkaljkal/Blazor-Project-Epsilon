using Microsoft.EntityFrameworkCore;
using BlazorApp.Models;

namespace BlazorApp.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = "6e9e0727-62ee-4b48-8cf7-76c1f24bee4c",
                    CompanyName = "Epsilon NET",
                    ContactName = "Γιάννης Καλογερόπουλος",
                    Address = "Νάουσα",
                    City = "Πάρος",
                    Region = "Κυκλάδες",
                    PostalCode = "84401",
                    Country = "Ελλάδα",
                    Phone = "6947093404"
                },
                new Customer
                {
                    Id = "5A6AD17C-A342-4DD6-9A5D-959732DEEA5E",
                    CompanyName = "Singular Logic",
                    ContactName = "Μαρία Παπαδοπούλου",
                    Address = "Στέφανου Σαράφη 12",
                    City = "Χαλάνδρι",
                    Region = "Αττική",
                    PostalCode = "12345",
                    Country = "Ελλάδα",
                    Phone = "6945899878"
                },
                new Customer
                {
                    Id = "D270A06D-6E5D-4845-9F8E-85FABEAA46C5",
                    CompanyName = "Data Communications",
                    ContactName = "Antonia Damascus",
                    Address = "Green Hill 2312",
                    City = "California",
                    Region = null,
                    PostalCode = "05023",
                    Country = "USA",
                    Phone = "(5) 555-3932"
                }
            );
        }
    }
}