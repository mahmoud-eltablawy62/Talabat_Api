using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;
using Talabat.Repository.Identity.Oreder_Aggregate;

namespace Talabat.Repository.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.OwnsOne(o => o.Address, Address => Address.WithOwner());
            builder.Property(o => o.Status).HasConversion
                (
                OStatus => OStatus.ToString(),
                OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                );
            builder.Property(o => o.Sub_Collection).HasColumnType("decimal(18 , 2)");
            builder.HasOne(o => o.DelievryType).WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
