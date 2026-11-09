using FashionCommerce.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionCommerce.Api.Data
{
    // Manage database access through Entity Framework Core.
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Represent the products stored in the database.
        public DbSet<Product> Products { get; set; }
    }
}
