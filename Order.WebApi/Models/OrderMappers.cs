using Order.WebApi.Models.Dto;

namespace Order.WebApi.Models;

public static class OrderMappers
{
    public static OrderDto ToDto(this Domain.OrderEntity order)
    {
        return new OrderDto
        {
            OrderId = order.OrderId,
            CustomerId = order.CustomerId,
            ProductId = order.ProductId,
            DeliveryDate = order.DeliveryDate,
            IsDelivered = !order.DeliveryDate.Equals(default),
            IsCancelled = order.IsCancelled
        };
    }
}