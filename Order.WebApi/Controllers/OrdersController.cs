using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.WebApi.Core.Commands;
using Order.WebApi.Core.Queries;

namespace Order.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;
        private readonly IMediator mediator;

        public OrdersController(ILogger<OrdersController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllOrdersQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(string orderId, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetOrderByIdQuery(orderId), cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("place")]
        public async Task<IActionResult> Place([FromBody] PlaceOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new { orderId = result }, result);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel([FromBody] CancelOrderCommand command, CancellationToken cancellationToken)
        {
            var success = await mediator.Send(command, cancellationToken);
            return !success ? NotFound() : Ok(success);
        }

    }
}
