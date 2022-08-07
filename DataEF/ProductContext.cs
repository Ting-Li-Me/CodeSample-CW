using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataEF
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
