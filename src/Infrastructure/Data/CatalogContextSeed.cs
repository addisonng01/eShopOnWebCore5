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
                new("Aldi"),            //was azure
                new("Super One"),       //was .net
                new("HyVee"),           //was visual studio
                new("Walmart"),         //was sql server
                new("Target")           //was other
            };
        }

        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>
            {
                new("Cup"),             //was mug   
                new("Pants"),         //was tshirt
                new("Blanket"),           //was sheet
                new("Candle") //was usb memory stick
            };
        }

        static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>
            {
                new(2,2, "Super One Bot Black Pants", "Super One Bot Black Pants", 19.5M,  "http://catalogbaseurltobereplaced/images/products/1.png", "M"),
                new(1,2, "Super One Black & White Cup", "Super One Black & White Cup", 8.50M, "http://catalogbaseurltobereplaced/images/products/2.png", "M"),
                new(2,5, "Target White Pants", "Target White Pants", 12,  "http://catalogbaseurltobereplaced/images/products/3.png", "M"),
                new(3,2, "Super One Foundation Blanket", "Super One Foundation Blanket", 12, "http://catalogbaseurltobereplaced/images/products/4.png", "M"),
                new(3,5, "Target Red Blanket", "Target Red Blanket", 8.5M, "http://catalogbaseurltobereplaced/images/products/5.png", "M"),
                new(3,2, "Super One Blue Blanket", "Super One Blue Blanket", 12, "http://catalogbaseurltobereplaced/images/products/6.png", "M"),
                new(4,4, "Walmart Red Candle", "Walmart Red Candle",  12, "http://catalogbaseurltobereplaced/images/products/7.png", "M"),
                new(2,3, "HyVee Purple Pants", "HyVee Purple Pants", 8.5M, "http://catalogbaseurltobereplaced/images/products/8.png", "M"),
                new(1,5, "Target White Cup", "Target White Cup", 12, "http://catalogbaseurltobereplaced/images/products/9.png", "M"),
                new(3,2, "Super One Foundation Blanket", "Super One Foundation Blanket", 12, "http://catalogbaseurltobereplaced/images/products/10.png", "M"),
                new(3,1, "Aldi Blanket", "Aldi Blanket", 8.5M, "http://catalogbaseurltobereplaced/images/products/11.png", "M"),
                new(4,1, "Aldi White Candle", "Aldi White Candle", 12, "http://catalogbaseurltobereplaced/images/products/12.png", "M")
            };
        }
    }
}
