using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.ExtensionMethods
{
    internal static class ModelBuilderExtensions
    {
        internal static void SeedDate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderStatus>().HasData(
                               new OrderStatus { Id = 1, Status = "Pending" },
                               new OrderStatus { Id = 2, Status = "Processing" },
                               new OrderStatus { Id = 3, Status = "Shipped" },
                               new OrderStatus { Id = 4, Status = "Delivered" },
                               new OrderStatus { Id = 5, Status = "Cancelled" }
                                                      );
        }
    }
}
