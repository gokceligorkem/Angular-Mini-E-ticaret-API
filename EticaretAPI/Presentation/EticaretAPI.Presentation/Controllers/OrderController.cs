using EticaretAPI.Application.Feature_Özellikler_.Orders.Command.CompletedOrder;
using EticaretAPI.Application.Feature_Özellikler_.Orders.Command.CreateOrder;
using EticaretAPI.Application.Feature_Özellikler_.Orders.Query.GetAllOrder;
using EticaretAPI.Application.Feature_Özellikler_.Orders.Query.GetByIdOrder;
using EticaretAPI.Application.Feature_Özellikler_.Products.Queries.GetByIdProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrderController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdOrders([FromRoute] GetByIdQueryRequest request)
        {
            GetByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrders(CreateOrderCommandRequest request)
        {
            CreateOrderCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrderQueryRequest request)
        {
            GetAllOrderQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("complete-order/{Id}")]
        public async Task<IActionResult> GetAllOrders([FromRoute]CompletedOrderCommandRequest request)
        {
            CompletedOrderCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
