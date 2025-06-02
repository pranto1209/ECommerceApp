using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Text.Json;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context, UserManager<AppUser> userManager)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!context.Products.Any())
        {
            var productsData = await File
                .ReadAllTextAsync(path + @"/Data/SeedData/products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products == null) return;

            await context.Products.AddRangeAsync(products);

            await context.SaveChangesAsync();
        }

        if (!context.DeliveryMethods.Any())
        {
            var dmData = await File
                .ReadAllTextAsync(path + @"/Data/SeedData/delivery.json");

            var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

            if (methods == null) return;

            await context.DeliveryMethods.AddRangeAsync(methods);

            await context.SaveChangesAsync();
        }
    }
}
