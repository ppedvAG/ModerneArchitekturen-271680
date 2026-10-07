using Order.WebApi.Models.Domain;

namespace Order.WebApi.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> CancelOrderAsync(string orderId);
        Task<List<OrderEntity>> GetAllOrdersAsync();
        Task<OrderEntity?> GetOrderByIdAsync(string orderId);
        Task<string> PlaceOrderAsync(string customerId, string productId);
    }
}