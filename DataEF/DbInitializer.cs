using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.Models;

namespace DataEF
{
    public static class DbInitializer
    {
        public static void Initialize(ProductContext context)
        {

            // Look for any students.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var Products = new Product[]
            {
                new Product { Name = "Product 1",   Type = "Books", Price = 1.00m, Active=true },
                new Product { Name = "Product 2",   Type = "Books", Price = 2.00m, Active=true },
                new Product { Name = "Product 3",   Type = "Books", Price = 3.00m, Active=true },
                new Product { Name = "Product 4",   Type = "Books", Price = 4.00m, Active=true },
                new Product { Name = "Product 5",   Type = "Books", Price = 5.00m, Active=true },
                new Product { Name = "Product 6",   Type = "Books", Price = 6.00m, Active=true },
                new Product { Name = "Product 7",   Type = "Books", Price = 7.00m, Active=true },
                new Product { Name = "Product 8",   Type = "Books", Price = 8.00m, Active=true },
                new Product { Name = "Product 9",   Type = "Books", Price = 9.00m, Active=true },
                new Product { Name = "Product 10",   Type = "Books", Price = 10.00m, Active=true }
            };

            foreach (Product p in Products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();


        }
    }
}