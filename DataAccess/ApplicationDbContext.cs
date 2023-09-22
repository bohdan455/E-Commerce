using DataAccess.Entities;
using DataAccess.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Product> Products { get; set; }
        
        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

        public virtual DbSet<OrderPart> OrderParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedDate();
        }
    }
}
