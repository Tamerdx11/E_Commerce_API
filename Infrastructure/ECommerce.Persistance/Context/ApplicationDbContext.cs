using Microsoft.Extensions.Options;
using System.Reflection;

namespace ECommerce.Persistance.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
