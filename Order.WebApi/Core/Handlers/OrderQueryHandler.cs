using MediatR;
using Order.WebApi.Core.Queries;
using Order.WebApi.Core.Repositories;
using Order.WebApi.Models;
using Order.WebApi.Models.Dto;

namespace Order.WebApi.Core.Handlers;

public class  OrderQueryHandler :
    IRequestHandler<GetOrderByIdQuery, OrderDto?>,
    IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderQueryHandler> _logger;

    public OrderQueryHandler(IOrderRepository orderRepository, ILogger<OrderQueryHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
        if (order == null)
        {
            _logger.LogWarning($"Order not found: {request.OrderId}");
            return null;
        }
        return order.ToDto();
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllOrdersAsync();
        return orders.Select(OrderMappers.ToDto);
    }
}
