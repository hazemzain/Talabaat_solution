using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabaat.Core.Entity;
using Talabaat.Repository.Data.Configration;

namespace Talabaat.Repository.Data
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ProductConfigration());
            //modelBuilder.ApplyConfiguration(new ProductBrandConfigration());
            //modelBuilder.ApplyConfiguration(new ProductCatogryConfigration());
            //i can apply configration assamply to each class inherate from IEntetyClassConfigration 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCatogry> ProductCatogrys { get; set; }
    }
}
