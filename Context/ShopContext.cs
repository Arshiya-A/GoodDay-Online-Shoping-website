using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Database
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> option)
        : base(option)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ware> Wares { get; set; }
        public DbSet<ShopingCart> ShopingCarts { get; set; }
        public DbSet<WareGroup> WareGroups { get; set; }
        public DbSet<WareSubgroup> WareSubgroups { get; set; }


    }
}