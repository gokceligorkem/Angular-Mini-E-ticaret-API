using Microsoft.AspNetCore.Mvc;
using EticaretAPI.Application.Feature_Özellikler_.Products.Queries;
using MediatR;
using EticaretAPI.Application.Feature_Özellikler_.Products.Queries.GetByIdProduct;
using EticaretAPI.Application.Feature_Özellikler_.Products.Command.UpdateProduct;
using EticaretAPI.Application.Feature_Özellikler_.Products.Command;
using EticaretAPI.Application.Feature_Özellikler_.Products.Command.DeleteProduct;
using EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.UploadProductImage;
using EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.RemoveProductImage;
using EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.GetProductImage;
using Microsoft.AspNetCore.Authorization;

namespace EticaretAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ProductsController : ControllerBase
    {

        readonly IMediator _mediatR;

        public ProductsController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {

            GetAllProductQueryResponse result = await _mediatR.Send(getAllProductQueryRequest);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductResponse response = await _mediatR.Send(getByIdProductQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediatR.Send(createProductCommandRequest);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommanRequest updateProductCommanRequest)
        {
            UpdateProductCommanResponse response = await _mediatR.Send(updateProductCommanRequest);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            DeleteProductCommandResponse response = await _mediatR.Send(deleteProductCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductCommandRequest uploadProductCommandRequest)
        {

            uploadProductCommandRequest.Files = Request.Form.Files;
            UploadProductCommandResponse response = await _mediatR.Send(uploadProductCommandRequest);
            return Ok();

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductCommandRequest getProductCommandRequest)
        {
          List<GetProductCommandResponse> response= await _mediatR.Send(getProductCommandRequest);
            return Ok(response);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult>DeleteProductImage([FromRoute, FromQuery] RemoveProductImageRequest request) 
        {
           RemoveProductImageResponse response= await _mediatR.Send(request);
            return Ok();

        }
    }
}
