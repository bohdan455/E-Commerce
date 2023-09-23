using Application.Model;
using Application.Services.Interfaces;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;

        public OrderService(ApplicationDbContext context,
            IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task CreateOrder(OrderModel orderModel)
        {
            var order = await ParseModelToOrder(orderModel);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        private async Task<decimal> CountPrice(OrderModel order)
        {
            var products = await _productService.GetMultiple(order.OrderParts.Select(op => op.ProductId).ToList());

            var sum = products.Sum(p => p.Price * GetOrderPartQuantity(order.OrderParts, p.Id));

            return sum;

        }

        private static int GetOrderPartQuantity(List<OrderPartModel> orderParts, int id)
        {
            return orderParts.First(op => op.ProductId == id).Quantity;
        }

        private async Task<Order> ParseModelToOrder(OrderModel orderModel)
        {
            const int pendingOrderStatusId = 1;

            return new Order
            {
                Customer = new Customer
                {
                    Name = orderModel.Name,
                    Email = orderModel.Email
                },
                ShippingAddress = orderModel.ShippingAddress,
                TotalPrice = await CountPrice(orderModel),
                OrderParts = orderModel.OrderParts.Select(op => new OrderPart
                {
                    ProductId = op.ProductId,
                    Quantity = op.Quantity
                }).ToList(),
                OrderStatusId = pendingOrderStatusId,
            };
        }
    }
}
