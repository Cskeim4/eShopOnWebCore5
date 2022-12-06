using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public class CatalogContextSeed
    {
        public static async Task SeedAsync(CatalogContext catalogContext,
            ILoggerFactory loggerFactory, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (catalogContext.Database.IsSqlServer())
                {
                    catalogContext.Database.Migrate();
                }

                if (!await catalogContext.CatalogBrands.AnyAsync())
                {
                    await catalogContext.CatalogBrands.AddRangeAsync(
                        GetPreconfiguredCatalogBrands());

                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.CatalogTypes.AnyAsync())
                {
                    await catalogContext.CatalogTypes.AddRangeAsync(
                        GetPreconfiguredCatalogTypes());

                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.CatalogItems.AnyAsync())
                {
                    await catalogContext.CatalogItems.AddRangeAsync(
                        GetPreconfiguredItems());

                    await catalogContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;
                var log = loggerFactory.CreateLogger<CatalogContextSeed>();
                log.LogError(ex.Message);
                await SeedAsync(catalogContext, loggerFactory, retryForAvailability);
                throw;
            }
        }

        static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>
            {
                new("Ecomended"),
                new("Other")
            };
        }

        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>
            {
                new("Cleaning Brush"),
                new("T-Shirt"),
                new("Bag"),
                new("Sponge"),
                new("Plant Based Glitter"),
                new("Pouch"),
                new("Reusable Straws")
            };
        }

        static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>
            {
                new(1,1, "Coconut Bottle Cleaning Brush", "Coconut Bottle Cleaning Brush", 4.99M,  "images/products/one.png"),
                new(2,1, "Eye Heart Sea Turtle fair trade tee", "Eye Heart Sea Turtle fair trade tee", 14.99M, "images/products/two.jpeg"),
                new(3,2, "Vegan Leaf Leather Tote (Small)", "Vegan Leaf Leather Tote (Small)", 39.99M,  "images/products/three.jpeg"),
                new(7,2, "Heart Shaped Reusable Stainless Steel Straws", "Heart Shaped Reusable Stainless Steel Straws", 2.49M, "images/products/four.jpeg"),
                new(4,2, "Konjac Exfoliating Sponge", "Konjac Exfoliating Sponge", 4.99M, "images/products/five.webp"),
                new(5,2, "Eco-Friendly Plant Based Glitter", "Eco-Friendly Plant Based Glitter", 2.99M, "images/products/six.webp"),
                new(1,1, "Coconut Dish/Cup Cleaning Brush", "Coconut Dish/Cup Cleaning Brush",  4.99M, "images/products/seven.webp"),
                new(2,1, "Eye Heart Baby Panda fair trade tee", "Eye Heart Baby Panda fair trade tee", 14.99M, "images/products/eight.jpeg"),
                new(4,2, "Kids Fun Shapes Konjac Sponges", "Kids Fun Shapes Konjac Sponges", 4.99M, "images/products/nine.webp"),
                new(7,2, "Reusable Stainless Steel Straws(Straight)", "Reusable Stainless Steel Straws(Straight", 1.99M, "images/products/ten.webp"),
                new(2,1, "Eye Heart Sloth fair trade tee", "Eye Heart Sloth fair trade tee", 14.99M, "images/products/eleven.jpeg"),
                new(6,1, "Leaf Leather keyring pouch", "Leaf Leather keyring pouch", 4.99M, "images/products/twelve.webp")
            };
        }
    }
}