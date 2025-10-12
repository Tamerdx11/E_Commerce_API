using E_Commerce.Domain.Entities.Products;
using System.Text.Json;

namespace E_Commerce.Persistance.DbInitializer;

public class DbInitializer(ApplicationDbContext appDbContext)
    : IDbInitializer
{
    public async Task InitializeAsync()
    {
        try
        {
            //if ((await appDbContext.Database.GetAppliedMigrationsAsync()).Any())
            await appDbContext.Database.MigrateAsync();// will do everything
            // Add Brands
            if (!appDbContext.ProductBrands.Any())
            {
                var brandData = await File.ReadAllBytesAsync(@"..\Infrastructure\ECommerce.Persistance\Context\DataSeed\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if (brands is not null && brands.Any())
                {
                    await appDbContext.ProductBrands.AddRangeAsync(brands);
                    await appDbContext.SaveChangesAsync();
                }
            }
            // Add Product Types
            if (!appDbContext.ProductTypes.Any())
            {
                var typesData = await File.ReadAllBytesAsync(@"..\Infrastructure\ECommerce.Persistance\Context\DataSeed\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types is not null && types.Any())
                {
                    await appDbContext.ProductTypes.AddRangeAsync(types);
                    await appDbContext.SaveChangesAsync();
                }
            }
            // Add Products
            if (!appDbContext.Products.Any())
            {
                var productsData = await File.ReadAllBytesAsync(@"..\Infrastructure\ECommerce.Persistance\Context\DataSeed\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products is not null && products.Any())
                {
                    await appDbContext.Products.AddRangeAsync(products);
                    await appDbContext.SaveChangesAsync();
                }
            }


        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex);
        }




        
    }
}
