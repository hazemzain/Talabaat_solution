using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabaat.Core.Entity;

namespace Talabaat.Repository.Data
{
    public static class StoreDbContextSeed
    {
        //have method that do seeding for data from json file
        public static async Task SeedAsyn(StoreDBContext _context)
        {


            if (_context.ProductBrands.Count() <= 0)
            {
                //Data Seeding for brands
                //1-read data from file 
                var BrandData = File.ReadAllText("../Talabaat.Repository/Data/DataSeeding/brands.json");
                Console.WriteLine(BrandData);
                // i want to convert data in varible to list of product 
                //to do this i will use library json serilize
                //convert json to list of product
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                //then start to stoe this product in data base
                if (brands?.Count() > 0)
                {
                    foreach (var item in brands)
                    {
                        _context.Set<ProductBrand>().Add(item);

                    }
                    await _context.SaveChangesAsync();
                }
            }
            //=======================================================================================================
            if (_context.ProductCatogrys.Count() <= 0)
            {
                //Data Seeding for brands
                //1-read data from file 
                var CatogryData = File.ReadAllText("../Talabaat.Repository/Data/DataSeeding/types.json");
                Console.WriteLine(CatogryData);
                // i want to convert data in varible to list of product 
                //to do this i will use library json serilize
                //convert json to list of product
                var catogry = JsonSerializer.Deserialize<List<ProductCatogry>>(CatogryData);
                //then start to stoe this product in data base
                if (catogry?.Count() > 0)
                {
                    foreach (var item in catogry)
                    {
                        _context.Set<ProductCatogry>().Add(item);

                    }
                    await _context.SaveChangesAsync();
                }
            }
            //=======================================================================================================
            if (_context.Products.Count() <= 0)
            {
                //1-read data from file 
                var productData = File.ReadAllText("../Talabaat.Repository/Data/DataSeeding/products.json");
                Console.WriteLine(productData);
                // i want to convert data in varible to list of product 
                //to do this i will use library json serilize
                //convert json to list of product
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                //then start to stoe this product in data base
                if (products?.Count() > 0)
                {
                    foreach (var item in products)
                    {
                        _context.Set<Product>().Add(item);

                    }
                    await _context.SaveChangesAsync();
                }

            }
        }
    }
}
