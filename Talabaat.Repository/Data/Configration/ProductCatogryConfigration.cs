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
    internal class ProductCatogryConfigration : IEntityTypeConfiguration<ProductCatogry>
    {
        public void Configure(EntityTypeBuilder<ProductCatogry> builder)
        {
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
