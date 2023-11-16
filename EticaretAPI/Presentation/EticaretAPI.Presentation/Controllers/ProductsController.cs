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
using EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.ChangeShowCaseImage;
using EticaretAPI.Application.Consts_Sabitler_;
using EticaretAPI.Application.CustomAttribute;
using EticaretAPI.Application.Enums;
using EticaretAPI.Application.Abstraction.Services;

namespace EticaretAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class ProductsController : ControllerBase
    {

        readonly IMediator _mediatR;
        readonly IProductService _productService;
        public ProductsController(IMediator mediatR, IProductService productService)
        {
            _mediatR = mediatR;
            _productService = productService;
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
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Create Product")]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediatR.Send(createProductCommandRequest);
            return Ok(response);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Update Product")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommanRequest updateProductCommanRequest)
        {
            UpdateProductCommanResponse response = await _mediatR.Send(updateProductCommanRequest);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Remove Product")]
        public async Task<IActionResult> Remove([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            DeleteProductCommandResponse response = await _mediatR.Send(deleteProductCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "File image upload  Product")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductCommandRequest uploadProductCommandRequest)
        {

            uploadProductCommandRequest.Files = Request.Form.Files;
            UploadProductCommandResponse response = await _mediatR.Send(uploadProductCommandRequest);
            return Ok();

        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Reading, Definition = "Get Product  image")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductCommandRequest getProductCommandRequest)
        {
          List<GetProductCommandResponse> response= await _mediatR.Send(getProductCommandRequest);
            return Ok(response);
        }
        [HttpDelete("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Deleting  Product")]
        public async Task<IActionResult>DeleteProductImage([FromRoute, FromQuery] RemoveProductImageRequest request) 
        {
           RemoveProductImageResponse response= await _mediatR.Send(request);
            return Ok();

        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Update change show case ımage  Product")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery]ChangeShowCaseImageCommandRequest changeShowCaseImageCommandRequest)
        {
          ChangeShowCaseImageCommandResponse response=  await _mediatR.Send(changeShowCaseImageCommandRequest);
            return Ok(response);
        }

        [HttpGet("qrcode/{productId}")]
        public async Task<IActionResult> QRCodeGetProduct([FromRoute]string productId)
        {
            var qrCode = await _productService.QRCodeToProductAsync(productId);
            return File(qrCode,"image/png");
        }

    }
}
