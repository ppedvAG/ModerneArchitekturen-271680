using MediatR;

namespace Order.WebApi.Core.Commands;

public class CancelOrderCommand : IRequest<bool>
{
    public string OrderId { get; set; }
}