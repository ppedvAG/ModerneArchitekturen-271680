using MediatR;
using Order.WebApi.Models.Dto;

namespace Order.WebApi.Core.Queries;

public class GetOrderByIdQuery : IRequest<OrderDto?>
{
    public GetOrderByIdQuery(string orderId)
    {
        OrderId = orderId;
    }

    public string OrderId { get; }
}
