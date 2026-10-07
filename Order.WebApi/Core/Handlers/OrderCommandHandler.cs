using MediatR;
using Order.WebApi.Core.Commands;
using Order.WebApi.Core.Repositories;

namespace Order.WebApi.Core.Handlers;

public class OrderCommandHandler : 
    IRequestHandler<PlaceOrderCommand, string>,
    IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderCommandHandler> _logger;

    public OrderCommandHandler(IOrderRepository orderRepository, ILogger<OrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<string> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = await _orderRepository.PlaceOrderAsync(request.CustomerId, request.ProductId);
        _logger.LogInformation($"Order placed: {orderId}");
        return orderId;
    }

    public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var result = await _orderRepository.CancelOrderAsync(request.OrderId);
        if (result)
        {
            _logger.LogInformation($"Order cancelled: {request.OrderId}");
        }
        else
        {
            _logger.LogWarning($"Failed to cancel order: {request.OrderId}");
        }
        return result;
    }
}
