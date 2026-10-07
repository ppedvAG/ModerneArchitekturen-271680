using Order.WebApi.Models.Domain;

namespace Order.WebApi.Core.Repositories;

public class Seed
{
    public static IEnumerable<OrderEntity> GetOrders()
    {
        return new List<OrderEntity>
        {
            new OrderEntity
            {
                OrderId = "1",
                CustomerId = "ACME-0815",
                ProductId = "Hoover-123",
                DeliveryDate = DateTime.Now.AddDays(5),
                IsCancelled = false
            },
            new OrderEntity
            {
                OrderId = "2",
                CustomerId = "ACME-0815",
                ProductId = "Anvil-456",
                DeliveryDate = DateTime.Now.AddDays(10),
                IsCancelled = false
            }
        };
    }
}