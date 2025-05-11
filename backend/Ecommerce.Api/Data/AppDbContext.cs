using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Api.Models;

namespace Ecommerce.Api.Data
{
    /// <summary>
    /// This is our main EF Core DbContext.
    /// It inherits from IdentityDbContext so it includes all Identity tables like Users and Roles.
    /// We also add DbSets here for custom tables like Products, Orders, etc.
    /// </summary>
    public class AppDbContext : IdentityDbContext
    {
        // Constructor — needed for DI in Program.cs
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Products table — EF Core will create this as a table in the database.
        /// </summary>
        public DbSet<Product> Products => Set<Product>();
    }
}
