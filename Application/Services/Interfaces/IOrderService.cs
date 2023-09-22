using Application.Model;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(OrderModel orderModel);
    }
}