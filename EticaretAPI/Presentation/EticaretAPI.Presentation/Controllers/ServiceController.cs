using EticaretAPI.Application.Abstraction.Services.Configurations;
using EticaretAPI.Application.Consts_Sabitler_;
using EticaretAPI.Application.CustomAttribute;
using EticaretAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ServiceController : ControllerBase
    {
        IApplicationService _applicationService;

        public ServiceController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }   
        [HttpGet]
        [AuthorizeDefition(Menu = "Application Services", ActionType = ActionType.Reading, Definition = "GetAuthorizeDefinitionEndpoint ")]
        public IActionResult GetAuthorizeDefinitionEndpoint()
        {
           var datas= _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
