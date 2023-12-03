using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Repository.Identity.Oreder_Aggregate;

namespace Talabat.Repository.Data.Config
{
    internal class DelievryConfig : IEntityTypeConfiguration<DelievryType>
    {
        public void Configure(EntityTypeBuilder<DelievryType> builder)
        {
            builder.Property(o => o.Cost).HasColumnType("decimal(18,2)");

        }
    }
}
