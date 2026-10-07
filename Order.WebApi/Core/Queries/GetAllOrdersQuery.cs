using MediatR;
using Order.WebApi.Models.Dto;

namespace Order.WebApi.Core.Queries;

public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>
{
}