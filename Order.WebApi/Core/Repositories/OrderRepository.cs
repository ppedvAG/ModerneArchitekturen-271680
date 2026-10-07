using Order.WebApi.Models.Domain;

namespace Order.WebApi.Core.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly Dictionary<string, OrderEntity> _orders = Seed.GetOrders().ToDictionary(o => o.OrderId);

    public Task<string> PlaceOrderAsync(string customerId, string productId)
    {
        var orderId = Guid.NewGuid().ToString();
        var order = new OrderEntity
        {
            OrderId = orderId,
            CustomerId = customerId,
            ProductId = productId,
            IsCancelled = false
        };
        _orders[orderId] = order;
        return Task.FromResult(orderId);
    }

    public Task<bool> CancelOrderAsync(string orderId)
    {
        if (_orders.TryGetValue(orderId, out var order))
        {
            order.IsCancelled = true;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<OrderEntity?> GetOrderByIdAsync(string orderId)
    {
        return Task.FromResult(_orders.GetValueOrDefault(orderId));
    }

    public Task<List<OrderEntity>> GetAllOrdersAsync()
    {
        return Task.FromResult(_orders.Values.ToList());
    }
}
