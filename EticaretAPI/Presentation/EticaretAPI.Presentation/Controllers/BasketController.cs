using EticaretAPI.Application.Feature_Özellikler_.Basket.Command.AddItemToBasket;
using EticaretAPI.Application.Feature_Özellikler_.Basket.Command.RemoveBasketItem;
using EticaretAPI.Application.Feature_Özellikler_.Basket.Command.UpdateQuantity;
using EticaretAPI.Application.Feature_Özellikler_.Basket.Query.GetBasketItem;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class BasketController : ControllerBase
    {
        readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetBasketItems([FromQuery]GetBasketItemsCommandRequest request)
        {
            List<GetBasketItemsCommandResponse> response = await _mediator.Send(request);
            return Ok(response);
        } 
        [HttpPost]
        public async Task<IActionResult> AddBasketItems(AddBasketItemToBasketCommandRequest request)
        {
          AddBasketItemToBasketCommandResponse response  = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBasketItems(UpdateQuantityCommandRequest request)
        {
            UpdateQuantityCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{BasketItemId}")]
        public async Task<IActionResult> RemoveBasketItems([FromRoute]RemoveBasketItemCommandRequest request)
        {
            RemoveBasketItemCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
