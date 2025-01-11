using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabaat.Core.Entity;

namespace Talabaat.Repository.Data.Configration
{
    public class ProductConfigration : IEntityTypeConfiguration<Product> //to configre any class you shoud implment this interface
    {
        //this method is Class cofigration there are many type of configration for entity like flaunt configration
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Description)
                .IsRequired();
            builder.Property(p => p.ImageUrl)
               .IsRequired();
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            //configure the relation between product table with Brand the relation is one (brand) to many(product)
            builder.HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p=>p.BrandId);
            //configure the relation between product table with Catogry the relation is one (Catogry) to many(product)

            builder.HasOne(p => p.Catogry)
              .WithMany()
              .HasForeignKey(p => p.CatogryId);
        }
    }
}
