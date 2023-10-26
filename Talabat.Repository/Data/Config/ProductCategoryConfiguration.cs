using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Config
{
    internal class ProductCategoryConfiguration : IEntityTypeConfiguration<productCategory>
    {
        public void Configure(EntityTypeBuilder<productCategory> builder)
        {
            builder.Property(C => C.Name)
                .IsRequired();
        }
    }
}
