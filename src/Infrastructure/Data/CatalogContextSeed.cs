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

                //add an if statement to await catalog colors
                if (!await catalogContext.CatalogColors.AnyAsync())
                {
                    await catalogContext.CatalogColors.AddRangeAsync(
                        GetPreconfiguredCatalogColors());

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
                //Updated the catalog brands
                new("Ecomended"),
                new("Other")
            };
        }

        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>
            {
                //Updated the catalog item types
                new("Cleaning Brush"),
                new("T-Shirt"),
                new("Bag"),
                new("Sponge"),
                new("Plant Based Glitter"),
                new("Pouch"),
                new("Reusable Straws")
            };
        }

        //add a new filter dropdown with the catalog colors
        static IEnumerable<CatalogColor> GetPreconfiguredCatalogColors()
        {
            return new List<CatalogColor>
            {
                //Updated the catalog item types
                new("Tan"),
                new("Teal"),
                new("Green"),
                new("Multiple Colors"),
                new("Gold"),
                new("Charcoal"),
                new("Heathered Olive"),
                new("Black")
            };
        }

        static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            //When the catalog item constructor is called -add a color attribute
            return new List<CatalogItem>
            {
                //Changed the catalog items, added a color attribute, and modified the first two integers(item type and brand)
                new(1,1,1, "Coconut Bottle Cleaning Brush", "- Tan", "Coconut Bottle Cleaning Brush", 4.99M,  "images/products/one.png"),
                new(2,1,2, "Eye Heart Sea Turtle fair trade tee", "- Teal", "Eye Heart Sea Turtle fair trade tee", 14.99M, "images/products/two.jpeg"),
                new(3,2,3, "Vegan Leaf Leather Tote (Small)", "- Green", "Vegan Leaf Leather Tote (Small)", 39.99M,  "images/products/three.jpeg"),
                new(7,2,4, "Heart Shaped Reusable Stainless Steel Straws", "- Multiple Colors", "Heart Shaped Reusable Stainless Steel Straws", 2.49M, "images/products/four.jpeg"),
                new(4,2,4, "Konjac Exfoliating Sponge", "- Multiple Colors", "Konjac Exfoliating Sponge", 4.99M, "images/products/five.webp"),
                new(5,2,5, "Eco-Friendly Plant Based Glitter", "- Gold", "Eco-Friendly Plant Based Glitter", 2.99M, "images/products/six.webp"),
                new(1,1,1, "Coconut Dish/Cup Cleaning Brush", "- Tan", "Coconut Dish/Cup Cleaning Brush",  4.99M, "images/products/seven.webp"),
                new(2,1,6, "Eye Heart Baby Panda fair trade tee", "- Charcoal", "Eye Heart Baby Panda fair trade tee", 14.99M, "images/products/eight.jpeg"),
                new(4,2,4, "Kids Fun Shapes Konjac Sponges", "- Multiple Colors", "Kids Fun Shapes Konjac Sponges", 4.99M, "images/products/nine.webp"),
                new(7,2,4, "Reusable Stainless Steel Straws(Straight)", "- Multiple Colors", "Reusable Stainless Steel Straws(Straight", 1.99M, "images/products/ten.webp"),
                new(2,1,7, "Eye Heart Sloth fair trade tee", "- Heathered Olive", "Eye Heart Sloth fair trade tee", 14.99M, "images/products/eleven.jpeg"),
                new(6,1,8, "Leaf Leather keyring pouch", "- Black", "Leaf Leather keyring pouch", 4.99M, "images/products/twelve.webp")
            };
        }
    }
}