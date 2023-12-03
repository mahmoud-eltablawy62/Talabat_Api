using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;
using Talabat.Repository.Identity.Oreder_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreContext :DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<productCategory> ProductCategories { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItem> OrdersItems { get; set; }
        public DbSet<DelievryType> DelievryTypes { get; set; }

    }
}
